using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Services;

namespace ProjectBReady.Views
{
    /// <summary>
    /// Default view when the app starts. Read-only shelter info for residents.
    /// Barangay officials press Ctrl+Shift+O → enter PIN → open DashboardForm.
    /// </summary>
    public class ResidentKioskForm : Form
    {
        // UI controls we need to reference later
        private Panel pnlShelterList;
        private Label lblAvailableSlots;
        private Label lblBestShelter;
        private Label lblBestSlots;
        private Label lblLastUpdated;

        public ResidentKioskForm()
        {
            InitializeComponent();
            LoadData();

            // Ctrl+Shift+O → PIN dialog
            this.KeyPreview = true;
            this.KeyDown += OnKeyDown;
        }

        // ── Keyboard shortcut ─────────────────────────────────────────────

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
            using var dlg = new Form
            {
                Text = "Admin Access",
                Size = new Size(360, 210),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                BackColor = Color.FromArgb(17, 34, 51),
                MaximizeBox = false,
                MinimizeBox = false
            };

            dlg.Controls.Add(new Label
            {
                Text = "🔐 Enter Admin PIN",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(126, 211, 33),
                Location = new Point(20, 18),
                AutoSize = true
            });

            var txtPIN = new TextBox
            {
                Location = new Point(20, 60),
                Size = new Size(310, 30),
                Font = new Font("Segoe UI", 12),
                PasswordChar = '●',
                BackColor = Color.FromArgb(26, 26, 26),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Enter PIN"
            };

            var lblMsg = new Label
            {
                Text = "",
                ForeColor = Color.FromArgb(226, 75, 74),
                Font = new Font("Segoe UI", 9),
                Location = new Point(20, 96),
                Size = new Size(310, 18)
            };

            var btnUnlock = new Button
            {
                Text = "Unlock",
                Location = new Point(20, 122),
                Size = new Size(145, 36),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(126, 211, 33),
                ForeColor = Color.FromArgb(17, 17, 17),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnUnlock.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(185, 122),
                Size = new Size(145, 36),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(37, 37, 37),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            btnUnlock.Click += (s, e) =>
            {
                if (UserService.ValidatePIN(txtPIN.Text))
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

            txtPIN.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) btnUnlock.PerformClick();
            };

            dlg.Controls.AddRange(new Control[] { txtPIN, lblMsg, btnUnlock, btnCancel });

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataRow official = UserService.GetDefaultOfficial();
                string officialName = official != null
                    ? official["FullName"].ToString()
                    : "Official";

