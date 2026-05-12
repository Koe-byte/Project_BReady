using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using ProjectBReadyWPF.Frontend.Views.Shelter;
using ProjectBReadyWPF.Frontend.Views.Reports;
using ProjectBReadyWPF.Frontend.Views.Dispatch;

using ProjectBReadyWPF.Frontend.Views.Inventory;
using ProjectBReadyWPF.Frontend.Views.MainDashboard;
using ProjectBReadyWPF.Frontend.Components;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Frontend.Views.Admin
{
    public partial class AdminWindow : Window
    {
        private DispatcherTimer _inactivityTimer;
        private readonly TimeSpan _timeoutDuration = TimeSpan.FromMinutes(1);
        private bool _isClosing = false; // Guard flag to prevent double-fire

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
            if (_isClosing) return; // Don't reset if we're already closing
            _inactivityTimer.Stop();
            _inactivityTimer.Start();
        }

        private void OnInactivityTimeout(object? sender, EventArgs e)
        {
            if (_isClosing) return; // Prevent double-fire
            _isClosing = true;
            _inactivityTimer.Stop();

            // Close the admin window FIRST, then show notification
            this.Close();

            // Show message AFTER close — it will appear on the Resident window
            MessageBox.Show(
                "You've been inactive for too long. Returning to Resident View.",
                "Session Timeout",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void PerformLogout(string message, string title, MessageBoxImage icon)
        {
            if (_isClosing) return;
            _isClosing = true;
            _inactivityTimer.Stop();
            this.Close();
            MessageBox.Show(message, title, MessageBoxButton.OK, icon);
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

        private void NavDispatch_Click(object sender, RoutedEventArgs e)
        {
            var dispatchService = App.ServiceProvider.GetRequiredService<IDispatchService>();
            var inventoryService = App.ServiceProvider.GetRequiredService<IInventoryService>();
            var shelterService = App.ServiceProvider.GetRequiredService<IShelterService>();
            MainContentArea.Content = new DispatchView(dispatchService, inventoryService, shelterService);
        }

        private void NavReport_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new ProjectBReadyWPF.Frontend.Views.Reports.ReportView();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Toggle Admin mode using Ctrl + Shift + O (Logout)
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.O)
            {
                PerformLogout("Logged out. Returning to Resident View.", "Logout", MessageBoxImage.Information);
                e.Handled = true;
            }
        }
    }
}
