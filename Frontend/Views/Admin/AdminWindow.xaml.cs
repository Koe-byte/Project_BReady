using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using ProjectBReadyWPF.Frontend.Views.Shelter;
using ProjectBReadyWPF.Frontend.Views.Reports;

using ProjectBReadyWPF.Frontend.Views.Inventory;
using ProjectBReadyWPF.Frontend.Views.MainDashboard;
using ProjectBReadyWPF.Frontend.Components;

namespace ProjectBReadyWPF.Frontend.Views.Admin
{
    public partial class AdminWindow : Window
    {
        private DispatcherTimer _inactivityTimer;
        private readonly TimeSpan _timeoutDuration = TimeSpan.FromMinutes(1);

        public AdminWindow()
        {
            InitializeComponent();
            
            // Set initial state (Admin Mode)
            AppSidebar.SetAdminMode(true);
            MainContentArea.Content = new MainDashboardView();

            // Setup Inactivity Timer
            _inactivityTimer = new DispatcherTimer();
            _inactivityTimer.Interval = _timeoutDuration;
            _inactivityTimer.Tick += OnInactivityTimeout;
            _inactivityTimer.Start();

            this.PreviewMouseMove += (s, e) => ResetTimer();
            this.PreviewMouseDown += (s, e) => ResetTimer();
            this.PreviewKeyDown += (s, e) => ResetTimer();
            this.PreviewTouchDown += (s, e) => ResetTimer();
        }

        private void ResetTimer()
        {
            _inactivityTimer.Stop();
            _inactivityTimer.Start();
        }

        private void OnInactivityTimeout(object? sender, EventArgs e)
        {
            // Auto-logout admin due to inactivity
            _inactivityTimer.Stop();
            MessageBox.Show("Admin session expired due to inactivity. Returning to Resident View.", "Timeout", MessageBoxButton.OK, MessageBoxImage.Warning);
            this.Close();
        }

        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new MainDashboardView();
        }

        private void NavShelter_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new ShelterView();
        }

        private void NavInventory_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new InventoryView();
        }

        private void NavReport_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new ReportView();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Toggle Admin mode using Ctrl + Shift + O (Logout)
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.O)
            {
                // Manually logging out
                _inactivityTimer.Stop();
                MessageBox.Show("Logged out. Returning to Resident View.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                e.Handled = true;
            }
        }
    }
}