                this.Hide();
                var dash = new DashboardForm(officialName);
                dash.FormClosed += (s2, e2) =>
                {
                    this.Show();
                    LoadData();   // Refresh after returning from admin
                };
                dash.Show();
            }
        }

        // ── UI Setup ──────────────────────────────────────────────────────

        private void InitializeComponent()
        {
            this.Text = "B-Ready — Evacuation Shelter Kiosk";
            this.Size = new Size(900, 660);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(26, 26, 26);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ── Sidebar ───────────────────────────────────────────────────
            var sidebar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(200, 660),
                BackColor = Color.FromArgb(17, 17, 17)
            };

            sidebar.Controls.Add(new Label
            {
                Text = "B-READY",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(126, 211, 33),
                Location = new Point(16, 20),
                AutoSize = true
            });

            sidebar.Controls.Add(new Label
            {
                Text = "Disaster Relief System",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(16, 48),
                AutoSize = true
            });

            // Divider
            var div = new Panel
            {
                Location = new Point(0, 72),
                Size = new Size(200, 1),
                BackColor = Color.FromArgb(35, 35, 35)
            };
            sidebar.Controls.Add(div);

            // Nav item
            var navShelters = new Panel
            {
                Location = new Point(0, 85),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(126, 211, 33, 25)
            };
            navShelters.Controls.Add(new Label
            {
                Text = "🏠  Shelters",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(126, 211, 33),
                Location = new Point(16, 10),
                AutoSize = true
            });
            var accent = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(3, 40),
                BackColor = Color.FromArgb(126, 211, 33)
            };
            navShelters.Controls.Add(accent);
            sidebar.Controls.Add(navShelters);

            // Admin login hint at bottom
            var adminHint = new Panel
            {
                Location = new Point(0, 590),
                Size = new Size(200, 70),
                BackColor = Color.Transparent
            };
            adminHint.Controls.Add(new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(200, 1),
                BackColor = Color.FromArgb(35, 35, 35)
            });
            adminHint.Controls.Add(new Label
            {
                Text = "🔒  Admin Login",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(16, 12),
                AutoSize = true
            });
            adminHint.Controls.Add(new Label
            {
                Text = "Ctrl+Shift+O to toggle",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(55, 55, 55),
                Location = new Point(16, 32),
                AutoSize = true
            });
            sidebar.Controls.Add(adminHint);

            // ── Main content ──────────────────────────────────────────────
            var main = new Panel
            {
                Location = new Point(200, 0),
                Size = new Size(700, 660),
                BackColor = Color.FromArgb(26, 26, 26)
            };

            main.Controls.Add(new Label
            {
                Text = "Evacuation Shelters",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            });

            main.Controls.Add(new Label
            {
                Text = "Find an available shelter near you",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(22, 52),
                AutoSize = true
            });

            // ── Shelter list (left, scrollable) ───────────────────────────
            pnlShelterList = new Panel
            {
                Location = new Point(20, 80),
                Size = new Size(460, 560),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            main.Controls.Add(pnlShelterList);

            // ── Right stats panel ─────────────────────────────────────────
            var pnlStats = new Panel
            {
                Location = new Point(495, 80),
                Size = new Size(190, 560),
                BackColor = Color.Transparent
            };

            // Available slots card
            var cardSlots = MakeStatCard(0);
            lblAvailableSlots = new Label
            {
                Text = "—",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.FromArgb(126, 211, 33),
                Location = new Point(14, 34),
                AutoSize = true
            };
            cardSlots.Controls.Add(new Label
            {
                Text = "Available slots",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(14, 14),
                AutoSize = true
            });
            cardSlots.Controls.Add(lblAvailableSlots);
            cardSlots.Controls.Add(new Label
            {
                Text = "across all shelters",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(14, 72),
                AutoSize = true
            });
            pnlStats.Controls.Add(cardSlots);

            // Most available card
            var cardBest = MakeStatCard(108);
            cardBest.Controls.Add(new Label
            {
                Text = "Most available",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(14, 14),
                AutoSize = true
            });
            lblBestShelter = new Label
            {
                Text = "—",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(14, 36),
                Size = new Size(162, 36)
            };
            lblBestSlots = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(126, 211, 33),
                Location = new Point(14, 72),
                AutoSize = true
            };
            cardBest.Controls.Add(lblBestShelter);
            cardBest.Controls.Add(lblBestSlots);
            pnlStats.Controls.Add(cardBest);

            // Refresh card
            var cardRefresh = MakeStatCard(226);
            cardRefresh.Controls.Add(new Label
            {
                Text = "Last updated",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(14, 14),
                AutoSize = true
            });
            lblLastUpdated = new Label
            {
                Text = "—",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(14, 36),
                Size = new Size(162, 30)
            };
            var btnRefresh = new Button
            {
                Text = "⟳  Refresh",
                Location = new Point(14, 72),
                Size = new Size(162, 32),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(126, 211, 33),
                ForeColor = Color.FromArgb(17, 17, 17),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadData();

            cardRefresh.Controls.Add(lblLastUpdated);
            cardRefresh.Controls.Add(btnRefresh);
            pnlStats.Controls.Add(cardRefresh);

            main.Controls.Add(pnlStats);

            this.Controls.Add(sidebar);
            this.Controls.Add(main);
        }

        // ── Helpers ───────────────────────────────────────────────────────

        private Panel MakeStatCard(int y) => new Panel
        {
            Location = new Point(0, y),
            Size = new Size(190, 110),
            BackColor = Color.FromArgb(37, 37, 37)
        };

        // ── Data Loading ──────────────────────────────────────────────────

        private void LoadData()
        {
            // Summary stats
            int totalSlots = ShelterService.GetTotalAvailableSlots();
            lblAvailableSlots.Text = totalSlots.ToString();

            DataRow best = ShelterService.GetMostAvailable();
            if (best != null)
            {
                lblBestShelter.Text = best["ShelterName"].ToString();
                lblBestSlots.Text = $"{best["AvailableSlots"]} slots open";
            }
            else
            {
                lblBestShelter.Text = "No open shelters";
                lblBestSlots.Text = "";
            }

            lblLastUpdated.Text = DateTime.Now.ToString("MMM dd, yyyy — hh:mm tt");

            // Shelter cards
            pnlShelterList.Controls.Clear();
            DataTable dt = ShelterService.GetAll();
            int yPos = 0;

            foreach (DataRow row in dt.Rows)
            {
                string name = row["ShelterName"].ToString();
                int current = Convert.ToInt32(row["CurrentOccupancy"]);
                int max = Convert.ToInt32(row["MaxCapacity"]);
                string status = row["Status"].ToString();
                double pct = max > 0 ? (double)current / max : 0;

                // Bar color
                Color barColor = pct >= 1.0
                    ? Color.FromArgb(226, 75, 74)   // red = full
                    : pct >= 0.75
                        ? Color.FromArgb(232, 80, 10)  // orange = almost full
                        : Color.FromArgb(126, 211, 33); // green = plenty

                var card = new Panel
                {
                    Location = new Point(0, yPos),
                    Size = new Size(450, 72),
                    BackColor = Color.FromArgb(37, 37, 37)
                };

                card.Controls.Add(new Label
                {
                    Text = name,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(14, 10),
                    Size = new Size(280, 20)
                });

                // Status badge
                var badge = new Label
                {
                    Text = status,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = status == "Full" ? Color.FromArgb(226, 75, 74) : Color.FromArgb(126, 211, 33),
                    Location = new Point(360, 10),
                    AutoSize = true
                };
                card.Controls.Add(badge);

                // Progress bar background
                var barBg = new Panel
                {
                    Location = new Point(14, 38),
                    Size = new Size(300, 8),
                    BackColor = Color.FromArgb(55, 55, 55)
                };

                int fillWidth = (int)(300 * pct);
                barBg.Controls.Add(new Panel
                {
                    Location = new Point(0, 0),
                    Size = new Size(Math.Max(fillWidth, 0), 8),
                    BackColor = barColor
                });
                card.Controls.Add(barBg);

                // Occupancy label
                card.Controls.Add(new Label
                {
                    Text = $"{current} / {max}",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    Location = new Point(322, 35),
                    AutoSize = true
                });

                pnlShelterList.Controls.Add(card);
                yPos += 80;
            }
        }
    }
}