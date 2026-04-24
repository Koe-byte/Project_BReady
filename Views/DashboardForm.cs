
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectBReady.Views;

public partial class DashboardForm : Form
{
    public DashboardForm()
    {
        InitializeComponent();

        this.ActionButton1.Click += new System.EventHandler(this.ActionButton1_Click);
    }

    private void LoadShelterData()
    {
        string query = "SELECT * FROM Shelters WHERE ShelterID = 1";
        DataTable dt = ProjectBReady.Data.DBHelper.GetData(query);

        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            int current = Convert.ToInt32(row["CurrentOccupancy"]);
            int max = Convert.ToInt32(row["MaxCapacity"]);
            string status = row["Status"].ToString();

            LBstatus1.Text = status;
            label4.Text = $"{current} / {max}";
            progressBar1.Maximum = max;
            progressBar1.Value = current;

            double percentage = ((double)current / max) * 100;
            LBstatus2.Text = $"{Math.Round(percentage)}% occupied";
            LBstatus1.ForeColor = (status == "Full") ? Color.Red : Color.DarkGreen;
        }
    }

    private void DashboardForm1_Load(object sender, EventArgs e)
    {
        LoadShelterData(); // Tawagin ito para pagbukas ng app, may laman agad
    }

    private void ActionButton1_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Button Clicked!");
        DashboardForm2 updateForm = new DashboardForm2();
        if (updateForm.ShowDialog() == DialogResult.OK)
        {
            LoadShelterData(); // Pag-close ng Form 2, mag-uupdate ang UI dito
        }
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void pictureBox1_Click_1(object sender, EventArgs e)
    {

    }

    private void dashboard_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click_1(object sender, EventArgs e)
    {

    }

    private void pictureBox1_Click_2(object sender, EventArgs e)
    {

    }

    private void shelterButton_Click(object sender, EventArgs e)
    {

    }

    private void pictureBox1_Click_3(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void EvacShelLabel_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void addshelterButton_Click(object sender, EventArgs e)
    {

    }

    private void pictureBox2_Click(object sender, EventArgs e)
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

    private void LBstatus2_Click(object sender, EventArgs e)
    {

    }

    private void label4_Click_1(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {

    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void ActionButton1_Click_1(object sender, EventArgs e)
    {

    }
}