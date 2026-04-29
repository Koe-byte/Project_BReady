namespace ProjectBReady.Views
{
    partial class Inventory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            panel1 = new Panel();
            button1 = new Button();
            inventoryButton = new Button();
            shelterButton = new Button();
            dashboard = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button2 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 64);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(inventoryButton);
            panel1.Controls.Add(shelterButton);
            panel1.Controls.Add(dashboard);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(2, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 574);
            panel1.TabIndex = 0;
            panel1.Paint += this.panel1_Paint;
            // 
            // button1
            // 
            button1.ForeColor = Color.Black;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(30, 363);
            button1.Name = "button1";
            button1.Size = new Size(158, 69);
            button1.TabIndex = 2;
            button1.Text = "Reports";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // inventoryButton
            // 
            inventoryButton.ForeColor = Color.Black;
            inventoryButton.Image = (Image)resources.GetObject("inventoryButton.Image");
            inventoryButton.ImageAlign = ContentAlignment.MiddleLeft;
            inventoryButton.Location = new Point(30, 288);
            inventoryButton.Name = "inventoryButton";
            inventoryButton.Size = new Size(158, 69);
            inventoryButton.TabIndex = 3;
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
            shelterButton.Location = new Point(30, 215);
            shelterButton.Name = "shelterButton";
            shelterButton.Size = new Size(158, 67);
            shelterButton.TabIndex = 4;
            shelterButton.Text = "Shelter";
            shelterButton.TextAlign = ContentAlignment.MiddleRight;
            shelterButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            shelterButton.UseVisualStyleBackColor = true;
            // 
            // dashboard
            // 
            dashboard.ForeColor = Color.Transparent;
            dashboard.Image = (Image)resources.GetObject("dashboard.Image");
            dashboard.ImageAlign = ContentAlignment.MiddleLeft;
            dashboard.Location = new Point(30, 142);
            dashboard.Name = "dashboard";
            dashboard.Size = new Size(158, 67);
            dashboard.TabIndex = 5;
            dashboard.Text = "Dashboard";
            dashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            dashboard.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-18, -19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(243, 130);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            pictureBox1.Click += this.pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(58, 75);
            label1.Name = "label1";
            label1.Size = new Size(155, 20);
            label1.TabIndex = 1;
            label1.Text = "Disaster Relief System";
            label1.Click += this.label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(233, 37);
            label2.Name = "label2";
            label2.Size = new Size(318, 38);
            label2.TabIndex = 1;
            label2.Text = "Relief Goods Inventory";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(283, 75);
            label3.Name = "label3";
            label3.Size = new Size(268, 20);
            label3.TabIndex = 2;
            label3.Text = "Manage stock and dispatch relief items";
            label3.Click += this.label3_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(1260, 34);
            button2.Name = "button2";
            button2.RightToLeft = RightToLeft.No;
            button2.Size = new Size(149, 61);
            button2.TabIndex = 3;
            button2.Text = "+ Add";
            button2.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.LightGray;
            flowLayoutPanel1.Controls.Add(button3);
            flowLayoutPanel1.Controls.Add(button4);
            flowLayoutPanel1.Controls.Add(button5);
            flowLayoutPanel1.Location = new Point(283, 113);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(278, 55);
            flowLayoutPanel1.TabIndex = 4;
            flowLayoutPanel1.Paint += this.flowLayoutPanel1_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(276, 191);
            label4.Name = "label4";
            label4.Size = new Size(0, 20);
            label4.TabIndex = 5;
            label4.Click += this.label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(379, 208);
            label5.Name = "label5";
            label5.Size = new Size(0, 20);
            label5.TabIndex = 5;
            label5.Click += this.label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(412, 314);
            label6.Name = "label6";
            label6.Size = new Size(0, 20);
            label6.TabIndex = 5;
            // 
            // button3
            // 
            button3.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            button3.Location = new Point(3, 3);
            button3.Name = "button3";
            button3.Size = new Size(78, 44);
            button3.TabIndex = 6;
            button3.Text = "All (n)";
            button3.UseVisualStyleBackColor = true;
            button3.Click += this.button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(87, 3);
            button4.Name = "button4";
            button4.Size = new Size(78, 44);
            button4.TabIndex = 7;
            button4.Text = "Food (n)";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(171, 3);
            button5.Name = "button5";
            button5.Size = new Size(97, 44);
            button5.TabIndex = 8;
            button5.Text = "Medical (n)";
            button5.UseVisualStyleBackColor = true;
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1442, 557);
            Controls.Add(label4);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            Name = "Inventory";
            Text = "Form1";
            Load += this.Inventory_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Button inventoryButton;
        private Button shelterButton;
        private Button dashboard;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}