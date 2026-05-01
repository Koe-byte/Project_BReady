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

        public ReportForm(bool adminMode = false)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += ReportForm_KeyDown;
            LoadAllReports();
            SetAdminMode(adminMode);
        }

        // ── CTRL+SHIFT+O ──────────────────────────────────────────
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

        // ── LOAD ALL ──────────────────────────────────────────────
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
                DataTable dt = DBHelper.GetData(@"
                    SELECT ShelterName AS 'Shelter Name',
                           MaxCapacity AS 'Max Capacity',
                           CurrentOccupancy AS 'Current',
                           MaxCapacity - CurrentOccupancy AS 'Available',
                           CAST(CurrentOccupancy AS REAL) / NULLIF(MaxCapacity, 0) * 100 AS '% Full',
                           Status
                    FROM SHELTERS ORDER BY ShelterName");

                dgvShelterReport.DataSource = dt;
                foreach (DataGridViewRow row in dgvShelterReport.Rows)
                    if (row.Cells["Status"].Value?.ToString() == "Full")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
            }
            catch { }
        }

        private void LoadInventoryReport()
        {
            try
            {
                dgvFoodReport.DataSource = DBHelper.GetData(@"
                    SELECT ItemName AS 'Item', Quantity AS 'Qty',
                           ExpirationDate AS 'Expires', 'Food' AS Type
                    FROM INVENTORY WHERE ItemType = 'Food' ORDER BY ExpirationDate");

                dgvMedReport.DataSource = DBHelper.GetData(@"
                    SELECT ItemName AS 'Item', Quantity AS 'Qty', Dosage,
                           CASE WHEN IsPrescriptionRequired = 1 THEN 'Yes' ELSE 'No' END AS 'Rx Required'
                    FROM INVENTORY WHERE ItemType = 'Medical' ORDER BY ItemName");
            }
            catch { }
        }

        private void LoadExpiringItems()
        {
            try
            {
                DataTable dt = DBHelper.GetData(@"
                    SELECT ItemName AS 'Food Item', Quantity AS 'Qty',
                           ExpirationDate AS 'Expires On',
                           CAST(julianday(ExpirationDate) - julianday('now') AS INTEGER) AS 'Days Left'
                    FROM INVENTORY
                    WHERE ItemType = 'Food' AND ExpirationDate <= date('now', '+30 days')
                    ORDER BY ExpirationDate");

                dgvExpiring.DataSource = dt;
                lblExpiringCount.Text = $"⚠ {dt.Rows.Count} item(s) expiring within 30 days";
                lblExpiringCount.ForeColor = dt.Rows.Count > 0
                    ? Color.FromArgb(220, 53, 69) : Color.FromArgb(31, 158, 117);

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
                DataTable s = DBHelper.GetData(@"
                    SELECT COUNT(*) AS Total,
                           COALESCE(SUM(CurrentOccupancy), 0) AS TotalOcc,
                           COALESCE(SUM(MaxCapacity), 0) AS TotalCap,
                           SUM(CASE WHEN Status = 'Full' THEN 1 ELSE 0 END) AS FullCount
                    FROM SHELTERS");

                if (s.Rows.Count > 0)
                {
                    int occ = Convert.ToInt32(s.Rows[0]["TotalOcc"]);
                    int cap = Convert.ToInt32(s.Rows[0]["TotalCap"]);
                    int pct = cap > 0 ? (int)((double)occ / cap * 100) : 0;
                    lblCardShelters.Text = Convert.ToInt32(s.Rows[0]["Total"]).ToString();
                    lblCardOccupancy.Text = $"{occ} / {cap}";
                    lblCardPct.Text = $"{pct}% Full";
                    lblCardFull.Text = $"{Convert.ToInt32(s.Rows[0]["FullCount"])} Shelter(s) Full";
                }

                DataTable f = DBHelper.GetData(
                    "SELECT COUNT(*) AS Cnt, COALESCE(SUM(Quantity), 0) AS Total FROM INVENTORY WHERE ItemType = 'Food'");
                DataTable m = DBHelper.GetData(
                    "SELECT COUNT(*) AS Cnt, COALESCE(SUM(Quantity), 0) AS Total FROM INVENTORY WHERE ItemType = 'Medical'");

                if (f.Rows.Count > 0)
                {
                    lblCardFoodTypes.Text = Convert.ToInt32(f.Rows[0]["Cnt"]).ToString();
                    lblCardFoodUnits.Text = $"{Convert.ToInt32(f.Rows[0]["Total"]):N0} units";
                }
                if (m.Rows.Count > 0)
                {
                    lblCardMedTypes.Text = Convert.ToInt32(m.Rows[0]["Cnt"]).ToString();
                    lblCardMedUnits.Text = $"{Convert.ToInt32(m.Rows[0]["Total"]):N0} units";
                }
            }
            catch { }
        }

        // ── EXPORT CSV ────────────────────────────────────────────
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
                        foreach (DataRow row in DBHelper.GetData(
                            "SELECT ShelterName, MaxCapacity, CurrentOccupancy, MaxCapacity - CurrentOccupancy, Status FROM SHELTERS").Rows)
                            sb.AppendLine(string.Join(",", row.ItemArray));

                        sb.AppendLine();
                        sb.AppendLine("=== FOOD INVENTORY ===");
                        sb.AppendLine("ItemName,Quantity,ExpirationDate");
                        foreach (DataRow row in DBHelper.GetData(
                            "SELECT ItemName, Quantity, ExpirationDate FROM INVENTORY WHERE ItemType = 'Food'").Rows)
                            sb.AppendLine(string.Join(",", row.ItemArray));

                        sb.AppendLine();
                        sb.AppendLine("=== MEDICAL INVENTORY ===");
                        sb.AppendLine("ItemName,Quantity,Dosage,PrescriptionRequired");
                        foreach (DataRow row in DBHelper.GetData(
                            "SELECT ItemName, Quantity, Dosage, IsPrescriptionRequired FROM INVENTORY WHERE ItemType = 'Medical'").Rows)
                            sb.AppendLine(string.Join(",", row.ItemArray));

                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString());
                        MessageBox.Show("Report exported!", "Export Complete",
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

        // ── NAVIGATION ────────────────────────────────────────────
        private void Navigate(Form destination)
        {
            destination.FormClosed += (s, e) => { this.Show(); LoadAllReports(); };
            this.Hide();
            destination.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadAllReports();

        private void btnDashboard_Click(object sender, EventArgs e) => this.Close();

        private void btnShelter_Click(object sender, EventArgs e)
            => Navigate(new ShelterForm(isAdminMode));

        private void btnInventory_Click(object sender, EventArgs e)
            => Navigate(new InventoryForm(isAdminMode));
    }
}