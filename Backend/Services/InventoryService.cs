using System;
using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Inventory;
using ProjectBReadyWPF.Database.DataAccess;
using Npgsql;

using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Backend.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly DBHelper _dbHelper;

        public InventoryService()
        {
            _dbHelper = new DBHelper();
        }

        // ── READ: Kunin lahat ng items (Food at Medical) ─────────────
        // Polymorphism: Returns List<InventoryItem> pero ang laman ay
        // FoodItem o MedicalSupply depende sa item_type column.
        public List<InventoryItem> GetCurrentInventory()
        {
            var items = new List<InventoryItem>();

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"SELECT item_id, item_name, item_type, quantity,
                             expiration_date, dosage, is_prescription_required
                      FROM inventory_items
                      ORDER BY item_id",
                    conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string itemType = reader.GetString(2);

                    if (itemType == "Food")
                    {
                        items.Add(new FoodItem
                        {
                            ItemID = reader.GetInt32(0),
                            ItemName = reader.GetString(1),
                            Quantity = reader.GetInt32(3),
                            ExpirationDate = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                        });
                    }
                    else // Medical
                    {
                        items.Add(new MedicalSupply
                        {
                            ItemID = reader.GetInt32(0),
                            ItemName = reader.GetString(1),
                            Quantity = reader.GetInt32(3),
                            Dosage = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            IsPrescriptionRequired = !reader.IsDBNull(6) && reader.GetBoolean(6)
                        });
                    }
                }
            }

            return items;
        }

        // ── READ: Food items lang ────────────────────────────────────
        public List<FoodItem> GetFoodItems()
        {
            var items = new List<FoodItem>();

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"SELECT item_id, item_name, quantity, expiration_date
                      FROM inventory_items
                      WHERE item_type = 'Food'
                      ORDER BY item_id",
                    conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new FoodItem
                    {
                        ItemID = reader.GetInt32(0),
                        ItemName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        ExpirationDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3)
                    });
                }
            }

            return items;
        }

        // ── READ: Medical supplies lang ──────────────────────────────
        public List<MedicalSupply> GetMedicalSupplies()
        {
            var items = new List<MedicalSupply>();

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"SELECT item_id, item_name, quantity, dosage, is_prescription_required
                      FROM inventory_items
                      WHERE item_type = 'Medical'
                      ORDER BY item_id",
                    conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new MedicalSupply
                    {
                        ItemID = reader.GetInt32(0),
                        ItemName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        Dosage = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        IsPrescriptionRequired = !reader.IsDBNull(4) && reader.GetBoolean(4)
                    });
                }
            }

            return items;
        }

        // ── CREATE: Magdagdag ng FoodItem ────────────────────────────
        public bool AddFoodItem(FoodItem item)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"INSERT INTO inventory_items (item_name, item_type, quantity, expiration_date)
                      VALUES (@name, 'Food', @qty, @expDate)",
                    conn);

                cmd.Parameters.AddWithValue("@name", item.ItemName);
                cmd.Parameters.AddWithValue("@qty", item.Quantity);
                cmd.Parameters.AddWithValue("@expDate", (object?)item.ExpirationDate ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding food item: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        // ── CREATE: Magdagdag ng MedicalSupply ───────────────────────
        public bool AddMedicalSupply(MedicalSupply item)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"INSERT INTO inventory_items (item_name, item_type, quantity, dosage, is_prescription_required)
                      VALUES (@name, 'Medical', @qty, @dosage, @rx)",
                    conn);

                cmd.Parameters.AddWithValue("@name", item.ItemName);
                cmd.Parameters.AddWithValue("@qty", item.Quantity);
                cmd.Parameters.AddWithValue("@dosage", (object?)item.Dosage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@rx", item.IsPrescriptionRequired);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding medical supply: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        // ── UPDATE: I-update ang quantity ng item ────────────────────
        public bool UpdateQuantity(int itemId, int newQty)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "UPDATE inventory_items SET quantity = @qty, updated_at = NOW() WHERE item_id = @id",
                    conn);

                cmd.Parameters.AddWithValue("@qty", newQty);
                cmd.Parameters.AddWithValue("@id", itemId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error updating quantity: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        // ── DELETE: Tanggalin ang item ───────────────────────────────
        public bool DeleteItem(int itemId)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "DELETE FROM inventory_items WHERE item_id = @id",
                    conn);

                cmd.Parameters.AddWithValue("@id", itemId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error deleting item: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }
    }
}