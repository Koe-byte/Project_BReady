using System.Linq;
using ProjectBReady.Data;
using ProjectBReady.Models.Users;

namespace ProjectBReady.Services
{
    public class UserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Finds a Person by UserID. Returns null if not found.
        /// Used by the PIN unlock flow to get the BarangayOfficial object.
        /// </summary>
        public Person Login(string userID)
        {
            return _db.Persons.FirstOrDefault(p => p.UserID == userID);
        }

        /// <summary>Returns the first BarangayOfficial found (used as default admin).</summary>
        public BarangayOfficial GetDefaultOfficial()
        {
            return _db.BarangayOfficials.FirstOrDefault();
        }
    }
}