namespace ProjectBReady

{

    partial class DashboardForm

    {

        /// <summary>

        ///  Required designer variable.

        /// </summary>

        private System.ComponentModel.IContainer components = null;



        /// <summary>

        ///  Clean up any resources being used.

        /// </summary>

        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing)

        {

            if (disposing && (components != null))

            {

                components.Dispose();

            }

            base.Dispose(disposing);

        }



        #region Windows Form Designer generated code



        /// <summary>

        ///  Required method for Designer support - do not modify

        ///  the contents of this method with the code editor.

        /// </summary>

        private void InitializeComponent()

        {

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));

            pnlSidebar = new Panel();

            toggleText = new TextBox();

            label1 = new Label();

            pictureBox1 = new PictureBox();

            button1 = new Button();

            inventoryButton = new Button();

            shelterButton = new Button();

            dashboard = new Button();

            EvacShelLabel = new Label();

            label2 = new Label();

            addshelterButton = new Button();

            panel1 = new Panel();

            progressBar1 = new ProgressBar();

            LBoccupany1 = new Label();

            label3 = new Label();

            pictureBox2 = new PictureBox();

            LBstatus1 = new Label();

            LBstatus2 = new Label();

            label4 = new Label();

            ActionButton1 = new Button();

            pnlSidebar.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();

            panel1.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();

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

            pnlSidebar.Name = "pnlSidebar";

            pnlSidebar.Size = new Size(242, 737);

            pnlSidebar.TabIndex = 0;

            // 

            // toggleText

            // 

            toggleText.BackColor = Color.FromArgb(0, 0, 64);

            toggleText.BorderStyle = BorderStyle.None;

            toggleText.ForeColor = Color.White;

            toggleText.Location = new Point(3, 707);

            toggleText.Name = "toggleText";

            toggleText.Size = new Size(236, 20);

            toggleText.TabIndex = 1;

            toggleText.Text = "Ctrl+Shift+O to toggle admin";

            toggleText.TextAlign = HorizontalAlignment.Center;

            // 

            // label1

            // 

            label1.AutoSize = true;

            label1.ForeColor = SystemColors.ButtonFace;

            label1.Location = new Point(84, 77);

            label1.Name = "label1";

            label1.Size = new Size(155, 20);

            label1.TabIndex = 2;

            label1.Text = "Disaster Relief System";

            label1.Click += label1_Click;

            // 

            // pictureBox1

            // 

            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");

            pictureBox1.Location = new Point(-3, -22);

            pictureBox1.Name = "pictureBox1";

            pictureBox1.Size = new Size(242, 140);

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.TabIndex = 1;

            pictureBox1.TabStop = false;

            pictureBox1.Click += pictureBox1_Click_3;

            // 

            // button1

            // 

            button1.ForeColor = Color.Black;

            button1.Image = (Image)resources.GetObject("button1.Image");

            button1.ImageAlign = ContentAlignment.MiddleLeft;

            button1.Location = new Point(43, 380);

            button1.Name = "button1";

            button1.Size = new Size(158, 69);

            button1.TabIndex = 1;

            button1.Text = "Reports";

            button1.TextAlign = ContentAlignment.MiddleRight;

            button1.TextImageRelation = TextImageRelation.ImageBeforeText;

            button1.UseVisualStyleBackColor = true;

            button1.Click += button1_Click_1;

            // 

            // inventoryButton

            // 

            inventoryButton.ForeColor = Color.Black;

            inventoryButton.Image = (Image)resources.GetObject("inventoryButton.Image");

            inventoryButton.ImageAlign = ContentAlignment.MiddleLeft;

            inventoryButton.Location = new Point(43, 305);

            inventoryButton.Name = "inventoryButton";

            inventoryButton.Size = new Size(158, 69);

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

            shelterButton.Location = new Point(43, 232);

            shelterButton.Name = "shelterButton";

            shelterButton.Size = new Size(158, 67);

            shelterButton.TabIndex = 1;

            shelterButton.Text = "Shelter";

            shelterButton.TextAlign = ContentAlignment.MiddleRight;

            shelterButton.TextImageRelation = TextImageRelation.ImageBeforeText;

            shelterButton.UseVisualStyleBackColor = true;

            shelterButton.Click += shelterButton_Click;

            // 

            // dashboard

            // 

            dashboard.ForeColor = Color.Transparent;

            dashboard.Image = (Image)resources.GetObject("dashboard.Image");

            dashboard.ImageAlign = ContentAlignment.MiddleLeft;

            dashboard.Location = new Point(43, 159);

            dashboard.Name = "dashboard";

            dashboard.Size = new Size(158, 67);

            dashboard.TabIndex = 1;

            dashboard.Text = "Dashboard";

            dashboard.TextImageRelation = TextImageRelation.ImageBeforeText;

            dashboard.UseVisualStyleBackColor = false;

            dashboard.Click += dashboard_Click;

            // 

            // EvacShelLabel

            // 

            EvacShelLabel.AccessibleRole = AccessibleRole.None;

            EvacShelLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);

            EvacShelLabel.ForeColor = Color.DimGray;

            EvacShelLabel.Location = new Point(262, 50);

            EvacShelLabel.Name = "EvacShelLabel";

            EvacShelLabel.RightToLeft = RightToLeft.No;

            EvacShelLabel.Size = new Size(405, 62);

            EvacShelLabel.TabIndex = 1;

            EvacShelLabel.Text = "Evacuation Shelters";

            EvacShelLabel.UseCompatibleTextRendering = true;

            EvacShelLabel.Click += EvacShelLabel_Click;

            // 

            // label2

            // 

            label2.AutoSize = true;

            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);

            label2.ForeColor = Color.Black;

            label2.Location = new Point(298, 102);

            label2.Name = "label2";

            label2.Size = new Size(338, 28);

            label2.TabIndex = 2;

            label2.Text = "Manage shelter occupancy and status";

            label2.Click += label2_Click;

            // 

            // addshelterButton

            // 

            addshelterButton.BackColor = Color.FromArgb(255, 125, 40);

            addshelterButton.BackgroundImageLayout = ImageLayout.None;

            addshelterButton.Cursor = Cursors.Hand;

            addshelterButton.FlatStyle = FlatStyle.Flat;

            addshelterButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);

            addshelterButton.ForeColor = Color.White;

            addshelterButton.Location = new Point(1216, 58);

            addshelterButton.Name = "addshelterButton";

            addshelterButton.Size = new Size(154, 54);

            addshelterButton.TabIndex = 3;

            addshelterButton.TabStop = false;

            addshelterButton.Text = "+ Add Shelter";

            addshelterButton.UseVisualStyleBackColor = false;

            addshelterButton.Click += addshelterButton_Click;

            // 

            // panel1

            // 

            panel1.BorderStyle = BorderStyle.Fixed3D;

            panel1.Controls.Add(ActionButton1);

            panel1.Controls.Add(label4);

            panel1.Controls.Add(LBstatus2);

            panel1.Controls.Add(LBstatus1);

            panel1.Controls.Add(progressBar1);

            panel1.Controls.Add(LBoccupany1);

            panel1.Controls.Add(label3);

            panel1.Controls.Add(pictureBox2);

            panel1.Location = new Point(298, 159);

            panel1.Name = "panel1";

            panel1.Size = new Size(484, 180);

            panel1.TabIndex = 4;

            // 

            // progressBar1

            // 

            progressBar1.AccessibleRole = AccessibleRole.ProgressBar;

            progressBar1.ForeColor = Color.Black;

            progressBar1.Location = new Point(29, 117);

            progressBar1.Name = "progressBar1";

            progressBar1.Size = new Size(407, 26);

            progressBar1.Style = ProgressBarStyle.Continuous;

            progressBar1.TabIndex = 6;

            // 

            // LBoccupany1

            // 

            LBoccupany1.AutoSize = true;

            LBoccupany1.ForeColor = Color.Black;

            LBoccupany1.Location = new Point(29, 94);

            LBoccupany1.Name = "LBoccupany1";

            LBoccupany1.Size = new Size(81, 20);

            LBoccupany1.TabIndex = 5;

            LBoccupany1.Text = "Occupancy";

            LBoccupany1.Click += label4_Click;

            // 

            // label3

            // 

            label3.AutoSize = true;

            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);

            label3.ForeColor = Color.Black;

            label3.Location = new Point(75, 9);

            label3.Name = "label3";

            label3.Size = new Size(297, 31);

            label3.TabIndex = 5;

            label3.Text = "Barangay Hall Gymnasium";

            label3.Click += label3_Click;

            // 

            // pictureBox2

            // 

            pictureBox2.BackColor = Color.White;

            pictureBox2.BackgroundImageLayout = ImageLayout.None;

            pictureBox2.BorderStyle = BorderStyle.FixedSingle;

            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");

            pictureBox2.Location = new Point(3, 11);

            pictureBox2.Name = "pictureBox2";

            pictureBox2.Size = new Size(66, 57);

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox2.TabIndex = 5;

            pictureBox2.TabStop = false;

            // 

            // LBstatus1

            // 

            LBstatus1.AutoSize = true;

            LBstatus1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);

            LBstatus1.ForeColor = Color.Black;

            LBstatus1.Location = new Point(355, 86);

            LBstatus1.Name = "LBstatus1";

            LBstatus1.Size = new Size(81, 28);

            LBstatus1.TabIndex = 7;

            LBstatus1.Text = "1 / 200";

            LBstatus1.Click += label5_Click;

            // 

            // LBstatus2

            // 

            LBstatus2.AutoSize = true;

            LBstatus2.ForeColor = Color.Black;

            LBstatus2.Location = new Point(342, 146);

            LBstatus2.Name = "LBstatus2";

            LBstatus2.Size = new Size(94, 20);

            LBstatus2.TabIndex = 8;

            LBstatus2.Text = "1% occupied";

            LBstatus2.Click += LBstatus2_Click;

            // 

            // label4

            // 

            label4.AutoSize = true;

            label4.BackColor = Color.FromArgb(22, 27, 45);

            label4.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);

            label4.ForeColor = Color.White;

            label4.Location = new Point(89, 45);

            label4.Name = "label4";

            label4.Size = new Size(52, 23);

            label4.TabIndex = 9;

            label4.Text = "Open";

            label4.Click += label4_Click_1;

            // 

            // ActionButton1

            // 

            ActionButton1.Image = (Image)resources.GetObject("ActionButton1.Image");

            ActionButton1.Location = new Point(433, 7);

            ActionButton1.Name = "ActionButton1";

            ActionButton1.Size = new Size(44, 42);

            ActionButton1.TabIndex = 10;

            ActionButton1.TextAlign = ContentAlignment.MiddleLeft;

            ActionButton1.UseVisualStyleBackColor = true;

            // 

            // Form1

            // 

            AutoScaleDimensions = new SizeF(8F, 20F);

            AutoScaleMode = AutoScaleMode.Font;

            BackColor = Color.White;

            ClientSize = new Size(1417, 737);

            Controls.Add(panel1);

            Controls.Add(addshelterButton);

            Controls.Add(label2);

            Controls.Add(EvacShelLabel);

            Controls.Add(pnlSidebar);

            ForeColor = Color.White;

            FormBorderStyle = FormBorderStyle.FixedDialog;

            Name = "Form1";

            Text = "Form1";

            pnlSidebar.ResumeLayout(false);

            pnlSidebar.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();

            panel1.ResumeLayout(false);

            panel1.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();

            ResumeLayout(false);

            PerformLayout();

        }



        #endregion



        private Panel pnlSidebar;

        private Button dashboard;

        private Button shelterButton;

        private Button inventoryButton;

        private Button button1;

        private Label label1;

        private PictureBox pictureBox1;

        private TextBox toggleText;

        private Label EvacShelLabel;

        private Label label2;

        private Button addshelterButton;

        private Panel panel1;

        private PictureBox pictureBox2;

        private Label label3;

        private Label LBoccupany1;

        private ProgressBar progressBar1;

        private Label LBstatus1;

        private Label LBstatus2;

        private Label label4;

        private Button ActionButton1;

    }

}