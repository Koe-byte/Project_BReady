using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBReady.Data;
using ProjectBReady.Models.Facilities;

namespace ProjectBReady.Services
{
    public class ShelterService
    {
        private readonly AppDbContext _db;

        public ShelterService(AppDbContext db)
        {
            _db = db;
        }

        // ── READ ────────────────────────────────────────────────────────────

        /// <summary>Returns all shelters as a list (used by DataGridView).</summary>
        public List<Shelter> GetAll()
        {
            return _db.Shelters.ToList();
        }

        /// <summary>Find one shelter by ID. Returns null if not found.</summary>
        public Shelter GetByID(string shelterID)
        {
            return _db.Shelters.FirstOrDefault(s => s.ShelterID == shelterID);
        }

        // ── UPDATE ──────────────────────────────────────────────────────────

        /// <summary>
        /// Adds <paramref name="count"/> evacuees to the shelter's CurrentOccupancy.
        /// Throws if shelter not found or capacity exceeded.
        /// </summary>
        public void UpdateShelterOccupancy(string shelterID, int count)
        {
            var shelter = GetByID(shelterID);
            if (shelter == null)
                throw new Exception($"Shelter '{shelterID}' not found.");

            // Uses the encapsulated method in Shelter.cs (throws if over capacity)
            shelter.UpdateOccupancy(count);
            _db.SaveChanges();
        }

        /// <summary>Sets the Status field of a shelter (e.g. "Active", "Full", "Standby").</summary>
        public void SetStatus(string shelterID, string newStatus)
        {
            var shelter = GetByID(shelterID);
            if (shelter == null)
                throw new Exception($"Shelter '{shelterID}' not found.");

            shelter.Status = newStatus;
            _db.SaveChanges();
        }

        // ── CREATE ──────────────────────────────────────────────────────────

        /// <summary>Adds a brand-new shelter to the database.</summary>
        public void AddShelter(string shelterID, string shelterName, int maxCapacity, string status = "Active")
        {
            if (_db.Shelters.Any(s => s.ShelterID == shelterID))
                throw new Exception($"Shelter ID '{shelterID}' already exists.");

            _db.Shelters.Add(new Shelter
            {
                ShelterID = shelterID,
                ShelterName = shelterName,
                MaxCapacity = maxCapacity,
                Status = status
            });
            _db.SaveChanges();
        }

        // ── DELETE ──────────────────────────────────────────────────────────

        public void DeleteShelter(string shelterID)
        {
            var shelter = GetByID(shelterID);
            if (shelter == null)
                throw new Exception($"Shelter '{shelterID}' not found.");

            _db.Shelters.Remove(shelter);
            _db.SaveChanges();
        }
    }
}