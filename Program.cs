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

            // Creates BReadyDB.db + tables + seed data if it doesn't exist yet
            DBHelper.InitializeDB();

            // Launch kiosk directly — no login needed
            Application.Run(new ResidentKioskForm());
        }
    }
}