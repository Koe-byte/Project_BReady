using System.Windows;
using System.Windows.Controls;

namespace ProjectBReadyWPF.Frontend.Components
{
    public partial class SidebarComponent : UserControl
    {
        public event RoutedEventHandler? DashboardClick;
        public event RoutedEventHandler? ShelterClick;
        public event RoutedEventHandler? InventoryClick;
        public event RoutedEventHandler? ReportClick;

        public SidebarComponent()
        {
            InitializeComponent();

            if (BtnDashboard != null) BtnDashboard.Click += (s, e) => DashboardClick?.Invoke(s, e);
            if (BtnShelter != null) BtnShelter.Click += (s, e) => ShelterClick?.Invoke(s, e);
            if (BtnInventory != null) BtnInventory.Click += (s, e) => InventoryClick?.Invoke(s, e);
            if (BtnReport != null) BtnReport.Click += (s, e) => ReportClick?.Invoke(s, e);
        }

        public void SetAdminMode(bool isAdmin)
        {
            var visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
            
            if (BtnShelter != null) BtnShelter.Visibility = visibility;
            if (BtnInventory != null) BtnInventory.Visibility = visibility;
            if (BtnReport != null) BtnReport.Visibility = visibility;
        }
    }
}