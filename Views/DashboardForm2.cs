using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace ProjectBReady
{

    public partial class DashboardForm2 : Form

    {

        public DashboardForm2()

        {

            InitializeComponent();

        }

        private void UpdateOccupancy(bool isAdding)
        {
            int amount = (int)numericUpDown1.Value;
            if (amount <= 0) 

            {
                MessageBox.Show("Maglagay muna ng bilang ng tao.");
                return;
            }

            string mathOp = isAdding ? "+" : "-";
            string query = $@"
            UPDATE Shelters 
            SET CurrentOccupancy = CASE 
                WHEN (CurrentOccupancy {mathOp} {amount}) < 0 THEN 0 
                WHEN (CurrentOccupancy {mathOp} {amount}) > MaxCapacity THEN MaxCapacity
                ELSE (CurrentOccupancy {mathOp} {amount})
            END
            WHERE ShelterID = 1;

            UPDATE Shelters 
            SET Status = CASE 
                WHEN CurrentOccupancy >= MaxCapacity THEN 'Full'
                ELSE 'Open'
            END
            WHERE ShelterID = 1;";

            bool success = ProjectBReady.Data.DBHelper.ExecuteQuery(query);

            if (success)
            {
                MessageBox.Show("Database Updated Successfully!"); // Tingnan kung lalabas ito
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update database. Check your connection string.");
            }
        }

        private void button2_Click(object sender, EventArgs e) // ADD BUTTON
        {
            UpdateOccupancy(true);
        }

        private void button1_Click(object sender, EventArgs e) // REMOVE BUTTON
        {
            UpdateOccupancy(false);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)

        {



        }



        private void label1_Click(object sender, EventArgs e)

        {



        }



        private void label2_Click(object sender, EventArgs e)

        {



        }



        private void label3_Click(object sender, EventArgs e)

        {



        }



        private void label4_Click(object sender, EventArgs e)

        {



        }



        private void label5_Click(object sender, EventArgs e)

        {



        }

    }
}