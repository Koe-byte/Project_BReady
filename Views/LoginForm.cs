using System;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;
using ProjectBReady.Models.Users;
using ProjectBReady.Services;

namespace ProjectBReady.Views
{
    /// <summary>
    /// Entry point form. Nagre-redirect sa DashboardForm (Official) 
    /// o ResidentKioskForm (Resident) depende sa role.
    /// </summary>
    public class LoginForm : Form
    {
        private readonly UserService _userService;

        private Label    lblTitle;
        private Label    lblSubtitle;
        private Label    lblUserID;
        private TextBox  txtUserID;
        private Button   btnLogin;
        private Label    lblStatus;
        private Panel    pnlCard;

        public LoginForm(AppDbContext db)
        {
            _userService = new UserService(db);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // ── Form setup ──────────────────────────────────────────────
            this.Text            = "ProjectBReady — Login";
            this.Size            = new Size(480, 520);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = Color.FromArgb(15, 23, 42);      // slate-900
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;

            // ── Card panel ──────────────────────────────────────────────
            pnlCard = new Panel
            {
                Size      = new Size(380, 340),
                Location  = new Point(50, 90),
                BackColor = Color.FromArgb(30, 41, 59),             // slate-800
            };
            pnlCard.Paint += (s, e) =>
            {
                // Rounded-rect border
                e.Graphics.DrawRectangle(
                    new Pen(Color.FromArgb(99, 102, 241), 1),       // indigo-500
                    0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            };

            // ── Title ────────────────────────────────────────────────────
            lblTitle = new Label
            {
                Text      = "🛡 ProjectBReady",
                Font      = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize  = true,
                Location  = new Point(60, 30)
            };

            lblSubtitle = new Label
            {
                Text      = "Barangay Disaster Relief Management System",
                Font      = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(148, 163, 184),          // slate-400
                AutoSize  = true,
                Location  = new Point(60, 65)
            };

            // ── Card contents ─────────────────────────────────────────────
            lblUserID = new Label
            {
                Text      = "User ID",
                Font      = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 213, 225),
                Location  = new Point(40, 50),
                AutoSize  = true
            };

            txtUserID = new TextBox
            {
                Location    = new Point(40, 75),
                Size        = new Size(300, 32),
                Font        = new Font("Segoe UI", 11),
                BackColor   = Color.FromArgb(15, 23, 42),
                ForeColor   = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "e.g. OFF-001 or RES-001"
            };

            btnLogin = new Button
            {
                Text      = "Login",
                Location  = new Point(40, 135),
                Size      = new Size(300, 42),
                Font      = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            lblStatus = new Label
            {
                Text      = "",
                Font      = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(248, 113, 113),          // red-400
                Location  = new Point(40, 195),
                Size      = new Size(300, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblHint = new Label
            {
                Text      = "Demo users: OFF-001, OFF-002, RES-001",
                Font      = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location  = new Point(40, 260),
                AutoSize  = true
            };

            pnlCard.Controls.AddRange(new Control[]
                { lblUserID, txtUserID, btnLogin, lblStatus, lblHint });

            this.Controls.AddRange(new Control[]
                { lblTitle, lblSubtitle, pnlCard });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            string id = txtUserID.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                lblStatus.Text = "Please enter your User ID.";
                return;
            }

            var db   = ((AppDbContext)((UserService)_userService).GetType()
                        .GetField("_db", System.Reflection.BindingFlags.NonPublic
                                        | System.Reflection.BindingFlags.Instance)
                        .GetValue(_userService));

            var person = _userService.Login(id);

            if (person == null)
            {
                lblStatus.Text = "User ID not found. Please try again.";
                return;
            }

            // Role-based routing
            if (person is BarangayOfficial official)
            {
                this.Hide();
                var dash = new DashboardForm(db, official);
                dash.FormClosed += (s2, e2) => this.Close();
                dash.Show();
            }
            else if (person is Resident resident)
            {
                this.Hide();
                var kiosk = new ResidentKioskForm(db);
                kiosk.FormClosed += (s2, e2) => this.Close();
                kiosk.Show();
            }
        }
    }
}
