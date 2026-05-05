using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Inventory;

namespace ProjectBReadyWPF.Backend.Interfaces
{
    public interface IInventoryService
    {
        List<InventoryItem> GetCurrentInventory();
        List<FoodItem> GetFoodItems();
        List<MedicalSupply> GetMedicalSupplies();
        bool AddFoodItem(FoodItem item);
        bool AddMedicalSupply(MedicalSupply item);
        bool UpdateQuantity(int itemId, int newQty);
        bool DeleteItem(int itemId);
    }
}
