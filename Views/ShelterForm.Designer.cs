namespace ProjectBReady.Forms
{
    partial class ShelterForm
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
            pnlSidebar = new System.Windows.Forms.Panel();
            toggleText = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            btnDashboard = new System.Windows.Forms.Button();
            btnShelterNav = new System.Windows.Forms.Button();
            btnInventory = new System.Windows.Forms.Button();
            btnReports = new System.Windows.Forms.Button();

            pnlTopBar = new System.Windows.Forms.Panel();
            lblPageTitle = new System.Windows.Forms.Label();
            lblPageSubtitle = new System.Windows.Forms.Label();
            btnAddShelter = new System.Windows.Forms.Button();

            pnlStats = new System.Windows.Forms.Panel();
            lblTotalShelters = new System.Windows.Forms.Label();
            lblTotalCapacity = new System.Windows.Forms.Label();

            pnlActions = new System.Windows.Forms.Panel();
            btnEditShelter = new System.Windows.Forms.Button();
            btnDeleteShelter = new System.Windows.Forms.Button();
            btnUpdateOccupancy = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();

            dgvShelters = new System.Windows.Forms.DataGridView();

            pnlSidebar.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShelters).BeginInit();
            SuspendLayout();

            // ── pnlSidebar ──────────────────────────────────────
            pnlSidebar.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            pnlSidebar.Controls.Add(toggleText);
            pnlSidebar.Controls.Add(label1);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(btnShelterNav);
            pnlSidebar.Controls.Add(btnInventory);
            pnlSidebar.Controls.Add(btnReports);
            pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlSidebar.Location = new System.Drawing.Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new System.Drawing.Size(242, 737);
            pnlSidebar.TabIndex = 0;

            // ── toggleText ──────────────────────────────────────
            toggleText.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            toggleText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            toggleText.ForeColor = System.Drawing.Color.White;
            toggleText.Location = new System.Drawing.Point(3, 707);
            toggleText.Name = "toggleText";
            toggleText.ReadOnly = true;
            toggleText.Size = new System.Drawing.Size(236, 20);
            toggleText.TabIndex = 1;
            toggleText.Text = "Ctrl+Shift+O to toggle admin";
            toggleText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // ── label1 (App Name) ───────────────────────────────
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(55, 55);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(155, 20);
            label1.TabIndex = 2;
            label1.Text = "Disaster Relief System";
            label1.Click += label1_Click;

            // ── btnDashboard ────────────────────────────────────
            btnDashboard.ForeColor = System.Drawing.Color.Black;
            btnDashboard.Location = new System.Drawing.Point(43, 110);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new System.Drawing.Size(158, 67);
            btnDashboard.TabIndex = 3;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;

            // ── btnShelterNav (active) ──────────────────────────
            btnShelterNav.BackColor = System.Drawing.Color.FromArgb(255, 125, 40);
            btnShelterNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnShelterNav.ForeColor = System.Drawing.Color.White;
            btnShelterNav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnShelterNav.Location = new System.Drawing.Point(43, 183);
            btnShelterNav.Name = "btnShelterNav";
            btnShelterNav.Size = new System.Drawing.Size(158, 67);
            btnShelterNav.TabIndex = 4;
            btnShelterNav.Text = "Shelter";
            btnShelterNav.UseVisualStyleBackColor = false;

            // ── btnInventory ────────────────────────────────────
            btnInventory.ForeColor = System.Drawing.Color.Black;
            btnInventory.Location = new System.Drawing.Point(43, 256);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new System.Drawing.Size(158, 69);
            btnInventory.TabIndex = 5;
            btnInventory.Text = "Inventory";
            btnInventory.UseVisualStyleBackColor = true;
            btnInventory.Click += btnInventory_Click;

            // ── btnReports ──────────────────────────────────────
            btnReports.ForeColor = System.Drawing.Color.Black;
            btnReports.Location = new System.Drawing.Point(43, 331);
            btnReports.Name = "btnReports";
            btnReports.Size = new System.Drawing.Size(158, 69);
            btnReports.TabIndex = 6;
            btnReports.Text = "Reports";
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;

            // ── pnlTopBar ───────────────────────────────────────
            pnlTopBar.Controls.Add(lblPageTitle);
            pnlTopBar.Controls.Add(lblPageSubtitle);
            pnlTopBar.Controls.Add(btnAddShelter);
            pnlTopBar.Location = new System.Drawing.Point(252, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new System.Drawing.Size(1165, 115);
            pnlTopBar.TabIndex = 1;

            // ── lblPageTitle ────────────────────────────────────
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            lblPageTitle.ForeColor = System.Drawing.Color.DimGray;
            lblPageTitle.Location = new System.Drawing.Point(10, 10);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(405, 55);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Evacuation Shelters";

            // ── lblPageSubtitle ─────────────────────────────────
            lblPageSubtitle.AutoSize = true;
            lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblPageSubtitle.ForeColor = System.Drawing.Color.Black;
            lblPageSubtitle.Location = new System.Drawing.Point(14, 65);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new System.Drawing.Size(338, 28);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Manage shelter occupancy and status";

            // ── btnAddShelter ───────────────────────────────────
            btnAddShelter.BackColor = System.Drawing.Color.FromArgb(255, 125, 40);
            btnAddShelter.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddShelter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddShelter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnAddShelter.ForeColor = System.Drawing.Color.White;
            btnAddShelter.Location = new System.Drawing.Point(964, 30);
            btnAddShelter.Name = "btnAddShelter";
            btnAddShelter.Size = new System.Drawing.Size(154, 54);
            btnAddShelter.TabIndex = 2;
            btnAddShelter.Text = "+ Add Shelter";
            btnAddShelter.UseVisualStyleBackColor = false;
            btnAddShelter.Click += btnAddShelter_Click;

            // ── pnlStats ────────────────────────────────────────
            pnlStats.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlStats.Controls.Add(lblTotalShelters);
            pnlStats.Controls.Add(lblTotalCapacity);
            pnlStats.Location = new System.Drawing.Point(252, 120);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new System.Drawing.Size(1165, 50);
            pnlStats.TabIndex = 2;

            // ── lblTotalShelters ────────────────────────────────
            lblTotalShelters.AutoSize = true;
            lblTotalShelters.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblTotalShelters.ForeColor = System.Drawing.Color.FromArgb(0, 0, 64);
            lblTotalShelters.Location = new System.Drawing.Point(15, 14);
            lblTotalShelters.Name = "lblTotalShelters";
            lblTotalShelters.TabIndex = 0;
            lblTotalShelters.Text = "Total Shelters: 0";

            // ── lblTotalCapacity ────────────────────────────────
            lblTotalCapacity.AutoSize = true;
            lblTotalCapacity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblTotalCapacity.ForeColor = System.Drawing.Color.FromArgb(0, 0, 64);
            lblTotalCapacity.Location = new System.Drawing.Point(220, 14);
            lblTotalCapacity.Name = "lblTotalCapacity";
            lblTotalCapacity.TabIndex = 1;
            lblTotalCapacity.Text = "Total Capacity: 0 / 0";

            // ── pnlActions ──────────────────────────────────────
            pnlActions.Controls.Add(btnEditShelter);
            pnlActions.Controls.Add(btnDeleteShelter);
            pnlActions.Controls.Add(btnUpdateOccupancy);
            pnlActions.Controls.Add(btnRefresh);
            pnlActions.Location = new System.Drawing.Point(252, 175);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new System.Drawing.Size(1165, 55);
            pnlActions.TabIndex = 3;

            // ── btnEditShelter ──────────────────────────────────
            btnEditShelter.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            btnEditShelter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEditShelter.ForeColor = System.Drawing.Color.White;
            btnEditShelter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnEditShelter.Location = new System.Drawing.Point(0, 8);
            btnEditShelter.Name = "btnEditShelter";
            btnEditShelter.Size = new System.Drawing.Size(120, 38);
            btnEditShelter.TabIndex = 0;
            btnEditShelter.Text = "✏ Edit";
            btnEditShelter.UseVisualStyleBackColor = false;
            btnEditShelter.Click += btnEditShelter_Click;

            // ── btnDeleteShelter ────────────────────────────────
            btnDeleteShelter.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            btnDeleteShelter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDeleteShelter.ForeColor = System.Drawing.Color.White;
            btnDeleteShelter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnDeleteShelter.Location = new System.Drawing.Point(128, 8);
            btnDeleteShelter.Name = "btnDeleteShelter";
            btnDeleteShelter.Size = new System.Drawing.Size(120, 38);
            btnDeleteShelter.TabIndex = 1;
            btnDeleteShelter.Text = "🗑 Delete";
            btnDeleteShelter.UseVisualStyleBackColor = false;
            btnDeleteShelter.Click += btnDeleteShelter_Click;

            // ── btnUpdateOccupancy ──────────────────────────────
            btnUpdateOccupancy.BackColor = System.Drawing.Color.FromArgb(31, 158, 117);
            btnUpdateOccupancy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpdateOccupancy.ForeColor = System.Drawing.Color.White;
            btnUpdateOccupancy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnUpdateOccupancy.Location = new System.Drawing.Point(256, 8);
            btnUpdateOccupancy.Name = "btnUpdateOccupancy";
            btnUpdateOccupancy.Size = new System.Drawing.Size(160, 38);
            btnUpdateOccupancy.TabIndex = 2;
            btnUpdateOccupancy.Text = "👥 Update Occupancy";
            btnUpdateOccupancy.UseVisualStyleBackColor = false;
            btnUpdateOccupancy.Click += btnUpdateOccupancy_Click;

            // ── btnRefresh ──────────────────────────────────────
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRefresh.Location = new System.Drawing.Point(424, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(100, 38);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            // ── dgvShelters ─────────────────────────────────────
            dgvShelters.AllowUserToAddRows = false;
            dgvShelters.AllowUserToDeleteRows = false;
            dgvShelters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvShelters.BackgroundColor = System.Drawing.Color.White;
            dgvShelters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dgvShelters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvShelters.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            dgvShelters.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvShelters.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvShelters.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvShelters.Location = new System.Drawing.Point(252, 235);
            dgvShelters.MultiSelect = false;
            dgvShelters.Name = "dgvShelters";
            dgvShelters.ReadOnly = true;
            dgvShelters.RowHeadersVisible = false;
            dgvShelters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvShelters.Size = new System.Drawing.Size(1165, 490);
            dgvShelters.TabIndex = 4;
            dgvShelters.SelectionChanged += dgvShelters_SelectionChanged;
            dgvShelters.RowPrePaint += dgvShelters_RowPrePaint;

            // ── ShelterForm ─────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1417, 737);
            Controls.Add(dgvShelters);
            Controls.Add(pnlActions);
            Controls.Add(pnlStats);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlSidebar);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "ShelterForm";
            Text = "Shelters — ProjectBReady";

            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            pnlActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvShelters).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.TextBox toggleText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnShelterNav;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Button btnAddShelter;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblTotalShelters;
        private System.Windows.Forms.Label lblTotalCapacity;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnEditShelter;
        private System.Windows.Forms.Button btnDeleteShelter;
        private System.Windows.Forms.Button btnUpdateOccupancy;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvShelters;
    }
}