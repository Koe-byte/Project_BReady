using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjectBReady.Views
{
    public partial class MainDashboardForm : Form
    {
        // Color Palette based on the image
        private Color sidebarColor = Color.FromArgb(24, 30, 46);
        private Color mainBackColor = Color.FromArgb(248, 249, 250);
        private Color cardBackColor = Color.White;
        private Color accentOrange = Color.FromArgb(249, 115, 22);
        private Color textDark = Color.FromArgb(30, 41, 59);
        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
        private Button dashboard;
        private Button button1;
        private Button shelterButton;
        private Button inventoryButton;
        private Label label2;
        private Label label3;
        private Panel panel2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Panel panel3;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panel4;
        private Label label10;
        private Label label11;
        private Label label12;
        private Panel panel5;
        private Label label13;
        private Label label14;
        private Label label15;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private Color textMuted = Color.FromArgb(100, 116, 139);

        public MainDashboardForm()
        {
            InitializeComponent();
            InitializeDashboardUI();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
        }

        private void InitializeDashboardUI()
        {
            this.Text = "Barangay Disaster Relief Dashboard";
            this.Size = new Size(1300, 800);
            this.BackColor = mainBackColor;
            this.StartPosition = FormStartPosition.CenterScreen;

            // 1. Setup Sidebar
            Panel sidebar = new Panel
            {
                BackColor = sidebarColor,
                Dock = DockStyle.Left,
                Width = 250
            };

            // Sidebar Logo/Title
            Label lblLogo = new Label
            {
                Text = "BDMS\nDisaster Relief System",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(60, 20),
                AutoSize = true
            };
            sidebar.Controls.Add(lblLogo);

            // Sidebar Buttons
            sidebar.Controls.Add(CreateNavButton("Dashboard", 80, true));
            sidebar.Controls.Add(CreateNavButton("Shelters", 130, false));
            sidebar.Controls.Add(CreateNavButton("Inventory", 180, false));
            sidebar.Controls.Add(CreateNavButton("Reports", 230, false));

            // Admin Mode Badge
            Label lblAdmin = new Label
            {
                Text = "Admin Mode Active",
                ForeColor = Color.MediumSeaGreen,
                BackColor = Color.FromArgb(20, 40, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(210, 35),
                Location = new Point(20, this.ClientSize.Height - 100),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            sidebar.Controls.Add(lblAdmin);

            this.Controls.Add(sidebar);

            // 2. Setup Main Content Area
            Panel mainContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                AutoScroll = true, // Enables the scrollbar
                AutoScrollMinSize = new Size(0, 1600) // FORCES the scroll area to be tall enough for the tables
            };
            this.Controls.Add(mainContent);
            mainContent.BringToFront();

            // Header Title
            Label lblTitle = new Label
            {
                Text = "Barangay Disaster Relief Dashboard",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = textDark,
                AutoSize = true,
                Location = new Point(30, 20)
            };

            Label lblSubtitle = new Label
            {
                Text = "Real-time overview of evacuation shelters and relief goods",
                Font = new Font("Segoe UI", 11),
                ForeColor = textMuted,
                AutoSize = true,
                Location = new Point(30, 80) // <--- Pushed down from 75 to 95
            };
            mainContent.Controls.Add(lblTitle);
            mainContent.Controls.Add(lblSubtitle);

            // 3. Top Metrics Cards
            FlowLayoutPanel cardsPanel = new FlowLayoutPanel
            {
                Location = new Point(30, 120),
                Size = new Size(1000, 190),
                WrapContents = false,
                AutoScroll = false,
                // ADDED: Allows the container to stretch to the right edge
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            cardsPanel.Controls.Add(CreateSummaryCard("Total Evacuees", "545", "of 780 capacity"));
            cardsPanel.Controls.Add(CreateSummaryCard("Open Shelters", "3", "5 total"));
            cardsPanel.Controls.Add(CreateSummaryCard("Relief Items", "2528", "8 item types"));
            cardsPanel.Controls.Add(CreateSummaryCard("Dispatches", "5", "recent records"));

            mainContent.Controls.Add(cardsPanel);

            // 4. Charts Section
            Panel chartPanelLeft = CreateShadowPanel(30, 330, 650, 400);
            // ADDED: Makes the left panel stretch across the empty space
            chartPanelLeft.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Panel chartPanelRight = CreateShadowPanel(730, 330, 340, 400);
            // ADDED: Keeps the right panel anchored to the right side so they don't overlap
            chartPanelRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Shelter Occupancy Chart (Bar Chart)
            Label lblChart1 = new Label { Text = "Shelter Occupancy", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            chartPanelLeft.Controls.Add(lblChart1);
            Chart barChart = CreateBarChart();
            barChart.Location = new Point(20, 60);
            barChart.Size = new Size(610, 320);
            // ADDED: Forces the actual graph inside the panel to stretch
            barChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartPanelLeft.Controls.Add(barChart);

            // Shelter Status Chart (Doughnut Chart)
            Label lblChart2 = new Label { Text = "Shelter Status", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            chartPanelRight.Controls.Add(lblChart2);
            Chart donutChart = CreateDonutChart();
            donutChart.Location = new Point(10, 60);
            donutChart.Size = new Size(300, 300);
            chartPanelRight.Controls.Add(donutChart);

            mainContent.Controls.Add(chartPanelLeft);
            mainContent.Controls.Add(chartPanelRight);

            // ... existing chart code ...
            mainContent.Controls.Add(chartPanelRight);

            // 5. Evacuation Shelters Table
            // Placed at Y: 760 (below the charts), width matches the two charts combined
            Panel sheltersTable = CreateSheltersTable(30, 760, 1040);
            mainContent.Controls.Add(sheltersTable);

            // 6. Recent Dispatch Logs Table
            // Placed at Y: 1200 (below the shelters table)
            Panel dispatchTable = CreateDispatchTable(30, 1200, 1040);
            mainContent.Controls.Add(dispatchTable);
        }

        // --- Helper Methods to generate UI components ---

        private Button CreateNavButton(string text, int yPos, bool isActive)
        {
            Button btn = new Button
            {
                Text = "   " + text, // Space for imaginary icon
                Location = new Point(10, yPos),
                Size = new Size(230, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = isActive ? Color.FromArgb(30, 45, 75) : sidebarColor,
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Panel CreateSummaryCard(string title, string value, string subtitle)
        {
            Panel card = new Panel
            {
                Size = new Size(290, 150),
                Margin = new Padding(0, 0, 20, 20),
                // Set the base color to match your app background so the rounded corners blend in
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.None // Remove the harsh standard border
            };

            // 1. Add Labels (Background set to White to match the painted card)
            card.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 20), AutoSize = true, BackColor = Color.White });

            card.Controls.Add(new Label { Text = value, Font = new Font("Segoe UI", 32, FontStyle.Bold), ForeColor = textDark, Location = new Point(15, 45), AutoSize = true, BackColor = Color.White });

            card.Controls.Add(new Label { Text = subtitle, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(108, 117, 125), Location = new Point(20, 110), AutoSize = true, BackColor = Color.White });

            // 2. Custom Draw the Rounded Shapes
            card.Paint += (s, e) =>
            {
                // Turn on Anti-Aliasing for smooth, non-pixelated curves
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // --- Draw White Rounded Card Background ---
                Rectangle rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                int radius = 20;
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                e.Graphics.FillPath(Brushes.White, path);
                e.Graphics.DrawPath(new Pen(Color.FromArgb(230, 230, 230), 1), path); // Light gray outline

                // --- Draw Light Peach Circle Accent ---
                Brush peachBrush = new SolidBrush(Color.FromArgb(255, 237, 224));
                e.Graphics.FillEllipse(peachBrush, card.Width - 90, -30, 120, 120);

                // --- Draw Rounded Orange Icon Box ---
                Rectangle iconRect = new Rectangle(card.Width - 65, 30, 40, 40);
                int iconRadius = 12;
                System.Drawing.Drawing2D.GraphicsPath iconPath = new System.Drawing.Drawing2D.GraphicsPath();
                iconPath.AddArc(iconRect.X, iconRect.Y, iconRadius, iconRadius, 180, 90);
                iconPath.AddArc(iconRect.Right - iconRadius, iconRect.Y, iconRadius, iconRadius, 270, 90);
                iconPath.AddArc(iconRect.Right - iconRadius, iconRect.Bottom - iconRadius, iconRadius, iconRadius, 0, 90);
                iconPath.AddArc(iconRect.X, iconRect.Bottom - iconRadius, iconRadius, iconRadius, 90, 90);
                iconPath.CloseFigure();

                Brush orangeBrush = new SolidBrush(accentOrange);
                e.Graphics.FillPath(orangeBrush, iconPath);
            };

            return card;
        }

        private Panel CreateShadowPanel(int x, int y, int width, int height)
        {
            Panel pnl = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = cardBackColor,
                BorderStyle = BorderStyle.FixedSingle
            };
            return pnl;
        }

        private Chart CreateBarChart()
        {
            Chart chart = new Chart();
            ChartArea ca = new ChartArea();
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            ca.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            ca.AxisY.Maximum = 260;
            ca.AxisY.Interval = 65;
            chart.ChartAreas.Add(ca);

            Series sOccupied = new Series("Occupied") { ChartType = SeriesChartType.Column, Color = accentOrange };
            Series sCapacity = new Series("Capacity") { ChartType = SeriesChartType.Column, Color = Color.FromArgb(188, 195, 208) };

            string[] labels = { "Barangay Hal...", "San Isidro E...", "Community Ce...", "Chapel of Ou...", "Multi-Purpos..." };
            int[] occupied = { 160, 0, 95, 30, 250 };
            int[] capacity = { 200, 150, 95, 80, 250 };

            for (int i = 0; i < labels.Length; i++)
            {
                sOccupied.Points.AddXY(labels[i], occupied[i]);
                sCapacity.Points.AddXY(labels[i], capacity[i]);
            }

            chart.Series.Add(sOccupied);
            chart.Series.Add(sCapacity);
            return chart;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDashboardForm));
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            dashboard = new Button();
            button1 = new Button();
            shelterButton = new Button();
            inventoryButton = new Button();
            label2 = new Label();
            label3 = new Label();
            panel2 = new Panel();
            panel6 = new Panel();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            panel3 = new Panel();
            panel7 = new Panel();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            panel4 = new Panel();
            panel8 = new Panel();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            panel5 = new Panel();
            panel9 = new Panel();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 64);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(dashboard);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(shelterButton);
            panel1.Controls.Add(inventoryButton);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(289, 1048);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(94, 128);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(184, 25);
            label1.TabIndex = 8;
            label1.Text = "Disaster Relief System";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-15, 4);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(302, 175);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // dashboard
            // 
            dashboard.ForeColor = Color.Transparent;
            dashboard.Image = (Image)resources.GetObject("dashboard.Image");
            dashboard.ImageAlign = ContentAlignment.MiddleLeft;
            dashboard.Location = new Point(43, 231);
            dashboard.Margin = new Padding(4);
            dashboard.Name = "dashboard";
            dashboard.Size = new Size(198, 84);
            dashboard.TabIndex = 7;
            dashboard.Text = "Dashboard";
            dashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            dashboard.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.ForeColor = Color.Black;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(43, 507);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(198, 86);
            button1.TabIndex = 4;
            button1.Text = "Reports";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // shelterButton
            // 
            shelterButton.ForeColor = Color.Black;
            shelterButton.Image = (Image)resources.GetObject("shelterButton.Image");
            shelterButton.ImageAlign = ContentAlignment.MiddleLeft;
            shelterButton.Location = new Point(43, 322);
            shelterButton.Margin = new Padding(4);
            shelterButton.Name = "shelterButton";
            shelterButton.Size = new Size(198, 84);
            shelterButton.TabIndex = 6;
            shelterButton.Text = "Shelter";
            shelterButton.TextAlign = ContentAlignment.MiddleRight;
            shelterButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            shelterButton.UseVisualStyleBackColor = true;
            // 
            // inventoryButton
            // 
            inventoryButton.ForeColor = Color.Black;
            inventoryButton.Image = (Image)resources.GetObject("inventoryButton.Image");
            inventoryButton.ImageAlign = ContentAlignment.MiddleLeft;
            inventoryButton.Location = new Point(43, 413);
            inventoryButton.Margin = new Padding(4);
            inventoryButton.Name = "inventoryButton";
            inventoryButton.Size = new Size(198, 86);
            inventoryButton.TabIndex = 5;
            inventoryButton.Text = "Inventory";
            inventoryButton.TextAlign = ContentAlignment.MiddleRight;
            inventoryButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            inventoryButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(306, 20);
            label2.Name = "label2";
            label2.Size = new Size(470, 38);
            label2.TabIndex = 1;
            label2.Text = "Barangay Disaster Relief Dashboard";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(315, 58);
            label3.Name = "label3";
            label3.Size = new Size(439, 21);
            label3.TabIndex = 2;
            label3.Text = "Real-time overview of evacuation shelters and relief goods";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(315, 113);
            panel2.Name = "panel2";
            panel2.Size = new Size(262, 130);
            panel2.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.BackColor = Color.OrangeRed;
            panel6.Location = new Point(186, 16);
            panel6.Name = "panel6";
            panel6.Size = new Size(53, 48);
            panel6.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Gray;
            label6.Location = new Point(16, 74);
            label6.Name = "label6";
            label6.Size = new Size(121, 21);
            label6.TabIndex = 5;
            label6.Text = "of 780 capacity";
            label6.Click += label6_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(35, 36);
            label5.Name = "label5";
            label5.Size = new Size(68, 38);
            label5.TabIndex = 4;
            label5.Text = "545";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Gray;
            label4.Location = new Point(16, 15);
            label4.Name = "label4";
            label4.Size = new Size(115, 21);
            label4.TabIndex = 4;
            label4.Text = "Total Evacuees";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(panel7);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(label9);
            panel3.Location = new Point(615, 113);
            panel3.Name = "panel3";
            panel3.Size = new Size(262, 130);
            panel3.TabIndex = 6;
            // 
            // panel7
            // 
            panel7.BackColor = Color.OrangeRed;
            panel7.Location = new Point(190, 13);
            panel7.Name = "panel7";
            panel7.Size = new Size(53, 48);
            panel7.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Gray;
            label7.Location = new Point(16, 74);
            label7.Name = "label7";
            label7.Size = new Size(57, 21);
            label7.TabIndex = 5;
            label7.Text = "5 total";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(35, 36);
            label8.Name = "label8";
            label8.Size = new Size(34, 38);
            label8.TabIndex = 4;
            label8.Text = "3";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Gray;
            label9.Location = new Point(16, 15);
            label9.Name = "label9";
            label9.Size = new Size(113, 21);
            label9.TabIndex = 4;
            label9.Text = "Open Shelters";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(panel8);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(label12);
            panel4.Location = new Point(911, 113);
            panel4.Name = "panel4";
            panel4.Size = new Size(262, 130);
            panel4.TabIndex = 7;
            // 
            // panel8
            // 
            panel8.BackColor = Color.OrangeRed;
            panel8.Location = new Point(190, 16);
            panel8.Name = "panel8";
            panel8.Size = new Size(53, 48);
            panel8.TabIndex = 7;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Gray;
            label10.Location = new Point(16, 74);
            label10.Name = "label10";
            label10.Size = new Size(100, 21);
            label10.TabIndex = 5;
            label10.Text = "8 item types";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(35, 36);
            label11.Name = "label11";
            label11.Size = new Size(85, 38);
            label11.TabIndex = 4;
            label11.Text = "2528";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Gray;
            label12.Location = new Point(16, 15);
            label12.Name = "label12";
            label12.Size = new Size(97, 21);
            label12.TabIndex = 4;
            label12.Text = "Relief Items";
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.Fixed3D;
            panel5.Controls.Add(panel9);
            panel5.Controls.Add(label13);
            panel5.Controls.Add(label14);
            panel5.Controls.Add(label15);
            panel5.Location = new Point(1210, 113);
            panel5.Name = "panel5";
            panel5.Size = new Size(262, 130);
            panel5.TabIndex = 8;
            // 
            // panel9
            // 
            panel9.BackColor = Color.OrangeRed;
            panel9.Location = new Point(189, 13);
            panel9.Name = "panel9";
            panel9.Size = new Size(53, 48);
            panel9.TabIndex = 7;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.Gray;
            label13.Location = new Point(16, 74);
            label13.Name = "label13";
            label13.Size = new Size(117, 21);
            label13.TabIndex = 5;
            label13.Text = "recent records";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(35, 36);
            label14.Name = "label14";
            label14.Size = new Size(34, 38);
            label14.TabIndex = 4;
            label14.Text = "5";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.Gray;
            label15.Location = new Point(16, 15);
            label15.Name = "label15";
            label15.Size = new Size(89, 21);
            label15.TabIndex = 4;
            label15.Text = "Dispatches";
            // 
            // MainDashboardForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(1626, 1050);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            Name = "MainDashboardForm";
            Load += MainDashboardForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        private Chart CreateDonutChart()
        {
            Chart chart = new Chart();
            ChartArea ca = new ChartArea();
            chart.ChartAreas.Add(ca);

            Series s = new Series
            {
                ChartType = SeriesChartType.Doughnut,
                CustomProperties = "DoughnutRadius=60" // Makes the hole bigger
            };

            s.Points.AddXY("Open: 3", 3);
            s.Points[0].Color = Color.FromArgb(32, 167, 149); // Teal

            s.Points.AddXY("Full: 2", 2);
            s.Points[1].Color = accentOrange;

            chart.Series.Add(s);
            return chart;
        }

        private Panel CreateSheltersTable(int x, int y, int width)
        {
            Panel card = CreateShadowPanel(x, y, width, 400);

            // 1. ADD THIS LINE: Para bumuka yung puting background card
            card.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label lblTitle = new Label { Text = "Evacuation Shelters", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = textDark, Location = new Point(25, 20), AutoSize = true };
            card.Controls.Add(lblTitle);

            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Location = new Point(25, 70),
                Size = new Size(width - 50, 310),

                // 2. ADD THIS LINE: Para bumuka yung mismong table at columns
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,

                ColumnCount = 4,
                RowCount = 6,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };
            // Column Widths: Shelter (40%), Occupancy (30%), Capacity (15%), Status (15%)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));

            // Header Row
            string[] headers = { "Shelter", "Occupancy", "Capacity", "Status" };
            for (int i = 0; i < headers.Length; i++)
            {
                tlp.Controls.Add(new Label { Text = headers[i], Font = new Font("Segoe UI", 10, FontStyle.Regular), ForeColor = textMuted, Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true, Margin = new Padding(0, 10, 0, 10) }, i, 0);
            }

            // Data Rows (Hardcoded to match image)
            AddShelterRow(tlp, 1, "Barangay Hall Gymnasium", 165, 200, accentOrange, "Open");
            AddShelterRow(tlp, 2, "San Isidro Elementary School", 0, 150, Color.LightGray, "Open");
            AddShelterRow(tlp, 3, "Community Center Bldg A", 100, 100, Color.FromArgb(239, 68, 68), "Full");
            AddShelterRow(tlp, 4, "Chapel of Our Lady", 30, 80, Color.FromArgb(20, 184, 166), "Open");
            AddShelterRow(tlp, 5, "Multi-Purpose Hall", 250, 250, Color.FromArgb(239, 68, 68), "Full");

            card.Controls.Add(tlp);
            return card;
        }

        private void AddShelterRow(TableLayoutPanel tlp, int rowIndex, string name, int occ, int cap, Color barColor, string status)
        {
            // Column 0: Name
            tlp.Controls.Add(new Label { Text = name, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = textDark, Anchor = AnchorStyles.Left, AutoSize = true }, 0, rowIndex);

            // Column 1: Occupancy Bar + Text
            FlowLayoutPanel occPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, Anchor = AnchorStyles.Left, AutoSize = true, WrapContents = false };

            Panel track = new Panel { Size = new Size(100, 8), BackColor = Color.FromArgb(226, 232, 240), Margin = new Padding(0, 10, 10, 0) };
            int fillWidth = cap > 0 ? (int)((double)occ / cap * 100) : 0;
            Panel fill = new Panel { Size = new Size(fillWidth, 8), BackColor = barColor };
            track.Controls.Add(fill);

            occPanel.Controls.Add(track);
            occPanel.Controls.Add(new Label { Text = $"{occ}/{cap}", Font = new Font("Segoe UI", 10), ForeColor = textMuted, AutoSize = true });
            tlp.Controls.Add(occPanel, 1, rowIndex);

            // Column 2: Capacity
            tlp.Controls.Add(new Label { Text = cap.ToString(), Font = new Font("Segoe UI", 10), ForeColor = textDark, Anchor = AnchorStyles.Left, AutoSize = true }, 2, rowIndex);

            // Column 3: Status Pill
            Color statusColor = status == "Open" ? Color.FromArgb(30, 41, 59) : Color.FromArgb(239, 68, 68);
            Label lblStatus = new Label
            {
                Text = "  " + status + "  ", // padding spaces
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = statusColor,
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Padding = new Padding(5)
            };
            tlp.Controls.Add(lblStatus, 3, rowIndex);
        }

        private Panel CreateDispatchTable(int x, int y, int width)
        {
            Panel card = CreateShadowPanel(x, y, width, 350);

            // 1. ADD THIS LINE: Anchors the outer background card to stretch horizontally
            card.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label lblTitle = new Label { Text = "Recent Dispatch Logs", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = textDark, Location = new Point(25, 20), AutoSize = true };
            card.Controls.Add(lblTitle);

            TableLayoutPanel tlp = new TableLayoutPanel
            {
                Location = new Point(25, 70),
                Size = new Size(width - 50, 260),

                // 2. ADD THIS LINE: Anchors the table itself to stretch with the card
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,

                ColumnCount = 4,
                RowCount = 6,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); // Item
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); // Shelter
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F)); // Qty
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Date

            string[] headers = { "Item", "Shelter", "Qty", "Date" };
            for (int i = 0; i < headers.Length; i++)
            {
                tlp.Controls.Add(new Label { Text = headers[i], Font = new Font("Segoe UI", 10), ForeColor = textMuted, Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(0, 10, 0, 10) }, i, 0);
            }

            // Data Rows
            AddDispatchRow(tlp, 1, "Amoxicillin 500mg", "Community Center Bldg A", "10", "Apr 22, 2026 22:31");
            AddDispatchRow(tlp, 2, "Rice (25kg sack)", "Barangay Hall Gymnasium", "20", "Apr 20, 2026 16:30");
            AddDispatchRow(tlp, 3, "Canned Sardines", "San Isidro Elementary School", "100", "Apr 19, 2026 22:00");
            AddDispatchRow(tlp, 4, "First Aid Kits", "Multi-Purpose Hall", "10", "Apr 21, 2026 18:15");
            AddDispatchRow(tlp, 5, "Bottled Water (500ml)", "Community Center Bldg A", "200", "Apr 22, 2026 15:00");

            card.Controls.Add(tlp);
            return card;
        }

        private void AddDispatchRow(TableLayoutPanel tlp, int rowIndex, string item, string shelter, string qty, string date)
        {
            tlp.Controls.Add(new Label { Text = item, Font = new Font("Segoe UI", 10), ForeColor = textDark, Anchor = AnchorStyles.Left, AutoSize = true }, 0, rowIndex);
            tlp.Controls.Add(new Label { Text = shelter, Font = new Font("Segoe UI", 10), ForeColor = textDark, Anchor = AnchorStyles.Left, AutoSize = true }, 1, rowIndex);
            tlp.Controls.Add(new Label { Text = qty, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = textDark, Anchor = AnchorStyles.Left, AutoSize = true }, 2, rowIndex);
            tlp.Controls.Add(new Label { Text = date, Font = new Font("Segoe UI", 10), ForeColor = textMuted, Anchor = AnchorStyles.Left, AutoSize = true }, 3, rowIndex);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void MainDashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
