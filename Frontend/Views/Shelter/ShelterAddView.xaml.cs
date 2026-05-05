using System.Windows;
using System.Windows.Controls;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Backend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Backend.Models.Facilities;

namespace ProjectBReadyWPF.Frontend.Views.Shelter
{
    public partial class ShelterAddView : UserControl
    {
        private readonly IShelterService _shelterService;

        public ShelterAddView()
        {
            InitializeComponent();
            _shelterService = App.ServiceProvider.GetRequiredService<IShelterService>();
        }

        private void OnSaveShelter(object sender, RoutedEventArgs e)
        {
            // Reset feedback
            ErrorBorder.Visibility = Visibility.Collapsed;
            SuccessBorder.Visibility = Visibility.Collapsed;

            // Validate
            string name = ShelterNameEntry.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(name))
            {
                ShowError("Shelter name is required.");
                return;
            }

            if (!int.TryParse(MaxCapEntry.Text, out int maxCap) || maxCap <= 0)
            {
                ShowError("Maximum capacity must be a positive number.");
                return;
            }

            int currOcc = 0;
            if (!string.IsNullOrEmpty(CurrOccEntry.Text) && (!int.TryParse(CurrOccEntry.Text, out currOcc) || currOcc < 0))
            {
                ShowError("Current occupancy must be a non-negative number.");
                return;
            }

            if (currOcc > maxCap)
            {
                ShowError($"Current occupancy ({currOcc}) cannot exceed max capacity ({maxCap}).");
                return;
            }

            // Get status
            string status = "Open";
            if (StatusPicker.SelectedItem is ComboBoxItem selected)
            {
                status = selected.Content?.ToString() ?? "Open";
            }

            // Auto-set to Full if at capacity
            if (currOcc >= maxCap)
            {
                status = "Full";
            }

            // Save to database
            var shelter = new Backend.Models.Facilities.Shelter
            {
                ShelterName = name,
                MaxCapacity = maxCap,
                CurrentOccupancy = currOcc,
                Status = status
            };

            bool success = _shelterService.AddShelter(shelter);
            if (success)
            {
                SuccessLabel.Text = $"✅ Shelter \"{name}\" has been successfully registered!";
                SuccessBorder.Visibility = Visibility.Visible;

                // Clear fields
                ShelterNameEntry.Text = "";
                MaxCapEntry.Text = "";
                CurrOccEntry.Text = "0";
                StatusPicker.SelectedIndex = 0;
            }
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = $"⚠️ {message}";
            ErrorBorder.Visibility = Visibility.Visible;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            OnBackToShelters(sender, e);
        }

        private void OnBackToShelters(object sender, RoutedEventArgs e)
        {
            // Navigate back to Shelter list
            var mainWindow = Window.GetWindow(this) as ProjectBReadyWPF.Frontend.Views.MainDashboard.MainWindow;
            if (mainWindow != null)
            {
                var contentArea = mainWindow.FindName("MainContentArea") as ContentControl;
                if (contentArea != null)
                {
                    contentArea.Content = new ShelterView();
                }
            }
        }
    }
}
