using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectBReadyWPF.Frontend.Views.MainDashboard
{
    public partial class MainDashboardView : UserControl
    {
        public MainDashboardView()
        {
            InitializeComponent();
            var vm = new MainDashboardViewModel();
            DataContext = vm;
            UpdateDonutChart(vm);
            UpdateLabels(vm);
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            var vm = new MainDashboardViewModel();
            DataContext = vm;
            UpdateDonutChart(vm);
            UpdateLabels(vm);
        }

        private void UpdateDonutChart(MainDashboardViewModel vm)
        {
            // Calculate the StrokeDashArray for the occupied arc
            // The circumference of the ellipse (approx circle): C = π * diameter
            double diameter = 160;
            double circumference = Math.PI * diameter;

            double pct = vm.TotalCapacity > 0
                ? (double)vm.TotalEvacuees / vm.TotalCapacity
                : 0;

            pct = Math.Min(pct, 1.0); // Clamp to 100%

            double occupiedLength = circumference * pct;
            double gapLength = circumference - occupiedLength;

            // StrokeDashArray values are in multiples of StrokeThickness
            double strokeThickness = 28.0;
            double dashOccupied = occupiedLength / strokeThickness;
            double dashGap = gapLength / strokeThickness;
            OccupiedArc.StrokeDashArray = new DoubleCollection { dashOccupied, dashGap };

            // Update center label
            DonutPctLabel.Text = $"{pct * 100:F0}%";
        }

        private void UpdateLabels(MainDashboardViewModel vm)
        {
            int available = vm.TotalCapacity - vm.TotalEvacuees;
            if (available < 0) available = 0;

            LegendOccupied.Text = vm.TotalEvacuees.ToString();
            LegendAvailable.Text = available.ToString();
            LegendTotal.Text = vm.TotalCapacity.ToString();

            int fullCount = 0;
            foreach (var s in vm.Shelters)
            {
                if (s.CurrentOccupancy >= s.MaxCapacity) fullCount++;
            }
            FullSheltersBadge.Text = fullCount == 0
                ? "All shelters open"
                : $"{fullCount} shelter{(fullCount > 1 ? "s" : "")} full";
        }
    }
}
