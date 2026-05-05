using System;
using System.Collections.Generic;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Backend.Models.Facilities;

namespace ProjectBReadyWPF.Frontend.Views.MainDashboard
{
    // ── Helper classes para sa Dashboard bindings ──────────────────────

    public class ShelterDisplayItem
    {
        public string Name { get; set; } = "";
        public int CurrentOccupancy { get; set; }
        public int MaxCapacity { get; set; }

        // Bar chart
        public string OccupancyDisplay => $"{CurrentOccupancy} / {MaxCapacity}";
        public double BarWidth => MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 280 : 0;
        public SolidColorBrush FillColor
        {
            get
            {
                double pct = MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 100 : 0;
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(239, 68, 68));
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(234, 124, 60));
                return new SolidColorBrush(Color.FromRgb(20, 184, 166));
            }
        }

        // Table
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
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(239, 68, 68));
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(234, 124, 60));
                return new SolidColorBrush(Color.FromRgb(22, 163, 74));
            }
        }
        public string Status
        {
            get
            {
                double pct = MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 100 : 0;
                if (pct >= 100) return "Full";
                return "Open";
            }
        }
        public SolidColorBrush StatusBadgeBg => Status == "Full"
            ? new SolidColorBrush(Color.FromRgb(254, 226, 226))
            : new SolidColorBrush(Color.FromRgb(209, 250, 229));
        public SolidColorBrush StatusTextColor => Status == "Full"
            ? new SolidColorBrush(Color.FromRgb(153, 27, 27))
            : new SolidColorBrush(Color.FromRgb(22, 101, 52));
    }

    public class DispatchDisplayItem
    {
        public string ItemName { get; set; } = "";
        public string Destination { get; set; } = "";
        public int Qty { get; set; }
        public DateTime DispatchDate { get; set; }
        public string DateTimeDisplay => DispatchDate.ToString("MMM dd, yyyy  hh:mm tt");
    }

    // ── Main ViewModel — Now wired to REAL services ───────────────────

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

        public MainDashboardViewModel()
        {
            LoadFromDatabase();
        }

        private void LoadFromDatabase()
        {
            try
            {
                // ── Dashboard Stats (aggregated counts) ──
                var dashService = new DashboardService();
                var stats = dashService.GetDashboardStats();

                TotalEvacuees = stats.TotalEvacuees;
                TotalCapacity = stats.TotalCapacity;
                OpenShelters = stats.OpenShelters;
                TotalShelters = stats.TotalShelters;
                ReliefItems = stats.TotalReliefItems;
                ItemTypes = stats.ItemTypes;
                DispatchCount = stats.DispatchCount;
                ExpiringFoodCount = stats.ExpiringFoodCount;

                // ── Shelter list (for bar chart + table) ──
                var shelterService = new ShelterService();
                var dbShelters = shelterService.GetAllShelters();

                Shelters = new List<ShelterDisplayItem>();
                foreach (var s in dbShelters)
                {
                    Shelters.Add(new ShelterDisplayItem
                    {
                        Name = s.ShelterName,
                        CurrentOccupancy = s.CurrentOccupancy,
                        MaxCapacity = s.MaxCapacity
                    });
                }

                // ── Recent Dispatches (for table) ──
                var dispatchService = new DispatchService();
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
                // Kung walang DB connection, fallback sa empty lists
                System.Diagnostics.Debug.WriteLine($"Dashboard load error: {ex.Message}");
            }
        }
    }
}
