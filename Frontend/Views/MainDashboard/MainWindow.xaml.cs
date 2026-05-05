using System;
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
using System.Windows.Threading;

using ProjectBReadyWPF.Frontend.Views.Shelter;
using ProjectBReadyWPF.Frontend.Views.Reports;
using ProjectBReadyWPF.Frontend.Views.Inventory;
using ProjectBReadyWPF.Frontend.Components;

namespace ProjectBReadyWPF.Frontend.Views.MainDashboard
{

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
    public partial class MainWindow : Window
    {
        private bool _isAdmin = false;
        private DispatcherTimer _inactivityTimer;
        private readonly TimeSpan _timeoutDuration = TimeSpan.FromMinutes(1);

        public MainWindow()
        {
            InitializeComponent();
            
            // Set initial state (Resident Mode)
            AppSidebar.SetAdminMode(_isAdmin);
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
            if (_isAdmin)
            {
                // Auto-logout admin due to inactivity
                _isAdmin = false;
                AppSidebar.SetAdminMode(_isAdmin);
                MainContentArea.Content = new MainDashboardView();
                MessageBox.Show("Session expired due to inactivity. Returning to Resident View.", "Timeout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new MainDashboardView();
        }

        private void NavShelter_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdmin) MainContentArea.Content = new ShelterView();
        }

        private void NavInventory_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdmin) MainContentArea.Content = new InventoryView();
        }

        private void NavReport_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdmin) MainContentArea.Content = new ReportView();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Reset timer is handled by ResetTimerEvent hook

            // Toggle Admin mode using Ctrl + Shift + O
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.O)
            {
                if (_isAdmin)
                {
                    // Manually logging out
                    _isAdmin = false;
                    AppSidebar.SetAdminMode(_isAdmin);
                    MainContentArea.Content = new MainDashboardView();
                    MessageBox.Show("Switched to Resident Mode. Returning to Dashboard.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Prompt for PIN to login
                    var pinPrompt = new PinPromptWindow();
                    pinPrompt.Owner = this;
                    bool? result = pinPrompt.ShowDialog();

                    if (result == true && pinPrompt.IsAuthenticated)
                    {
                        _isAdmin = true;
                        AppSidebar.SetAdminMode(_isAdmin);
                        MessageBox.Show("Switched to Admin Mode. All functions unlocked.", "Admin Login", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                
                // Prevent further handling of this key combination
                e.Handled = true;
            }
        }
    }
}