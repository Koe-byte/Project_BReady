// ============================================================
//  ShelterForm.cs  —  ProjectBReady
//  Feature: Read + Add Shelters
//  Connects to: DBHelper, Shelter model, SHELTERS table
// ============================================================
using System;
using System.Data;
using System.Windows.Forms;
using ProjectBReady.Data;
using ProjectBReady.Models.Facilities;

namespace ProjectBReady.Forms
{
    public partial class ShelterForm : Form
    {
        // ── Constructor ──────────────────────────────────────────
        public ShelterForm()
        {
            InitializeComponent();
            LoadShelters();
        }

        // ── READ: Kunin lahat ng shelters mula sa DB ─────────────
        private void LoadShelters()
        {
            string query = @"
                SELECT
                    ShelterID       AS [ID],
                    ShelterName     AS [Shelter Name],
                    MaxCapacity     AS [Max Capacity],
                    CurrentOccupancy AS [Current Occupancy],
                    Status
                FROM SHELTERS
                ORDER BY ShelterName";

            DataTable dt = DBHelper.GetData(query);
            dgvShelters.DataSource = dt;

            // Style the DataGridView after loading
            FormatGrid();
        }

        // ── ADD: I-save ang bagong Shelter sa DB ─────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Basic validation
            if (string.IsNullOrWhiteSpace(txtShelterName.Text))
            {
                MessageBox.Show("Please enter a shelter name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtShelterName.Focus();
                return;
            }

            if (!int.TryParse(txtMaxCapacity.Text, out int maxCap) || maxCap <= 0)
            {
                MessageBox.Show("Please enter a valid max capacity (positive number).", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaxCapacity.Focus();
                return;
            }

            if (!int.TryParse(txtCurrentOccupancy.Text, out int currOcc) || currOcc < 0)
            {
                MessageBox.Show("Please enter a valid current occupancy (0 or more).", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentOccupancy.Focus();
                return;
            }

            // 2. Use the Shelter class (OOP!) to validate business rules
            //    This is ENCAPSULATION in action — logic lives in the model, not the form
            if (currOcc > maxCap)
            {
                MessageBox.Show("Current occupancy cannot exceed max capacity!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Generate a simple ID (in production, use IDENTITY column or GUID)
            string shelterID = "SH" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string shelterName = txtShelterName.Text.Trim().Replace("'", "''"); // basic SQL injection guard
            string status = cboStatus.SelectedItem?.ToString() ?? "Active";

            // 4. Build and run the INSERT query via DBHelper
            string insertQuery = $@"
                INSERT INTO SHELTERS (ShelterID, ShelterName, MaxCapacity, CurrentOccupancy, Status)
                VALUES ('{shelterID}', '{shelterName}', {maxCap}, {currOcc}, '{status}')";

            bool success = DBHelper.ExecuteQuery(insertQuery);

            if (success)
            {
                MessageBox.Show($"Shelter '{txtShelterName.Text}' added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadShelters(); // Refresh the grid
            }
        }

        // ── CLEAR: I-reset ang form fields ───────────────────────
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtShelterName.Clear();
            txtMaxCapacity.Clear();
            txtCurrentOccupancy.Text = "0";
            cboStatus.SelectedIndex = 0;
            txtShelterName.Focus();
        }

        // ── REFRESH button ───────────────────────────────────────
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadShelters();
        }

        // ── FORMAT: Style ang DataGridView ───────────────────────
        private void FormatGrid()
        {
            if (dgvShelters.Columns.Count == 0) return;

            dgvShelters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShelters.RowHeadersVisible = false;
            dgvShelters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShelters.ReadOnly = true;

            // Color-code rows by occupancy status
            foreach (DataGridViewRow row in dgvShelters.Rows)
            {
                if (row.IsNewRow) continue;

                if (int.TryParse(row.Cells["Max Capacity"].Value?.ToString(), out int max) &&
                    int.TryParse(row.Cells["Current Occupancy"].Value?.ToString(), out int curr))
                {
                    double pct = max > 0 ? (double)curr / max : 0;

                    if (pct >= 1.0)
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 220, 220); // red — full
                    else if (pct >= 0.75)
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 243, 205); // yellow — near full
                    else
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 242, 220); // green — ok
                }
            }
        }
    }
}
