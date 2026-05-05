using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Inventory;

namespace ProjectBReadyWPF.Backend.Interfaces
{
    public interface IDispatchService
    {
        List<DispatchLog> GetRecentDispatches(int limit = 10);
        bool DispatchItem(int itemId, int shelterId, int quantity);
    }
}
