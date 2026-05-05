using ProjectBReadyWPF.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReadyWPF.Backend.Models.Facilities
{
    public class Shelter : IReportable, ITrackable
    {
        public int ShelterID { get; set; }
        public string ShelterName { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }

        // Public set para mabasa ng DB reader. Business validation nasa UpdateOccupancy().
        public int CurrentOccupancy { get; set; }
        public string Status { get; set; } = string.Empty;

        public void UpdateOccupancy(int count)
        {
            if (CurrentOccupancy + count <= MaxCapacity)
            {
                CurrentOccupancy += count;
                UpdateStatus();
            }
            else
            {
                throw new Exception("Shelter is at full capacity!");
            }
        }

        public void UpdateStatus()
        {
            if (CurrentOccupancy >= MaxCapacity)
            {
                Status = "Full";
            }
            else
            {
                Status = "Open";
            }
        }

        public bool CheckCapacity()
        {
            return CurrentOccupancy >= MaxCapacity;
        }

        public string GenerateSummary()
        {
            return $"{ShelterName}: {CurrentOccupancy}/{MaxCapacity} Occupied.";
        }
    }
}
