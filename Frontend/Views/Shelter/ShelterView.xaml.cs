using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Services;
using ProjectBReadyWPF.Backend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Backend.Models.Facilities;

namespace ProjectBReadyWPF.Frontend.Views.Shelter
{
    // ── Display model para sa table rows ──────────────────────────────
    public class ShelterRowItem
    {
        public int ShelterID { get; set; }
        public int RowNumber { get; set; }
        public string Name { get; set; } = "";
        public int MaxCapacity { get; set; }
        public int CurrentOccupancy { get; set; }
        public string Status { get; set; } = "Open"; // From DB: Open, Full, Closed, Under Maintenance

        public int Available => MaxCapacity - CurrentOccupancy;
        public SolidColorBrush AvailableColor => Available > 0
            ? new SolidColorBrush(Color.FromRgb(20, 184, 166))
            : new SolidColorBrush(Color.FromRgb(239, 68, 68));

        public double FillPercent => MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity : 0;
        public SolidColorBrush FillColor
        {
            get
            {
                if (Status == "Closed" || Status == "Under Maintenance")
                    return new SolidColorBrush(Color.FromRgb(148, 163, 184)); // Slate gray
                double pct = MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity * 100 : 0;
                if (pct >= 90) return new SolidColorBrush(Color.FromRgb(239, 68, 68));
                if (pct >= 70) return new SolidColorBrush(Color.FromRgb(234, 124, 60));
                return new SolidColorBrush(Color.FromRgb(20, 184, 166));
            }
        }
        public string PctFull => MaxCapacity > 0 ? $"{(double)CurrentOccupancy / MaxCapacity * 100:F0}%" : "0%";
        public SolidColorBrush PctColor => FillColor;

        public SolidColorBrush StatusBadgeBg => Status switch
        {
            "Full" => new SolidColorBrush(Color.FromRgb(254, 226, 226)),       // Red bg
            "Closed" => new SolidColorBrush(Color.FromRgb(226, 232, 240)),     // Slate bg
            "Under Maintenance" => new SolidColorBrush(Color.FromRgb(254, 243, 199)), // Amber bg
            _ => new SolidColorBrush(Color.FromRgb(209, 250, 229))            // Green bg
        };
        public SolidColorBrush StatusTextColor => Status switch
        {
            "Full" => new SolidColorBrush(Color.FromRgb(153, 27, 27)),         // Red text
            "Closed" => new SolidColorBrush(Color.FromRgb(51, 65, 85)),        // Slate text
            "Under Maintenance" => new SolidColorBrush(Color.FromRgb(146, 64, 14)), // Amber text
            _ => new SolidColorBrush(Color.FromRgb(22, 101, 52))              // Green text
        };
    }

    // ── ViewModel ────────────────────────────────────────────────────
    public class ShelterPageViewModel
    {
        public List<ShelterRowItem> Shelters { get; set; } = new();
        public int TotalShelters { get; set; }
        public int TotalOccupancy { get; set; }
        public string CombinedCapacityNote { get; set; } = "";
        public int FullShelterCount { get; set; }
    }

    // ── View ─────────────────────────────────────────────────────────
    public partial class ShelterView : UserControl
    {
        private readonly IShelterService _shelterService;
        private readonly IRealTimeService _realTimeService;
        private List<ShelterRowItem> _allShelters = new();
        private int _selectedShelterId = -1;

        public ShelterView()
        {
            InitializeComponent();
            _shelterService = App.ServiceProvider.GetRequiredService<IShelterService>();
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
                var dbShelters = _shelterService.GetAllShelters();
                _allShelters = new List<ShelterRowItem>();

                int row = 1;
                int totalOcc = 0;
                int totalCap = 0;
                int fullCount = 0;

                foreach (var s in dbShelters)
                {
                    _allShelters.Add(new ShelterRowItem
                    {
                        ShelterID = s.ShelterID,
                        RowNumber = row++,
                        Name = s.ShelterName,
                        MaxCapacity = s.MaxCapacity,
                        CurrentOccupancy = s.CurrentOccupancy,
                        Status = s.Status
                    });

                    totalOcc += s.CurrentOccupancy;
                    totalCap += s.MaxCapacity;
                    if (s.Status == "Full") fullCount++;
                }

                var vm = new ShelterPageViewModel
                {
                    Shelters = _allShelters,
                    TotalShelters = _allShelters.Count,
                    TotalOccupancy = totalOcc,
                    CombinedCapacityNote = $"of {totalCap} combined capacity",
                    FullShelterCount = fullCount
                };

                DataContext = vm;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shelters: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ── Event handlers ───────────────────────────────────────────

        private void OnAddShelter(object sender, RoutedEventArgs e)
        {
            // Navigate to Add Shelter form
            var mainWindow = Window.GetWindow(this) ;
            if (mainWindow != null)
            {
                var contentArea = mainWindow.FindName("MainContentArea") as ContentControl;
                if (contentArea != null)
                {
                    contentArea.Content = new ShelterAddView();
                }
            }
        }

        private void OnSearchChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchBox.Text?.Trim().ToLower() ?? "";
            var filtered = string.IsNullOrEmpty(searchText)
                ? _allShelters
                : _allShelters.Where(s => s.Name.ToLower().Contains(searchText)).ToList();

            ShelterTable.ItemsSource = filtered;
        }

