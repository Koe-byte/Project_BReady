using System;
using ProjectBReadyWPF.Database.DataAccess;
using Npgsql;

using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Backend.Services
{
    /// <summary>
    /// Service para sa Dashboard stat cards — aggregated queries.
    /// Isa lang ang DB call sa GetDashboardStats() para mabilis ang load.
    /// </summary>
    public class DashboardService : IDashboardService
    {
        private readonly DBHelper _dbHelper;

        public DashboardService()
        {
            _dbHelper = new DBHelper();
        }

        // ── Aggregated stats para sa Dashboard stat cards ────────────
        public DashboardStats GetDashboardStats()
        {
            var stats = new DashboardStats();

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();

                // Query 1: Shelter stats
                using (var cmd = new NpgsqlCommand(
                    @"SELECT
                        COALESCE(SUM(current_occupancy), 0) AS total_evacuees,
                        COALESCE(SUM(max_capacity), 0) AS total_capacity,
                        COUNT(*) AS total_shelters,
                        COUNT(*) FILTER (WHERE status = 'Open') AS open_shelters
                      FROM shelters",
                    conn))
                {
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        stats.TotalEvacuees = reader.GetInt32(0);
                        stats.TotalCapacity = reader.GetInt32(1);
                        stats.TotalShelters = reader.GetInt32(2);
                        stats.OpenShelters = reader.GetInt32(3);
                    }
                }

                // Query 2: Inventory stats
                using (var cmd = new NpgsqlCommand(
                    @"SELECT
                        COALESCE(SUM(quantity), 0) AS total_items,
                        COUNT(DISTINCT item_type) AS item_types
                      FROM inventory_items",
                    conn))
                {
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        stats.TotalReliefItems = reader.GetInt32(0);
                        stats.ItemTypes = reader.GetInt32(1);
                    }
                }

                // Query 3: Dispatch count
                using (var cmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM dispatch_logs",
                    conn))
                {
                    stats.DispatchCount = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Query 4: Expiring food items (within 7 days)
                using (var cmd = new NpgsqlCommand(
                    @"SELECT COUNT(*) FROM inventory_items
                      WHERE item_type = 'Food'
                        AND expiration_date IS NOT NULL
                        AND expiration_date <= CURRENT_DATE + INTERVAL '7 days'",
                    conn))
                {
                    stats.ExpiringFoodCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return stats;
        }
    }

    /// <summary>
    /// DTO para sa aggregated dashboard numbers.
    /// </summary>
    public class DashboardStats
    {
        public int TotalEvacuees { get; set; }
        public int TotalCapacity { get; set; }
        public int TotalShelters { get; set; }
        public int OpenShelters { get; set; }
        public int TotalReliefItems { get; set; }
        public int ItemTypes { get; set; }
        public int DispatchCount { get; set; }
        public int ExpiringFoodCount { get; set; }
    }
}
