// ============================================================
//  ShelterEditForm.cs  —  ProjectBReady
//  Add or Edit a Shelter record
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;

namespace ProjectBReady.Forms
{
    public partial class ShelterEditForm : Form
    {
        private int shelterId = -1;
        private bool isEdit = false;

        // New shelter
        public ShelterEditForm()
        {
            InitializeComponent();
            this.Text = "Add Shelter";
            lblFormTitle.Text = "Add New Shelter";
        }

        // Edit existing
        public ShelterEditForm(int id)
        {
            InitializeComponent();
            shelterId = id;
            isEdit = true;
            this.Text = "Edit Shelter";
            lblFormTitle.Text = "Edit Shelter";
            LoadShelterData();
        }

        private void LoadShelterData()
        {
            try
            {
                string query = "SELECT * FROM SHELTERS WHERE ShelterID = @id";
                DataTable dt = DBHelper.GetData(query,
                    new System.Collections.Generic.Dictionary<string, object> { { "@id", shelterId } });

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtShelterName.Text = row["ShelterName"].ToString();
                    numMaxCapacity.Value = Convert.ToInt32(row["MaxCapacity"]);
                    numCurrentOccupancy.Value = Convert.ToInt32(row["CurrentOccupancy"]);
                    cmbStatus.SelectedItem = row["Status"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shelter data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtShelterName.Text))
            {
                MessageBox.Show("Please enter a shelter name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var parms = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@name", txtShelterName.Text.Trim() },
                    { "@max",  (int)numMaxCapacity.Value },
                    { "@curr", (int)numCurrentOccupancy.Value },
                    { "@status", cmbStatus.SelectedItem?.ToString() ?? "Open" }
                };

                if (isEdit)
                {
                    parms.Add("@id", shelterId);
                    DBHelper.ExecuteNonQuery(
                        @"UPDATE SHELTERS SET ShelterName=@name, MaxCapacity=@max,
                          CurrentOccupancy=@curr, Status=@status WHERE ShelterID=@id",
                        parms);
                }
                else
                {
                    DBHelper.ExecuteNonQuery(
                        @"INSERT INTO SHELTERS (ShelterName, MaxCapacity, CurrentOccupancy, Status)
                          VALUES (@name, @max, @curr, @status)",
                        parms);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving shelter: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}