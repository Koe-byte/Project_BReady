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

using ProjectBReadyWPF.Frontend.Views.MainDashboard;
using ProjectBReadyWPF.Frontend.Views.Admin;
using ProjectBReadyWPF.Frontend.Components;

namespace ProjectBReadyWPF.Frontend.Views.Resident
{
    public partial class ResidentWindow : Window
    {
        public ResidentWindow()
        {
            InitializeComponent();
            
            // Set initial state
            AppSidebar.SetAdminMode(false);
            MainContentArea.Content = new MainDashboardView();
        }

        // Routing inside Resident Window
        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new MainDashboardView();
        }

        private void NavShelter_Click(object sender, RoutedEventArgs e)
        {
            // Read-only shelter status view for Residents
            MainContentArea.Content = new ResidentShelterView();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Toggle Admin mode using Ctrl + Shift + O
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.O)
            {
                // Prompt for PIN to login
                var pinPrompt = new PinPromptWindow();
                pinPrompt.Owner = this;
                bool? result = pinPrompt.ShowDialog();

                if (result == true && pinPrompt.IsAuthenticated)
                {
                    MessageBox.Show("Switched to Admin Mode. Opening Admin Window.", "Admin Login", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    var adminWindow = new AdminWindow();
                    adminWindow.Closed += (s, args) => this.Show(); // Show resident window when admin window closes
                    
                    this.Hide();
                    adminWindow.Show();
                }
                
                // Prevent further handling of this key combination
                e.Handled = true;
            }
        }
    }
}
