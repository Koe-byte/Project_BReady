using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectBReadyWPF.Database.DataAccess;
using ProjectBReadyWPF.Frontend.Views.Shelter;
using ProjectBReadyWPF.Frontend.Views.Report;
using ProjectBReadyWPF.Frontend.Views.Inventory;

namespace ProjectBReadyWPF.Frontend.Views.MainDashboard
{

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContentArea.Content = new ShelterView();

            // TESTING LANG: I-run ang connection test pagkabukas ng app
            DBHelper db = new DBHelper();
            db.TestConnection();
        }

        private void NavShelter_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new ShelterView(); // Papalitan ang gitna ng Shelter View
        }

        private void NavInventory_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new InventoryView(); // Papalitan ang gitna ng Inventory View
        }

        private void NavReport_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new ReportView(); // Papalitan ang gitna ng Report View
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logging out...");
            Application.Current.Shutdown(); // Isasara ang app
        }
    }
}