using ProjectBReady.Data;
using ProjectBReady.Forms;
using System;
using System.Windows.Forms;

namespace ProjectBReady
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Creates BReadyDB.db + tables + seed data kung wala pa
            DBHelper.InitializeDB();

            Application.Run(new DashboardForm());
        }
    }
}