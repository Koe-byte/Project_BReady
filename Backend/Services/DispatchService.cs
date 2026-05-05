using System;
using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Inventory;
using ProjectBReadyWPF.Database.DataAccess;
using Npgsql;

using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Backend.Services
{
    public class DispatchService : IDispatchService
    {
        private readonly DBHelper _dbHelper;

        public DispatchService()
        {
            _dbHelper = new DBHelper();
        }

        // ── READ: Kunin ang mga recent dispatch logs ─────────────────
        // Uses JOIN para makuha ang item_name at shelter_name (hindi lang IDs)
        public List<DispatchLog> GetRecentDispatches(int limit = 10)
        {
            var logs = new List<DispatchLog>();

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"SELECT d.log_id, d.item_id, d.shelter_id, d.quantity_dispatched,
                             d.dispatch_date, i.item_name, s.shelter_name
                      FROM dispatch_logs d
                      JOIN inventory_items i ON d.item_id = i.item_id
                      JOIN shelters s ON d.shelter_id = s.shelter_id
                      ORDER BY d.dispatch_date DESC
                      LIMIT @limit",
                    conn);

                cmd.Parameters.AddWithValue("@limit", limit);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logs.Add(new DispatchLog
                    {
                        LogID = reader.GetInt32(0),
                        ItemID = reader.GetInt32(1),
                        ShelterID = reader.GetInt32(2),
                        QuantityDispatched = reader.GetInt32(3),
                        DispatchDate = reader.GetDateTime(4),
                        ItemName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        ShelterName = reader.IsDBNull(6) ? "" : reader.GetString(6)
                    });
                }
            }

            return logs;
        }

        // ── CREATE: Dispatch items sa shelter (Transaction) ──────────
        // Ginagamit ang TRANSACTION para siguradong sabay ang:
        //   1. Bawasan ang inventory quantity
        //   2. I-log sa dispatch_logs
        // Kung may mag-fail, walang mababago (rollback).
        public bool DispatchItem(int itemId, int shelterId, int quantity)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // Step 1: Check kung may sapat na stock
                using var checkCmd = new NpgsqlCommand(
                    "SELECT quantity FROM inventory_items WHERE item_id = @id",
                    conn, transaction);
                checkCmd.Parameters.AddWithValue("@id", itemId);

                var currentQty = (int?)checkCmd.ExecuteScalar();
                if (currentQty == null || currentQty < quantity)
                {
                    System.Windows.MessageBox.Show(
                        $"Hindi sapat ang stock. Available: {currentQty ?? 0}, Requested: {quantity}",
                        "Insufficient Stock", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    transaction.Rollback();
                    return false;
                }

                // Step 2: Bawasan ang inventory
                using var updateCmd = new NpgsqlCommand(
                    "UPDATE inventory_items SET quantity = quantity - @qty, updated_at = NOW() WHERE item_id = @id",
                    conn, transaction);
                updateCmd.Parameters.AddWithValue("@qty", quantity);
                updateCmd.Parameters.AddWithValue("@id", itemId);
                updateCmd.ExecuteNonQuery();

                // Step 3: I-log sa dispatch_logs
                using var insertCmd = new NpgsqlCommand(
                    @"INSERT INTO dispatch_logs (item_id, shelter_id, quantity_dispatched)
                      VALUES (@itemId, @shelterId, @qty)",
                    conn, transaction);
                insertCmd.Parameters.AddWithValue("@itemId", itemId);
                insertCmd.Parameters.AddWithValue("@shelterId", shelterId);
                insertCmd.Parameters.AddWithValue("@qty", quantity);
                insertCmd.ExecuteNonQuery();

                // Commit — lahat ay successful
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Windows.MessageBox.Show($"Error dispatching item: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }
    }
}
