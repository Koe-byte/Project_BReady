using System;
using System.Collections.Generic;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectBReadyWPF.Frontend.Views.MainDashboard
{
    public class ShelterDisplayItem
    {
        public string Name { get; set; } = "";
        public int CurrentOccupancy { get; set; }
        public int MaxCapacity { get; set; }
        public string RawStatus { get; set; } = "Open";

        private string NormalizedStatus
        {
            get
            {
                var value = RawStatus?.Trim() ?? string.Empty;
                if (value.Equals("UnderMaintenance", StringComparison.OrdinalIgnoreCase) ||
                    value.Equals("Under Maintenance", StringComparison.OrdinalIgnoreCase))
                    return "Under Maintenance";
                if (value.Equals("Closed", StringComparison.OrdinalIgnoreCase)) return "Closed";
                if (value.Equals("Full", StringComparison.OrdinalIgnoreCase)) return "Full";
                return "Open";
            }
        }

        public string OccupancyDisplay => $"{CurrentOccupancy} / {MaxCapacity}";
        public double FillPercent => MaxCapacity > 0 ? Math.Min((double)CurrentOccupancy / MaxCapacity, 1.0) : 0;

        public SolidColorBrush FillColor
        {
            get
            {
                double pct = MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 100 : 0;
                if (NormalizedStatus is "Closed" or "Under Maintenance")
                    return new SolidColorBrush(Color.FromRgb(148, 163, 184));
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(239, 68, 68));
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(234, 124, 60));
                return new SolidColorBrush(Color.FromRgb(163, 230, 53));
            }
        }

        public string PctFull
        {
            get
            {
                double pct = MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 100 : 0;
                return $"{pct:F0}%";
            }
        }

        public SolidColorBrush PctColor
        {
            get
            {
                double pct = MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 100 : 0;
                if (NormalizedStatus is "Closed" or "Under Maintenance")
                    return new SolidColorBrush(Color.FromRgb(71, 85, 105));
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(239, 68, 68));
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(234, 124, 60));
                return new SolidColorBrush(Color.FromRgb(22, 163, 74));
            }
        }

        public string Status => NormalizedStatus;

        public SolidColorBrush StatusBadgeBg => Status switch
        {
            "Full" => new SolidColorBrush(Color.FromRgb(254, 226, 226)),
            "Closed" => new SolidColorBrush(Color.FromRgb(226, 232, 240)),
            "Under Maintenance" => new SolidColorBrush(Color.FromRgb(254, 243, 199)),
            _ => new SolidColorBrush(Color.FromRgb(209, 250, 229))
        };

        public SolidColorBrush StatusTextColor => Status switch
        {
            "Full" => new SolidColorBrush(Color.FromRgb(153, 27, 27)),
            "Closed" => new SolidColorBrush(Color.FromRgb(51, 65, 85)),
            "Under Maintenance" => new SolidColorBrush(Color.FromRgb(146, 64, 14)),
            _ => new SolidColorBrush(Color.FromRgb(22, 101, 52))
        };
    }

    public class DispatchDisplayItem
    {
        public string ItemName { get; set; } = "";
        public string Destination { get; set; } = "";
        public int Qty { get; set; }
        public DateTime DispatchDate { get; set; }
        public string DateTimeDisplay => DispatchDate.ToString("MMM dd, yyyy  hh:mm tt");
    }

    public class MainDashboardViewModel
    {
        public List<ShelterDisplayItem> Shelters { get; set; } = new();
        public List<DispatchDisplayItem> RecentDispatches { get; set; } = new();

        public int TotalEvacuees { get; set; }
        public int TotalCapacity { get; set; }
        public int OpenShelters { get; set; }
        public int TotalShelters { get; set; }
        public int ReliefItems { get; set; }
        public int ItemTypes { get; set; }
        public int DispatchCount { get; set; }
        public int ExpiringFoodCount { get; set; }

        public int AvailableSlots => Math.Max(TotalCapacity - TotalEvacuees, 0);
        public string ShelterCountNote => $"of {TotalShelters} total";
        public string CapacityNote => $"of {TotalCapacity} slots";
        public int FullShelterCount { get; set; }

        public MainDashboardViewModel()
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
                ItemTypes = stats.ItemTypes;
                DispatchCount = stats.DispatchCount;
                ExpiringFoodCount = stats.ExpiringFoodCount;

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
                    if ((s.Status ?? string.Empty).Trim().Equals("Full", StringComparison.OrdinalIgnoreCase))
                        fullCount++;
                }
                FullShelterCount = fullCount;

                var dispatchService = App.ServiceProvider.GetRequiredService<IDispatchService>();
                var dbDispatches = dispatchService.GetRecentDispatches(5);

                RecentDispatches = new List<DispatchDisplayItem>();
                foreach (var d in dbDispatches)
                {
                    RecentDispatches.Add(new DispatchDisplayItem
                    {
                        ItemName = d.ItemName ?? "",
                        Destination = d.ShelterName ?? "",
                        Qty = d.QuantityDispatched,
                        DispatchDate = d.DispatchDate
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Dashboard load error: {ex.Message}");
            }
        }
    }
}
