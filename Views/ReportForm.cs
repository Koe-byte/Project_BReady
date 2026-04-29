// ============================================================
//  ReportForm.cs  —  ProjectBReady
//  Features: Shelter occupancy report, Inventory summary,
//            Expiring items alert, Print/Export (Admin only)
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;

namespace ProjectBReady.Forms
{
    public partial class ReportForm : Form
    {
        private bool isAdminMode = false;
        private const string ADMIN_PIN = "1234";

        public ReportForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += ReportForm_KeyDown;
            LoadAllReports();
            SetAdminMode(false);
        }

        // ── CTRL+SHIFT+O ─────────────────────────────────────────
        private void ReportForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.O)
            {
                if (isAdminMode)
                    SetAdminMode(false);
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
            btnExportReport.Visible = isAdmin;

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

        // ── LOAD ALL REPORT DATA ─────────────────────────────────
        private void LoadAllReports()
        {
            LoadShelterReport();
            LoadInventoryReport();
            LoadExpiringItems();
            LoadSummaryCards();
        }

        private void LoadShelterReport()
        {
            try
            {
                string query = @"
                    SELECT ShelterName AS 'Shelter Name',
                           MaxCapacity AS 'Max Capacity',
                           CurrentOccupancy AS 'Current',
                           MaxCapacity - CurrentOccupancy AS 'Available',
                           CAST(CurrentOccupancy AS FLOAT) / NULLIF(MaxCapacity,0) * 100 AS '% Full',
                           Status
                    FROM SHELTERS ORDER BY ShelterName";

                DataTable dt = DBHelper.GetData(query);
                dgvShelterReport.DataSource = dt;

                // Color rows
                foreach (DataGridViewRow row in dgvShelterReport.Rows)
                {
                    if (row.Cells["Status"].Value?.ToString() == "Full")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                }
            }
            catch { }
        }

        private void LoadInventoryReport()
        {
            try
            {
                // Food
                string foodQuery = @"
                    SELECT ItemName AS 'Item', Quantity AS 'Qty', 
                           ExpirationDate AS 'Expires',
                           'Food' AS Type
                    FROM FOOD_ITEMS ORDER BY ExpirationDate";
                DataTable foodDt = DBHelper.GetData(foodQuery);

                // Medical
                string medQuery = @"
                    SELECT ItemName AS 'Item', Quantity AS 'Qty',
                           Dosage, 
                           CASE WHEN IsPrescriptionRequired=1 THEN 'Yes' ELSE 'No' END AS 'Rx Required',
                           'Medical' AS Type
                    FROM MEDICAL_SUPPLIES ORDER BY ItemName";
                DataTable medDt = DBHelper.GetData(medQuery);

                dgvFoodReport.DataSource = foodDt;
                dgvMedReport.DataSource = medDt;
            }
            catch { }
        }

        private void LoadExpiringItems()
        {
            try
            {
                string query = @"
                    SELECT ItemName AS 'Food Item',
                           Quantity AS 'Qty',
                           ExpirationDate AS 'Expires On',
                           DATEDIFF(DAY, GETDATE(), ExpirationDate) AS 'Days Left'
                    FROM FOOD_ITEMS
                    WHERE ExpirationDate <= DATEADD(DAY, 30, GETDATE())
                    ORDER BY ExpirationDate";

                DataTable dt = DBHelper.GetData(query);
                dgvExpiring.DataSource = dt;

                lblExpiringCount.Text = $"⚠ {dt.Rows.Count} item(s) expiring within 30 days";
                lblExpiringCount.ForeColor = dt.Rows.Count > 0
                    ? Color.FromArgb(220, 53, 69)
                    : Color.FromArgb(31, 158, 117);

                foreach (DataGridViewRow row in dgvExpiring.Rows)
                {
                    if (row.Cells["Days Left"].Value != null)
                    {
                        int days = Convert.ToInt32(row.Cells["Days Left"].Value);
                        if (days < 0)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                        else if (days <= 7)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    }
                }
            }
            catch { }
        }

        private void LoadSummaryCards()
        {
            try
            {
                // Shelter summary
                DataTable shelterData = DBHelper.GetData(@"
                    SELECT COUNT(*) AS Total,
                           SUM(CurrentOccupancy) AS TotalOcc,
                           SUM(MaxCapacity) AS TotalCap,
                           SUM(CASE WHEN Status='Full' THEN 1 ELSE 0 END) AS FullCount
                    FROM SHELTERS");

                if (shelterData.Rows.Count > 0)
                {
                    var r = shelterData.Rows[0];
                    int total = r["Total"] != DBNull.Value ? Convert.ToInt32(r["Total"]) : 0;
                    int occ = r["TotalOcc"] != DBNull.Value ? Convert.ToInt32(r["TotalOcc"]) : 0;
                    int cap = r["TotalCap"] != DBNull.Value ? Convert.ToInt32(r["TotalCap"]) : 0;
                    int full = r["FullCount"] != DBNull.Value ? Convert.ToInt32(r["FullCount"]) : 0;
                    int pct = cap > 0 ? (int)((double)occ / cap * 100) : 0;

                    lblCardShelters.Text = total.ToString();
                    lblCardOccupancy.Text = $"{occ} / {cap}";
                    lblCardPct.Text = $"{pct}% Full";
                    lblCardFull.Text = $"{full} Shelter(s) Full";
                }

                // Inventory summary
                DataTable foodData = DBHelper.GetData("SELECT COUNT(*) AS Cnt, SUM(Quantity) AS Total FROM FOOD_ITEMS");
                DataTable medData = DBHelper.GetData("SELECT COUNT(*) AS Cnt, SUM(Quantity) AS Total FROM MEDICAL_SUPPLIES");

                if (foodData.Rows.Count > 0)
                {
                    int cnt = foodData.Rows[0]["Cnt"] != DBNull.Value ? Convert.ToInt32(foodData.Rows[0]["Cnt"]) : 0;
                    int tot = foodData.Rows[0]["Total"] != DBNull.Value ? Convert.ToInt32(foodData.Rows[0]["Total"]) : 0;
                    lblCardFoodTypes.Text = cnt.ToString();
                    lblCardFoodUnits.Text = $"{tot:N0} units";
                }

                if (medData.Rows.Count > 0)
                {
                    int cnt = medData.Rows[0]["Cnt"] != DBNull.Value ? Convert.ToInt32(medData.Rows[0]["Cnt"]) : 0;
                    int tot = medData.Rows[0]["Total"] != DBNull.Value ? Convert.ToInt32(medData.Rows[0]["Total"]) : 0;
                    lblCardMedTypes.Text = cnt.ToString();
                    lblCardMedUnits.Text = $"{tot:N0} units";
                }
            }
            catch { }
        }

        // ── EXPORT (simple CSV) ───────────────────────────────────
        private void btnExportReport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FileName = $"ProjectBReady_Report_{DateTime.Now:yyyyMMdd}";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine("=== SHELTER REPORT ===");
                        sb.AppendLine("ShelterName,MaxCapacity,CurrentOccupancy,Available,Status");

                        DataTable shelters = DBHelper.GetData(
                            "SELECT ShelterName, MaxCapacity, CurrentOccupancy, MaxCapacity-CurrentOccupancy AS Available, Status FROM SHELTERS");
                        foreach (DataRow row in shelters.Rows)
                            sb.AppendLine(string.Join(",", row.ItemArray));

                        sb.AppendLine();
                        sb.AppendLine("=== FOOD INVENTORY ===");
                        sb.AppendLine("ItemName,Quantity,ExpirationDate");

                        DataTable food = DBHelper.GetData("SELECT ItemName, Quantity, ExpirationDate FROM FOOD_ITEMS");
                        foreach (DataRow row in food.Rows)
                            sb.AppendLine(string.Join(",", row.ItemArray));

                        sb.AppendLine();
                        sb.AppendLine("=== MEDICAL INVENTORY ===");
                        sb.AppendLine("ItemName,Quantity,Dosage,PrescriptionRequired");

                        DataTable med = DBHelper.GetData("SELECT ItemName, Quantity, Dosage, IsPrescriptionRequired FROM MEDICAL_SUPPLIES");
                        foreach (DataRow row in med.Rows)
                            sb.AppendLine(string.Join(",", row.ItemArray));

                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString());
                        MessageBox.Show("Report exported successfully!", "Export Complete",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Export failed: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ── REFRESH ───────────────────────────────────────────────
        private void btnRefresh_Click(object sender, EventArgs e) => LoadAllReports();

        // ── NAVIGATION ────────────────────────────────────────────
        private void btnDashboard_Click(object sender, EventArgs e) { new DashboardForm().Show(); this.Close(); }
        private void btnShelter_Click(object sender, EventArgs e) { new ShelterForm().Show(); this.Close(); }
        private void btnInventory_Click(object sender, EventArgs e) { new InventoryForm().Show(); this.Close(); }
    }
}