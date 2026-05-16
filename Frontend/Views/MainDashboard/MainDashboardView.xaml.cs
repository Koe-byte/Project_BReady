using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Frontend.Views.MainDashboard
{
    public partial class MainDashboardView : UserControl
    {
        private readonly IRealTimeService _realTimeService;

        public MainDashboardView()
        {
            InitializeComponent();
            _realTimeService = App.ServiceProvider.GetRequiredService<IRealTimeService>();
            RefreshDashboard();

            _realTimeService.OnTableUpdated += RealTime_OnTableUpdated;
            Unloaded += (s, e) => _realTimeService.OnTableUpdated -= RealTime_OnTableUpdated;
        }

        private void RealTime_OnTableUpdated(object? sender, string tableName)
        {
            if (tableName is "shelters" or "inventory_items" or "dispatch_logs")
                RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            var vm = new MainDashboardViewModel();
            DataContext = vm;
            UpdateDonutChart(vm);
            UpdateLabels(vm);
        }

        private void UpdateDonutChart(MainDashboardViewModel vm)
        {
            const double diameter = 132;
            const double strokeThickness = 22;
            double circumference = Math.PI * diameter;

            double pct = vm.OccupancyFillPercent;
            double availablePct = 1.0 - pct;

            double occupiedLength = circumference * pct;
            double availableLength = circumference * availablePct;
            double occupiedGap = circumference - occupiedLength;
            double availableGap = circumference - availableLength;

            OccupiedArc.StrokeDashArray = new DoubleCollection
            {
                occupiedLength / strokeThickness,
                occupiedGap / strokeThickness
            };

            AvailableArc.StrokeDashArray = new DoubleCollection
            {
                availableLength / strokeThickness,
                availableGap / strokeThickness
            };
            AvailableArc.RenderTransform = new RotateTransform(-90 + pct * 360);

            DonutPctLabel.Text = $"{pct * 100:F0}%";
        }

        private void UpdateLabels(MainDashboardViewModel vm)
        {
            int available = Math.Max(vm.TotalCapacity - vm.TotalEvacuees, 0);
            LegendOccupied.Text = vm.TotalEvacuees.ToString();
            LegendAvailable.Text = available.ToString();
            LegendTotal.Text = vm.TotalCapacity.ToString();

            FullSheltersBadge.Text = vm.FullShelterCount == 0
                ? "All shelters open"
                : $"{vm.FullShelterCount} shelter{(vm.FullShelterCount > 1 ? "s" : "")} full";
        }


    }
}
