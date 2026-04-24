using System;
using System.Data;
using Microsoft.Data.SqlClient; // Siguraduhing naka-install ang NuGet package na ito
using System.Windows.Forms;
using System.Drawing;

namespace ProjectBReady.Views
{
    public partial class DashboardForm : Form
    {
        // Palitan mo 'to ng actual connection string mo mula sa Properties ng BReadyDB.mdf
        string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\BReadyDB.mdf;Integrated Security=True;Connect Timeout=30";

        public DashboardForm()
        {
            InitializeComponent();
            SetupPreviewUI();
        }

        private void SetupPreviewUI()
        {
            this.Text = "Project B-Ready - Developer Preview";
            this.Size = new Size(900, 600);

            Label lblTitle = new Label { Text = "SHELTER & INVENTORY MONITORING", Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) };
            this.Controls.Add(lblTitle);

            // Gagawa tayo ng grid para sa Shelters
            DataGridView dgvShelters = new DataGridView { Name = "dgvShelters", Location = new Point(20, 60), Size = new Size(840, 200), ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(dgvShelters);

            // Gagawa tayo ng grid para sa Inventory
            DataGridView dgvInventory = new DataGridView { Name = "dgvInventory", Location = new Point(20, 280), Size = new Size(840, 200), ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(dgvInventory);

            Button btnLoad = new Button { Text = "REFRESH DATA", Location = new Point(20, 500), Size = new Size(150, 40) };
            btnLoad.Click += (s, e) => LoadDataFromDB();
            this.Controls.Add(btnLoad);
        }

        private void LoadDataFromDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Load Shelters
                    SqlDataAdapter daShelter = new SqlDataAdapter("SELECT * FROM Shelters", conn);
                    DataTable dtShelter = new DataTable();
                    daShelter.Fill(dtShelter);
                    ((DataGridView)this.Controls["dgvShelters"]).DataSource = dtShelter;

                    // Load Inventory
                    SqlDataAdapter daInv = new SqlDataAdapter("SELECT ItemName, ItemType, Quantity FROM InventoryItems", conn);
                    DataTable dtInv = new DataTable();
                    daInv.Fill(dtInv);
                    ((DataGridView)this.Controls["dgvInventory"]).DataSource = dtInv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
    }
}