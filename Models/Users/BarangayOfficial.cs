using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReady.Models.Users
{
    // Inherits from Person 
    public class BarangayOfficial : Person
    {
        private string AdminPIN { get; set; }

        public bool UnlockAdminControls(string pin)
        {
            // Logic para i-check kung tama ang PIN
            return true;
        }

        public override void ViewDashboard()
        {
            // Logic para ipakita ang full Admin Controls
        }

        public void UpdateShelterOccupancy() { }
        public void DispatchReliefGoods() { }
    }
}
