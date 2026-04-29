namespace ProjectBReady.Forms
{
    partial class ReportForm
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
            lblAppName = new System.Windows.Forms.Label();
            btnDashboard = new System.Windows.Forms.Button();
            btnShelter = new System.Windows.Forms.Button();
            btnInventory = new System.Windows.Forms.Button();
            btnReportsNav = new System.Windows.Forms.Button();

            pnlTopBar = new System.Windows.Forms.Panel();
            lblPageTitle = new System.Windows.Forms.Label();
            lblPageSubtitle = new System.Windows.Forms.Label();
            btnExportReport = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();

            // Summary cards
            pnlCards = new System.Windows.Forms.Panel();
            pnlCardShelter = new System.Windows.Forms.Panel();
            lblCardShelterTitle = new System.Windows.Forms.Label();
            lblCardShelters = new System.Windows.Forms.Label();
            lblCardOccupancy = new System.Windows.Forms.Label();
            lblCardPct = new System.Windows.Forms.Label();
            lblCardFull = new System.Windows.Forms.Label();

            pnlCardFood = new System.Windows.Forms.Panel();
            lblCardFoodTitle = new System.Windows.Forms.Label();
            lblCardFoodTypes = new System.Windows.Forms.Label();
            lblCardFoodUnits = new System.Windows.Forms.Label();

            pnlCardMed = new System.Windows.Forms.Panel();
            lblCardMedTitle = new System.Windows.Forms.Label();
            lblCardMedTypes = new System.Windows.Forms.Label();
            lblCardMedUnits = new System.Windows.Forms.Label();

            // Tab control
            tabReport = new System.Windows.Forms.TabControl();
            tabShelters = new System.Windows.Forms.TabPage();
            tabFood = new System.Windows.Forms.TabPage();
            tabMedical = new System.Windows.Forms.TabPage();
            tabExpiring = new System.Windows.Forms.TabPage();

            dgvShelterReport = new System.Windows.Forms.DataGridView();
            dgvFoodReport = new System.Windows.Forms.DataGridView();
            dgvMedReport = new System.Windows.Forms.DataGridView();
            pnlExpiringHeader = new System.Windows.Forms.Panel();
            lblExpiringCount = new System.Windows.Forms.Label();
            dgvExpiring = new System.Windows.Forms.DataGridView();

            pnlSidebar.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlCards.SuspendLayout();
            tabReport.SuspendLayout();
            tabShelters.SuspendLayout();
            tabFood.SuspendLayout();
            tabMedical.SuspendLayout();
            tabExpiring.SuspendLayout();
            pnlExpiringHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShelterReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFoodReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMedReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvExpiring).BeginInit();
            SuspendLayout();

            // ── pnlSidebar ──────────────────────────────────────
            pnlSidebar.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            pnlSidebar.Controls.Add(toggleText);
            pnlSidebar.Controls.Add(lblAppName);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(btnShelter);
            pnlSidebar.Controls.Add(btnInventory);
            pnlSidebar.Controls.Add(btnReportsNav);
            pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlSidebar.Size = new System.Drawing.Size(242, 737);

            toggleText.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            toggleText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            toggleText.ForeColor = System.Drawing.Color.White;
            toggleText.Location = new System.Drawing.Point(3, 707);
            toggleText.ReadOnly = true;
            toggleText.Size = new System.Drawing.Size(236, 20);
            toggleText.Text = "Ctrl+Shift+O to toggle admin";
            toggleText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            lblAppName.AutoSize = true;
            lblAppName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            lblAppName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblAppName.Location = new System.Drawing.Point(55, 55);
            lblAppName.Text = "Disaster Relief System";

            SetupSidebarButton(btnDashboard, "Dashboard", 110);
            btnDashboard.Click += btnDashboard_Click;

            SetupSidebarButton(btnShelter, "Shelter", 183);
            btnShelter.Click += btnShelter_Click;

            SetupSidebarButton(btnInventory, "Inventory", 256);
            btnInventory.Click += btnInventory_Click;

            btnReportsNav.BackColor = System.Drawing.Color.FromArgb(255, 125, 40);
            btnReportsNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReportsNav.ForeColor = System.Drawing.Color.White;
            btnReportsNav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnReportsNav.Location = new System.Drawing.Point(43, 331);
            btnReportsNav.Size = new System.Drawing.Size(158, 69);
            btnReportsNav.Text = "Reports";
            btnReportsNav.UseVisualStyleBackColor = false;

            // ── pnlTopBar ───────────────────────────────────────
            pnlTopBar.Controls.Add(lblPageTitle);
            pnlTopBar.Controls.Add(lblPageSubtitle);
            pnlTopBar.Controls.Add(btnExportReport);
            pnlTopBar.Controls.Add(btnRefresh);
            pnlTopBar.Location = new System.Drawing.Point(252, 0);
            pnlTopBar.Size = new System.Drawing.Size(1165, 100);

            lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            lblPageTitle.ForeColor = System.Drawing.Color.DimGray;
            lblPageTitle.Location = new System.Drawing.Point(10, 10);
            lblPageTitle.Size = new System.Drawing.Size(405, 55);
            lblPageTitle.Text = "Reports";

            lblPageSubtitle.AutoSize = true;
            lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblPageSubtitle.ForeColor = System.Drawing.Color.Black;
            lblPageSubtitle.Location = new System.Drawing.Point(14, 65);
            lblPageSubtitle.Text = "Shelter occupancy & inventory summary";

            btnExportReport.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            btnExportReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExportReport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnExportReport.ForeColor = System.Drawing.Color.White;
            btnExportReport.Location = new System.Drawing.Point(970, 28);
            btnExportReport.Size = new System.Drawing.Size(150, 42);
            btnExportReport.Text = "📥 Export CSV";
            btnExportReport.UseVisualStyleBackColor = false;
            btnExportReport.Click += btnExportReport_Click;

            btnRefresh.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(855, 28);
            btnRefresh.Size = new System.Drawing.Size(110, 42);
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            // ── pnlCards ────────────────────────────────────────
            pnlCards.Controls.Add(pnlCardShelter);
            pnlCards.Controls.Add(pnlCardFood);
            pnlCards.Controls.Add(pnlCardMed);
            pnlCards.Location = new System.Drawing.Point(252, 105);
            pnlCards.Size = new System.Drawing.Size(1165, 130);

            // Card: Shelters
            pnlCardShelter.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            pnlCardShelter.Controls.Add(lblCardShelterTitle);
            pnlCardShelter.Controls.Add(lblCardShelters);
            pnlCardShelter.Controls.Add(lblCardOccupancy);
            pnlCardShelter.Controls.Add(lblCardPct);
            pnlCardShelter.Controls.Add(lblCardFull);
            pnlCardShelter.Location = new System.Drawing.Point(0, 0);
            pnlCardShelter.Size = new System.Drawing.Size(380, 120);

            MakeCardLabel(lblCardShelterTitle, "SHELTERS", 10, 10, 14, System.Drawing.Color.FromArgb(255, 125, 40), true);
            MakeCardLabel(lblCardShelters, "0", 10, 35, 28, System.Drawing.Color.White, true);
            MakeCardLabel(lblCardOccupancy, "0 / 0 occupied", 10, 70, 11, System.Drawing.Color.FromArgb(200, 200, 220), false);
            MakeCardLabel(lblCardPct, "0% Full", 200, 35, 16, System.Drawing.Color.White, false);
            MakeCardLabel(lblCardFull, "0 Full", 200, 65, 10, System.Drawing.Color.FromArgb(220, 53, 69), false);

            // Card: Food
            pnlCardFood.BackColor = System.Drawing.Color.FromArgb(20, 120, 90);
            pnlCardFood.Controls.Add(lblCardFoodTitle);
            pnlCardFood.Controls.Add(lblCardFoodTypes);
            pnlCardFood.Controls.Add(lblCardFoodUnits);
            pnlCardFood.Location = new System.Drawing.Point(390, 0);
            pnlCardFood.Size = new System.Drawing.Size(380, 120);

            MakeCardLabel(lblCardFoodTitle, "FOOD ITEMS", 10, 10, 14, System.Drawing.Color.White, true);
            MakeCardLabel(lblCardFoodTypes, "0 types", 10, 35, 22, System.Drawing.Color.White, true);
            MakeCardLabel(lblCardFoodUnits, "0 units total", 10, 75, 11, System.Drawing.Color.FromArgb(200, 240, 220), false);

            // Card: Medical
            pnlCardMed.BackColor = System.Drawing.Color.FromArgb(60, 60, 130);
            pnlCardMed.Controls.Add(lblCardMedTitle);
            pnlCardMed.Controls.Add(lblCardMedTypes);
            pnlCardMed.Controls.Add(lblCardMedUnits);
            pnlCardMed.Location = new System.Drawing.Point(780, 0);
            pnlCardMed.Size = new System.Drawing.Size(380, 120);

            MakeCardLabel(lblCardMedTitle, "MEDICAL SUPPLIES", 10, 10, 14, System.Drawing.Color.White, true);
            MakeCardLabel(lblCardMedTypes, "0 types", 10, 35, 22, System.Drawing.Color.White, true);
            MakeCardLabel(lblCardMedUnits, "0 units total", 10, 75, 11, System.Drawing.Color.FromArgb(220, 220, 255), false);

            // ── tabReport ───────────────────────────────────────
            tabReport.Controls.Add(tabShelters);
            tabReport.Controls.Add(tabFood);
            tabReport.Controls.Add(tabMedical);
            tabReport.Controls.Add(tabExpiring);
            tabReport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            tabReport.Location = new System.Drawing.Point(252, 240);
            tabReport.Size = new System.Drawing.Size(1165, 490);

            // Tab: Shelters
            tabShelters.BackColor = System.Drawing.Color.White;
            tabShelters.Controls.Add(dgvShelterReport);
            tabShelters.Text = "🏠 Shelters";
            SetupReportGrid(dgvShelterReport, System.Drawing.Color.FromArgb(0, 0, 64));

            // Tab: Food
            tabFood.BackColor = System.Drawing.Color.White;
            tabFood.Controls.Add(dgvFoodReport);
            tabFood.Text = "🍱 Food Inventory";
            SetupReportGrid(dgvFoodReport, System.Drawing.Color.FromArgb(20, 120, 90));

            // Tab: Medical
            tabMedical.BackColor = System.Drawing.Color.White;
            tabMedical.Controls.Add(dgvMedReport);
            tabMedical.Text = "💊 Medical Inventory";
            SetupReportGrid(dgvMedReport, System.Drawing.Color.FromArgb(60, 60, 130));

            // Tab: Expiring
            tabExpiring.BackColor = System.Drawing.Color.White;
            tabExpiring.Controls.Add(pnlExpiringHeader);
            tabExpiring.Controls.Add(dgvExpiring);
            tabExpiring.Text = "⚠ Expiring Items";

            pnlExpiringHeader.BackColor = System.Drawing.Color.FromArgb(255, 243, 205);
            pnlExpiringHeader.Controls.Add(lblExpiringCount);
            pnlExpiringHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlExpiringHeader.Height = 40;

            lblExpiringCount.AutoSize = true;
            lblExpiringCount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblExpiringCount.ForeColor = System.Drawing.Color.FromArgb(180, 80, 0);
            lblExpiringCount.Location = new System.Drawing.Point(10, 10);
            lblExpiringCount.Text = "⚠ 0 item(s) expiring within 30 days";

            SetupReportGrid(dgvExpiring, System.Drawing.Color.FromArgb(180, 80, 0));
            dgvExpiring.Location = new System.Drawing.Point(0, 40);
            dgvExpiring.Dock = System.Windows.Forms.DockStyle.None;
            dgvExpiring.Height = 410;
            dgvExpiring.Dock = System.Windows.Forms.DockStyle.Fill;

            // ── ReportForm ──────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1417, 737);
            Controls.Add(tabReport);
            Controls.Add(pnlCards);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlSidebar);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "ReportForm";
            Text = "Reports — ProjectBReady";

            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlTopBar.ResumeLayout(false);
            pnlCards.ResumeLayout(false);
            pnlExpiringHeader.ResumeLayout(false);
            pnlExpiringHeader.PerformLayout();
            tabShelters.ResumeLayout(false);
            tabFood.ResumeLayout(false);
            tabMedical.ResumeLayout(false);
            tabExpiring.ResumeLayout(false);
            tabReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvShelterReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFoodReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMedReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvExpiring).EndInit();
            ResumeLayout(false);
        }

        private void SetupSidebarButton(System.Windows.Forms.Button btn, string text, int y)
        {
            btn.ForeColor = System.Drawing.Color.Black;
            btn.Location = new System.Drawing.Point(43, y);
            btn.Size = new System.Drawing.Size(158, 67);
            btn.Text = text;
            btn.UseVisualStyleBackColor = true;
        }

        private void SetupReportGrid(System.Windows.Forms.DataGridView dgv, System.Drawing.Color headerColor)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        }

        private void MakeCardLabel(System.Windows.Forms.Label lbl, string text, int x, int y,
            float fontSize, System.Drawing.Color color, bool bold)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", fontSize,
                bold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular);
            lbl.ForeColor = color;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.TextBox toggleText;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnShelter;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnReportsNav;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Button btnExportReport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Panel pnlCardShelter;
        private System.Windows.Forms.Label lblCardShelterTitle;
        private System.Windows.Forms.Label lblCardShelters;
        private System.Windows.Forms.Label lblCardOccupancy;
        private System.Windows.Forms.Label lblCardPct;
        private System.Windows.Forms.Label lblCardFull;
        private System.Windows.Forms.Panel pnlCardFood;
        private System.Windows.Forms.Label lblCardFoodTitle;
        private System.Windows.Forms.Label lblCardFoodTypes;
        private System.Windows.Forms.Label lblCardFoodUnits;
        private System.Windows.Forms.Panel pnlCardMed;
        private System.Windows.Forms.Label lblCardMedTitle;
        private System.Windows.Forms.Label lblCardMedTypes;
        private System.Windows.Forms.Label lblCardMedUnits;
        private System.Windows.Forms.TabControl tabReport;
        private System.Windows.Forms.TabPage tabShelters;
        private System.Windows.Forms.TabPage tabFood;
        private System.Windows.Forms.TabPage tabMedical;
        private System.Windows.Forms.TabPage tabExpiring;
        private System.Windows.Forms.DataGridView dgvShelterReport;
        private System.Windows.Forms.DataGridView dgvFoodReport;
        private System.Windows.Forms.DataGridView dgvMedReport;
        private System.Windows.Forms.Panel pnlExpiringHeader;
        private System.Windows.Forms.Label lblExpiringCount;
        private System.Windows.Forms.DataGridView dgvExpiring;
    }
}