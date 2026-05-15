using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Frontend.Views.MainDashboard;

namespace ProjectBReadyWPF.Frontend.Views.AdminDashboard
{
    public class AlertDisplayItem
    {
        public string Message { get; set; } = "";
        public string Severity { get; set; } = "Warning";
        public SolidColorBrush IconColor => Severity switch
        {
            "Critical" => new SolidColorBrush(Color.FromRgb(239, 68, 68)),
            "Warning" => new SolidColorBrush(Color.FromRgb(245, 158, 11)),
            _ => new SolidColorBrush(Color.FromRgb(37, 99, 235))
        };
    }

    public class InventoryLegendItem
    {
        public string Label { get; set; } = "";
        public int Quantity { get; set; }
        public string ColorHex { get; set; } = "#2563EB";
        public SolidColorBrush SwatchBrush
        {
            get
            {
                var color = (Color)ColorConverter.ConvertFromString(ColorHex);
                return new SolidColorBrush(color);
            }
        }
        public string QuantityDisplay => Quantity.ToString("N0");
    }

    public class AdminDashboardViewModel
    {
        public List<ShelterDisplayItem> Shelters { get; set; } = new();
        public List<AlertDisplayItem> Alerts { get; set; } = new();
        public List<InventoryLegendItem> InventoryLegend { get; set; } = new();

        public List<double> LineChartTrend { get; set; } = new();
        public List<double> OpenSheltersTrend { get; set; } = new();
        public List<double> EvacueeTrend { get; set; } = new();
        public List<double> AvailableBedsTrend { get; set; } = new();
        public List<double> OccupancyTrend { get; set; } = new();
        public List<double> ReliefTrend { get; set; } = new();
        public List<double> FoodTrend { get; set; } = new();
        public List<double> MedicalTrend { get; set; } = new();

        public int TotalEvacuees { get; set; }
        public int TotalCapacity { get; set; }
        public int OpenShelters { get; set; }
        public int TotalShelters { get; set; }
        public int ReliefItems { get; set; }
        public int DispatchCount { get; set; }
        public int FullShelterCount { get; set; }

        public int AvailableSlots => Math.Max(TotalCapacity - TotalEvacuees, 0);
        public string OccupancyPercentDisplay => $"{OccupancyPercent:F0}% full";
        public double OccupancyPercent { get; set; }

        public string FoodSupplyStatus { get; set; } = "ADEQUATE";
        public string MedicalSupplyStatus { get; set; } = "ADEQUATE";
        public SolidColorBrush FoodStatusColor => FoodSupplyStatus switch
        {
            "CRITICAL" => new SolidColorBrush(Color.FromRgb(239, 68, 68)),
            "LOW" => new SolidColorBrush(Color.FromRgb(245, 158, 11)),
            _ => new SolidColorBrush(Color.FromRgb(15, 23, 42))
        };
        public SolidColorBrush MedicalStatusColor => MedicalSupplyStatus switch
        {
            "CRITICAL" => new SolidColorBrush(Color.FromRgb(239, 68, 68)),
            "LOW" => new SolidColorBrush(Color.FromRgb(245, 158, 11)),
            _ => new SolidColorBrush(Color.FromRgb(15, 23, 42))
        };

        public string EvacueesDisplay => TotalEvacuees.ToString("N0");
        public string ReliefItemsDisplay => ReliefItems.ToString("N0");
        public string AvailableBedsDisplay => AvailableSlots.ToString("N0");

        public AdminDashboardViewModel()
        {
            LoadFromDatabase();
        }

