using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectBReadyWPF.Backend.Services;
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

        public int Available => MaxCapacity - CurrentOccupancy;
        public SolidColorBrush AvailableColor => Available > 0
            ? new SolidColorBrush(Color.FromRgb(20, 184, 166))
            : new SolidColorBrush(Color.FromRgb(239, 68, 68));

        public double FillPercent => MaxCapacity > 0 ? (double)CurrentOccupancy / MaxCapacity : 0;
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
        public string PctFull => MaxCapacity > 0 ? $"{(double)CurrentOccupancy / MaxCapacity * 100:F0}%" : "0%";

        public string Status => CurrentOccupancy >= MaxCapacity ? "Full" : "Open";
        public SolidColorBrush StatusBadgeBg => Status == "Full"
            ? new SolidColorBrush(Color.FromRgb(254, 226, 226))
            : new SolidColorBrush(Color.FromRgb(209, 250, 229));
        public SolidColorBrush StatusTextColor => Status == "Full"
            ? new SolidColorBrush(Color.FromRgb(153, 27, 27))
            : new SolidColorBrush(Color.FromRgb(22, 101, 52));
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
        private readonly ShelterService _shelterService;
        private List<ShelterRowItem> _allShelters = new();
        private int _selectedShelterId = -1;

        public ShelterView()
        {
            InitializeComponent();
            _shelterService = new ShelterService();
            LoadData();
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
                        CurrentOccupancy = s.CurrentOccupancy
                    });

                    totalOcc += s.CurrentOccupancy;
                    totalCap += s.MaxCapacity;
                    if (s.CurrentOccupancy >= s.MaxCapacity) fullCount++;
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

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            _selectedShelterId = -1;
            LoadData();
        }

        private void OnAddShelter(object sender, RoutedEventArgs e)
        {
            // Navigate to Add Shelter form
            var mainWindow = Window.GetWindow(this) as ProjectBReadyWPF.Frontend.Views.MainDashboard.MainWindow;
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

        // ── Update Occupancy Modal ───────────────────────────────────

        private void OnUpdateOccupancy(object sender, RoutedEventArgs e)
        {
            if (_selectedShelterId < 0)
            {
                MessageBox.Show("Pumili muna ng shelter sa table.", "No Selection");
                return;
            }

            var shelter = _allShelters.FirstOrDefault(s => s.ShelterID == _selectedShelterId);
            if (shelter != null)
            {
                OccShelterName.Text = $"{shelter.Name} (Max: {shelter.MaxCapacity})";
                OccInput.Text = shelter.CurrentOccupancy.ToString();
            }
            ModalOccupancyOverlay.Visibility = Visibility.Visible;
        }

        private void OnCloseOccModal(object sender, RoutedEventArgs e)
        {
            ModalOccupancyOverlay.Visibility = Visibility.Collapsed;
        }

        private void OnSaveOccupancy(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(OccInput.Text, out int newOcc) || newOcc < 0)
            {
                MessageBox.Show("Invalid number.", "Error");
                return;
            }

            var shelter = _allShelters.FirstOrDefault(s => s.ShelterID == _selectedShelterId);
            if (shelter != null && newOcc > shelter.MaxCapacity)
            {
                MessageBox.Show($"Hindi puwede lumampas sa max capacity ({shelter.MaxCapacity}).", "Error");
                return;
            }

            bool success = _shelterService.UpdateOccupancy(_selectedShelterId, newOcc);
            if (success)
            {
                // Auto-update status
                string newStatus = (shelter != null && newOcc >= shelter.MaxCapacity) ? "Full" : "Open";
                _shelterService.UpdateStatus(_selectedShelterId, newStatus);

                ModalOccupancyOverlay.Visibility = Visibility.Collapsed;
                LoadData(); // Refresh table
            }
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