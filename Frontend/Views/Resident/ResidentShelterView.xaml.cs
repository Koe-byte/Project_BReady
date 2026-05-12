using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Frontend.Views.Resident
{
    // ── Display model for shelter cards ──────────────────────────────
    public class ResidentShelterItem
    {
        public string Name { get; set; } = "";
        public int CurrentOccupancy { get; set; }
        public int MaxCapacity { get; set; }

        public string OccupancyDisplay => $"{CurrentOccupancy} / {MaxCapacity}";
        
        // Progress bar fill (0.0 to 1.0)
        public double FillPercent => MaxCapacity > 0 ? Math.Min((double)CurrentOccupancy / MaxCapacity, 1.0) : 0;
        
        // Legacy fixed-width (kept for reference)
        public double BarWidth => MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 300 : 0;
        
        public SolidColorBrush FillColor
        {
            get
            {
                double pct = MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 100 : 0;
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(239, 68, 68));   // Red
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(234, 124, 60));  // Orange
                return new SolidColorBrush(Color.FromRgb(163, 230, 53));                  // Green
            }
        }

        public string PctFull => MaxCapacity > 0 ? $"{(double)CurrentOccupancy / MaxCapacity * 100:F0}% Full" : "0%";
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

        public string Status { get; set; } = "Open"; // From DB
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

    // ── ViewModel ────────────────────────────────────────────────────
    public class ResidentShelterViewModel
    {
        public List<ResidentShelterItem> Shelters { get; set; } = new();
        public int TotalShelters { get; set; }
        public int TotalEvacuees { get; set; }
        public int AvailableSlots { get; set; }
    }

    // ── View ─────────────────────────────────────────────────────────
    public partial class ResidentShelterView : UserControl
    {
        private readonly IRealTimeService _realTimeService;

        public ResidentShelterView()
        {
            InitializeComponent();
            _realTimeService = App.ServiceProvider.GetRequiredService<IRealTimeService>();
            LoadData();

            // Subscribe to real-time database events
            _realTimeService.OnTableUpdated += RealTime_OnTableUpdated;
            this.Unloaded += (s, e) => _realTimeService.OnTableUpdated -= RealTime_OnTableUpdated;
        }

        private void RealTime_OnTableUpdated(object? sender, string tableName)
        {
            if (tableName == "shelters")
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                var shelterService = App.ServiceProvider.GetRequiredService<IShelterService>();
                var dbShelters = shelterService.GetAllShelters();

                var items = new List<ResidentShelterItem>();
                int totalOcc = 0;
                int totalCap = 0;

                foreach (var s in dbShelters)
                {
                    items.Add(new ResidentShelterItem
                    {
                        Name = s.ShelterName,
                        MaxCapacity = s.MaxCapacity,
                        CurrentOccupancy = s.CurrentOccupancy,
                        Status = s.Status
                    });

                    totalOcc += s.CurrentOccupancy;
                    totalCap += s.MaxCapacity;
                }

                DataContext = new ResidentShelterViewModel
                {
                    Shelters = items,
                    TotalShelters = items.Count,
                    TotalEvacuees = totalOcc,
                    AvailableSlots = totalCap - totalOcc
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ResidentShelterView load error: {ex.Message}");
            }
        }
    }
}
