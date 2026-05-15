using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Frontend.Views.Shelter;
using ProjectBReadyWPF.Frontend.Views.Inventory;
using ProjectBReadyWPF.Backend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows.Documents;

namespace ProjectBReadyWPF.Frontend.Views.Reports
{
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

    public partial class ReportView : UserControl
    {
        private readonly IDashboardService _dashboardService;
        private readonly IShelterService _shelterService;
        private readonly IInventoryService _inventoryService;

        public ReportView()
        {
            InitializeComponent();
            _dashboardService = App.ServiceProvider.GetRequiredService<IDashboardService>();
            _shelterService = App.ServiceProvider.GetRequiredService<IShelterService>();
            _inventoryService = App.ServiceProvider.GetRequiredService<IInventoryService>();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var shelters = _shelterService.GetAllShelters();
                var food = _inventoryService.GetFoodItems();
                var med = _inventoryService.GetMedicalSupplies();

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

                var foodRows = food.Select(f => new ReportFoodItem
                {
                    Name = f.ItemName,
                    Qty = f.Quantity,
                    ExpirationDate = f.ExpirationDate
                }).OrderBy(f => f.DaysLeft).ToList();

                var medRows = med.Select(m => new ReportMedItem
                {
                    Name = m.ItemName,
                    Qty = m.Quantity,
                    Dosage = m.Dosage,
                    IsPrescriptionRequired = m.IsPrescriptionRequired
                }).ToList();

                var expiringCount = foodRows.Count(f => f.DaysLeft <= 30);
                var summary = new SummaryStats
                {
                    TotalShelters = shelters.Count,
                    ShelterNote = $"{shelters.Count(s => s.Status == "Full")} at full capacity",
                    OccupancyRate = sTotals.TotalMax > 0 ? (double)sTotals.TotalCurrent / sTotals.TotalMax * 100 : 0,
                    OccupancyNote = "Across all centers",
                    ExpiringCount = expiringCount
                };

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

        private void OnExportCsv(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ReportViewModel;
            if (vm == null) return;

            var saveDialog = new SaveFileDialog
            {
                FileName = $"BReady_Report_{DateTime.Now:yyyy-MM-dd}",
                DefaultExt = ".csv",
                Filter = "CSV Files (*.csv)|*.csv"
            };

            if (saveDialog.ShowDialog() != true) return;

            var sb = new StringBuilder();

            sb.AppendLine("SHELTER STATUS REPORT");
            sb.AppendLine($"Generated: {DateTime.Now:MMMM dd, yyyy hh:mm tt}");
            sb.AppendLine();
            sb.AppendLine("#,Shelter Name,Max Capacity,Current Occupancy,Available,% Full");
            foreach (var s in vm.ShelterRows)
            {
                int available = s.MaxCapacity - s.CurrentOccupancy;
                double pct = s.MaxCapacity > 0 ? (double)s.CurrentOccupancy / s.MaxCapacity * 100 : 0;
                sb.AppendLine($"{s.RowNumber},{s.Name},{s.MaxCapacity},{s.CurrentOccupancy},{available},{pct:F0}%");
            }
            sb.AppendLine($"TOTAL,,{vm.ShelterTotals.TotalMax},{vm.ShelterTotals.TotalCurrent},{vm.ShelterTotals.TotalAvailable},{vm.ShelterTotals.OverallPct}");
            sb.AppendLine();

            sb.AppendLine("FOOD INVENTORY");
            sb.AppendLine("Item Name,Quantity,Expiration Date,Status");
            foreach (var f in vm.FoodReportRows)
                sb.AppendLine($"{f.Name},{f.Qty},{f.ExpiryDisplay},{f.StatusLabel}");
            sb.AppendLine($"TOTAL,,{vm.FoodTotal}");
            sb.AppendLine();

            sb.AppendLine("MEDICAL SUPPLIES");
            sb.AppendLine("Item Name,Quantity,Dosage,Prescription Required");
            foreach (var m in vm.MedReportRows)
                sb.AppendLine($"{m.Name},{m.Qty},{m.Dosage},{(m.IsPrescriptionRequired ? "Yes" : "No")}");
            sb.AppendLine($"TOTAL,,{vm.MedTotal}");

            File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show($"Report exported successfully!\n\n{saveDialog.FileName}",
                "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnPrint(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ReportViewModel;
            if (vm == null) return;

            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() != true) return;

            var doc = new FlowDocument
            {
                FontFamily = new FontFamily("Calibri"),
                FontSize = 12,
                PagePadding = new Thickness(60),
                ColumnWidth = double.MaxValue
            };

            doc.Blocks.Add(new Paragraph(new Run("PROJECT B-READY — SITUATIONAL REPORT"))
            { FontSize = 18, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            doc.Blocks.Add(new Paragraph(new Run($"Generated: {DateTime.Now:MMMM dd, yyyy  hh:mm tt}"))
            { FontSize = 10, TextAlignment = TextAlignment.Center, Foreground = Brushes.Gray });
            doc.Blocks.Add(new Paragraph());

            // Shelter Section
            doc.Blocks.Add(new Paragraph(new Run("SHELTER STATUS"))
            { FontSize = 14, FontWeight = FontWeights.Bold });

            var shelterTable = new Table();
            shelterTable.Columns.Add(new TableColumn { Width = new GridLength(30) });
            shelterTable.Columns.Add(new TableColumn { Width = new GridLength(180) });
            shelterTable.Columns.Add(new TableColumn { Width = new GridLength(80) });
            shelterTable.Columns.Add(new TableColumn { Width = new GridLength(80) });
            shelterTable.Columns.Add(new TableColumn { Width = new GridLength(80) });

            var shelterGroup = new TableRowGroup();
            var shelterHeader = new TableRow { Background = Brushes.LightGray };
            foreach (var h in new[] { "#", "Shelter Name", "Max Cap.", "Occupancy", "Available" })
                shelterHeader.Cells.Add(new TableCell(new Paragraph(new Run(h)) { FontWeight = FontWeights.Bold }));
            shelterGroup.Rows.Add(shelterHeader);

            foreach (var s in vm.ShelterRows)
            {
                var row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(s.RowNumber.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(s.Name))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(s.MaxCapacity.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(s.CurrentOccupancy.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run((s.MaxCapacity - s.CurrentOccupancy).ToString()))));
                shelterGroup.Rows.Add(row);
            }
            shelterTable.RowGroups.Add(shelterGroup);
            doc.Blocks.Add(shelterTable);
            doc.Blocks.Add(new Paragraph());

            // Food Section
            doc.Blocks.Add(new Paragraph(new Run("FOOD INVENTORY"))
            { FontSize = 14, FontWeight = FontWeights.Bold });

            var foodTable = new Table();
            foodTable.Columns.Add(new TableColumn { Width = new GridLength(200) });
            foodTable.Columns.Add(new TableColumn { Width = new GridLength(80) });
            foodTable.Columns.Add(new TableColumn { Width = new GridLength(120) });
            foodTable.Columns.Add(new TableColumn { Width = new GridLength(80) });

            var foodGroup = new TableRowGroup();
            var foodHeader = new TableRow { Background = Brushes.LightGray };
            foreach (var h in new[] { "Item Name", "Quantity", "Expiration Date", "Status" })
                foodHeader.Cells.Add(new TableCell(new Paragraph(new Run(h)) { FontWeight = FontWeights.Bold }));
            foodGroup.Rows.Add(foodHeader);

            foreach (var f in vm.FoodReportRows)
            {
                var row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(f.Name))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(f.Qty.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(f.ExpiryDisplay))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(f.StatusLabel))));
                foodGroup.Rows.Add(row);
            }
            foodTable.RowGroups.Add(foodGroup);
            doc.Blocks.Add(foodTable);
            doc.Blocks.Add(new Paragraph());

