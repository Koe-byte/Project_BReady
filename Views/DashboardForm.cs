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
    public partial class DashboardForm : Form
    {
        // ── Admin State ──────────────────────────────────────────
        private bool isAdminMode = false;
        private const string ADMIN_PIN = "1234"; // Palitan ng gusto ninyong PIN

        public DashboardForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // Para makuha ng Form ang keyboard events
            this.KeyDown += DashboardForm_KeyDown;
            LoadShelterStats();
            SetAdminMode(false); // Start as Resident/Kiosk mode
        }

        // ── CTRL+SHIFT+O — Admin Toggle ──────────────────────────
        private void DashboardForm_KeyDown(object sender, KeyEventArgs e)
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
        private void panel2_Paint(object sender, PaintEventArgs e) { }
    }
}