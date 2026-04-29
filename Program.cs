using System;
using System.Windows.Forms;
using ProjectBReady.Data;
using ProjectBReady.Views;

namespace ProjectBReady
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ── 1. Boot database ─────────────────────────────────────────
            using var db = new AppDbContext();

            try
            {
                DbInitializer.Initialize(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Database initialization failed:\n\n{ex.Message}\n\n" +
                    "Make sure SQL Server LocalDB is installed.\n" +
                    "Install via: Visual Studio Installer → SQL Server Express LocalDB",
                    "DB Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // ── 2. Launch Resident Kiosk directly (default view for residents) ──
            //    Ctrl+Shift+O inside that form opens the PIN dialog for officials.
            Application.Run(new ResidentKioskForm(db));
        }
    }
}