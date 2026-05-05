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
        bool DeleteShelter(int shelterId);
    }
}
