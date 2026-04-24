using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReady.Models.Inventory
{
    public class FoodItem : InventoryItem
    {
        public DateTime ExpirationDate { get; set; }

        public bool CheckExpiration()
        {
            return DateTime.Now > ExpirationDate;
        }
    }
}
