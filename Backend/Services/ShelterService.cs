using System;
using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Facilities;
using ProjectBReadyWPF.Database.DataAccess;
using Npgsql;

namespace ProjectBReadyWPF.Backend.Services
{
    public class ShelterService
    {
        private readonly DBHelper _dbHelper;

        public ShelterService()
        {
            _dbHelper = new DBHelper();
        }

        // PLACEHOLDER: Kunin lahat ng shelter mula sa Supabase
        public List<Shelter> GetAllShelters()
        {
            // TODO: Nash, dito mo ilalagay yung "SELECT * FROM shelters"
            return new List<Shelter>();
        }

        // PLACEHOLDER: Update ng occupancy (Add/Remove people)
        public bool UpdateOccupancy(string shelterId, int newCount)
        {
            // TODO: Nash, dito mo ilalagay yung "UPDATE shelters SET occupancy = ..."
            return true;
        }
    }
}