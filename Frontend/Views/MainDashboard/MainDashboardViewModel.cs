using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Interfaces;
using ProjectBReadyWPF.Backend.Services;
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
                    return new SolidColorBrush(Color.FromRgb(75, 85, 99));
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(248, 113, 113));
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(251, 191, 36));
                return new SolidColorBrush(Color.FromRgb(74, 222, 128));
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
                    return new SolidColorBrush(Color.FromRgb(148, 163, 184));
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(248, 113, 113));
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(251, 191, 36));
                return new SolidColorBrush(Color.FromRgb(74, 222, 128));
            }
        }

        public string Status => NormalizedStatus;

        public SolidColorBrush StatusBadgeBg => Status switch
        {
            "Full" => new SolidColorBrush(Color.FromRgb(69, 10, 10)),
            "Closed" => new SolidColorBrush(Color.FromRgb(30, 41, 59)),
            "Under Maintenance" => new SolidColorBrush(Color.FromRgb(66, 32, 6)),
            _ => new SolidColorBrush(Color.FromRgb(20, 83, 45))
        };

        public SolidColorBrush StatusTextColor => Status switch
        {
            "Full" => new SolidColorBrush(Color.FromRgb(252, 165, 165)),
            "Closed" => new SolidColorBrush(Color.FromRgb(203, 213, 225)),
            "Under Maintenance" => new SolidColorBrush(Color.FromRgb(253, 224, 71)),
            _ => new SolidColorBrush(Color.FromRgb(134, 239, 172))
        };
    }

    public class DispatchDisplayItem
    {
        public string ItemName { get; set; } = "";
        public string Destination { get; set; } = "";
        public int Qty { get; set; }
        public DateTime DispatchDate { get; set; }
        public string DateTimeDisplay => DispatchDate.ToString("MMM d, h:mm tt");
        public string TitleLine => string.IsNullOrWhiteSpace(ItemName) ? "Relief dispatch" : ItemName;
        public string DetailLine => Qty > 0
            ? $"{Qty:N0} units → {Destination}"
            : $"Delivered to {Destination}";
    }

    public class AlertDisplayItem
    {
        public string Message { get; set; } = "";
        public string Severity { get; set; } = "Warning";
        public SolidColorBrush IconColor => Severity switch
        {
            "Critical" => new SolidColorBrush(Color.FromRgb(248, 113, 113)),
            "Warning" => new SolidColorBrush(Color.FromRgb(252, 211, 77)),
            _ => new SolidColorBrush(Color.FromRgb(74, 222, 128))
        };
    }

    public class MainDashboardViewModel
    {
        public List<ShelterDisplayItem> Shelters { get; set; } = new();
        public List<DispatchDisplayItem> RecentDispatches { get; set; } = new();
        public List<AlertDisplayItem> Alerts { get; set; } = new();

        public int TotalEvacuees { get; set; }
        public int TotalCapacity { get; set; }
        public int OpenShelters { get; set; }
        public int TotalShelters { get; set; }
        public int ReliefItems { get; set; }
        public int ItemTypes { get; set; }
        public int DispatchCount { get; set; }
        public int ExpiringFoodCount { get; set; }
        public double OccupancyPercent { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public string LastUpdatedDisplay => $"Updated {LastUpdated:h:mm tt}";
        public bool HasRecentDispatches => RecentDispatches.Count > 0;
        public bool HasLoadError { get; set; }
        public string LoadErrorMessage { get; set; } = "";
        public bool HasUrgentAlerts { get; set; }

        public int AvailableSlots => Math.Max(TotalCapacity - TotalEvacuees, 0);
        public string ShelterCountNote => $"of {TotalShelters} total";
        public string CapacityNote => $"of {TotalCapacity} slots";
        public int FullShelterCount { get; set; }

        public double OccupancyFillPercent => TotalCapacity > 0
            ? Math.Min((double)TotalEvacuees / TotalCapacity, 1.0)
            : 0;

        public string OccupancyHeadline => OccupancyPercent >= 90
            ? "Near capacity"
            : OccupancyPercent >= 70
                ? "Elevated occupancy"
                : "Healthy capacity";

        public string OccupancySummary =>
            $"{TotalEvacuees:N0} occupied · {AvailableSlots:N0} available";

        public string ShelterStatusLine =>
            $"{OpenShelters} open · {FullShelterCount} full · {TotalShelters} total";

        public SolidColorBrush OccupancyHeadlineColor => OccupancyPercent switch
        {
            >= 90 => new SolidColorBrush(Color.FromRgb(248, 113, 113)),
            >= 70 => new SolidColorBrush(Color.FromRgb(251, 191, 36)),
            _ => new SolidColorBrush(Color.FromRgb(74, 222, 128))
        };

        public MainDashboardViewModel()
        {
            LoadFromDatabase();
        }

        private void LoadFromDatabase()
        {
            HasLoadError = false;
            LoadErrorMessage = "";

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
                OccupancyPercent = stats.OccupancyPercent;
                LastUpdated = DateTime.Now;

                Alerts = BuildResidentAlerts(stats.Alerts);
                HasUrgentAlerts = Alerts.Any(a => a.Severity is "Critical" or "Warning");

                var shelterService = App.ServiceProvider.GetRequiredService<IShelterService>();
                var dbShelters = shelterService.GetAllShelters();

                var shelterList = new List<ShelterDisplayItem>();
                int fullCount = 0;
                foreach (var s in dbShelters)
                {
                    shelterList.Add(new ShelterDisplayItem
                    {
                        Name = s.ShelterName,
                        CurrentOccupancy = s.CurrentOccupancy,
                        MaxCapacity = s.MaxCapacity,
                        RawStatus = s.Status ?? "Open"
                    });
                    if ((s.Status ?? string.Empty).Trim().Equals("Full", StringComparison.OrdinalIgnoreCase))
                        fullCount++;
                }
                Shelters = shelterList
                    .OrderByDescending(s => s.FillPercent)
                    .ThenBy(s => s.Name)
                    .ToList();
                FullShelterCount = fullCount;

                var dispatchService = App.ServiceProvider.GetRequiredService<IDispatchService>();
                var dbDispatches = dispatchService.GetRecentDispatches(5);

                RecentDispatches = dbDispatches
                    .Select(d => new DispatchDisplayItem
                    {
                        ItemName = d.ItemName ?? "",
                        Destination = d.ShelterName ?? "",
                        Qty = d.QuantityDispatched,
                        DispatchDate = d.DispatchDate
                    })
                    .OrderByDescending(d => d.DispatchDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                HasLoadError = true;
                LoadErrorMessage = "Unable to load live data. Check your database connection and try again.";
                System.Diagnostics.Debug.WriteLine($"Dashboard load error: {ex.Message}");
            }
        }

        private static List<AlertDisplayItem> BuildResidentAlerts(
            IReadOnlyList<DashboardAlert> source)
        {
            var urgent = source
                .Where(a => a.Severity is "Critical" or "Warning")
                .Select(a => new AlertDisplayItem { Message = a.Message, Severity = a.Severity })
                .Take(3)
                .ToList();

            if (urgent.Count > 0)
                return urgent;

            var info = source.FirstOrDefault(a => a.Severity == "Info");
            return new List<AlertDisplayItem>
            {
                new()
                {
                    Severity = "Info",
                    Message = info?.Message ?? "No critical alerts. All monitored systems within normal thresholds."
                }
            };
        }
    }
}
