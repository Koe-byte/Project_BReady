namespace ProjectBReady
{
    partial class DashboardForm2
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
            panel1 = new Panel();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label5 = new Label();
            numericUpDown1 = new NumericUpDown();
            button1 = new Button();
            button2 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(240, 243, 247);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(34, 70);
            panel1.Name = "panel1";
            panel1.Size = new Size(470, 129);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(176, 92);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 2;
            label4.Text = "of 200 max";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Symbol", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(196, 42);
            label3.Name = "label3";
            label3.Size = new Size(43, 50);
            label3.TabIndex = 1;
            label3.Text = "1";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(122, 11);
            label2.Name = "label2";
            label2.Size = new Size(209, 31);
            label2.TabIndex = 0;
            label2.Text = "Current Occupancy";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(11, 27);
            label1.Name = "label1";
            label1.Size = new Size(524, 31);
            label1.TabIndex = 1;
            label1.Text = "Update Occupancy - Barangay Hall Gymnasium ";
            label1.Click += label1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 211);
            label5.Name = "label5";
            label5.Size = new Size(130, 20);
            label5.TabIndex = 2;
            label5.Text = "Number of People";
            label5.Click += label5_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDown1.Location = new Point(34, 234);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(470, 34);
            numericUpDown1.TabIndex = 3;
            // 
            // button1
            // 
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(292, 280);
            button1.Name = "button1";
            button1.Size = new Size(212, 47);
            button1.TabIndex = 4;
            button1.Text = "- Remove";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(34, 280);
            button2.Name = "button2";
            button2.Size = new Size(212, 47);
            button2.TabIndex = 5;
            button2.Text = "+ Add";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // DashboardForm2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 339);
            ControlBox = false;
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "DashboardForm2";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form2";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private NumericUpDown numericUpDown1;
        private Button button1;
        private Button button2;
    }
}