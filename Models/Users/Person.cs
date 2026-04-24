using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBReady.Models.Users
{
    // Abstract base class 
    public abstract class Person
    {
        public string UserID { get; set; }
        public string Role { get; set; }

        public bool Login()
        {
            // Logic para sa login validation
            return true;
        }

        // Abstract method para sa Polymorphism
        public abstract void ViewDashboard();
    }
}
