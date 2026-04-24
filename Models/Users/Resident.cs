using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReady.Models.Users
{
    // Inherits from Person 
    public class Resident : Person
    {
        public bool IsReadOnly { get; set; } = true;

        public override void ViewDashboard()
        {
            // Logic para ipakita ang Kiosk Mode (Read-Only)
        }
    }
}
