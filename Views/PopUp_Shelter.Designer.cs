namespace ProjectBReady.Views
{
    partial class PopUp_Shelter
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
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            label5 = new Label();
            numericUpDown1 = new NumericUpDown();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(662, 41);
            label1.TabIndex = 0;
            label1.Text = "Update Occpancy - Barangay Hall Gymnasium";
            label1.Click += this.label1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(13, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(661, 131);
            panel1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(225, 12);
            label2.Name = "label2";
            label2.Size = new Size(201, 31);
            label2.TabIndex = 0;
            label2.Text = "Curent Occupancy";
            label2.Click += this.label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(304, 43);
            label3.Name = "label3";
            label3.Size = new Size(45, 54);
            label3.TabIndex = 1;
            label3.Text = "0";
            label3.Click += this.label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(286, 97);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 2;
            label4.Text = "of 150 max";
            label4.Click += this.label4_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 128, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(29, 266);
            button1.Name = "button1";
            button1.RightToLeft = RightToLeft.No;
            button1.Size = new Size(271, 61);
            button1.TabIndex = 2;
            button1.Text = "+ Add";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.White;
            button2.Location = new Point(403, 266);
            button2.Name = "button2";
            button2.Size = new Size(271, 61);
            button2.TabIndex = 3;
            button2.Text = "- Remove";
            button2.UseVisualStyleBackColor = false;
            button2.Click += this.button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 210);
            label5.Name = "label5";
            label5.Size = new Size(132, 20);
            label5.TabIndex = 4;
            label5.Text = "Number Of People";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(29, 233);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(645, 27);
            numericUpDown1.TabIndex = 5;
            // 
            // PopUp_Shelter
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 339);
            Controls.Add(numericUpDown1);
            Controls.Add(label5);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "PopUp_Shelter";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button1;
        private Button button2;
        private Label label5;
        private NumericUpDown numericUpDown1;
    }
}