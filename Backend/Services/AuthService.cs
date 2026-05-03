using System;
using ProjectBReadyWPF.Database.DataAccess;

namespace ProjectBReadyWPF.Backend.Services
{
    public class AuthService
    {
        private readonly DBHelper _dbHelper;

        public AuthService()
        {
            _dbHelper = new DBHelper();
        }

        // PLACEHOLDER: Login logic gamit ang PIN
        public bool Login(string pin)
        {
            // TODO: I-verify ang PIN sa database
            if (pin == "1234") return true; // Default temporary PIN
            return false;
        }
    }
}