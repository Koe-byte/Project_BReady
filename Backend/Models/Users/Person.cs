using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReadyWPF.Backend.Models.Users
{
    // Abstract base class 
    public abstract class Person
    {
        public string UserID { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public bool Login()
        {
            // Logic para sa login validation
            return true;
        }

        // Abstract method para sa Polymorphism
        public abstract void ViewDashboard();
    }
}
