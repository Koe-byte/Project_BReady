// ============================================================
//  InventoryItemEditForm.cs  —  ProjectBReady
//  Add a new Food Item or Medical Supply
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;

namespace ProjectBReady.Forms
{
    public partial class InventoryItemEditForm : Form
    {
        private string itemType; // "Food" or "Medical"

        public InventoryItemEditForm(string type)
        {
            InitializeComponent();
            itemType = type;

            if (type == "Food")
            {
                lblFormTitle.Text = "Add Food Item";
                pnlMedicalFields.Visible = false;
                pnlFoodFields.Visible = true;
                this.ClientSize = new Size(400, 400);
            }
            else
            {
                lblFormTitle.Text = "Add Medical Supply";
                pnlFoodFields.Visible = false;
                pnlMedicalFields.Visible = true;
                this.ClientSize = new Size(400, 430);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Please enter an item name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (itemType == "Food")
                {
                    DBHelper.ExecuteNonQuery(
                        @"INSERT INTO FOOD_ITEMS (ItemName, Quantity, ExpirationDate)
                          VALUES (@name, @qty, @exp)",
                        new System.Collections.Generic.Dictionary<string, object>
                        {
                            { "@name", txtItemName.Text.Trim() },
                            { "@qty",  (int)numQuantity.Value },
                            { "@exp",  dtpExpiration.Value }
                        });
                }
                else
                {
                    DBHelper.ExecuteNonQuery(
                        @"INSERT INTO MEDICAL_SUPPLIES (ItemName, Quantity, Dosage, IsPrescriptionRequired)
                          VALUES (@name, @qty, @dosage, @rx)",
                        new System.Collections.Generic.Dictionary<string, object>
                        {
                            { "@name",   txtItemName.Text.Trim() },
                            { "@qty",    (int)numQuantity.Value },
                            { "@dosage", txtDosage.Text.Trim() },
                            { "@rx",     chkPrescription.Checked }
                        });
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving item: {ex.Message}", "Error",
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