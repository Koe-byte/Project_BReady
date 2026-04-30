using System.Drawing;
using System.Windows.Forms;

namespace ProjectBReady.Views
{
    /// <summary>
    /// Admin dashboard — accessible only after correct PIN entry in ResidentKioskForm.
    /// Full implementation coming — this stub prevents build errors.
    /// </summary>
    public class DashboardForm : Form
    {
        private readonly string _officialName;

        public DashboardForm(string officialName)
        {
            _officialName = officialName;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "B-Ready — Admin Dashboard";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(26, 26, 26);

            this.Controls.Add(new Label
            {
                Text = $"Welcome, {_officialName} — Dashboard coming soon.",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(126, 211, 33),
                Location = new Point(40, 40),
                AutoSize = true
            });
        }
    }
}