namespace ProjectBReady.Views
{
    partial class InventoryControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryControl));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            label6 = new Label();
            pictureBox2 = new PictureBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            inventoryButton = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 46);
            label1.Name = "label1";
            label1.Size = new Size(39, 20);
            label1.TabIndex = 0;
            label1.Text = "Item";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(242, 46);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 1;
            label2.Text = "Quantity";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(476, 46);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 2;
            label3.Text = "Details";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(918, 59);
            label4.Name = "label4";
            label4.Size = new Size(58, 20);
            label4.TabIndex = 3;
            label4.Text = "Actions";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(56, 82);
            label5.Name = "label5";
            label5.Size = new Size(43, 20);
            label5.TabIndex = 4;
            label5.Text = "Food";
            label5.Click += label5_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(24, 82);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(26, 20);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(56, 108);
            label6.Name = "label6";
            label6.Size = new Size(62, 20);
            label6.TabIndex = 7;
            label6.Text = "Medical";
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Center;
            pictureBox2.Location = new Point(24, 108);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(26, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(260, 82);
            label7.Name = "label7";
            label7.Size = new Size(25, 20);
            label7.TabIndex = 9;
            label7.Text = "10";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(260, 108);
            label8.Name = "label8";
            label8.Size = new Size(17, 20);
            label8.TabIndex = 10;
            label8.Text = "5";
            label8.Click += label8_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(476, 82);
            label9.Name = "label9";
            label9.Size = new Size(56, 20);
            label9.TabIndex = 11;
            label9.Text = "5 Liters";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(476, 117);
            label10.Name = "label10";
            label10.Size = new Size(42, 20);
            label10.TabIndex = 12;
            label10.Text = "500g";
            // 
            // inventoryButton
            // 
            inventoryButton.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            inventoryButton.ForeColor = Color.Black;
            inventoryButton.Image = (Image)resources.GetObject("inventoryButton.Image");
            inventoryButton.ImageAlign = ContentAlignment.MiddleLeft;
            inventoryButton.Location = new Point(854, 82);
            inventoryButton.Name = "inventoryButton";
            inventoryButton.Size = new Size(83, 30);
            inventoryButton.TabIndex = 13;
            inventoryButton.Text = "Stock In";
            inventoryButton.TextAlign = ContentAlignment.MiddleRight;
            inventoryButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            inventoryButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(854, 113);
            button1.Name = "button1";
            button1.Size = new Size(83, 30);
            button1.TabIndex = 14;
            button1.Text = "Stock In";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(957, 82);
            button2.Name = "button2";
            button2.Size = new Size(83, 30);
            button2.TabIndex = 15;
            button2.Text = "Stock In";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.Red;
            button3.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Location = new Point(957, 113);
            button3.Name = "button3";
            button3.Size = new Size(83, 30);
            button3.TabIndex = 16;
            button3.Text = "Stock In";
            button3.TextAlign = ContentAlignment.MiddleRight;
            button3.TextImageRelation = TextImageRelation.ImageBeforeText;
            button3.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(676, 117);
            label11.Name = "label11";
            label11.Size = new Size(92, 20);
            label11.TabIndex = 19;
            label11.Text = "Jan. 25, 2024";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(676, 82);
            label12.Name = "label12";
            label12.Size = new Size(97, 20);
            label12.TabIndex = 18;
            label12.Text = "Dec. 25, 2025";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(697, 46);
            label13.Name = "label13";
            label13.Size = new Size(55, 20);
            label13.TabIndex = 17;
            label13.Text = "Details";
            // 
            // InventoryControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label11);
            Controls.Add(label12);
            Controls.Add(label13);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(inventoryButton);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(pictureBox2);
            Controls.Add(label6);
            Controls.Add(pictureBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "InventoryControl";
            Size = new Size(1090, 557);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private PictureBox pictureBox1;
        private Label label6;
        private PictureBox pictureBox2;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Button inventoryButton;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label11;
        private Label label12;
        private Label label13;
    }
}
