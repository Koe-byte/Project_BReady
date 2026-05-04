using System;
using System.Collections.Generic;
using System.Text;
using ProjectBReadyWPF.Backend.Models.Inventory;

namespace ProjectBReadyWPF.Backend.Models.Inventory
{
    public class MedicalSupply : InventoryItem
    {
        public string Dosage { get; set; } = string.Empty;
        public bool IsPrescriptionRequired { get; set; }
    }
}
