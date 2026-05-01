using System;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;

namespace ProjectBReady.Forms
{
    public partial class InventoryItemEditForm : Form
    {
        private string itemType;

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

            bool success;

            if (itemType == "Food")
            {
                // Single INVENTORY table — ItemType = 'Food'
                success = DBHelper.ExecuteNonQuery(
                    @"INSERT INTO INVENTORY (ItemName, Quantity, ItemType, ExpirationDate)
                      VALUES (@name, @qty, 'Food', @exp)",
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        { "@name", txtItemName.Text.Trim() },
                        { "@qty",  (int)numQuantity.Value },
                        { "@exp",  dtpExpiration.Value.ToString("yyyy-MM-dd") }
                    });
            }
            else
            {
                // Single INVENTORY table — ItemType = 'Medical'
                success = DBHelper.ExecuteNonQuery(
                    @"INSERT INTO INVENTORY (ItemName, Quantity, ItemType, Dosage, IsPrescriptionRequired)
                      VALUES (@name, @qty, 'Medical', @dosage, @rx)",
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        { "@name",   txtItemName.Text.Trim() },
                        { "@qty",    (int)numQuantity.Value },
                        { "@dosage", txtDosage.Text.Trim() },
                        { "@rx",     chkPrescription.Checked ? 1 : 0 }
                    });
            }

            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}