using System.Windows;
using System.Windows.Controls;
using ProjectBReadyWPF.Backend.Interfaces;
using ProjectBReadyWPF.Backend.Models.Inventory;
using ProjectBReadyWPF.Backend.Models.Facilities;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectBReadyWPF.Frontend.Views.Dispatch
{
    public partial class DispatchView : UserControl
    {
        private readonly IDispatchService _dispatchService;
        private readonly IInventoryService _inventoryService;
        private readonly IShelterService _shelterService;
        private readonly IRealTimeService _realTimeService;

        public DispatchViewModel ViewModel { get; set; }

        public DispatchView(IDispatchService dispatchService, IInventoryService inventoryService, IShelterService shelterService)
        {
            InitializeComponent();
            _dispatchService = dispatchService;
            _inventoryService = inventoryService;
            _shelterService = shelterService;
            
            // Resolve RealTimeService from App DI
            _realTimeService = App.ServiceProvider.GetRequiredService<IRealTimeService>();

            ViewModel = new DispatchViewModel();
            this.DataContext = ViewModel;

            LoadDropdowns();
            LoadLogs();

            // Subscribe to real-time events
            _realTimeService.OnTableUpdated += RealTimeService_OnTableUpdated;
            this.Unloaded += (s, e) => _realTimeService.OnTableUpdated -= RealTimeService_OnTableUpdated;
        }

        private void RealTimeService_OnTableUpdated(object? sender, string tableName)
        {
            if (tableName == "inventory_items" || tableName == "shelters")
            {
                LoadDropdowns();
            }
            if (tableName == "dispatch_logs")
            {
                LoadLogs();
            }
        }

        private void LoadDropdowns()
        {
            var items = _inventoryService.GetCurrentInventory();
            var displayItems = items.Select(i => new {
                ItemID = i.ItemID,
                DisplayLabel = $"{i.ItemName} (Stock: {i.Quantity})"
            }).ToList();
            CmbItems.ItemsSource = displayItems;

            var shelters = _shelterService.GetAllShelters();
            CmbShelters.ItemsSource = shelters;
        }

        private void LoadLogs()
        {
            var logs = _dispatchService.GetRecentDispatches(50);
            ViewModel.DispatchLogs.Clear();
            foreach (var log in logs)
            {
                ViewModel.DispatchLogs.Add(log);
            }
            ViewModel.HasLogs = ViewModel.DispatchLogs.Count > 0;
        }

        private void OnDispatchClicked(object sender, RoutedEventArgs e)
        {
            if (CmbItems.SelectedValue == null)
            {
                MessageBox.Show("Please select an item to dispatch.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CmbShelters.SelectedValue == null)
            {
                MessageBox.Show("Please select a target shelter.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TxtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid positive quantity.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int itemId = (int)CmbItems.SelectedValue;
            int shelterId = (int)CmbShelters.SelectedValue;

            // Get display names for confirmation message
            var selectedItem = CmbItems.SelectedItem as dynamic;
            var selectedShelter = CmbShelters.SelectedItem as ProjectBReadyWPF.Backend.Models.Facilities.Shelter;

            string itemName = selectedItem?.DisplayLabel ?? "selected item";
            string shelterName = selectedShelter?.ShelterName ?? "selected shelter";

            var confirm = MessageBox.Show(
                $"Are you sure you want to dispatch?\n\n" +
                $"Item     : {itemName}\n" +
                $"Shelter  : {shelterName}\n" +
                $"Quantity : {quantity} units\n\n" +
                $"This action cannot be undone.",
                "Confirm Dispatch",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes) return;

            bool success = _dispatchService.DispatchItem(itemId, shelterId, quantity);
            
            if (success)
            {
                MessageBox.Show("Relief goods dispatched successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                TxtQuantity.Clear();
                CmbItems.SelectedIndex = -1;
                CmbShelters.SelectedIndex = -1;

                // Refresh data
                LoadDropdowns();
                LoadLogs();
            }
        }
    }

    public class DispatchViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public ObservableCollection<DispatchLog> DispatchLogs { get; set; } = new ObservableCollection<DispatchLog>();

        private bool _hasLogs;
        public bool HasLogs
        {
            get => _hasLogs;
            set
            {
                _hasLogs = value;
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(HasLogs)));
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
    }
}
