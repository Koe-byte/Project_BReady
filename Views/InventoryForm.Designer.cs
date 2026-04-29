namespace ProjectBReady.Forms
{
    partial class InventoryForm
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
            btnInventoryNav = new System.Windows.Forms.Button();
            btnReports = new System.Windows.Forms.Button();

            pnlTopBar = new System.Windows.Forms.Panel();
            lblPageTitle = new System.Windows.Forms.Label();
            lblPageSubtitle = new System.Windows.Forms.Label();

            pnlSummary = new System.Windows.Forms.Panel();
            lblFoodCount = new System.Windows.Forms.Label();
            lblMedCount = new System.Windows.Forms.Label();
            lblFoodTotal = new System.Windows.Forms.Label();
            lblMedTotal = new System.Windows.Forms.Label();

            tabControl = new System.Windows.Forms.TabControl();
            tabFood = new System.Windows.Forms.TabPage();
            tabMedical = new System.Windows.Forms.TabPage();

            // Food tab controls
            pnlFoodActions = new System.Windows.Forms.Panel();
            btnAddFood = new System.Windows.Forms.Button();
            btnDeleteFood = new System.Windows.Forms.Button();
            btnDispatchFood = new System.Windows.Forms.Button();
            btnStockInFood = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();
            dgvFood = new System.Windows.Forms.DataGridView();

            // Medical tab controls
            pnlMedActions = new System.Windows.Forms.Panel();
            btnAddMed = new System.Windows.Forms.Button();
            btnDeleteMed = new System.Windows.Forms.Button();
            btnDispatchMed = new System.Windows.Forms.Button();
            btnStockInMed = new System.Windows.Forms.Button();
            dgvMedical = new System.Windows.Forms.DataGridView();

            pnlSidebar.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlSummary.SuspendLayout();
            tabControl.SuspendLayout();
            tabFood.SuspendLayout();
            tabMedical.SuspendLayout();
            pnlFoodActions.SuspendLayout();
            pnlMedActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFood).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMedical).BeginInit();
            SuspendLayout();

            // ── pnlSidebar ──────────────────────────────────────
            pnlSidebar.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            pnlSidebar.Controls.Add(toggleText);
            pnlSidebar.Controls.Add(lblAppName);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(btnShelter);
            pnlSidebar.Controls.Add(btnInventoryNav);
            pnlSidebar.Controls.Add(btnReports);
            pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            pnlSidebar.Location = new System.Drawing.Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new System.Drawing.Size(242, 737);

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

            lblAppName.AutoSize = true;
            lblAppName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            lblAppName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblAppName.Location = new System.Drawing.Point(55, 55);
            lblAppName.Name = "lblAppName";
            lblAppName.TabIndex = 2;
            lblAppName.Text = "Disaster Relief System";

            btnDashboard.ForeColor = System.Drawing.Color.Black;
            btnDashboard.Location = new System.Drawing.Point(43, 110);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new System.Drawing.Size(158, 67);
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;

            btnShelter.ForeColor = System.Drawing.Color.Black;
            btnShelter.Location = new System.Drawing.Point(43, 183);
            btnShelter.Name = "btnShelter";
            btnShelter.Size = new System.Drawing.Size(158, 67);
            btnShelter.Text = "Shelter";
            btnShelter.UseVisualStyleBackColor = true;
            btnShelter.Click += btnShelter_Click;

            btnInventoryNav.BackColor = System.Drawing.Color.FromArgb(255, 125, 40);
            btnInventoryNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnInventoryNav.ForeColor = System.Drawing.Color.White;
            btnInventoryNav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnInventoryNav.Location = new System.Drawing.Point(43, 256);
            btnInventoryNav.Name = "btnInventoryNav";
            btnInventoryNav.Size = new System.Drawing.Size(158, 69);
            btnInventoryNav.Text = "Inventory";
            btnInventoryNav.UseVisualStyleBackColor = false;

            btnReports.ForeColor = System.Drawing.Color.Black;
            btnReports.Location = new System.Drawing.Point(43, 331);
            btnReports.Name = "btnReports";
            btnReports.Size = new System.Drawing.Size(158, 69);
            btnReports.Text = "Reports";
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;

            // ── pnlTopBar ───────────────────────────────────────
            pnlTopBar.Controls.Add(lblPageTitle);
            pnlTopBar.Controls.Add(lblPageSubtitle);
            pnlTopBar.Location = new System.Drawing.Point(252, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new System.Drawing.Size(1165, 100);

            lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            lblPageTitle.ForeColor = System.Drawing.Color.DimGray;
            lblPageTitle.Location = new System.Drawing.Point(10, 10);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(405, 55);
            lblPageTitle.Text = "Relief Inventory";

            lblPageSubtitle.AutoSize = true;
            lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblPageSubtitle.ForeColor = System.Drawing.Color.Black;
            lblPageSubtitle.Location = new System.Drawing.Point(14, 65);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Text = "Manage food and medical supply stocks";

            // ── pnlSummary ──────────────────────────────────────
            pnlSummary.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlSummary.Controls.Add(lblFoodCount);
            pnlSummary.Controls.Add(lblMedCount);
            pnlSummary.Controls.Add(lblFoodTotal);
            pnlSummary.Controls.Add(lblMedTotal);
            pnlSummary.Location = new System.Drawing.Point(252, 105);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new System.Drawing.Size(1165, 50);

            lblFoodCount.AutoSize = true;
            lblFoodCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblFoodCount.ForeColor = System.Drawing.Color.FromArgb(31, 158, 117);
            lblFoodCount.Location = new System.Drawing.Point(15, 14);
            lblFoodCount.Name = "lblFoodCount";
            lblFoodCount.Text = "Food Items: 0 types";

            lblMedCount.AutoSize = true;
            lblMedCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblMedCount.ForeColor = System.Drawing.Color.FromArgb(0, 0, 64);
            lblMedCount.Location = new System.Drawing.Point(220, 14);
            lblMedCount.Name = "lblMedCount";
            lblMedCount.Text = "Medical Supplies: 0 types";

            lblFoodTotal.AutoSize = true;
            lblFoodTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblFoodTotal.ForeColor = System.Drawing.Color.Gray;
            lblFoodTotal.Location = new System.Drawing.Point(480, 14);
            lblFoodTotal.Name = "lblFoodTotal";
            lblFoodTotal.Text = "Total Food Units: 0";

            lblMedTotal.AutoSize = true;
            lblMedTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblMedTotal.ForeColor = System.Drawing.Color.Gray;
            lblMedTotal.Location = new System.Drawing.Point(680, 14);
            lblMedTotal.Name = "lblMedTotal";
            lblMedTotal.Text = "Total Med Units: 0";

            // ── tabControl ──────────────────────────────────────
            tabControl.Controls.Add(tabFood);
            tabControl.Controls.Add(tabMedical);
            tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            tabControl.Location = new System.Drawing.Point(252, 160);
            tabControl.Name = "tabControl";
            tabControl.Size = new System.Drawing.Size(1165, 570);
            tabControl.TabIndex = 5;

            // ── tabFood ─────────────────────────────────────────
            tabFood.BackColor = System.Drawing.Color.White;
            tabFood.Controls.Add(pnlFoodActions);
            tabFood.Controls.Add(dgvFood);
            tabFood.Name = "tabFood";
            tabFood.Padding = new System.Windows.Forms.Padding(3);
            tabFood.Text = "🍱 Food Items";

            pnlFoodActions.Controls.Add(btnAddFood);
            pnlFoodActions.Controls.Add(btnDeleteFood);
            pnlFoodActions.Controls.Add(btnDispatchFood);
            pnlFoodActions.Controls.Add(btnStockInFood);
            pnlFoodActions.Controls.Add(btnRefresh);
            pnlFoodActions.Dock = System.Windows.Forms.DockStyle.Top;
            pnlFoodActions.Height = 55;
            pnlFoodActions.Name = "pnlFoodActions";

            SetupActionButton(btnAddFood, "➕ Add Item", System.Drawing.Color.FromArgb(255, 125, 40), 0);
            btnAddFood.Click += btnAddFood_Click;

            SetupActionButton(btnDeleteFood, "🗑 Delete", System.Drawing.Color.FromArgb(220, 53, 69), 128);
            btnDeleteFood.Click += btnDeleteFood_Click;

            SetupActionButton(btnDispatchFood, "📦 Dispatch", System.Drawing.Color.FromArgb(0, 0, 64), 256);
            btnDispatchFood.Click += btnDispatchFood_Click;

            SetupActionButton(btnStockInFood, "📥 Stock In", System.Drawing.Color.FromArgb(31, 158, 117), 384);
            btnStockInFood.Click += btnStockInFood_Click;

            SetupActionButton(btnRefresh, "🔄 Refresh", System.Drawing.Color.FromArgb(108, 117, 125), 512);
            btnRefresh.Click += btnRefresh_Click;

            dgvFood.AllowUserToAddRows = false;
            dgvFood.AllowUserToDeleteRows = false;
            dgvFood.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvFood.BackgroundColor = System.Drawing.Color.White;
            dgvFood.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(31, 158, 117);
            dgvFood.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvFood.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvFood.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvFood.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvFood.Location = new System.Drawing.Point(0, 55);
            dgvFood.MultiSelect = false;
            dgvFood.Name = "dgvFood";
            dgvFood.ReadOnly = true;
            dgvFood.RowHeadersVisible = false;
            dgvFood.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvFood.SelectionChanged += dgvFood_SelectionChanged;
            dgvFood.RowPrePaint += dgvFood_RowPrePaint;

            // ── tabMedical ──────────────────────────────────────
            tabMedical.BackColor = System.Drawing.Color.White;
            tabMedical.Controls.Add(pnlMedActions);
            tabMedical.Controls.Add(dgvMedical);
            tabMedical.Name = "tabMedical";
            tabMedical.Padding = new System.Windows.Forms.Padding(3);
            tabMedical.Text = "💊 Medical Supplies";

            pnlMedActions.Controls.Add(btnAddMed);
            pnlMedActions.Controls.Add(btnDeleteMed);
            pnlMedActions.Controls.Add(btnDispatchMed);
            pnlMedActions.Controls.Add(btnStockInMed);
            pnlMedActions.Dock = System.Windows.Forms.DockStyle.Top;
            pnlMedActions.Height = 55;
            pnlMedActions.Name = "pnlMedActions";

            SetupActionButton(btnAddMed, "➕ Add Item", System.Drawing.Color.FromArgb(255, 125, 40), 0);
            btnAddMed.Click += btnAddMed_Click;

            SetupActionButton(btnDeleteMed, "🗑 Delete", System.Drawing.Color.FromArgb(220, 53, 69), 128);
            btnDeleteMed.Click += btnDeleteMed_Click;

            SetupActionButton(btnDispatchMed, "📦 Dispatch", System.Drawing.Color.FromArgb(0, 0, 64), 256);
            btnDispatchMed.Click += btnDispatchMed_Click;

            SetupActionButton(btnStockInMed, "📥 Stock In", System.Drawing.Color.FromArgb(31, 158, 117), 384);
            btnStockInMed.Click += btnStockInMed_Click;

            dgvMedical.AllowUserToAddRows = false;
            dgvMedical.AllowUserToDeleteRows = false;
            dgvMedical.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvMedical.BackgroundColor = System.Drawing.Color.White;
            dgvMedical.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 0, 64);
            dgvMedical.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvMedical.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvMedical.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvMedical.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvMedical.Location = new System.Drawing.Point(0, 55);
            dgvMedical.MultiSelect = false;
            dgvMedical.Name = "dgvMedical";
            dgvMedical.ReadOnly = true;
            dgvMedical.RowHeadersVisible = false;
            dgvMedical.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvMedical.SelectionChanged += dgvMedical_SelectionChanged;

            // ── InventoryForm ───────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1417, 737);
            Controls.Add(tabControl);
            Controls.Add(pnlSummary);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlSidebar);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "InventoryForm";
            Text = "Inventory — ProjectBReady";

            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlTopBar.ResumeLayout(false);
            pnlSummary.ResumeLayout(false);
            pnlFoodActions.ResumeLayout(false);
            pnlMedActions.ResumeLayout(false);
            tabFood.ResumeLayout(false);
            tabMedical.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFood).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMedical).EndInit();
            ResumeLayout(false);
        }

        private void SetupActionButton(System.Windows.Forms.Button btn, string text,
            System.Drawing.Color color, int x)
        {
            btn.BackColor = color;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.White;
            btn.Location = new System.Drawing.Point(x, 8);
            btn.Size = new System.Drawing.Size(120, 38);
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.TextBox toggleText;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnShelter;
        private System.Windows.Forms.Button btnInventoryNav;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblFoodCount;
        private System.Windows.Forms.Label lblMedCount;
        private System.Windows.Forms.Label lblFoodTotal;
        private System.Windows.Forms.Label lblMedTotal;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabFood;
        private System.Windows.Forms.TabPage tabMedical;
        private System.Windows.Forms.Panel pnlFoodActions;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.Button btnDeleteFood;
        private System.Windows.Forms.Button btnDispatchFood;
        private System.Windows.Forms.Button btnStockInFood;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvFood;
        private System.Windows.Forms.Panel pnlMedActions;
        private System.Windows.Forms.Button btnAddMed;
        private System.Windows.Forms.Button btnDeleteMed;
        private System.Windows.Forms.Button btnDispatchMed;
        private System.Windows.Forms.Button btnStockInMed;
        private System.Windows.Forms.DataGridView dgvMedical;
    }
}