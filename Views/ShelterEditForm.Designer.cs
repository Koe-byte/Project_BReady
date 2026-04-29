namespace ProjectBReady.Forms
{
    partial class ShelterEditForm
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
            lblName = new System.Windows.Forms.Label();
            txtShelterName = new System.Windows.Forms.TextBox();
            lblMaxCap = new System.Windows.Forms.Label();
            numMaxCapacity = new System.Windows.Forms.NumericUpDown();
            lblCurrOcc = new System.Windows.Forms.Label();
            numCurrentOccupancy = new System.Windows.Forms.NumericUpDown();
            lblStatus = new System.Windows.Forms.Label();
            cmbStatus = new System.Windows.Forms.ComboBox();
            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)numMaxCapacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCurrentOccupancy).BeginInit();
            SuspendLayout();

            // ── lblFormTitle ────────────────────────────────────
            lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(0, 0, 64);
            lblFormTitle.Location = new System.Drawing.Point(20, 20);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new System.Drawing.Size(360, 40);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "Add New Shelter";

            // ── lblName ─────────────────────────────────────────
            lblName.AutoSize = true;
            lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblName.Location = new System.Drawing.Point(20, 75);
            lblName.Name = "lblName";
            lblName.TabIndex = 1;
            lblName.Text = "Shelter Name";

            // ── txtShelterName ──────────────────────────────────
            txtShelterName.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtShelterName.Location = new System.Drawing.Point(20, 100);
            txtShelterName.Name = "txtShelterName";
            txtShelterName.Size = new System.Drawing.Size(360, 30);
            txtShelterName.TabIndex = 2;

            // ── lblMaxCap ───────────────────────────────────────
            lblMaxCap.AutoSize = true;
            lblMaxCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblMaxCap.Location = new System.Drawing.Point(20, 145);
            lblMaxCap.Name = "lblMaxCap";
            lblMaxCap.TabIndex = 3;
            lblMaxCap.Text = "Maximum Capacity";

            // ── numMaxCapacity ──────────────────────────────────
            numMaxCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            numMaxCapacity.Location = new System.Drawing.Point(20, 170);
            numMaxCapacity.Maximum = 10000;
            numMaxCapacity.Minimum = 0;
            numMaxCapacity.Name = "numMaxCapacity";
            numMaxCapacity.Size = new System.Drawing.Size(360, 30);
            numMaxCapacity.TabIndex = 4;

            // ── lblCurrOcc ──────────────────────────────────────
            lblCurrOcc.AutoSize = true;
            lblCurrOcc.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblCurrOcc.Location = new System.Drawing.Point(20, 215);
            lblCurrOcc.Name = "lblCurrOcc";
            lblCurrOcc.TabIndex = 5;
            lblCurrOcc.Text = "Current Occupancy";

            // ── numCurrentOccupancy ─────────────────────────────
            numCurrentOccupancy.Font = new System.Drawing.Font("Segoe UI", 10F);
            numCurrentOccupancy.Location = new System.Drawing.Point(20, 240);
            numCurrentOccupancy.Maximum = 10000;
            numCurrentOccupancy.Minimum = 0;
            numCurrentOccupancy.Name = "numCurrentOccupancy";
            numCurrentOccupancy.Size = new System.Drawing.Size(360, 30);
            numCurrentOccupancy.TabIndex = 6;

            // ── lblStatus ───────────────────────────────────────
            lblStatus.AutoSize = true;
            lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblStatus.Location = new System.Drawing.Point(20, 285);
            lblStatus.Name = "lblStatus";
            lblStatus.TabIndex = 7;
            lblStatus.Text = "Status";

            // ── cmbStatus ───────────────────────────────────────
            cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbStatus.Items.AddRange(new object[] { "Open", "Full", "Closed", "Under Maintenance" });
            cmbStatus.Location = new System.Drawing.Point(20, 310);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.SelectedIndex = 0;
            cmbStatus.Size = new System.Drawing.Size(360, 30);
            cmbStatus.TabIndex = 8;

            // ── btnSave ─────────────────────────────────────────
            btnSave.BackColor = System.Drawing.Color.FromArgb(255, 125, 40);
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(20, 365);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(170, 42);
            btnSave.TabIndex = 9;
            btnSave.Text = "💾 Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            // ── btnCancel ───────────────────────────────────────
            btnCancel.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(210, 365);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(170, 42);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "✖ Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            // ── ShelterEditForm ─────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(400, 430);
            Controls.Add(lblFormTitle);
            Controls.Add(lblName);
            Controls.Add(txtShelterName);
            Controls.Add(lblMaxCap);
            Controls.Add(numMaxCapacity);
            Controls.Add(lblCurrOcc);
            Controls.Add(numCurrentOccupancy);
            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ShelterEditForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add Shelter";

            ((System.ComponentModel.ISupportInitialize)numMaxCapacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCurrentOccupancy).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtShelterName;
        private System.Windows.Forms.Label lblMaxCap;
        private System.Windows.Forms.NumericUpDown numMaxCapacity;
        private System.Windows.Forms.Label lblCurrOcc;
        private System.Windows.Forms.NumericUpDown numCurrentOccupancy;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}