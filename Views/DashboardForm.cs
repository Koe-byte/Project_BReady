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
        private bool isAdminMode = false;

        public DashboardForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += DashboardForm_KeyDown;
            LoadShelterStats();
            SetAdminMode(false);
        }

        // ── CTRL+SHIFT+O ──────────────────────────────────────────
        private void DashboardForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.O)
            {
                if (isAdminMode)
                {
                    SetAdminMode(false);
                }
                else
                {
                    string pin = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter Admin PIN:", "Admin Access", "");
                    if (pin == "") return;

                    DataTable dt = DBHelper.GetData(
                        "SELECT SettingValue FROM SETTINGS WHERE SettingKey = 'AdminPIN'");
                    string storedPIN = dt.Rows.Count > 0
                        ? dt.Rows[0]["SettingValue"].ToString() : "1234";

                    if (pin == storedPIN)
                        SetAdminMode(true);
                    else
                        MessageBox.Show("Incorrect PIN.", "Access Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                e.Handled = true;
            }
        }

        private void SetAdminMode(bool isAdmin)
        {
            isAdminMode = isAdmin;
            addshelterButton.Visible = isAdmin;
            ActionButton1.Visible = isAdmin;

            if (isAdmin)
            {
                toggleText.Text = "🔓 Admin Mode Active  |  Lock: Ctrl+Shift+O";
                toggleText.ForeColor = Color.FromArgb(255, 125, 40);
            }
            else
            {
                toggleText.Text = "Ctrl+Shift+O to toggle admin";
                toggleText.ForeColor = Color.White;
            }
        }

        private void LoadShelterStats()
        {
            try
            {
                DataTable dt = DBHelper.GetData(@"
                    SELECT ShelterName, MaxCapacity, CurrentOccupancy, Status
                    FROM SHELTERS ORDER BY ShelterName LIMIT 1");

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    int max = Convert.ToInt32(row["MaxCapacity"]);
                    int curr = Convert.ToInt32(row["CurrentOccupancy"]);
                    string status = row["Status"].ToString();
                    int pct = max > 0 ? (int)((double)curr / max * 100) : 0;

                    label3.Text = row["ShelterName"].ToString();
                    LBstatus1.Text = $"{curr} / {max}";
                    LBstatus2.Text = $"{pct}% occupied";
                    progressBar1.Maximum = max > 0 ? max : 1;
                    progressBar1.Value = Math.Min(curr, max);
                    label4.Text = status;

                    if (status == "Full")
                    {
                        label4.BackColor = Color.FromArgb(220, 53, 69);
                        progressBar1.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                    else if (pct >= 75)
                    {
                        label4.BackColor = Color.FromArgb(255, 165, 0);
                        progressBar1.ForeColor = Color.Orange;
                    }
                    else
                    {
                        label4.BackColor = Color.FromArgb(22, 27, 45);
                        progressBar1.ForeColor = Color.FromArgb(31, 158, 117);
                    }
                }
            }
            catch
            {
                label3.Text = "No shelters yet";
                LBstatus1.Text = "0 / 0";
                LBstatus2.Text = "0% occupied";
            }
        }

        // ── NAVIGATION — Hide/Show instead of new Form + Close ───
        private void Navigate(Form destination)
        {
            destination.FormClosed += (s, e) =>
            {
                this.Show();
                LoadShelterStats(); // refresh kapag bumalik
            };
            this.Hide();
            destination.Show();
        }

        private void shelterButton_Click(object sender, EventArgs e)
            => Navigate(new ShelterForm(isAdminMode));

        private void addshelterButton_Click(object sender, EventArgs e)
            => Navigate(new ShelterForm(isAdminMode));

        private void dashboard_Click(object sender, EventArgs e)
            => LoadShelterStats();

        // inventoryButton at button1 (Reports) — nasa Designer pero
        // hindi pa naka-wire sa event — i-add sa Designer or gamitin ito:
        private void inventoryButton_Click(object sender, EventArgs e)
            => Navigate(new InventoryForm(isAdminMode));

        private void button1_Click_1(object sender, EventArgs e)
            => Navigate(new ReportForm(isAdminMode));

        // ── UNUSED ────────────────────────────────────────────────
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click_1(object sender, EventArgs e) { }
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