        private void OnShelterRadioChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && rb.Tag is int id)
            {
                _selectedShelterId = id;
            }
        }

        // ── Edit Shelter Modal ──────────────────────────────────────

        private void OnEditShelter(object sender, RoutedEventArgs e)
        {
            if (_selectedShelterId < 0)
            {
                MessageBox.Show("Pumili muna ng shelter sa table.", "No Selection");
                return;
            }

            var shelter = _allShelters.FirstOrDefault(s => s.ShelterID == _selectedShelterId);
            if (shelter != null)
            {
                EditShelterSubtitle.Text = $"Editing: {shelter.Name}";
                EditNameInput.Text = shelter.Name;
                EditOccInput.Text = shelter.CurrentOccupancy.ToString();
                EditMaxCapLabel.Text = $"(Max: {shelter.MaxCapacity})";

                // Set ComboBox to current status
                for (int i = 0; i < EditStatusPicker.Items.Count; i++)
                {
                    if (EditStatusPicker.Items[i] is ComboBoxItem item &&
                        item.Content?.ToString() == shelter.Status)
                    {
                        EditStatusPicker.SelectedIndex = i;
                        break;
                    }
                }
            }
            ModalEditOverlay.Visibility = Visibility.Visible;
        }

        private void OnCloseEditModal(object sender, RoutedEventArgs e)
        {
            ModalEditOverlay.Visibility = Visibility.Collapsed;
        }

        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            var shelter = _allShelters.FirstOrDefault(s => s.ShelterID == _selectedShelterId);
            if (shelter == null) return;

            // Validate name
            string newName = EditNameInput.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Shelter name is required.", "Validation Error");
                return;
            }

            // Validate occupancy
            if (!int.TryParse(EditOccInput.Text, out int newOcc) || newOcc < 0)
            {
                MessageBox.Show("Invalid occupancy number.", "Validation Error");
                return;
            }
            if (newOcc > shelter.MaxCapacity)
            {
                MessageBox.Show($"Occupancy cannot exceed max capacity ({shelter.MaxCapacity}).", "Validation Error");
                return;
            }

            // Get selected status
            string newStatus = "Open";
            if (EditStatusPicker.SelectedItem is ComboBoxItem selected)
            {
                newStatus = selected.Content?.ToString() ?? "Open";
            }

            // Save changes
            if (newName != shelter.Name)
                _shelterService.UpdateShelterName(_selectedShelterId, newName);

            if (newOcc != shelter.CurrentOccupancy)
                _shelterService.UpdateOccupancy(_selectedShelterId, newOcc);

            if (newStatus != shelter.Status)
                _shelterService.UpdateStatus(_selectedShelterId, newStatus);

            ModalEditOverlay.Visibility = Visibility.Collapsed;
            LoadData();
        }

        // ── Delete Modal ─────────────────────────────────────────────

        private void OnDeleteSelected(object sender, RoutedEventArgs e)
        {
            if (_selectedShelterId < 0)
            {
                MessageBox.Show("Pumili muna ng shelter sa table.", "No Selection");
                return;
            }

            var shelter = _allShelters.FirstOrDefault(s => s.ShelterID == _selectedShelterId);
            if (shelter != null)
            {
                DeleteShelterLabel.Text = $"Are you sure you want to delete \"{shelter.Name}\"? " +
                                          $"This shelter currently has {shelter.CurrentOccupancy} evacuees.";
            }
            ModalDeleteOverlay.Visibility = Visibility.Visible;
        }

        private void OnCloseDeleteModal(object sender, RoutedEventArgs e)
        {
            ModalDeleteOverlay.Visibility = Visibility.Collapsed;
        }

        private void OnConfirmDelete(object sender, RoutedEventArgs e)
        {
            bool success = _shelterService.DeleteShelter(_selectedShelterId);
            if (success)
            {
                _selectedShelterId = -1;
                ModalDeleteOverlay.Visibility = Visibility.Collapsed;
                LoadData();
            }
        }
    }
}