            // Medical Section
            doc.Blocks.Add(new Paragraph(new Run("MEDICAL SUPPLIES"))
            { FontSize = 14, FontWeight = FontWeights.Bold });

            var medTable = new Table();
            medTable.Columns.Add(new TableColumn { Width = new GridLength(200) });
            medTable.Columns.Add(new TableColumn { Width = new GridLength(80) });
            medTable.Columns.Add(new TableColumn { Width = new GridLength(120) });
            medTable.Columns.Add(new TableColumn { Width = new GridLength(80) });

            var medGroup = new TableRowGroup();
            var medHeader = new TableRow { Background = Brushes.LightGray };
            foreach (var h in new[] { "Item Name", "Quantity", "Dosage", "Prescription" })
                medHeader.Cells.Add(new TableCell(new Paragraph(new Run(h)) { FontWeight = FontWeights.Bold }));
            medGroup.Rows.Add(medHeader);

            foreach (var m in vm.MedReportRows)
            {
                var row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(m.Name))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(m.Qty.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(m.Dosage))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(m.IsPrescriptionRequired ? "Yes" : "No"))));
                medGroup.Rows.Add(row);
            }
            medTable.RowGroups.Add(medGroup);
            doc.Blocks.Add(medTable);

            var paginator = ((IDocumentPaginatorSource)doc).DocumentPaginator;
            paginator.PageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            printDialog.PrintDocument(paginator, "B-Ready Situational Report");
        }

        private void OnViewInventory(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this);
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