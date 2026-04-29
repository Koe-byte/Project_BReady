using System;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;
using ProjectBReady.Models.Users;
using ProjectBReady.Services;

namespace ProjectBReady.Views
{
    /// <summary>
    /// Default view when the app starts. Residents see shelter info read-only.
    /// Barangay officials press Ctrl+Shift+O to unlock the admin dashboard via PIN.
    /// </summary>
    public class ResidentKioskForm : Form
    {
        // ── Dependencies ────────────────────────────────────────────────────
        private readonly AppDbContext _db;
        private readonly ShelterService _shelterSvc;
        private readonly InventoryService _inventorySvc;
        private readonly UserService _userSvc;

        // Hardcoded admin PIN — in production, store this hashed in the DB/settings table
        private const string ADMIN_PIN = "1234";

        public ResidentKioskForm(AppDbContext db)
        {
            _db = db;
            _shelterSvc = new ShelterService(db);
            _inventorySvc = new InventoryService(db);
            _userSvc = new UserService(db);

            InitializeComponent();
            LoadData();

            // ── Ctrl+Shift+O anywhere on the form opens the PIN dialog ────
            this.KeyPreview = true;
            this.KeyDown += OnKeyDown;
        }

        // ── Keyboard shortcut ────────────────────────────────────────────────

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.O)
            {
                e.SuppressKeyPress = true;
                OpenAdminPinDialog();
            }
        }

        private void OpenAdminPinDialog()
        {
            // ── Inline PIN dialog (no extra Form file needed) ─────────────
            using var dlg = new Form
            {
                Text = "Admin Access",
                Size = new Size(380, 220),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                BackColor = Color.FromArgb(30, 41, 59),
                MaximizeBox = false,
                MinimizeBox = false
            };

            dlg.Controls.Add(new Label
            {
                Text = "🔐 Enter Admin PIN",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                Location = new Point(20, 20),
                AutoSize = true
            });

            var txtPIN = new TextBox
            {
                Location = new Point(20, 65),
                Size = new Size(320, 30),
                Font = new Font("Segoe UI", 12),
                PasswordChar = '●',
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Enter PIN"
            };

            var lblMsg = new Label
            {
                Text = "",
                ForeColor = Color.FromArgb(248, 113, 113),
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 100),
                Size = new Size(320, 20)
            };

            var btnUnlock = new Button
            {
                Text = "Unlock",
                Location = new Point(20, 130),
                Size = new Size(150, 36),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnUnlock.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(190, 130),
                Size = new Size(150, 36),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(51, 65, 85),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            btnUnlock.Click += (s, e) =>
            {
                if (txtPIN.Text == ADMIN_PIN)
                {
                    dlg.DialogResult = DialogResult.OK;
                    dlg.Close();
                }
                else
                {
                    lblMsg.Text = "Incorrect PIN. Try again.";
                    txtPIN.Clear();
                    txtPIN.Focus();
                }
            };

            // Allow Enter key to submit
            txtPIN.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) btnUnlock.PerformClick();
            };

            dlg.Controls.AddRange(new Control[] { txtPIN, lblMsg, btnUnlock, btnCancel });

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // PIN correct — get any official and open dashboard
                var official = _userSvc.GetDefaultOfficial();
                if (official == null)
                {
                    MessageBox.Show("No official accounts found in the database.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Hide();
                var dash = new DashboardForm(_db, official);
                dash.FormClosed += (s2, e2) =>
                {
                    // Return to kiosk after dashboard closes
                    this.Show();
                    LoadData();   // Refresh data when returning
                };
                dash.Show();
            }
        }

        // ── UI Setup ────────────────────────────────────────────────────────

        private void InitializeComponent()
        {
            this.Text = "ProjectBReady — Resident Kiosk";
            this.Size = new Size(800, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ── Header ──────────────────────────────────────────────────
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlHeader.Controls.Add(new Label
            {
                Text = "🛡 ProjectBReady — Resident View",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                Location = new Point(20, 14),
                AutoSize = true
            });
            pnlHeader.Controls.Add(new Label
            {
                Text = "👁 Read-Only Mode  •  Officials: press Ctrl+Shift+O to unlock admin",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(22, 46),
                AutoSize = true
            });

            // ── Shelter grid ─────────────────────────────────────────────
            var lblShelter = new Label
            {
                Text = "🏠 Available Shelters",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 90),
                AutoSize = true
            };

            var gridShelters = new DataGridView
            {
                Name = "gridShelters",
                Location = new Point(20, 120),
                Size = new Size(745, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(30, 41, 59),
                GridColor = Color.FromArgb(51, 65, 85),
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(30, 41, 59),
                    ForeColor = Color.FromArgb(203, 213, 225),
                    Font = new Font("Segoe UI", 10)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(51, 65, 85),
                    ForeColor = Color.FromArgb(99, 102, 241),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                EnableHeadersVisualStyles = false,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // ── Notices ──────────────────────────────────────────────────
            var lblNotice = new Label
            {
                Text = "📢 Important Notices",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 340),
                AutoSize = true
            };

            var lstNotices = new ListBox
            {
                Name = "lstNotices",
                Location = new Point(20, 370),
                Size = new Size(745, 190),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.FromArgb(203, 213, 225),
                BorderStyle = BorderStyle.None
            };

            this.Controls.AddRange(new Control[]
                { pnlHeader, lblShelter, gridShelters, lblNotice, lstNotices });
        }

        // ── Data Loading ─────────────────────────────────────────────────────

        private void LoadData()
        {
            var gridShelters = this.Controls["gridShelters"] as DataGridView;
            var lstNotices = this.Controls["lstNotices"] as ListBox;

            if (gridShelters != null)
                gridShelters.DataSource = _shelterSvc.GetAll();

            if (lstNotices != null)
            {
                lstNotices.Items.Clear();
                lstNotices.Items.Add("ℹ  Pumunta sa pinakamalapit na Barangay Hall para sa tulong.");
                lstNotices.Items.Add("⚠  Magdala ng valid ID at family documents sa shelter.");

                var expired = _inventorySvc.GetExpiredItems();
                if (expired.Count > 0)
                    lstNotices.Items.Add($"⚠  May {expired.Count} expired food item(s) — abisuhan ang opisyal.");

                foreach (var s in _shelterSvc.GetAll())
                    lstNotices.Items.Add($"📍 {s.GenerateSummary()}");
            }
        }
    }
}