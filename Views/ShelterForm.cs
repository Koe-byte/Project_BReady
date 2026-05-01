using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;

namespace ProjectBReady.Forms
{
    public partial class ShelterForm : Form
    {
        private bool isAdminMode = false;
        private int selectedShelterID = -1;

        public ShelterForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += ShelterForm_KeyDown;
            LoadShelters();
            SetAdminMode(false);
        }

        // ── CTRL+SHIFT+O ──────────────────────────────────────────
        private void ShelterForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.O)
            {
                if (isAdminMode)
                {
                    SetAdminMode(false);
                }
                else
                {
                    string pin = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter Admin PIN:", "Admin Access", "");
                    if (pin == "") return;

                    DataTable dt = DBHelper.GetData(
                        "SELECT SettingValue FROM SETTINGS WHERE SettingKey = 'AdminPIN'");
                    string storedPIN = dt.Rows.Count > 0
                        ? dt.Rows[0]["SettingValue"].ToString() : "1234";

                    if (pin == storedPIN)
                        SetAdminMode(true);
                    else
                        MessageBox.Show("Incorrect PIN.", "Access Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                e.Handled = true;
            }
        }

        private void SetAdminMode(bool isAdmin)
        {
            isAdminMode = isAdmin;
            btnAddShelter.Visible = isAdmin;
            btnEditShelter.Visible = isAdmin;
            btnDeleteShelter.Visible = isAdmin;
            btnUpdateOccupancy.Visible = isAdmin;

            if (isAdmin)
            {
                toggleText.Text = "🔓 Admin Mode Active  |  Lock: Ctrl+Shift+O";
                toggleText.ForeColor = Color.FromArgb(255, 125, 40);
            }
            else
            {
                toggleText.Text = "Ctrl+Shift+O to toggle admin";
                toggleText.ForeColor = Color.White;
            }
        }

        // ── LOAD SHELTERS ─────────────────────────────────────────
        private void LoadShelters()
        {
            try
            {
                // SQLite: CAST at NULLIF ay supported, okay ang query na ito
                string query = @"
                    SELECT ShelterID, ShelterName, MaxCapacity,
                           CurrentOccupancy, Status,
                           CAST(CurrentOccupancy AS REAL) / NULLIF(MaxCapacity, 0) * 100 AS PctFull
                    FROM SHELTERS
                    ORDER BY ShelterName";

                DataTable dt = DBHelper.GetData(query);
                dgvShelters.DataSource = dt;

                if (dgvShelters.Columns.Contains("ShelterID"))
                    dgvShelters.Columns["ShelterID"].Visible = false;

                lblTotalShelters.Text = $"Total Shelters: {dt.Rows.Count}";

                int totalMax = 0, totalCurr = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalMax += Convert.ToInt32(row["MaxCapacity"]);
                    totalCurr += Convert.ToInt32(row["CurrentOccupancy"]);
                }
                lblTotalCapacity.Text = $"Total Capacity: {totalCurr} / {totalMax}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shelters: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── ADD ───────────────────────────────────────────────────
        private void btnAddShelter_Click(object sender, EventArgs e)
        {
            using (ShelterEditForm form = new ShelterEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadShelters();
            }
        }

        // ── EDIT ──────────────────────────────────────────────────
        private void btnEditShelter_Click(object sender, EventArgs e)
        {
            if (selectedShelterID < 0) { ShowNoSelection(); return; }
            using (ShelterEditForm form = new ShelterEditForm(selectedShelterID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadShelters();
            }
        }

        // ── DELETE ────────────────────────────────────────────────
        private void btnDeleteShelter_Click(object sender, EventArgs e)
        {
            if (selectedShelterID < 0) { ShowNoSelection(); return; }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this shelter?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                DBHelper.ExecuteNonQuery(
                    "DELETE FROM SHELTERS WHERE ShelterID = @id",
                    new System.Collections.Generic.Dictionary<string, object>
                    { { "@id", selectedShelterID } });
                selectedShelterID = -1;
                LoadShelters();
            }
        }

        // ── UPDATE OCCUPANCY ──────────────────────────────────────
        private void btnUpdateOccupancy_Click(object sender, EventArgs e)
        {
            if (selectedShelterID < 0) { ShowNoSelection(); return; }

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new occupancy count:", "Update Occupancy", "0");

            if (int.TryParse(input, out int newOcc) && newOcc >= 0)
            {
                // SQLite: walang inline CASE sa UPDATE na ganito, pero supported naman
                DBHelper.ExecuteNonQuery(
                    @"UPDATE SHELTERS SET CurrentOccupancy = @occ,
                      Status = CASE WHEN @occ >= MaxCapacity THEN 'Full' ELSE 'Open' END
                      WHERE ShelterID = @id",
                    new System.Collections.Generic.Dictionary<string, object>
                    { { "@occ", newOcc }, { "@id", selectedShelterID } });
                LoadShelters();
            }
        }

        // ── GRID SELECTION ────────────────────────────────────────
        private void dgvShelters_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvShelters.SelectedRows.Count > 0)
            {
                var row = dgvShelters.SelectedRows[0];
                if (row.Cells["ShelterID"].Value != null)
                    selectedShelterID = Convert.ToInt32(row.Cells["ShelterID"].Value);
            }
        }

        // ── ROW COLORING ──────────────────────────────────────────
        private void dgvShelters_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvShelters.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
            {
                string status = drv["Status"]?.ToString();
                double pct = drv["PctFull"] != DBNull.Value
                    ? Convert.ToDouble(drv["PctFull"]) : 0;

                if (status == "Full")
                    dgvShelters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                else if (pct >= 75)
                    dgvShelters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                else
                    dgvShelters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        // ── NAVIGATION ────────────────────────────────────────────
        private void btnRefresh_Click(object sender, EventArgs e) => LoadShelters();

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            new DashboardForm().Show();
            this.Close();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            new InventoryForm().Show();
            this.Close();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            new ReportForm().Show();
            this.Close();
        }

        private void ShowNoSelection() =>
            MessageBox.Show("Please select a shelter first.", "No Selection",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void label1_Click(object sender, EventArgs e) { }
    }
}