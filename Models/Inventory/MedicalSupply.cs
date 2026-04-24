using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReady.Models.Inventory
{
    public class MedicalSupply : InventoryItem
    {
        public string Dosage { get; set; }
        public bool IsPrescriptionRequired { get; set; }
    }
}
