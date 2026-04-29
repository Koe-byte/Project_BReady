// ============================================================
//  ShelterForm.cs  —  ProjectBReady
//  Features: View/Add/Edit/Delete Shelters, Occupancy Updates,
//            Admin-only Add/Edit/Delete controls
// ============================================================
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
        private const string ADMIN_PIN = "1234";
        private int selectedShelterID = -1;

        public ShelterForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += ShelterForm_KeyDown;
            LoadShelters();
            SetAdminMode(false);
        }

        // ── CTRL+SHIFT+O — Admin Toggle ──────────────────────────
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
                    if (pin == ADMIN_PIN)
                        SetAdminMode(true);
                    else if (pin != "")
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

        // ── LOAD ALL SHELTERS ────────────────────────────────────
        private void LoadShelters()
        {
            try
            {
                string query = @"
                    SELECT ShelterID, ShelterName, MaxCapacity, 
                           CurrentOccupancy, Status,
                           CAST(CurrentOccupancy AS FLOAT) / NULLIF(MaxCapacity,0) * 100 AS PctFull
                    FROM SHELTERS
                    ORDER BY ShelterName";

                DataTable dt = DBHelper.GetData(query);
                dgvShelters.DataSource = dt;

                // Style the columns
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

        // ── ADD SHELTER ──────────────────────────────────────────
        private void btnAddShelter_Click(object sender, EventArgs e)
        {
            using (ShelterEditForm form = new ShelterEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadShelters();
            }
        }

        // ── EDIT SHELTER ─────────────────────────────────────────
        private void btnEditShelter_Click(object sender, EventArgs e)
        {
            if (selectedShelterID < 0)
            {
                MessageBox.Show("Please select a shelter first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (ShelterEditForm form = new ShelterEditForm(selectedShelterID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadShelters();
            }
        }

        // ── DELETE SHELTER ───────────────────────────────────────
        private void btnDeleteShelter_Click(object sender, EventArgs e)
        {
            if (selectedShelterID < 0)
            {
                MessageBox.Show("Please select a shelter first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this shelter?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    DBHelper.ExecuteNonQuery(
                        "DELETE FROM SHELTERS WHERE ShelterID = @id",
                        new System.Collections.Generic.Dictionary<string, object>
                        { { "@id", selectedShelterID } });
                    LoadShelters();
                    selectedShelterID = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting shelter: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── UPDATE OCCUPANCY ─────────────────────────────────────
        private void btnUpdateOccupancy_Click(object sender, EventArgs e)
        {
            if (selectedShelterID < 0)
            {
                MessageBox.Show("Please select a shelter first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter new occupancy count:", "Update Occupancy", "0");

            if (int.TryParse(input, out int newOccupancy) && newOccupancy >= 0)
            {
                try
                {
                    DBHelper.ExecuteNonQuery(
                        @"UPDATE SHELTERS SET CurrentOccupancy = @occ,
                          Status = CASE WHEN @occ >= MaxCapacity THEN 'Full' ELSE 'Open' END
                          WHERE ShelterID = @id",
                        new System.Collections.Generic.Dictionary<string, object>
                        {
                            { "@occ", newOccupancy },
                            { "@id", selectedShelterID }
                        });
                    LoadShelters();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating occupancy: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── GRID SELECTION ───────────────────────────────────────
        private void dgvShelters_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvShelters.SelectedRows.Count > 0)
            {
                var row = dgvShelters.SelectedRows[0];
                if (row.Cells["ShelterID"].Value != null)
                    selectedShelterID = Convert.ToInt32(row.Cells["ShelterID"].Value);
            }
        }

        // ── ROW COLORING ─────────────────────────────────────────
        private void dgvShelters_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvShelters.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
            {
                string status = drv["Status"]?.ToString();
                double pct = 0;
                if (drv["PctFull"] != DBNull.Value)
                    pct = Convert.ToDouble(drv["PctFull"]);

                if (status == "Full")
                    dgvShelters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                else if (pct >= 75)
                    dgvShelters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                else
                    dgvShelters.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        // ── REFRESH ──────────────────────────────────────────────
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadShelters();
        }

        // ── BACK TO DASHBOARD ────────────────────────────────────
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            DashboardForm dash = new DashboardForm();
            dash.Show();
            this.Close();
        }

        // ── NAVIGATE TO INVENTORY ────────────────────────────────
        private void btnInventory_Click(object sender, EventArgs e)
        {
            InventoryForm inv = new InventoryForm();
            inv.Show();
            this.Close();
        }

        // ── NAVIGATE TO REPORTS ──────────────────────────────────
        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportForm rep = new ReportForm();
            rep.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}