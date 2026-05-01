using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;

namespace ProjectBReady.Forms
{
    public partial class InventoryForm : Form
    {
        private bool isAdminMode = false;
        private int selectedFoodID = -1;
        private int selectedMedID = -1;

        public InventoryForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += InventoryForm_KeyDown;
            LoadFoodItems();
            LoadMedicalSupplies();
            LoadInventorySummary();
            SetAdminMode(false);
        }

        // ── CTRL+SHIFT+O ──────────────────────────────────────────
        private void InventoryForm_KeyDown(object sender, KeyEventArgs e)
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
            btnAddFood.Visible = isAdmin;
            btnAddMed.Visible = isAdmin;
            btnDeleteFood.Visible = isAdmin;
            btnDeleteMed.Visible = isAdmin;
            btnDispatchFood.Visible = isAdmin;
            btnDispatchMed.Visible = isAdmin;
            btnStockInFood.Visible = isAdmin;
            btnStockInMed.Visible = isAdmin;

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

        // ── LOAD DATA ─────────────────────────────────────────────
        // Single INVENTORY table — ItemType = 'Food' or 'Medical'
        // SQLite: datetime('now') instead of GETDATE()
        //         date(ExpirationDate, '+7 days') instead of DATEADD

        private void LoadFoodItems()
        {
            try
            {
                string query = @"
                    SELECT ItemID, ItemName, Quantity, ExpirationDate,
                           CASE
                               WHEN ExpirationDate < date('now') THEN 'Expired'
                               WHEN ExpirationDate < date('now', '+7 days') THEN 'Expiring Soon'
                               ELSE 'Good'
                           END AS ExpiryStatus
                    FROM INVENTORY
                    WHERE ItemType = 'Food'
                    ORDER BY ItemName";

                DataTable dt = DBHelper.GetData(query);
                dgvFood.DataSource = dt;

                if (dgvFood.Columns.Contains("ItemID"))
                    dgvFood.Columns["ItemID"].Visible = false;

                lblFoodCount.Text = $"Food Items: {dt.Rows.Count} types";
            }
            catch
            {
                lblFoodCount.Text = "Food Items: N/A";
            }
        }

        private void LoadMedicalSupplies()
        {
            try
            {
                string query = @"
                    SELECT ItemID, ItemName, Quantity, Dosage, IsPrescriptionRequired
                    FROM INVENTORY
                    WHERE ItemType = 'Medical'
                    ORDER BY ItemName";

                DataTable dt = DBHelper.GetData(query);
                dgvMedical.DataSource = dt;

                if (dgvMedical.Columns.Contains("ItemID"))
                    dgvMedical.Columns["ItemID"].Visible = false;

                lblMedCount.Text = $"Medical Supplies: {dt.Rows.Count} types";
            }
            catch
            {
                lblMedCount.Text = "Medical Supplies: N/A";
            }
        }

        private void LoadInventorySummary()
        {
            try
            {
                DataTable foodSum = DBHelper.GetData(
                    "SELECT COALESCE(SUM(Quantity), 0) AS Total FROM INVENTORY WHERE ItemType = 'Food'");
                DataTable medSum = DBHelper.GetData(
                    "SELECT COALESCE(SUM(Quantity), 0) AS Total FROM INVENTORY WHERE ItemType = 'Medical'");

                lblFoodTotal.Text = $"Total Food Units: {Convert.ToInt32(foodSum.Rows[0]["Total"]):N0}";
                lblMedTotal.Text = $"Total Med Units: {Convert.ToInt32(medSum.Rows[0]["Total"]):N0}";
            }
            catch
            {
                lblFoodTotal.Text = "Total Food Units: N/A";
                lblMedTotal.Text = "Total Med Units: N/A";
            }
        }

        // ── FOOD ACTIONS ──────────────────────────────────────────
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            using (InventoryItemEditForm form = new InventoryItemEditForm("Food"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadFoodItems();
                    LoadInventorySummary();
                }
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (selectedFoodID < 0) { ShowNoSelection(); return; }
            if (ConfirmDelete())
            {
                DBHelper.ExecuteNonQuery("DELETE FROM INVENTORY WHERE ItemID = @id",
                    new System.Collections.Generic.Dictionary<string, object>
                    { { "@id", selectedFoodID } });
                selectedFoodID = -1;
                LoadFoodItems();
                LoadInventorySummary();
            }
        }

