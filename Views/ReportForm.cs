// ============================================================
//  DashboardForm.cs  —  ProjectBReady
//  Features: Kiosk Mode, Ctrl+Shift+O Admin Toggle, 
//            Shelter navigation, Live DB stats
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjectBReady.Data;
using ProjectBReady.Forms;

namespace ProjectBReady
{
    public partial class ReportForm : Form
    {
        // ── Admin State ──────────────────────────────────────────
        private bool isAdminMode = false;
        private Panel pnlSidebar;
        private TextBox toggleText;
        private Label label1;
        private PictureBox pictureBox1;
        private Button button1;
        private Button inventoryButton;
        private Button shelterButton;
        private Button dashboard;
        private const string ADMIN_PIN = "1234"; // Palitan ng gusto ninyong PIN

        public ReportForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // Para makuha ng Form ang keyboard events
            this.KeyDown += ReportForm_KeyDown;
            LoadShelterStats();
            SetAdminMode(false); // Start as Resident/Kiosk mode
        }

        // ── CTRL+SHIFT+O — Admin Toggle ──────────────────────────
        private void ReportForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.O)
            {
                if (isAdminMode)
                {
                    // Kung admin na, i-lock agad
                    SetAdminMode(false);
                }
                else
                {
                    // Kung resident pa, humingi ng PIN
                    string pin = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter Admin PIN:", "Admin Access", "");

                    if (pin == ADMIN_PIN)
                        SetAdminMode(true);
                    else if (pin != "") // Hindi blank (hindi cancel)
                        MessageBox.Show("Incorrect PIN.", "Access Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                e.Handled = true;
            }
        }

        // ── SET ADMIN / RESIDENT MODE ────────────────────────────
        private void SetAdminMode(bool isAdmin)
        {
            isAdminMode = isAdmin;

            // Ipakita o itago ang admin-only controls
            addshelterButton.Visible = isAdmin;
            ActionButton1.Visible = isAdmin;

            // Update yung toggleText sa sidebar (katulad ng base44 design)
            if (isAdmin)
            {
                toggleText.Text = "🔓 Admin Mode Active  |  Lock: Ctrl+Shift+O";
                toggleText.ForeColor = Color.FromArgb(255, 125, 40); // Orange
            }
            else
            {
                toggleText.Text = "Ctrl+Shift+O to toggle admin";
                toggleText.ForeColor = Color.White;
            }
        }

        // ── LOAD SHELTER STATS mula sa DB ────────────────────────
        private void LoadShelterStats()
        {
            try
            {
                // Kunin ang FIRST shelter para sa panel1 display
                string query = @"
                    SELECT TOP 1 
                        ShelterName, MaxCapacity, CurrentOccupancy, Status
                    FROM SHELTERS 
                    ORDER BY ShelterName";

                DataTable dt = DBHelper.GetData(query);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int max = Convert.ToInt32(row["MaxCapacity"]);
                    int curr = Convert.ToInt32(row["CurrentOccupancy"]);
                    string status = row["Status"].ToString();
                    int pct = max > 0 ? (int)((double)curr / max * 100) : 0;

                    // Update controls sa panel1
                    label3.Text = row["ShelterName"].ToString();
                    LBstatus1.Text = $"{curr} / {max}";
                    LBstatus2.Text = $"{pct}% occupied";
                    progressBar1.Maximum = max > 0 ? max : 1;
                    progressBar1.Value = Math.Min(curr, max);
                    label4.Text = status;

                    // Color ng status badge
                    if (status == "Full")
                    {
                        label4.BackColor = Color.FromArgb(220, 53, 69);   // Red
                        progressBar1.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                    else if (pct >= 75)
                    {
                        label4.BackColor = Color.FromArgb(255, 165, 0);   // Orange
                        progressBar1.ForeColor = Color.Orange;
                    }
                    else
                    {
                        label4.BackColor = Color.FromArgb(22, 27, 45);    // Dark (Open)
                        progressBar1.ForeColor = Color.FromArgb(31, 158, 117);
                    }
                }
            }
            catch
            {
                // Kung walang DB pa, default values lang — hindi mag-crash
                label3.Text = "No shelters yet";
                LBstatus1.Text = "0 / 0";
                LBstatus2.Text = "0% occupied";
            }
        }

        // ── NAVIGATION BUTTONS ───────────────────────────────────
        private void shelterButton_Click(object sender, EventArgs e)
        {
            ShelterForm sf = new ShelterForm();
            sf.Show();
        }

        private void addshelterButton_Click(object sender, EventArgs e)
        {
            // Admin only — direkta sa ShelterForm
            ShelterForm sf = new ShelterForm();
            sf.Show();
        }

        private void dashboard_Click(object sender, EventArgs e)
        {
            // Nasa dashboard na tayo, i-refresh na lang ang stats
            LoadShelterStats();
        }

        // ── UNUSED EVENT HANDLERS (huwag burahin — kailangan ng Designer) ──
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click_1(object sender, EventArgs e) { }
        private void button1_Click_1(object sender, EventArgs e) { }
        private void pictureBox1_Click_2(object sender, EventArgs e) { }
        private void pictureBox1_Click_3(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void EvacShelLabel_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void LBstatus2_Click(object sender, EventArgs e) { }
        private void label4_Click_1(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            pnlSidebar = new Panel();
            toggleText = new TextBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            inventoryButton = new Button();
            shelterButton = new Button();
            dashboard = new Button();
            this.label2 = new Label();
            this.EvacShelLabel = new Label();
            button2 = new Button();
            listView1 = new ListView();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            listView2 = new ListView();
            label7 = new Label();
            label8 = new Label();
            listView3 = new ListView();
            label9 = new Label();
            label10 = new Label();
            listView4 = new ListView();
            tableLayoutPanel1 = new TableLayoutPanel();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            shelterBindingSource = new BindingSource(components);
            shelterIDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            shelterNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            maxCapacityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            currentOccupancyDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            statusDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataGridView2 = new DataGridView();
            medicalSupplyBindingSource = new BindingSource(components);
            dosageDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            isPrescriptionRequiredDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            itemIDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            itemNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            quantityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)shelterBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)medicalSupplyBindingSource).BeginInit();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(0, 0, 64);
            pnlSidebar.Controls.Add(toggleText);
            pnlSidebar.Controls.Add(label1);
            pnlSidebar.Controls.Add(pictureBox1);
            pnlSidebar.Controls.Add(button1);
            pnlSidebar.Controls.Add(inventoryButton);
            pnlSidebar.Controls.Add(shelterButton);
            pnlSidebar.Controls.Add(dashboard);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(3, 2, 3, 2);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(212, 749);
            pnlSidebar.TabIndex = 1;
            // 
            // toggleText
            // 
            toggleText.BackColor = Color.FromArgb(0, 0, 64);
            toggleText.BorderStyle = BorderStyle.None;
            toggleText.ForeColor = Color.White;
            toggleText.Location = new Point(3, 530);
            toggleText.Margin = new Padding(3, 2, 3, 2);
            toggleText.Name = "toggleText";
            toggleText.Size = new Size(206, 16);
            toggleText.TabIndex = 1;
            toggleText.Text = "Ctrl+Shift+O to toggle admin";
            toggleText.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(74, 58);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 2;
            label1.Text = "Disaster Relief System";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-3, -16);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(212, 105);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(0, 0, 64);
            button1.ForeColor = Color.White;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(38, 285);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(138, 52);
            button1.TabIndex = 1;
            button1.Text = "Reports";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = false;
            // 
            // inventoryButton
            // 
            inventoryButton.ForeColor = Color.Black;
            inventoryButton.Image = (Image)resources.GetObject("inventoryButton.Image");
            inventoryButton.ImageAlign = ContentAlignment.MiddleLeft;
            inventoryButton.Location = new Point(38, 229);
            inventoryButton.Margin = new Padding(3, 2, 3, 2);
            inventoryButton.Name = "inventoryButton";
            inventoryButton.Size = new Size(138, 52);
            inventoryButton.TabIndex = 1;
            inventoryButton.Text = "Inventory";
            inventoryButton.TextAlign = ContentAlignment.MiddleRight;
            inventoryButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            inventoryButton.UseVisualStyleBackColor = true;
            // 
            // shelterButton
            // 
            shelterButton.ForeColor = Color.Black;
            shelterButton.Image = (Image)resources.GetObject("shelterButton.Image");
            shelterButton.ImageAlign = ContentAlignment.MiddleLeft;
            shelterButton.Location = new Point(38, 174);
            shelterButton.Margin = new Padding(3, 2, 3, 2);
            shelterButton.Name = "shelterButton";
            shelterButton.Size = new Size(138, 50);
            shelterButton.TabIndex = 1;
            shelterButton.Text = "Shelter";
            shelterButton.TextAlign = ContentAlignment.MiddleRight;
            shelterButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            shelterButton.UseVisualStyleBackColor = true;
            // 
            // dashboard
            // 
            dashboard.BackColor = Color.LightGray;
            dashboard.ForeColor = Color.Black;
            dashboard.Image = (Image)resources.GetObject("dashboard.Image");
            dashboard.ImageAlign = ContentAlignment.MiddleLeft;
            dashboard.Location = new Point(38, 119);
            dashboard.Margin = new Padding(3, 2, 3, 2);
            dashboard.Name = "dashboard";
            dashboard.Size = new Size(138, 50);
            dashboard.TabIndex = 1;
            dashboard.Text = "Dashboard";
            dashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            dashboard.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.ForeColor = SystemColors.ActiveCaptionText;
            this.label2.Location = new Point(238, 65);
            this.label2.Name = "label2";
            this.label2.Size = new Size(259, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "System wide overview and analytics";
            this.label2.Click += this.label2_Click_1;
            // 
            // EvacShelLabel
            // 
            this.EvacShelLabel.AccessibleRole = AccessibleRole.None;
            this.EvacShelLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.EvacShelLabel.ForeColor = SystemColors.ActiveCaptionText;
            this.EvacShelLabel.Location = new Point(230, 27);
            this.EvacShelLabel.Name = "EvacShelLabel";
            this.EvacShelLabel.RightToLeft = RightToLeft.No;
            this.EvacShelLabel.Size = new Size(354, 46);
            this.EvacShelLabel.TabIndex = 3;
            this.EvacShelLabel.Text = "Report and Summary";
            this.EvacShelLabel.UseCompatibleTextRendering = true;
            this.EvacShelLabel.Click += this.EvacShelLabel_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(922, 40);
            button2.Name = "button2";
            button2.Size = new Size(141, 31);
            button2.TabIndex = 5;
            button2.Text = "Refresh Dashboard";
            button2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Location = new Point(225, 110);
            listView1.Name = "listView1";
            listView1.Size = new Size(203, 93);
            listView1.TabIndex = 6;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(231, 117);
            label3.Name = "label3";
            label3.Size = new Size(139, 25);
            label3.TabIndex = 7;
            label3.Text = "Total Evacuees";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(301, 147);
            label4.Name = "label4";
            label4.Size = new Size(125, 37);
            label4.TabIndex = 8;
            label4.Text = "545/780";
            label4.Click += label4_Click_2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(510, 147);
            label5.Name = "label5";
            label5.Size = new Size(81, 37);
            label5.TabIndex = 11;
            label5.Text = "2526";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.White;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(440, 117);
            label6.Name = "label6";
            label6.Size = new Size(110, 25);
            label6.TabIndex = 10;
            label6.Text = "Total Stock";
            // 
            // listView2
            // 
            listView2.Location = new Point(434, 110);
            listView2.Name = "listView2";
            listView2.Size = new Size(203, 93);
            listView2.TabIndex = 9;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.White;
            label7.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(748, 147);
            label7.Name = "label7";
            label7.Size = new Size(65, 37);
            label7.TabIndex = 14;
            label7.Text = "340";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.White;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(650, 117);
            label8.Name = "label8";
            label8.Size = new Size(158, 25);
            label8.TabIndex = 13;
            label8.Text = "Total Dispatched";
            // 
            // listView3
            // 
            listView3.Location = new Point(644, 110);
            listView3.Name = "listView3";
            listView3.Size = new Size(203, 93);
            listView3.TabIndex = 12;
            listView3.UseCompatibleStateImageBehavior = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.White;
            label9.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(942, 147);
            label9.Name = "label9";
            label9.Size = new Size(33, 37);
            label9.TabIndex = 17;
            label9.Text = "0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.White;
            label10.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(862, 117);
            label10.Name = "label10";
            label10.Size = new Size(64, 25);
            label10.TabIndex = 16;
            label10.Text = "Alerts";
            // 
            // listView4
            // 
            listView4.Location = new Point(856, 110);
            listView4.Name = "listView4";
            listView4.Size = new Size(203, 93);
            listView4.TabIndex = 15;
            listView4.UseCompatibleStateImageBehavior = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.181818F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.8181839F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 227F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 212F));
            tableLayoutPanel1.Controls.Add(label16, 0, 1);
            tableLayoutPanel1.Controls.Add(label15, 4, 0);
            tableLayoutPanel1.Controls.Add(label14, 3, 0);
            tableLayoutPanel1.Controls.Add(label13, 2, 0);
            tableLayoutPanel1.Controls.Add(label11, 0, 0);
            tableLayoutPanel1.Controls.Add(label12, 1, 0);
            tableLayoutPanel1.Controls.Add(label17, 0, 2);
            tableLayoutPanel1.Controls.Add(label18, 0, 3);
            tableLayoutPanel1.Controls.Add(label19, 0, 4);
            tableLayoutPanel1.Location = new Point(228, 237);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.33645F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.66355F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel1.Size = new Size(831, 284);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = SystemColors.ActiveCaptionText;
            label11.Location = new Point(3, 0);
            label11.Name = "label11";
            label11.Size = new Size(19, 21);
            label11.TabIndex = 19;
            label11.Text = "#";
            label11.Click += label11_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.ForeColor = SystemColors.ActiveCaptionText;
            label12.Location = new Point(49, 0);
            label12.Name = "label12";
            label12.Size = new Size(41, 21);
            label12.TabIndex = 20;
            label12.Text = "Item";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.ForeColor = SystemColors.ActiveCaptionText;
            label13.Location = new Point(259, 0);
            label13.Name = "label13";
            label13.Size = new Size(59, 21);
            label13.TabIndex = 21;
            label13.Text = "Shelter";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ActiveCaptionText;
            label14.Location = new Point(486, 0);
            label14.Name = "label14";
            label14.Size = new Size(35, 21);
            label14.TabIndex = 22;
            label14.Text = "Qty";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.ActiveCaptionText;
            label15.Location = new Point(621, 0);
            label15.Name = "label15";
            label15.Size = new Size(42, 21);
            label15.TabIndex = 23;
            label15.Text = "Date";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label16.ForeColor = SystemColors.ActiveCaptionText;
            label16.Location = new Point(3, 60);
            label16.Name = "label16";
            label16.Size = new Size(19, 21);
            label16.TabIndex = 24;
            label16.Text = "1";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label17.ForeColor = SystemColors.ActiveCaptionText;
            label17.Location = new Point(3, 115);
            label17.Name = "label17";
            label17.Size = new Size(19, 21);
            label17.TabIndex = 25;
            label17.Text = "2";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label18.ForeColor = SystemColors.ActiveCaptionText;
            label18.Location = new Point(3, 169);
            label18.Name = "label18";
            label18.Size = new Size(19, 21);
            label18.TabIndex = 26;
            label18.Text = "3";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label19.ForeColor = SystemColors.ActiveCaptionText;
            label19.Location = new Point(3, 229);
            label19.Name = "label19";
            label19.Size = new Size(19, 21);
            label19.TabIndex = 27;
            label19.Text = "4";
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(230, 539);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView2);
            splitContainer1.Size = new Size(829, 180);
            splitContainer1.SplitterDistance = 436;
            splitContainer1.TabIndex = 19;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { shelterIDDataGridViewTextBoxColumn, shelterNameDataGridViewTextBoxColumn, maxCapacityDataGridViewTextBoxColumn, currentOccupancyDataGridViewTextBoxColumn, statusDataGridViewTextBoxColumn });
            dataGridView1.DataSource = shelterBindingSource;
            dataGridView1.Location = new Point(36, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(132, 150);
            dataGridView1.TabIndex = 0;
            // 
            // shelterBindingSource
            // 
            shelterBindingSource.DataSource = typeof(Models.Facilities.Shelter);
            // 
            // shelterIDDataGridViewTextBoxColumn
            // 
            shelterIDDataGridViewTextBoxColumn.DataPropertyName = "ShelterID";
            shelterIDDataGridViewTextBoxColumn.HeaderText = "ShelterID";
            shelterIDDataGridViewTextBoxColumn.Name = "shelterIDDataGridViewTextBoxColumn";
            // 
            // shelterNameDataGridViewTextBoxColumn
            // 
            shelterNameDataGridViewTextBoxColumn.DataPropertyName = "ShelterName";
            shelterNameDataGridViewTextBoxColumn.HeaderText = "ShelterName";
            shelterNameDataGridViewTextBoxColumn.Name = "shelterNameDataGridViewTextBoxColumn";
            // 
            // maxCapacityDataGridViewTextBoxColumn
            // 
            maxCapacityDataGridViewTextBoxColumn.DataPropertyName = "MaxCapacity";
            maxCapacityDataGridViewTextBoxColumn.HeaderText = "MaxCapacity";
            maxCapacityDataGridViewTextBoxColumn.Name = "maxCapacityDataGridViewTextBoxColumn";
            // 
            // currentOccupancyDataGridViewTextBoxColumn
            // 
            currentOccupancyDataGridViewTextBoxColumn.DataPropertyName = "CurrentOccupancy";
            currentOccupancyDataGridViewTextBoxColumn.HeaderText = "CurrentOccupancy";
            currentOccupancyDataGridViewTextBoxColumn.Name = "currentOccupancyDataGridViewTextBoxColumn";
            currentOccupancyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            statusDataGridViewTextBoxColumn.HeaderText = "Status";
            statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToOrderColumns = true;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { dosageDataGridViewTextBoxColumn, isPrescriptionRequiredDataGridViewCheckBoxColumn, itemIDDataGridViewTextBoxColumn, itemNameDataGridViewTextBoxColumn, quantityDataGridViewTextBoxColumn });
            dataGridView2.DataSource = medicalSupplyBindingSource;
            dataGridView2.Location = new Point(44, 30);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(132, 150);
            dataGridView2.TabIndex = 1;
            // 
            // medicalSupplyBindingSource
            // 
            medicalSupplyBindingSource.DataSource = typeof(Models.Inventory.MedicalSupply);
            // 
            // dosageDataGridViewTextBoxColumn
            // 
            dosageDataGridViewTextBoxColumn.DataPropertyName = "Dosage";
            dosageDataGridViewTextBoxColumn.HeaderText = "Dosage";
            dosageDataGridViewTextBoxColumn.Name = "dosageDataGridViewTextBoxColumn";
            // 
            // isPrescriptionRequiredDataGridViewCheckBoxColumn
            // 
            isPrescriptionRequiredDataGridViewCheckBoxColumn.DataPropertyName = "IsPrescriptionRequired";
            isPrescriptionRequiredDataGridViewCheckBoxColumn.HeaderText = "IsPrescriptionRequired";
            isPrescriptionRequiredDataGridViewCheckBoxColumn.Name = "isPrescriptionRequiredDataGridViewCheckBoxColumn";
            // 
            // itemIDDataGridViewTextBoxColumn
            // 
            itemIDDataGridViewTextBoxColumn.DataPropertyName = "ItemID";
            itemIDDataGridViewTextBoxColumn.HeaderText = "ItemID";
            itemIDDataGridViewTextBoxColumn.Name = "itemIDDataGridViewTextBoxColumn";
            // 
            // itemNameDataGridViewTextBoxColumn
            // 
            itemNameDataGridViewTextBoxColumn.DataPropertyName = "ItemName";
            itemNameDataGridViewTextBoxColumn.HeaderText = "ItemName";
            itemNameDataGridViewTextBoxColumn.Name = "itemNameDataGridViewTextBoxColumn";
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
            quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            // 
            // ReportForm
            // 
            ClientSize = new Size(1073, 749);
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(listView4);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(listView3);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(listView2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(listView1);
            Controls.Add(button2);
            Controls.Add(this.label2);
            Controls.Add(this.EvacShelLabel);
            Controls.Add(pnlSidebar);
            Name = "ReportForm";
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)shelterBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)medicalSupplyBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_2(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private Button button2;
        private ListView listView1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ListView listView2;
        private Label label7;
        private Label label8;
        private ListView listView3;
        private Label label9;
        private Label label10;
        private ListView listView4;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label11;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label17;
        private Label label18;
        private Label label19;
        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn shelterIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn shelterNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn maxCapacityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn currentOccupancyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private BindingSource shelterBindingSource;
        private System.ComponentModel.IContainer components;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn dosageDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn isPrescriptionRequiredDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn itemIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn itemNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private BindingSource medicalSupplyBindingSource;
    }
}