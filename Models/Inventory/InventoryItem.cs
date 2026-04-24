using ProjectBReady.Models.Interfaces;
using ProjectBReady.Models.Facilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReady.Models.Inventory
{
    // Inherits ITrackable, Base Class for Inventory 
    public abstract class InventoryItem : ITrackable
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public virtual void StockIn(int amount)
        {
            Quantity += amount;
        }

        // Virtual para pwede i-override ng subclasses (Polymorphism) 
        public virtual void Dispatch(int amount, Shelter targetShelter)
        {
            if (Quantity >= amount)
            {
                Quantity -= amount;
                // Save to DISPATCH_LOGS logic here
            }
        }

        public void UpdateStatus() { }
    }
}