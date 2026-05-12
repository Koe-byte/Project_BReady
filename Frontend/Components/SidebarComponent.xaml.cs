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

        private Button? _activeButton;

        public SidebarComponent()
        {
            InitializeComponent();

            if (BtnDashboard != null) BtnDashboard.Click += (s, e) => { SetActiveButton(BtnDashboard); DashboardClick?.Invoke(s, e); };
            if (BtnShelter != null) BtnShelter.Click += (s, e) => { SetActiveButton(BtnShelter); ShelterClick?.Invoke(s, e); };
            if (BtnInventory != null) BtnInventory.Click += (s, e) => { SetActiveButton(BtnInventory); InventoryClick?.Invoke(s, e); };
            if (BtnReport != null) BtnReport.Click += (s, e) => { SetActiveButton(BtnReport); ReportClick?.Invoke(s, e); };

            // Default active
            _activeButton = BtnDashboard;
        }

        private void SetActiveButton(Button btn)
        {
            // Reset previous active to NavItem style
            if (_activeButton != null && _activeButton != btn)
            {
                _activeButton.Style = (Style)FindResource("NavItem");
            }

            // Set new active to NavItemActive style
            btn.Style = (Style)FindResource("NavItemActive");
            _activeButton = btn;
        }

        public void SetAdminMode(bool isAdmin)
        {
            var visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
            
            if (LblManagement != null) LblManagement.Visibility = visibility;
            if (BtnInventory != null) BtnInventory.Visibility = visibility;
            if (BtnReport != null) BtnReport.Visibility = visibility;
        }
    }
}