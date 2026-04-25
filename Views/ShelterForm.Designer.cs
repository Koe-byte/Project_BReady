// ============================================================
//  ShelterForm.Designer.cs  —  ProjectBReady
//
//  HOW TO USE THIS FILE:
//  1. Sa Visual Studio, right-click ang "ShelterForm.cs" sa Solution Explorer
//  2. Piliin "View Designer"
//  3. Kung gusto mong manual: i-replace ang laman ng
//     ShelterForm.Designer.cs ng code na ito.
//
//  OR: Gamitin ang Visual Studio Designer para i-drag-drop
//  ang mga controls below manually (mas madali).
//  Ang listahan ng kailangan mong i-drag:
//
//  CONTROLS NEEDED:
//  ┌────────────────────────────────────────────────────┐
//  │  [Label] "Shelter Name:"   [TextBox] txtShelterName│
//  │  [Label] "Max Capacity:"   [TextBox] txtMaxCapacity│
//  │  [Label] "Curr. Occupancy:"[TextBox] txtCurrentOcc │
//  │  [Label] "Status:"         [ComboBox] cboStatus    │
//  │  [Button] btnAdd  "Add Shelter"                    │
//  │  [Button] btnClear "Clear"                         │
//  │  [Button] btnRefresh "Refresh"                     │
//  │  [DataGridView] dgvShelters  (bottom half)         │
//  └────────────────────────────────────────────────────┘
// ============================================================

