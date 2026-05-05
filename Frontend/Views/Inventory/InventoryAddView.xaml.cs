using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Backend.Models.Inventory;

namespace ProjectBReadyWPF.Frontend.Views.Inventory
{
    public partial class InventoryAddView : UserControl
    {
        private readonly InventoryService _inventoryService;
        private bool _isFoodSelected = true;

        public InventoryAddView()
        {
            InitializeComponent();
            _inventoryService = new InventoryService();
            ExpDatePicker.SelectedDate = DateTime.Now.AddMonths(6); // Default 6 months from now
        }

        private void OnSelectFood(object sender, RoutedEventArgs e)
        {
            _isFoodSelected = true;
            
            // UI Updates
            BtnFoodFrame.Background = (SolidColorBrush)FindResource("AccentLight");
            BtnFoodFrame.BorderBrush = (SolidColorBrush)FindResource("Accent");
            BtnFood.Foreground = (SolidColorBrush)FindResource("Accent");
            BtnFood.FontWeight = FontWeights.Bold;

            BtnMedFrame.Background = Brushes.White;
            BtnMedFrame.BorderBrush = (SolidColorBrush)FindResource("Border");
            BtnMed.Foreground = (SolidColorBrush)FindResource("TextSecondary");
            BtnMed.FontWeight = FontWeights.Normal;

            FoodFields.Visibility = Visibility.Visible;
            MedFields.Visibility = Visibility.Collapsed;
        }

        private void OnSelectMedical(object sender, RoutedEventArgs e)
        {
            _isFoodSelected = false;

            // UI Updates
            BtnMedFrame.Background = (SolidColorBrush)FindResource("TealLight");
            BtnMedFrame.BorderBrush = (SolidColorBrush)FindResource("Teal");
            BtnMed.Foreground = (SolidColorBrush)FindResource("Teal");
            BtnMed.FontWeight = FontWeights.Bold;

            BtnFoodFrame.Background = Brushes.White;
            BtnFoodFrame.BorderBrush = (SolidColorBrush)FindResource("Border");
            BtnFood.Foreground = (SolidColorBrush)FindResource("TextSecondary");
            BtnFood.FontWeight = FontWeights.Normal;

            FoodFields.Visibility = Visibility.Collapsed;
            MedFields.Visibility = Visibility.Visible;
        }

        private void OnSaveItem(object sender, RoutedEventArgs e)
        {
            ErrorBorder.Visibility = Visibility.Collapsed;
            SuccessBorder.Visibility = Visibility.Collapsed;

            // Validations
            string name = ItemNameEntry.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(name))
            {
                ShowError("Item Name is required.");
                return;
            }

            if (!int.TryParse(ItemQtyEntry.Text, out int qty) || qty < 0)
            {
                ShowError("Quantity must be a valid non-negative number.");
                return;
            }

            bool success = false;

            if (_isFoodSelected)
            {
                if (!ExpDatePicker.SelectedDate.HasValue)
                {
                    ShowError("Expiration date is required for food items.");
                    return;
                }

                var food = new FoodItem
                {
                    ItemName = name,
                    Quantity = qty,
                    ExpirationDate = ExpDatePicker.SelectedDate.Value
                };
                success = _inventoryService.AddFoodItem(food);
            }
            else
            {
                var med = new MedicalSupply
                {
                    ItemName = name,
                    Quantity = qty,
                    Dosage = DosageEntry.Text?.Trim() ?? "",
                    IsPrescriptionRequired = RxCheckbox.IsChecked ?? false
                };
                success = _inventoryService.AddMedicalSupply(med);
            }

            if (success)
            {
                SuccessLabel.Text = $"✅ Successfully added {qty} units of \"{name}\".";
                SuccessBorder.Visibility = Visibility.Visible;

                // Reset form
                ItemNameEntry.Text = "";
                ItemQtyEntry.Text = "0";
                DosageEntry.Text = "";
                RxCheckbox.IsChecked = false;
                ExpDatePicker.SelectedDate = DateTime.Now.AddMonths(6);
            }
            else
            {
                ShowError("Failed to save item to database.");
            }
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = $"⚠️ {message}";
            ErrorBorder.Visibility = Visibility.Visible;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            OnBackToInventory(sender, e);
        }

        private void OnBackToInventory(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as ProjectBReadyWPF.Frontend.Views.MainDashboard.MainWindow;
            if (mainWindow != null)
            {
                var contentArea = mainWindow.FindName("MainContentArea") as ContentControl;
                if (contentArea != null)
                {
                    contentArea.Content = new InventoryView();
                }
            }
        }
    }
}