        private void LoadFromDatabase()
        {
            try
            {
                var dashService = App.ServiceProvider.GetRequiredService<IDashboardService>();
                var stats = dashService.GetDashboardStats();

                TotalEvacuees = stats.TotalEvacuees;
                TotalCapacity = stats.TotalCapacity;
                OpenShelters = stats.OpenShelters;
                TotalShelters = stats.TotalShelters;
                ReliefItems = stats.TotalReliefItems;
                DispatchCount = stats.DispatchCount;
                OccupancyPercent = stats.OccupancyPercent;
                FoodSupplyStatus = stats.FoodSupplyStatus;
                MedicalSupplyStatus = stats.MedicalSupplyStatus;

                Alerts = new List<AlertDisplayItem>();
                foreach (var a in stats.Alerts)
                    Alerts.Add(new AlertDisplayItem { Message = a.Message, Severity = a.Severity });

                InventoryLegend = new List<InventoryLegendItem>();
                foreach (var slice in stats.InventoryBreakdown)
                {
                    InventoryLegend.Add(new InventoryLegendItem
                    {
                        Label = slice.Label,
                        Quantity = slice.Quantity,
                        ColorHex = slice.ColorHex
                    });
                }

                var shelterService = App.ServiceProvider.GetRequiredService<IShelterService>();
                var dbShelters = shelterService.GetAllShelters();

                Shelters = new List<ShelterDisplayItem>();
                int fullCount = 0;
                foreach (var s in dbShelters)
                {
                    Shelters.Add(new ShelterDisplayItem
                    {
                        Name = s.ShelterName,
                        CurrentOccupancy = s.CurrentOccupancy,
                        MaxCapacity = s.MaxCapacity,
                        RawStatus = s.Status ?? "Open"
                    });

                    if ((s.Status ?? "").Trim().Equals("Full", StringComparison.OrdinalIgnoreCase))
                        fullCount++;

                    if (s.MaxCapacity > 0 && s.CurrentOccupancy >= s.MaxCapacity)
                    {
                        Alerts.Insert(0, new AlertDisplayItem
                        {
                            Severity = "Critical",
                            Message = $"{s.ShelterName}: Over capacity ({s.CurrentOccupancy}/{s.MaxCapacity} evacuees)."
                        });
                    }
                    else if (s.MaxCapacity > 0 && (double)s.CurrentOccupancy / s.MaxCapacity >= 0.9)
                    {
                        Alerts.Add(new AlertDisplayItem
                        {
                            Severity = "Warning",
                            Message = $"{s.ShelterName}: Nearing capacity ({s.CurrentOccupancy}/{s.MaxCapacity})."
                        });
                    }
                }
                FullShelterCount = fullCount;

                var tracker = App.ServiceProvider.GetRequiredService<IDashboardTrendTracker>();
                tracker.RecordSnapshot(
                    OccupancyPercent,
                    ReliefItems,
                    stats.TotalFoodQuantity,
                    stats.TotalMedicalQuantity,
                    TotalEvacuees,
                    AvailableSlots,
                    OpenShelters);

                OccupancyTrend = tracker.GetOccupancyTrend().ToList();
                ReliefTrend = tracker.GetReliefTrend().ToList();
                FoodTrend = tracker.GetFoodTrend().ToList();
                MedicalTrend = tracker.GetMedicalTrend().ToList();
                EvacueeTrend = tracker.GetEvacueeTrend().ToList();
                AvailableBedsTrend = tracker.GetAvailableBedsTrend().ToList();
                OpenSheltersTrend = tracker.GetOpenSheltersTrend().ToList();
                LineChartTrend = BuildLineChartTrend(Shelters, OccupancyTrend);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Admin dashboard load error: {ex.Message}");
            }
        }

        private static List<double> BuildLineChartTrend(List<ShelterDisplayItem> shelters, List<double> sessionTrend)
        {
            var perShelter = shelters
                .Where(s => s.MaxCapacity > 0)
                .Select(s => (double)s.CurrentOccupancy / s.MaxCapacity * 100.0)
                .OrderByDescending(p => p)
                .Take(7)
                .ToList();

            if (perShelter.Count >= 2)
                return perShelter;

            return sessionTrend.Count >= 2
                ? sessionTrend
                : new List<double> { sessionTrend.FirstOrDefault(), sessionTrend.FirstOrDefault() };
        }
    }
}
