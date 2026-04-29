namespace ProjectBReady.Forms
{
    partial class InventoryItemEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblFormTitle = new System.Windows.Forms.Label();
            lblItemName = new System.Windows.Forms.Label();
            txtItemName = new System.Windows.Forms.TextBox();
            lblQuantity = new System.Windows.Forms.Label();
            numQuantity = new System.Windows.Forms.NumericUpDown();
            pnlFoodFields = new System.Windows.Forms.Panel();
            lblExpiration = new System.Windows.Forms.Label();
            dtpExpiration = new System.Windows.Forms.DateTimePicker();
            pnlMedicalFields = new System.Windows.Forms.Panel();
            lblDosage = new System.Windows.Forms.Label();
            txtDosage = new System.Windows.Forms.TextBox();
            chkPrescription = new System.Windows.Forms.CheckBox();
            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            pnlFoodFields.SuspendLayout();
            pnlMedicalFields.SuspendLayout();
            SuspendLayout();

            // lblFormTitle
            lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(0, 0, 64);
            lblFormTitle.Location = new System.Drawing.Point(20, 20);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new System.Drawing.Size(360, 40);
            lblFormTitle.Text = "Add Item";

            // lblItemName
            lblItemName.AutoSize = true;
            lblItemName.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblItemName.Location = new System.Drawing.Point(20, 75);
            lblItemName.Name = "lblItemName";
            lblItemName.Text = "Item Name";

            // txtItemName
            txtItemName.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtItemName.Location = new System.Drawing.Point(20, 100);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new System.Drawing.Size(360, 30);

            // lblQuantity
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblQuantity.Location = new System.Drawing.Point(20, 145);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Text = "Quantity";

            // numQuantity
            numQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            numQuantity.Location = new System.Drawing.Point(20, 170);
            numQuantity.Maximum = 100000;
            numQuantity.Minimum = 0;
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new System.Drawing.Size(360, 30);

            // pnlFoodFields
            pnlFoodFields.Controls.Add(lblExpiration);
            pnlFoodFields.Controls.Add(dtpExpiration);
            pnlFoodFields.Location = new System.Drawing.Point(20, 215);
            pnlFoodFields.Name = "pnlFoodFields";
            pnlFoodFields.Size = new System.Drawing.Size(360, 80);

            lblExpiration.AutoSize = true;
            lblExpiration.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblExpiration.Location = new System.Drawing.Point(0, 0);
            lblExpiration.Name = "lblExpiration";
            lblExpiration.Text = "Expiration Date";

            dtpExpiration.Font = new System.Drawing.Font("Segoe UI", 10F);
            dtpExpiration.Location = new System.Drawing.Point(0, 25);
            dtpExpiration.Name = "dtpExpiration";
            dtpExpiration.Size = new System.Drawing.Size(360, 30);

            // pnlMedicalFields
            pnlMedicalFields.Controls.Add(lblDosage);
            pnlMedicalFields.Controls.Add(txtDosage);
            pnlMedicalFields.Controls.Add(chkPrescription);
            pnlMedicalFields.Location = new System.Drawing.Point(20, 215);
            pnlMedicalFields.Name = "pnlMedicalFields";
            pnlMedicalFields.Size = new System.Drawing.Size(360, 110);

            lblDosage.AutoSize = true;
            lblDosage.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDosage.Location = new System.Drawing.Point(0, 0);
            lblDosage.Name = "lblDosage";
            lblDosage.Text = "Dosage";

            txtDosage.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtDosage.Location = new System.Drawing.Point(0, 25);
            txtDosage.Name = "txtDosage";
            txtDosage.Size = new System.Drawing.Size(360, 30);

            chkPrescription.AutoSize = true;
            chkPrescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            chkPrescription.Location = new System.Drawing.Point(0, 70);
            chkPrescription.Name = "chkPrescription";
            chkPrescription.Text = "Prescription Required";

            // btnSave
            btnSave.BackColor = System.Drawing.Color.FromArgb(255, 125, 40);
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(20, 330);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(170, 42);
            btnSave.Text = "💾 Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(210, 330);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(170, 42);
            btnCancel.Text = "✖ Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            // InventoryItemEditForm
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(400, 395);
            Controls.Add(lblFormTitle);
            Controls.Add(lblItemName);
            Controls.Add(txtItemName);
            Controls.Add(lblQuantity);
            Controls.Add(numQuantity);
            Controls.Add(pnlFoodFields);
            Controls.Add(pnlMedicalFields);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InventoryItemEditForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add Item";

            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            pnlFoodFields.ResumeLayout(false);
            pnlFoodFields.PerformLayout();
            pnlMedicalFields.ResumeLayout(false);
            pnlMedicalFields.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Panel pnlFoodFields;
        private System.Windows.Forms.Label lblExpiration;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Panel pnlMedicalFields;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.CheckBox chkPrescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}