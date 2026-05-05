using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Backend.Models.Inventory;

namespace ProjectBReadyWPF.Frontend.Views.Inventory
{
    // ── Display models ───────────────────────────────────────────────

    public class FoodDisplayItem
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = "";
        public int Qty { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string ExpiryDisplay => ExpirationDate == DateTime.MinValue ? "N/A" : ExpirationDate.ToString("MMM dd, yyyy");
        public string StatusLabel
        {
            get
            {
                if (ExpirationDate != DateTime.MinValue && ExpirationDate <= DateTime.Now) return "Expired";
                if (ExpirationDate != DateTime.MinValue && ExpirationDate <= DateTime.Now.AddDays(7)) return "Expiring";
                return "Good";
            }
        }
        public SolidColorBrush StatusBadgeBg => StatusLabel switch
        {
            "Expired" => new SolidColorBrush(Color.FromRgb(254, 226, 226)),
            "Expiring" => new SolidColorBrush(Color.FromRgb(254, 243, 199)),
            _ => new SolidColorBrush(Color.FromRgb(209, 250, 229))
        };
        public SolidColorBrush StatusTextColor => StatusLabel switch
        {
            "Expired" => new SolidColorBrush(Color.FromRgb(153, 27, 27)),
            "Expiring" => new SolidColorBrush(Color.FromRgb(146, 64, 14)),
            _ => new SolidColorBrush(Color.FromRgb(22, 101, 52))
        };
    }

    public class MedDisplayItem
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = "";
        public int Qty { get; set; }
        public string Dosage { get; set; } = "";
        public bool IsPrescriptionRequired { get; set; }

