using System.Windows;
using System.Windows.Controls;

namespace ProjectBReadyWPF.Frontend.Components
{
    public partial class SidebarComponent : UserControl
    {
        public event RoutedEventHandler? ShelterClick;
        public event RoutedEventHandler? InventoryClick;
        public event RoutedEventHandler? ReportClick;
        public event RoutedEventHandler? LogoutClick;

        public SidebarComponent()
        {
            InitializeComponent();

            if (BtnShelter != null) BtnShelter.Click += (s, e) => ShelterClick?.Invoke(s, e);
            if (BtnInventory != null) BtnInventory.Click += (s, e) => InventoryClick?.Invoke(s, e);
            if (BtnReport != null) BtnReport.Click += (s, e) => ReportClick?.Invoke(s, e);
            if (BtnLogout != null) BtnLogout.Click += (s, e) => LogoutClick?.Invoke(s, e);
        }
    }
}