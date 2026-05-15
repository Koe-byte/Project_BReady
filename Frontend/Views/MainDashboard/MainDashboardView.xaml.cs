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
        private static bool _isDarkMode = true;

        public MainDashboardView()
        {
            InitializeComponent();
            _realTimeService = App.ServiceProvider.GetRequiredService<IRealTimeService>();
            RefreshDashboard();

            _realTimeService.OnTableUpdated += RealTime_OnTableUpdated;
            Unloaded += (s, e) => _realTimeService.OnTableUpdated -= RealTime_OnTableUpdated;

            // Sync button label with current state
            UpdateThemeButton();
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
            double diameter = 160;
            double circumference = Math.PI * diameter;

            double pct = vm.TotalCapacity > 0
                ? (double)vm.TotalEvacuees / vm.TotalCapacity
                : 0;
            pct = Math.Min(pct, 1.0);

            double occupiedLength = circumference * pct;
            double gapLength = circumference - occupiedLength;
            double strokeThickness = 28.0;
            OccupiedArc.StrokeDashArray = new DoubleCollection
            {
                occupiedLength / strokeThickness,
                gapLength / strokeThickness
            };

            DonutPctLabel.Text = $"{pct * 100:F0}%";
        }

        private void UpdateLabels(MainDashboardViewModel vm)
        {
            int available = Math.Max(vm.TotalCapacity - vm.TotalEvacuees, 0);
            LegendOccupied.Text = vm.TotalEvacuees.ToString();
            LegendAvailable.Text = available.ToString();
            LegendTotal.Text = vm.TotalCapacity.ToString();

            int fullCount = 0;
            foreach (var s in vm.Shelters)
            {
                if (s.Status == "Full") fullCount++;
            }
            FullSheltersBadge.Text = fullCount == 0
                ? "All shelters open"
                : $"{fullCount} shelter{(fullCount > 1 ? "s" : "")} full";
        }

        private void OnToggleTheme(object sender, RoutedEventArgs e)
        {
            _isDarkMode = !_isDarkMode;

            var app = Application.Current;
            var mergedDicts = app.Resources.MergedDictionaries;

            // Remove the current theme dictionary (first one)
            if (mergedDicts.Count > 0)
                mergedDicts.RemoveAt(0);

            // Add the appropriate theme
            string themeUri = _isDarkMode
                ? "Frontend/Assets/Styles.xaml"
                : "Frontend/Assets/LightStyles.xaml";

            mergedDicts.Insert(0, new ResourceDictionary
            {
                Source = new Uri(themeUri, UriKind.Relative)
            });

            UpdateThemeButton();
        }

        private void UpdateThemeButton()
        {
            if (_isDarkMode)
            {
                ThemeIcon.Text = "☀️";
                ThemeLabel.Text = "Light Mode";
            }
            else
            {
                ThemeIcon.Text = "🌙";
                ThemeLabel.Text = "Dark Mode";
            }
        }
    }
}
