using System;
using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Inventory;
using ProjectBReadyWPF.Database.DataAccess;

namespace ProjectBReadyWPF.Backend.Services
{
    public class InventoryService
    {
        private readonly DBHelper _dbHelper;

        public InventoryService()
        {
            _dbHelper = new DBHelper();
        }

        // PLACEHOLDER: Kunin lahat ng items (Food at Medical)
        public List<InventoryItem> GetCurrentInventory()
        {
            // TODO: Dito papasok yung query para sa items table
            return new List<InventoryItem>();
        }

        // PLACEHOLDER: Dispatch ng supplies sa shelter
        public bool DispatchSupplies(string itemId, int amount, string shelterId)
        {
            // TODO: Logic para bawasan ang inventory at gumawa ng log
            return true;
        }
    }
}