        private void btnDispatchFood_Click(object sender, EventArgs e)
        {
            if (selectedFoodID < 0) { ShowNoSelection(); return; }
            ShowDispatchDialog(selectedFoodID);
            LoadFoodItems();
            LoadInventorySummary();
        }

        private void btnStockInFood_Click(object sender, EventArgs e)
        {
            if (selectedFoodID < 0) { ShowNoSelection(); return; }
            ShowStockInDialog(selectedFoodID);
            LoadFoodItems();
            LoadInventorySummary();
        }

        // ── MEDICAL ACTIONS ───────────────────────────────────────
        private void btnAddMed_Click(object sender, EventArgs e)
        {
            using (InventoryItemEditForm form = new InventoryItemEditForm("Medical"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMedicalSupplies();
                    LoadInventorySummary();
                }
            }
        }

        private void btnDeleteMed_Click(object sender, EventArgs e)
        {
            if (selectedMedID < 0) { ShowNoSelection(); return; }
            if (ConfirmDelete())
            {
                DBHelper.ExecuteNonQuery("DELETE FROM INVENTORY WHERE ItemID = @id",
                    new System.Collections.Generic.Dictionary<string, object>
                    { { "@id", selectedMedID } });
                selectedMedID = -1;
                LoadMedicalSupplies();
                LoadInventorySummary();
            }
        }

        private void btnDispatchMed_Click(object sender, EventArgs e)
        {
            if (selectedMedID < 0) { ShowNoSelection(); return; }
            ShowDispatchDialog(selectedMedID);
            LoadMedicalSupplies();
            LoadInventorySummary();
        }

        private void btnStockInMed_Click(object sender, EventArgs e)
        {
            if (selectedMedID < 0) { ShowNoSelection(); return; }
            ShowStockInDialog(selectedMedID);
            LoadMedicalSupplies();
            LoadInventorySummary();
        }

        // ── HELPERS ───────────────────────────────────────────────
        private void ShowDispatchDialog(int itemId)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter quantity to dispatch:", "Dispatch", "0");
            if (int.TryParse(input, out int qty) && qty > 0)
            {
                DBHelper.ExecuteNonQuery(
                    "UPDATE INVENTORY SET Quantity = Quantity - @qty WHERE ItemID = @id AND Quantity >= @qty",
                    new System.Collections.Generic.Dictionary<string, object>
                    { { "@qty", qty }, { "@id", itemId } });
            }
        }

        private void ShowStockInDialog(int itemId)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter quantity to add:", "Stock In", "0");
            if (int.TryParse(input, out int qty) && qty > 0)
            {
                DBHelper.ExecuteNonQuery(
                    "UPDATE INVENTORY SET Quantity = Quantity + @qty WHERE ItemID = @id",
                    new System.Collections.Generic.Dictionary<string, object>
                    { { "@qty", qty }, { "@id", itemId } });
            }
        }

        private void ShowNoSelection() =>
            MessageBox.Show("Please select an item first.", "No Selection",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        private bool ConfirmDelete() =>
            MessageBox.Show("Delete this item?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

        // ── GRID SELECTION ────────────────────────────────────────
        private void dgvFood_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFood.SelectedRows.Count > 0 && dgvFood.SelectedRows[0].Cells["ItemID"].Value != null)
                selectedFoodID = Convert.ToInt32(dgvFood.SelectedRows[0].Cells["ItemID"].Value);
        }

        private void dgvMedical_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMedical.SelectedRows.Count > 0 && dgvMedical.SelectedRows[0].Cells["ItemID"].Value != null)
                selectedMedID = Convert.ToInt32(dgvMedical.SelectedRows[0].Cells["ItemID"].Value);
        }

        // ── ROW COLORING ──────────────────────────────────────────
        private void dgvFood_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvFood.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
            {
                string status = drv["ExpiryStatus"]?.ToString();
                if (status == "Expired")
                    dgvFood.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                else if (status == "Expiring Soon")
                    dgvFood.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                else
                    dgvFood.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        // ── NAVIGATION ────────────────────────────────────────────
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFoodItems();
            LoadMedicalSupplies();
            LoadInventorySummary();
        }

        private void btnDashboard_Click(object sender, EventArgs e) { new DashboardForm().Show(); this.Close(); }
        private void btnShelter_Click(object sender, EventArgs e) { new ShelterForm().Show(); this.Close(); }
        private void btnReports_Click(object sender, EventArgs e) { new ReportForm().Show(); this.Close(); }
    }
}