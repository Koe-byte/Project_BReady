using System;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;
using ProjectBReady.Models.Users;
using ProjectBReady.Services;

namespace ProjectBReady.Views
{
    /// <summary>
    /// Main admin dashboard — only accessible to BarangayOfficials.
    /// Uses TabControl para sa fluid navigation between modules.
    /// </summary>
    public class DashboardForm : Form
    {
        private readonly AppDbContext      _db;
        private readonly BarangayOfficial  _currentUser;
        private readonly ShelterService    _shelterSvc;
        private readonly InventoryService  _inventorySvc;

        private Panel      pnlSidebar;
        private Panel      pnlContent;
        private Label      lblWelcome;
        private TabControl tabMain;

        public DashboardForm(AppDbContext db, BarangayOfficial user)
        {
            _db           = db;
            _currentUser  = user;
            _shelterSvc   = new ShelterService(db);
            _inventorySvc = new InventoryService(db);
            InitializeComponent();
            LoadSummaryCards();
        }

        private void InitializeComponent()
        {
            this.Text            = $"ProjectBReady — Dashboard ({_currentUser.Role})";
            this.Size            = new Size(1100, 700);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = Color.FromArgb(15, 23, 42);
            this.MinimumSize     = new Size(900, 600);

            // ── Sidebar ───────────────────────────────────────────────────
            pnlSidebar = new Panel
            {
                Dock      = DockStyle.Left,
                Width     = 220,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblLogo = new Label
            {
                Text      = "🛡 ProjectBReady",
                Font      = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                Location  = new Point(16, 20),
                AutoSize  = true
            };

            lblWelcome = new Label
            {
                Text      = $"👤 {_currentUser.UserID}\n{_currentUser.Role}",
                Font      = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location  = new Point(16, 60),
                Size      = new Size(190, 40)
            };

            // Nav buttons
            string[] navItems = { "📊 Overview", "🏠 Shelters", "📦 Inventory", "🚚 Dispatch", "📋 Logs" };
            int yPos = 120;
            foreach (var nav in navItems)
            {
                int tabIdx = Array.IndexOf(navItems, nav);
                var btn = new Button
                {
                    Text      = nav,
                    Location  = new Point(10, yPos),
                    Size      = new Size(200, 38),
                    Font      = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(203, 213, 225),
                    BackColor = Color.Transparent,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding   = new Padding(10, 0, 0, 0),
                    Cursor    = Cursors.Hand,
                    Tag       = tabIdx
                };
                btn.FlatAppearance.BorderSize    = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
                btn.Click += (s, e) =>
                {
                    tabMain.SelectedIndex = (int)((Button)s).Tag;
                };
                pnlSidebar.Controls.Add(btn);
                yPos += 45;
            }

            var btnLogout = new Button
            {
                Text      = "⬅ Logout",
                Location  = new Point(10, 580),
                Size      = new Size(200, 38),
                Font      = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(248, 113, 113),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => { this.Close(); };

            pnlSidebar.Controls.AddRange(new Control[] { lblLogo, lblWelcome, btnLogout });

            // ── Content area with TabControl ──────────────────────────────
            tabMain = new TabControl
            {
                Dock        = DockStyle.Fill,
                Appearance  = TabAppearance.FlatButtons,
                SizeMode    = TabSizeMode.Fixed,
                ItemSize    = new Size(0, 1),   // hide tab headers (sidebar handles nav)
                BackColor   = Color.FromArgb(15, 23, 42)
            };

            tabMain.TabPages.Add(BuildOverviewTab());
            tabMain.TabPages.Add(BuildSheltersTab());
            tabMain.TabPages.Add(BuildInventoryTab());
            tabMain.TabPages.Add(BuildDispatchTab());
            tabMain.TabPages.Add(BuildLogsTab());

            pnlContent = new Panel { Dock = DockStyle.Fill };
            pnlContent.Controls.Add(tabMain);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlSidebar);
        }

        // ── Tab Builders ─────────────────────────────────────────────────

        private TabPage BuildOverviewTab()
        {
            var tab = new TabPage { BackColor = Color.FromArgb(15, 23, 42) };

            var lblHead = MakeHeader("📊 Overview");
            tab.Controls.Add(lblHead);

            // Summary card area — populated by LoadSummaryCards()
            var pnlCards = new FlowLayoutPanel
            {
                Location  = new Point(20, 70),
                Size      = new Size(820, 200),
                BackColor = Color.Transparent,
                Name      = "pnlCards"
            };
            tab.Controls.Add(pnlCards);

            var lblSummaries = MakeSectionLabel("Shelter Summaries", 290);
            tab.Controls.Add(lblSummaries);

            var lstSummaries = new ListBox
            {
                Location  = new Point(20, 320),
                Size      = new Size(820, 200),
                Font      = new Font("Consolas", 10),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.FromArgb(203, 213, 225),
                BorderStyle = BorderStyle.None,
                Name      = "lstSummaries"
            };
            tab.Controls.Add(lstSummaries);

            return tab;
        }

        private TabPage BuildSheltersTab()
        {
            var tab = new TabPage { BackColor = Color.FromArgb(15, 23, 42) };
            tab.Controls.Add(MakeHeader("🏠 Shelter Management"));

            var grid = MakeDataGrid("gridShelters");
            grid.Location = new Point(20, 70);
            grid.Size     = new Size(820, 300);
            tab.Controls.Add(grid);

            // Occupancy update panel
            tab.Controls.Add(MakeSectionLabel("Update Occupancy", 390));
            var lblSID = MakeSmallLabel("Shelter ID:", new Point(20, 420));
            var txtSID = MakeTextBox(new Point(120, 415), "SH-001");
            var lblCnt = MakeSmallLabel("Add Count:", new Point(280, 420));
            var txtCnt = MakeTextBox(new Point(380, 415), "10");
            var btnUpd = MakeActionButton("Update", new Point(520, 412));
            btnUpd.Click += (s, e) =>
            {
                try
                {
                    _shelterSvc.UpdateShelterOccupancy(txtSID.Text.Trim(), int.Parse(txtCnt.Text));
                    RefreshGrid(grid, _shelterSvc.GetAll());
                    MessageBox.Show("Occupancy updated!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { ShowError(ex.Message); }
            };

            tab.Controls.AddRange(new Control[] { lblSID, txtSID, lblCnt, txtCnt, btnUpd });
            tab.Tag = grid;     // so LoadSummaryCards can refresh it
            return tab;
        }

        private TabPage BuildInventoryTab()
        {
            var tab = new TabPage { BackColor = Color.FromArgb(15, 23, 42) };
            tab.Controls.Add(MakeHeader("📦 Inventory"));

            var grid = MakeDataGrid("gridInventory");
            grid.Location = new Point(20, 70);
            grid.Size     = new Size(820, 300);
            tab.Controls.Add(grid);

            // Stock-in panel
            tab.Controls.Add(MakeSectionLabel("Stock In", 390));
            var lblID  = MakeSmallLabel("Item ID:",  new Point(20, 420));
            var txtID  = MakeTextBox(new Point(120, 415), "FOOD-001");
            var lblAmt = MakeSmallLabel("Amount:",   new Point(280, 420));
            var txtAmt = MakeTextBox(new Point(380, 415), "50");
            var btnIn  = MakeActionButton("Stock In", new Point(520, 412));
            btnIn.Click += (s, e) =>
            {
                try
                {
                    _inventorySvc.StockIn(txtID.Text.Trim(), int.Parse(txtAmt.Text));
                    RefreshGrid(grid, _inventorySvc.GetAll());
                }
                catch (Exception ex) { ShowError(ex.Message); }
            };

            tab.Controls.AddRange(new Control[] { lblID, txtID, lblAmt, txtAmt, btnIn });
            return tab;
        }

        private TabPage BuildDispatchTab()
        {
            var tab = new TabPage { BackColor = Color.FromArgb(15, 23, 42) };
            tab.Controls.Add(MakeHeader("🚚 Dispatch Relief Goods"));

            tab.Controls.Add(MakeSectionLabel("Dispatch Form", 70));
            var lblItem    = MakeSmallLabel("Item ID:",     new Point(20, 110));
            var txtItem    = MakeTextBox(new Point(130, 105), "FOOD-001");
            var lblShelter = MakeSmallLabel("Shelter ID:",  new Point(20, 155));
            var txtShelter = MakeTextBox(new Point(130, 150), "SH-001");
            var lblQty     = MakeSmallLabel("Quantity:",    new Point(20, 200));
            var txtQty     = MakeTextBox(new Point(130, 195), "20");

            var btnDispatch = new Button
            {
                Text      = "🚚 Dispatch",
                Location  = new Point(130, 250),
                Size      = new Size(200, 44),
                Font      = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(34, 197, 94),    // green-500
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand
            };
            btnDispatch.FlatAppearance.BorderSize = 0;
            btnDispatch.Click += (s, e) =>
            {
                try
                {
                    _inventorySvc.Dispatch(
                        txtItem.Text.Trim(),
                        txtShelter.Text.Trim(),
                        int.Parse(txtQty.Text),
                        _currentUser.UserID);
                    MessageBox.Show("Relief goods dispatched successfully!", "Dispatch OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { ShowError(ex.Message); }
            };

            tab.Controls.AddRange(new Control[]
                { lblItem, txtItem, lblShelter, txtShelter, lblQty, txtQty, btnDispatch });
            return tab;
        }

        private TabPage BuildLogsTab()
        {
            var tab = new TabPage { BackColor = Color.FromArgb(15, 23, 42) };
            tab.Controls.Add(MakeHeader("📋 Dispatch Logs"));

            var grid = MakeDataGrid("gridLogs");
            grid.Location = new Point(20, 70);
            grid.Size     = new Size(820, 450);
            tab.Controls.Add(grid);

            tabMain.SelectedIndexChanged += (s, e) =>
            {
                if (tabMain.SelectedIndex == 4)
                    RefreshGrid(grid, _inventorySvc.GetDispatchLogs());
            };
            return tab;
        }

        // ── Helpers ───────────────────────────────────────────────────────

        private void LoadSummaryCards()
        {
            // Populate overview tab
            var overviewTab = tabMain.TabPages[0];
            var pnlCards    = overviewTab.Controls["pnlCards"] as FlowLayoutPanel;
            var lstSumm     = overviewTab.Controls["lstSummaries"] as ListBox;

            var shelters = _shelterSvc.GetAll();
            foreach (var s in shelters)
            {
                var card = new Panel
                {
                    Size      = new Size(180, 90),
                    BackColor = Color.FromArgb(30, 41, 59),
                    Margin    = new Padding(0, 0, 10, 0)
                };
                card.Controls.Add(new Label
                {
                    Text      = s.ShelterName,
                    Font      = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(99, 102, 241),
                    Location  = new Point(10, 10),
                    Size      = new Size(160, 30)
                });
                card.Controls.Add(new Label
                {
                    Text      = s.GenerateSummary().Split(':')[1].Trim(),
                    Font      = new Font("Segoe UI", 11),
                    ForeColor = Color.White,
                    Location  = new Point(10, 45),
                    AutoSize  = true
                });
                pnlCards?.Controls.Add(card);
                lstSumm?.Items.Add(s.GenerateSummary());
            }

            // Grid on shelters tab
            RefreshGrid(
                tabMain.TabPages[1].Controls["gridShelters"] as DataGridView,
                shelters);

            // Inventory grid
            RefreshGrid(
                tabMain.TabPages[2].Controls["gridInventory"] as DataGridView,
                _inventorySvc.GetAll());
        }

        private void RefreshGrid<T>(DataGridView grid, System.Collections.Generic.List<T> data)
        {
            if (grid == null) return;
            grid.DataSource = null;
            grid.DataSource = data;
        }

        private DataGridView MakeDataGrid(string name)
        {
            return new DataGridView
            {
                Name                  = name,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor       = Color.FromArgb(30, 41, 59),
                GridColor             = Color.FromArgb(51, 65, 85),
                DefaultCellStyle      = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(30, 41, 59),
                    ForeColor = Color.FromArgb(203, 213, 225),
                    Font      = new Font("Segoe UI", 9)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(51, 65, 85),
                    ForeColor = Color.FromArgb(99, 102, 241),
                    Font      = new Font("Segoe UI", 9, FontStyle.Bold)
                },
                EnableHeadersVisualStyles = false,
                BorderStyle              = BorderStyle.None,
                ReadOnly                 = true,
                AllowUserToAddRows       = false,
                RowHeadersVisible        = false
            };
        }

        private Label MakeHeader(string text) => new Label
        {
            Text      = text,
            Font      = new Font("Segoe UI", 16, FontStyle.Bold),
            ForeColor = Color.White,
            Location  = new Point(20, 20),
            AutoSize  = true
        };

        private Label MakeSectionLabel(string text, int y) => new Label
        {
            Text      = text,
            Font      = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(148, 163, 184),
            Location  = new Point(20, y),
            AutoSize  = true
        };

        private Label MakeSmallLabel(string text, Point loc) => new Label
        {
            Text      = text,
            Font      = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(148, 163, 184),
            Location  = loc,
            AutoSize  = true
        };

        private TextBox MakeTextBox(Point loc, string placeholder = "") => new TextBox
        {
            Location        = loc,
            Size            = new Size(140, 28),
            Font            = new Font("Segoe UI", 10),
            BackColor       = Color.FromArgb(30, 41, 59),
            ForeColor       = Color.White,
            BorderStyle     = BorderStyle.FixedSingle,
            PlaceholderText = placeholder
        };

        private Button MakeActionButton(string text, Point loc) => new Button
        {
            Text      = text,
            Location  = loc,
            Size      = new Size(110, 32),
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(99, 102, 241),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand
        };

        private void ShowError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
