using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Facilities;

namespace ProjectBReadyWPF.Backend.Interfaces
{
    public interface IShelterService
    {
        List<Shelter> GetAllShelters();
        Shelter? GetShelterById(int shelterId);
        bool AddShelter(Shelter shelter);
        bool UpdateOccupancy(int shelterId, int newOccupancy);
        bool UpdateStatus(int shelterId, string status);
        bool UpdateShelterName(int shelterId, string newName);
        bool DeleteShelter(int shelterId);
    }
}
