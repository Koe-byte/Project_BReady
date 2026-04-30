using System;
using System.Data;
using ProjectBReady.Data;

namespace ProjectBReady.Services
{
    public static class InventoryService
    {
        // ── READ ──────────────────────────────────────────────────────────

        public static DataTable GetAll()
        {
            return DBHelper.GetData(
                "SELECT ItemID, ItemName, Quantity, ItemType, ExpirationDate, Dosage, IsPrescriptionRequired " +
                "FROM dbo.INVENTORY ORDER BY ItemType, ItemName");
        }

        public static DataTable GetFoodItems()
        {
            return DBHelper.GetData(
                "SELECT ItemID, ItemName, Quantity, ExpirationDate " +
                "FROM dbo.INVENTORY WHERE ItemType = 'Food' ORDER BY ItemName");
        }

        public static DataTable GetMedicalSupplies()
        {
            return DBHelper.GetData(
                "SELECT ItemID, ItemName, Quantity, Dosage, IsPrescriptionRequired " +
                "FROM dbo.INVENTORY WHERE ItemType = 'Medical' ORDER BY ItemName");
        }

        public static DataTable GetExpiredItems()
        {
            return DBHelper.GetData(
                "SELECT ItemID, ItemName, Quantity, ExpirationDate " +
                "FROM dbo.INVENTORY " +
                "WHERE ItemType = 'Food' AND ExpirationDate < GETDATE()");
        }

        public static DataTable GetDispatchLogs()
        {
            return DBHelper.GetData(
                "SELECT dl.LogID, i.ItemName, s.ShelterName, dl.Quantity, " +
                "dl.DispatchedAt, dl.DispatchedBy " +
                "FROM dbo.DISPATCH_LOGS dl " +
                "JOIN dbo.INVENTORY i ON dl.ItemID = i.ItemID " +
                "JOIN dbo.SHELTERS s ON dl.ShelterID = s.ShelterID " +
                "ORDER BY dl.DispatchedAt DESC");
        }

        // ── STOCK IN ──────────────────────────────────────────────────────

        /// <summary>Returns false if item not found or amount <= 0.</summary>
        public static bool StockIn(string itemID, int amount)
        {
            if (amount <= 0) return false;
            DataTable dt = DBHelper.GetData(
                $"SELECT ItemID FROM dbo.INVENTORY WHERE ItemID = '{itemID}'");
            if (dt.Rows.Count == 0) return false;

            return DBHelper.ExecuteQuery(
                $"UPDATE dbo.INVENTORY SET Quantity = Quantity + {amount} " +
                $"WHERE ItemID = '{itemID}'");
        }

        // ── DISPATCH ──────────────────────────────────────────────────────

        /// <summary>
        /// Deducts qty from stock and logs to DISPATCH_LOGS.
        /// Returns false if insufficient stock or item/shelter not found.
        /// </summary>
        public static bool Dispatch(string itemID, string shelterID, int qty, string dispatchedByUserID)
        {
            if (qty <= 0) return false;

            // Check stock
            DataTable dt = DBHelper.GetData(
                $"SELECT Quantity FROM dbo.INVENTORY WHERE ItemID = '{itemID}'");
            if (dt.Rows.Count == 0) return false;

            int current = Convert.ToInt32(dt.Rows[0]["Quantity"]);
            if (current < qty) return false;

            // Deduct stock
            bool deducted = DBHelper.ExecuteQuery(
                $"UPDATE dbo.INVENTORY SET Quantity = Quantity - {qty} " +
                $"WHERE ItemID = '{itemID}'");
            if (!deducted) return false;

            // Write log
            return DBHelper.ExecuteQuery(
                $"INSERT INTO dbo.DISPATCH_LOGS (ItemID, ShelterID, Quantity, DispatchedAt, DispatchedBy) " +
                $"VALUES ('{itemID}', '{shelterID}', {qty}, GETDATE(), '{dispatchedByUserID}')");
        }

        // ── ADD ITEMS ─────────────────────────────────────────────────────

        public static bool AddFoodItem(string itemID, string name, int qty, DateTime expiration)
        {
            return DBHelper.ExecuteQuery(
                $"INSERT INTO dbo.INVENTORY (ItemID, ItemName, Quantity, ItemType, ExpirationDate) " +
                $"VALUES ('{itemID}', '{name}', {qty}, 'Food', '{expiration:yyyy-MM-dd}')");
        }

        public static bool AddMedicalSupply(string itemID, string name, int qty, string dosage, bool requiresPrescription)
        {
            int rx = requiresPrescription ? 1 : 0;
            return DBHelper.ExecuteQuery(
                $"INSERT INTO dbo.INVENTORY (ItemID, ItemName, Quantity, ItemType, Dosage, IsPrescriptionRequired) " +
                $"VALUES ('{itemID}', '{name}', {qty}, 'Medical', '{dosage}', {rx})");
        }

        // ── DELETE ────────────────────────────────────────────────────────

        public static bool DeleteItem(string itemID)
        {
            return DBHelper.ExecuteQuery(
                $"DELETE FROM dbo.INVENTORY WHERE ItemID = '{itemID}'");
        }
    }
}