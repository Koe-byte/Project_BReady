using System;

namespace ProjectBReadyWPF.Backend.Models.Inventory
{
    public class DispatchLog
    {
        // Sumasalamin ito sa PRIMARY KEY natin sa SQL na Auto-increment
        public int LogID { get; set; }

        // Foreign Key: Aling item ang kinuha?
        public int ItemID { get; set; }

        // Foreign Key: Saang shelter dinala?
        public int ShelterID { get; set; }

        // Gaano karami ang inilabas?
        public int QuantityDispatched { get; set; }

        // Petsa at oras kung kailan ito nangyari
        public DateTime DispatchDate { get; set; } = DateTime.Now;

        // Helper properties (Optional: Para mas madaling ipakita sa UI)
        public string? ItemName { get; set; }
        public string? ShelterName { get; set; }
    }
}