        public string RxLabel => IsPrescriptionRequired ? "Yes" : "No";
        public SolidColorBrush RxBadgeBg => IsPrescriptionRequired
            ? new SolidColorBrush(Color.FromRgb(254, 226, 226))
            : new SolidColorBrush(Color.FromRgb(209, 250, 229));
        public SolidColorBrush RxTextColor => IsPrescriptionRequired
            ? new SolidColorBrush(Color.FromRgb(153, 27, 27))
            : new SolidColorBrush(Color.FromRgb(22, 101, 52));
    }

    // ── ViewModel ────────────────────────────────────────────────────

    public class InventoryPageViewModel
    {
        public List<FoodDisplayItem> FoodItems { get; set; } = new();
        public List<MedDisplayItem> MedItems { get; set; } = new();
        public int FoodItemCount { get; set; }
        public int TotalFoodUnits { get; set; }
        public int MedItemCount { get; set; }
        public int TotalMedUnits { get; set; }
    }

    // ── View ─────────────────────────────────────────────────────────

    public partial class InventoryView : UserControl
    {
        private readonly InventoryService _inventoryService;
        private List<FoodDisplayItem> _allFood = new();
        private List<MedDisplayItem> _allMed = new();
        private int _selectedFoodId = -1;
        private int _selectedMedId = -1;
        private bool _isFoodTab = true;

        public InventoryView()
        {
            InitializeComponent();
            _inventoryService = new InventoryService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var foodItems = _inventoryService.GetFoodItems();
                var medItems = _inventoryService.GetMedicalSupplies();

                _allFood = foodItems.Select(f => new FoodDisplayItem
                {
                    ItemID = f.ItemID,
                    Name = f.ItemName,
                    Qty = f.Quantity,
                    ExpirationDate = f.ExpirationDate
                }).ToList();

                _allMed = medItems.Select(m => new MedDisplayItem
                {
                    ItemID = m.ItemID,
                    Name = m.ItemName,
                    Qty = m.Quantity,
                    Dosage = m.Dosage,
                    IsPrescriptionRequired = m.IsPrescriptionRequired
                }).ToList();

                DataContext = new InventoryPageViewModel
                {
                    FoodItems = _allFood,
                    MedItems = _allMed,
                    FoodItemCount = _allFood.Count,
                    TotalFoodUnits = _allFood.Sum(f => f.Qty),
                    MedItemCount = _allMed.Count,
                    TotalMedUnits = _allMed.Sum(m => m.Qty)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ── Tabs ─────────────────────────────────────────────────────

        private void OnTabFood(object sender, RoutedEventArgs e)
        {
            _isFoodTab = true;
            FoodPane.Visibility = Visibility.Visible;
            MedPane.Visibility = Visibility.Collapsed;
            TabFoodBtn.Foreground = (SolidColorBrush)FindResource("Accent");
            TabFoodBtn.FontWeight = FontWeights.Bold;
            TabMedBtn.Foreground = (SolidColorBrush)FindResource("TextMuted");
            TabMedBtn.FontWeight = FontWeights.Normal;
        }

        private void OnTabMedical(object sender, RoutedEventArgs e)
        {
            _isFoodTab = false;
            FoodPane.Visibility = Visibility.Collapsed;
            MedPane.Visibility = Visibility.Visible;
            TabMedBtn.Foreground = (SolidColorBrush)FindResource("Accent");
            TabMedBtn.FontWeight = FontWeights.Bold;
            TabFoodBtn.Foreground = (SolidColorBrush)FindResource("TextMuted");
            TabFoodBtn.FontWeight = FontWeights.Normal;
        }

        // ── Search ───────────────────────────────────────────────────

        private void OnFoodSearchChanged(object sender, TextChangedEventArgs e)
        {
            var q = FoodSearchBox.Text?.Trim().ToLower() ?? "";
            FoodTable.ItemsSource = string.IsNullOrEmpty(q) ? _allFood
                : _allFood.Where(f => f.Name.ToLower().Contains(q)).ToList();
        }

        private void OnMedSearchChanged(object sender, TextChangedEventArgs e)
        {
            var q = MedSearchBox.Text?.Trim().ToLower() ?? "";
            MedTable.ItemsSource = string.IsNullOrEmpty(q) ? _allMed
                : _allMed.Where(m => m.Name.ToLower().Contains(q)).ToList();
        }

        // ── Radio selection ──────────────────────────────────────────

        private void OnFoodRadioChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && rb.Tag is int id) _selectedFoodId = id;
        }

        private void OnMedRadioChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && rb.Tag is int id) _selectedMedId = id;
        }

        // ── Stock In ─────────────────────────────────────────────────

        private void OnStockInFood(object sender, RoutedEventArgs e) => OpenStockIn(true);
        private void OnStockInMed(object sender, RoutedEventArgs e) => OpenStockIn(false);

        private void OpenStockIn(bool isFood)
        {
            int id = isFood ? _selectedFoodId : _selectedMedId;
            if (id < 0) { MessageBox.Show("Pumili muna ng item."); return; }

            string name = isFood
                ? _allFood.FirstOrDefault(f => f.ItemID == id)?.Name ?? ""
                : _allMed.FirstOrDefault(m => m.ItemID == id)?.Name ?? "";

            StockInItemName.Text = name;
            StockInQtyEntry.Text = "";
            StockInOverlay.Visibility = Visibility.Visible;
        }

        private void OnCloseStockIn(object sender, RoutedEventArgs e)
        {
            StockInOverlay.Visibility = Visibility.Collapsed;
        }

        private void OnConfirmStockIn(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(StockInQtyEntry.Text, out int addQty) || addQty <= 0)
            {
                MessageBox.Show("Invalid quantity."); return;
            }

            int id = _isFoodTab ? _selectedFoodId : _selectedMedId;
            int currentQty = _isFoodTab
                ? _allFood.FirstOrDefault(f => f.ItemID == id)?.Qty ?? 0
                : _allMed.FirstOrDefault(m => m.ItemID == id)?.Qty ?? 0;

            bool success = _inventoryService.UpdateQuantity(id, currentQty + addQty);
            if (success)
            {
                StockInOverlay.Visibility = Visibility.Collapsed;
                LoadData();
            }
        }

        // ── Delete ───────────────────────────────────────────────────

        private void OnDeleteFood(object sender, RoutedEventArgs e) => DoDelete(true);
        private void OnDeleteMed(object sender, RoutedEventArgs e) => DoDelete(false);

        private void DoDelete(bool isFood)
        {
            int id = isFood ? _selectedFoodId : _selectedMedId;
            if (id < 0) { MessageBox.Show("Pumili muna ng item."); return; }

            string name = isFood
                ? _allFood.FirstOrDefault(f => f.ItemID == id)?.Name ?? ""
                : _allMed.FirstOrDefault(m => m.ItemID == id)?.Name ?? "";

            var result = MessageBox.Show($"Delete \"{name}\"? This cannot be undone.",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _inventoryService.DeleteItem(id);
                if (isFood) _selectedFoodId = -1; else _selectedMedId = -1;
                LoadData();
            }
        }

        // ── Navigation ───────────────────────────────────────────────

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            _selectedFoodId = -1;
            _selectedMedId = -1;
            LoadData();
        }

        private void OnAddItem(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as ProjectBReadyWPF.Frontend.Views.MainDashboard.MainWindow;
            if (mainWindow != null)
            {
                var contentArea = mainWindow.FindName("MainContentArea") as ContentControl;
                if (contentArea != null)
                {
                    contentArea.Content = new InventoryAddView();
                }
            }
        }
    }
}