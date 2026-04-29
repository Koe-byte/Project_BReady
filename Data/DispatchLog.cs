using System;
using ProjectBReady.Models.Facilities;
using ProjectBReady.Models.Inventory;

namespace ProjectBReady.Data
{
    /// <summary>
    /// Records every time relief goods are dispatched to a shelter.
    /// </summary>
    public class DispatchLog
    {
        public int    LogID      { get; set; }
        public string ItemID     { get; set; }
        public string ShelterID  { get; set; }
        public int    Quantity   { get; set; }
        public DateTime DispatchedAt { get; set; } = DateTime.Now;
        public string DispatchedBy  { get; set; }   // UserID ng official

        // Navigation properties
        public InventoryItem Item    { get; set; }
        public Shelter        Shelter { get; set; }
    }
}
