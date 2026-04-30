using System;
using System.Data;
using ProjectBReady.Data;
using ProjectBReady.Models.Facilities;

namespace ProjectBReady.Services
{
    public static class ShelterService
    {
        // ── READ ─────────────────────────────────────────────────────────

        public static DataTable GetAll()
        {
            return DBHelper.GetData(
                "SELECT ShelterID, ShelterName, CurrentOccupancy, MaxCapacity, Status " +
                "FROM SHELTERS ORDER BY ShelterName");
        }

        public static DataRow GetByID(string shelterID)
        {
            DataTable dt = DBHelper.GetData(
                $"SELECT * FROM SHELTERS WHERE ShelterID = '{shelterID}'");
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static int GetTotalAvailableSlots()
        {
            DataTable dt = DBHelper.GetData(
                "SELECT COALESCE(SUM(MaxCapacity - CurrentOccupancy), 0) AS Slots " +
                "FROM SHELTERS WHERE Status != 'Closed'");
            if (dt == null || dt.Rows.Count == 0) return 0;
            return Convert.ToInt32(dt.Rows[0]["Slots"]);
        }

        public static DataRow GetMostAvailable()
        {
            DataTable dt = DBHelper.GetData(
                "SELECT ShelterName, (MaxCapacity - CurrentOccupancy) AS AvailableSlots " +
                "FROM SHELTERS WHERE Status = 'Open' " +
                "ORDER BY AvailableSlots DESC LIMIT 1");
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // ── UPDATE OCCUPANCY ──────────────────────────────────────────────

        /// <summary>
        /// Adds count to CurrentOccupancy. Auto-sets Status to Full if at max.
        /// Returns false if shelter not found or count exceeds remaining capacity.
        /// </summary>
        public static bool AddToOccupancy(string shelterID, int count)
        {
            DataRow row = GetByID(shelterID);
            if (row == null) return false;

            int current = Convert.ToInt32(row["CurrentOccupancy"]);
            int max = Convert.ToInt32(row["MaxCapacity"]);
            int newVal = current + count;

            if (newVal > max) return false;

            string status = newVal >= max ? "Full" : "Open";
            return DBHelper.ExecuteQuery(
                $"UPDATE SHELTERS SET CurrentOccupancy = {newVal}, Status = '{status}' " +
                $"WHERE ShelterID = '{shelterID}'");
        }

        public static bool SetStatus(string shelterID, string status)
        {
            return DBHelper.ExecuteQuery(
                $"UPDATE SHELTERS SET Status = '{status}' " +
                $"WHERE ShelterID = '{shelterID}'");
        }

        // ── CREATE / DELETE ───────────────────────────────────────────────

        public static bool AddShelter(string shelterID, string shelterName, int maxCapacity)
        {
            return DBHelper.ExecuteQuery(
                $"INSERT INTO SHELTERS (ShelterID, ShelterName, MaxCapacity, CurrentOccupancy, Status, DateAdded) " +
                $"VALUES ('{shelterID}', '{shelterName}', {maxCapacity}, 0, 'Open', GETDATE())");
        }

        public static bool DeleteShelter(string shelterID)
        {
            return DBHelper.ExecuteQuery(
                $"DELETE FROM SHELTERS WHERE ShelterID = '{shelterID}'");
        }
    }
}