namespace ProjectBReady.Forms
{
    partial class ShelterForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblOcc;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtShelterName;
        private System.Windows.Forms.TextBox txtMaxCapacity;
        private System.Windows.Forms.TextBox txtCurrentOccupancy;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvShelters;
        private System.Windows.Forms.Panel pnlForm;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle           = new System.Windows.Forms.Label();
            this.pnlForm            = new System.Windows.Forms.Panel();
            this.lblName            = new System.Windows.Forms.Label();
            this.lblMax             = new System.Windows.Forms.Label();
            this.lblOcc             = new System.Windows.Forms.Label();
            this.lblStatus          = new System.Windows.Forms.Label();
            this.txtShelterName     = new System.Windows.Forms.TextBox();
            this.txtMaxCapacity     = new System.Windows.Forms.TextBox();
            this.txtCurrentOccupancy= new System.Windows.Forms.TextBox();
            this.cboStatus          = new System.Windows.Forms.ComboBox();
            this.btnAdd             = new System.Windows.Forms.Button();
            this.btnClear           = new System.Windows.Forms.Button();
            this.btnRefresh         = new System.Windows.Forms.Button();
            this.dgvShelters        = new System.Windows.Forms.DataGridView();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShelters)).BeginInit();
            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────
            this.Text            = "B-Ready — Shelter Management";
            this.Size            = new System.Drawing.Size(900, 620);
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor       = System.Drawing.Color.WhiteSmoke;
            this.Font            = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize     = new System.Drawing.Size(800, 550);

            // ── Title Label ──────────────────────────────────────
            this.lblTitle.Text      = "Shelter Management";
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 158, 117);
            this.lblTitle.Location  = new System.Drawing.Point(12, 12);
            this.lblTitle.Size      = new System.Drawing.Size(300, 30);

            // ── Form Panel ───────────────────────────────────────
            this.pnlForm.BackColor   = System.Drawing.Color.White;
            this.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForm.Location    = new System.Drawing.Point(12, 50);
            this.pnlForm.Size        = new System.Drawing.Size(860, 120);
            this.pnlForm.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblName, txtShelterName,
                lblMax, txtMaxCapacity,
                lblOcc, txtCurrentOccupancy,
                lblStatus, cboStatus,
                btnAdd, btnClear
            });

            // ── Labels ───────────────────────────────────────────
            SetLabel(lblName,   "Shelter Name:",    10,  15);
            SetLabel(lblMax,    "Max Capacity:",    240, 15);
            SetLabel(lblOcc,    "Curr. Occupancy:", 380, 15);
            SetLabel(lblStatus, "Status:",          520, 15);

            // ── TextBoxes ────────────────────────────────────────
            SetTextBox(txtShelterName,      10,  35, 210);
            SetTextBox(txtMaxCapacity,      240, 35, 120);
            SetTextBox(txtCurrentOccupancy, 380, 35, 120);
            txtCurrentOccupancy.Text = "0";

            // ── ComboBox ─────────────────────────────────────────
            cboStatus.Location  = new System.Drawing.Point(520, 35);
            cboStatus.Size      = new System.Drawing.Size(130, 25);
            cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboStatus.Items.AddRange(new object[] { "Active", "Full", "Closed" });
            cboStatus.SelectedIndex = 0;

            // ── Buttons ──────────────────────────────────────────
            SetButton(btnAdd,   "Add Shelter", 670, 30, System.Drawing.Color.FromArgb(31, 158, 117), System.Drawing.Color.White);
            SetButton(btnClear, "Clear",       790, 30, System.Drawing.Color.FromArgb(230, 230, 230), System.Drawing.Color.Black);
            btnAdd.Size   = new System.Drawing.Size(100, 32);
            btnClear.Size = new System.Drawing.Size(60,  32);
            btnAdd.Click   += new System.EventHandler(this.btnAdd_Click);
            btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // ── Refresh Button (outside panel) ───────────────────
            this.btnRefresh.Text      = "↻ Refresh";
            this.btnRefresh.Location  = new System.Drawing.Point(772, 180);
            this.btnRefresh.Size      = new System.Drawing.Size(100, 28);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Click    += new System.EventHandler(this.btnRefresh_Click);

            // ── DataGridView ─────────────────────────────────────
            this.dgvShelters.Location             = new System.Drawing.Point(12, 215);
            this.dgvShelters.Size                 = new System.Drawing.Size(860, 360);
            this.dgvShelters.Anchor               = System.Windows.Forms.AnchorStyles.Top
                                                  | System.Windows.Forms.AnchorStyles.Bottom
                                                  | System.Windows.Forms.AnchorStyles.Left
                                                  | System.Windows.Forms.AnchorStyles.Right;
            this.dgvShelters.AllowUserToAddRows   = false;
            this.dgvShelters.AllowUserToDeleteRows= false;
            this.dgvShelters.BorderStyle          = System.Windows.Forms.BorderStyle.None;
            this.dgvShelters.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShelters.BackgroundColor      = System.Drawing.Color.White;
            this.dgvShelters.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(31, 158, 117);
            this.dgvShelters.ColumnHeadersDefaultCellStyle.ForeColor =
                System.Drawing.Color.White;
            this.dgvShelters.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvShelters.EnableHeadersVisualStyles = false;

            // ── Add controls to Form ─────────────────────────────
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, pnlForm, btnRefresh, dgvShelters
            });

            this.pnlForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShelters)).EndInit();
            this.ResumeLayout(false);
        }

        // ── Helpers to keep InitializeComponent short ────────────
        private void SetLabel(System.Windows.Forms.Label lbl, string text, int x, int y)
        {
            lbl.Text     = text;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.AutoSize = true;
            lbl.ForeColor= System.Drawing.Color.FromArgb(80, 80, 80);
        }

        private void SetTextBox(System.Windows.Forms.TextBox tb, int x, int y, int w)
        {
            tb.Location  = new System.Drawing.Point(x, y);
            tb.Size      = new System.Drawing.Size(w, 25);
            tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void SetButton(System.Windows.Forms.Button btn, string text,
            int x, int y,
            System.Drawing.Color bg, System.Drawing.Color fg)
        {
            btn.Text      = text;
            btn.Location  = new System.Drawing.Point(x, y);
            btn.Size      = new System.Drawing.Size(90, 32);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor    = System.Windows.Forms.Cursors.Hand;
        }
    }
}
