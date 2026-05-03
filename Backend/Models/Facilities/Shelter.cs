using ProjectBReadyWPF.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReadyWPF.Backend.Models.Facilities
{
    public class Shelter : IReportable
    {
        public int ShelterID { get; set; }
        public string ShelterName { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }

        // Private set para Encapsulated. Method lang ang pwedeng magbago.
        public int CurrentOccupancy { get; private set; }
        public string Status { get; set; } = string.Empty;

        public void UpdateOccupancy(int count)
        {
            if (CurrentOccupancy + count <= MaxCapacity)
            {
                CurrentOccupancy += count;
            }
            else
            {
                throw new Exception("Shelter is at full capacity!");
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
