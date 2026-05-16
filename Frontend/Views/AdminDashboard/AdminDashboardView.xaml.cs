using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Backend.Interfaces;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Frontend.Views.Admin;

namespace ProjectBReadyWPF.Frontend.Views.AdminDashboard
{
    public partial class AdminDashboardView : UserControl
    {
        private readonly IRealTimeService _realTimeService;
        private DispatcherTimer? _clockTimer;
        private DispatcherTimer? _pollTimer;
        private AdminDashboardViewModel? _viewModel;
        private bool _chartsPending;

        public AdminDashboardView()
        {
            InitializeComponent();
            _realTimeService = App.ServiceProvider.GetRequiredService<IRealTimeService>();
            RefreshDashboard();

            _realTimeService.OnTableUpdated += RealTime_OnTableUpdated;
            Unloaded += OnUnloaded;
        }

        public void RefreshNow() => RefreshDashboard();

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _realTimeService.OnTableUpdated -= RealTime_OnTableUpdated;
            _clockTimer?.Stop();
            _pollTimer?.Stop();
        }

        private void AdminDashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            StartClock();
            StartPolling();
            RequestChartRedraw();
        }

        private void AdminDashboardView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RequestChartRedraw();
        }

        private void StartClock()
        {
            UpdateClock();
            _clockTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
            _clockTimer.Tick += (_, _) => UpdateClock();
            _clockTimer.Start();
        }

        private void StartPolling()
        {
            _pollTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(15) };
            _pollTimer.Tick += (_, _) => RefreshDashboard();
            _pollTimer.Start();
        }

        private void UpdateClock()
        {
            var now = DateTime.Now;
            ClockLabel.Text = now.ToString("dddd, MMM dd, yyyy  hh:mm tt");
            Day1Label.Text = now.AddDays(-6).ToString("MMM dd");
            Day2Label.Text = now.AddDays(-5).ToString("MMM dd");
            Day3Label.Text = now.AddDays(-4).ToString("MMM dd");
            Day4Label.Text = now.AddDays(-3).ToString("MMM dd");
            Day5Label.Text = now.AddDays(-2).ToString("MMM dd");
            Day6Label.Text = now.AddDays(-1).ToString("MMM dd");
        }

        private void RealTime_OnTableUpdated(object? sender, string tableName)
        {
            if (tableName is "shelters" or "inventory_items" or "dispatch_logs")
            {
                Dispatcher.BeginInvoke(() =>
                {
                    if (IsLoaded && IsVisible)
                        RefreshDashboard();
                });
            }
        }

        private void RefreshDashboard()
        {
            _viewModel = new AdminDashboardViewModel();
            DataContext = _viewModel;
            RequestChartRedraw();
        }

        private void RequestChartRedraw()
        {
            if (!IsLoaded)
                return;

            Dispatcher.BeginInvoke(() =>
            {
                if (LineChartCanvas.ActualWidth >= 10)
                    RedrawCharts();
                else if (!_chartsPending)
                {
                    _chartsPending = true;
                    void OnLayout(object? s, EventArgs e)
                    {
                        LineChartCanvas.LayoutUpdated -= OnLayout;
                        _chartsPending = false;
                        RedrawCharts();
                    }
                    LineChartCanvas.LayoutUpdated += OnLayout;
                }
            }, DispatcherPriority.Loaded);
        }

        private void RedrawCharts()
        {
            if (_viewModel == null) return;

            AdminDashboardCharts.DrawSparkline(SparkShelters, _viewModel.OpenSheltersTrend, (Brush)FindResource("Accent"));
            AdminDashboardCharts.DrawSparkline(SparkEvacuees, _viewModel.EvacueeTrend, (Brush)FindResource("Blue"));
            AdminDashboardCharts.DrawSparkline(SparkBeds, _viewModel.AvailableBedsTrend, (Brush)FindResource("Teal"));
            AdminDashboardCharts.DrawSparkline(SparkCapacity, _viewModel.OccupancyTrend, (Brush)FindResource("Amber"));
            AdminDashboardCharts.DrawSparkline(SparkRelief, _viewModel.ReliefTrend, (Brush)FindResource("Green"));
            AdminDashboardCharts.DrawSparkline(SparkFood, _viewModel.FoodTrend, (Brush)FindResource("Red"));
            AdminDashboardCharts.DrawSparkline(SparkMedical, _viewModel.MedicalTrend, (Brush)FindResource("Blue"));

            AdminDashboardCharts.DrawLineChart(LineChartCanvas, _viewModel.LineChartTrend, maxY: 100);
            AdminDashboardCharts.DrawPieChart(PieChartCanvas, MapInventorySlices());
        }

        private List<InventorySlice> MapInventorySlices()
        {
            var list = new List<InventorySlice>();
            if (_viewModel == null) return list;
            foreach (var item in _viewModel.InventoryLegend)
            {
                list.Add(new InventorySlice
                {
                    Label = item.Label,
                    Quantity = item.Quantity,
                    ColorHex = item.ColorHex
                });
            }
            return list;
        }

        private AdminWindow? GetAdminWindow() => Window.GetWindow(this) as AdminWindow;

        private void QuickAction_RegisterShelter_Click(object sender, RoutedEventArgs e)
            => GetAdminWindow()?.NavigateToShelters();

        private void QuickAction_LogDonation_Click(object sender, RoutedEventArgs e)
            => GetAdminWindow()?.NavigateToInventory();

        private void QuickAction_DistributeGoods_Click(object sender, RoutedEventArgs e)
            => GetAdminWindow()?.NavigateToDispatch();
    }
}
