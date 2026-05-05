using System.Windows.Controls;

namespace ProjectBReadyWPF.Frontend.Views.MainDashboard
{
    public partial class MainDashboardView : UserControl
    {
        public MainDashboardView()
        {
            InitializeComponent();
            DataContext = new MainDashboardViewModel();
        }

        private void OnRefresh(object sender, System.Windows.RoutedEventArgs e)
        {
            // Reload data
            DataContext = new MainDashboardViewModel();
        }

        private void OnAddShelter(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Navigate to Add Shelter form
        }

        private void OnViewAllShelters(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Navigate to Shelters list
        }
    }
}
