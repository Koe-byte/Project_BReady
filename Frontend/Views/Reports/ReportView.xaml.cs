using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Frontend.Views.Shelter;
using ProjectBReadyWPF.Frontend.Views.Inventory;

namespace ProjectBReadyWPF.Frontend.Views.Reports
{
    // ── Display Models ────────────────────────────────────────────────────────

    public class SummaryStats
    {
        public int TotalShelters { get; set; }
        public string ShelterNote { get; set; } = "";
        public double OccupancyRate { get; set; }
        public string OccupancyRateDisplay => $"{OccupancyRate:F1}%";
        public string OccupancyNote { get; set; } = "";
        public int ExpiringCount { get; set; }
    }

    public class ShelterTotals
    {
        public int TotalMax { get; set; }
        public int TotalCurrent { get; set; }
        public int TotalAvailable { get; set; }
        public string OverallPct => TotalMax > 0 ? $"{(double)TotalCurrent / TotalMax * 100:F0}%" : "0%";
    }

    // Reuse FoodDisplayItem and MedDisplayItem from Inventory or create local versions tailored for reporting
    public class ReportFoodItem
    {
        public string Name { get; set; } = "";
        public int Qty { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string ExpiryDisplay => ExpirationDate == DateTime.MinValue ? "N/A" : ExpirationDate.ToString("MMM dd, yyyy");
        public int DaysLeft => ExpirationDate == DateTime.MinValue ? 999 : (ExpirationDate - DateTime.Now).Days;

        public SolidColorBrush ExpiryColor => DaysLeft <= 7 ? new SolidColorBrush(Color.FromRgb(220, 38, 38)) : new SolidColorBrush(Color.FromRgb(100, 116, 139));
        
        public string StatusLabel
        {
            get
            {
                if (DaysLeft < 0) return "Expired";
                if (DaysLeft <= 30) return "Expiring";
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

        public SolidColorBrush DaysBadgeBg => DaysLeft <= 30 ? new SolidColorBrush(Color.FromRgb(254, 226, 226)) : new SolidColorBrush(Color.FromRgb(241, 245, 249));
        public SolidColorBrush DaysBadgeText => DaysLeft <= 30 ? new SolidColorBrush(Color.FromRgb(153, 27, 27)) : new SolidColorBrush(Color.FromRgb(71, 85, 105));
    }

    public class ReportMedItem
    {
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

    public class ReportViewModel
    {
        public SummaryStats Summary { get; set; } = new();
        public List<ShelterRowItem> ShelterRows { get; set; } = new();
        public ShelterTotals ShelterTotals { get; set; } = new();
        
        public List<ReportFoodItem> FoodReportRows { get; set; } = new();
        public int FoodTotal { get; set; }
        
        public List<ReportMedItem> MedReportRows { get; set; } = new();
        public int MedTotal { get; set; }
    }

    // ── View ───────────────────────────────────────────────────────────────

    public partial class ReportView : UserControl
    {
        private readonly DashboardService _dashboardService;
        private readonly ShelterService _shelterService;
        private readonly InventoryService _inventoryService;

        public ReportView()
        {
            InitializeComponent();
            _dashboardService = new DashboardService();
            _shelterService = new ShelterService();
            _inventoryService = new InventoryService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var shelters = _shelterService.GetAllShelters();
                var food = _inventoryService.GetFoodItems();
                var med = _inventoryService.GetMedicalSupplies();

                // 1. Shelter data
                var shelterRows = shelters.Select((s, i) => new ShelterRowItem
                {
                    RowNumber = i + 1,
                    ShelterID = s.ShelterID,
                    Name = s.ShelterName,
                    MaxCapacity = s.MaxCapacity,
                    CurrentOccupancy = s.CurrentOccupancy
                }).ToList();

                var sTotals = new ShelterTotals
                {
                    TotalMax = shelters.Sum(s => s.MaxCapacity),
                    TotalCurrent = shelters.Sum(s => s.CurrentOccupancy),
                    TotalAvailable = shelters.Sum(s => s.MaxCapacity - s.CurrentOccupancy)
                };

                // 2. Food data
                var foodRows = food.Select(f => new ReportFoodItem
                {
                    Name = f.ItemName,
                    Qty = f.Quantity,
                    ExpirationDate = f.ExpirationDate
                }).OrderBy(f => f.DaysLeft).ToList();

                // 3. Med data
                var medRows = med.Select(m => new ReportMedItem
                {
                    Name = m.ItemName,
                    Qty = m.Quantity,
                    Dosage = m.Dosage,
                    IsPrescriptionRequired = m.IsPrescriptionRequired
                }).ToList();

                // 4. Summary Stats
                var expiringCount = foodRows.Count(f => f.DaysLeft <= 30);
                var summary = new SummaryStats
                {
                    TotalShelters = shelters.Count,
                    ShelterNote = $"{shelters.Count(s => s.Status == "Full")} at full capacity",
                    OccupancyRate = sTotals.TotalMax > 0 ? (double)sTotals.TotalCurrent / sTotals.TotalMax * 100 : 0,
                    OccupancyNote = $"Across all centers",
                    ExpiringCount = expiringCount
                };

                // Binding
                DataContext = new ReportViewModel
                {
                    Summary = summary,
                    ShelterRows = shelterRows,
                    ShelterTotals = sTotals,
                    FoodReportRows = foodRows,
                    FoodTotal = foodRows.Sum(f => f.Qty),
                    MedReportRows = medRows,
                    MedTotal = medRows.Sum(m => m.Qty)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating reports: {ex.Message}");
            }
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void OnPrint(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Printing is not implemented yet.", "Print Report");
        }

        private void OnExportCsv(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CSV Export is not implemented yet.", "Export Data");
        }

        private void OnViewInventory(object sender, RoutedEventArgs e)
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
