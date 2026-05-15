using System;
using System.Collections.Generic;
using ProjectBReadyWPF.Database.DataAccess;
using Npgsql;

using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Backend.Services
{
    public class InventorySlice
    {
        public string Label { get; set; } = "";
        public int Quantity { get; set; }
        public string ColorHex { get; set; } = "#2563EB";
    }

    public class DashboardAlert
    {
        public string Message { get; set; } = "";
        public string Severity { get; set; } = "Warning";
    }
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

                // Query 5: Inventory breakdown by type (pie chart)
                using (var cmd = new NpgsqlCommand(
                    @"SELECT item_type, COALESCE(SUM(quantity), 0)
                      FROM inventory_items
                      GROUP BY item_type
                      ORDER BY item_type",
                    conn))
                {
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var type = reader.GetString(0);
                        stats.InventoryBreakdown.Add(new InventorySlice
                        {
                            Label = type,
                            Quantity = reader.GetInt32(1),
                            ColorHex = type switch
                            {
                                "Food" => "#1E3A8A",
                                "Medical" => "#38BDF8",
                                "Water" => "#F97316",
                                _ => "#FDBA74"
                            }
                        });
                    }
                }

                // Query 6: Food / medical totals
                using (var cmd = new NpgsqlCommand(
                    @"SELECT
                        COALESCE(SUM(quantity) FILTER (WHERE item_type = 'Food'), 0),
                        COALESCE(SUM(quantity) FILTER (WHERE item_type = 'Medical'), 0)
                      FROM inventory_items",
                    conn))
                {
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        stats.TotalFoodQuantity = reader.GetInt32(0);
                        stats.TotalMedicalQuantity = reader.GetInt32(1);
                    }
                }
            }

            stats.OccupancyPercent = stats.TotalCapacity > 0
                ? Math.Round((double)stats.TotalEvacuees / stats.TotalCapacity * 100, 1)
                : 0;

            stats.FoodSupplyStatus = ResolveFoodStatus(stats);
            stats.MedicalSupplyStatus = ResolveMedicalStatus(stats);
            stats.Alerts = BuildAlerts(stats);
            stats.OccupancyTrend = BuildOccupancyTrend(stats.OccupancyPercent);

            return stats;
        }

        private static string ResolveFoodStatus(DashboardStats stats)
        {
            if (stats.ExpiringFoodCount > 0 || stats.TotalFoodQuantity < 500)
                return "CRITICAL";
            if (stats.TotalFoodQuantity < 1500)
                return "LOW";
            return "ADEQUATE";
        }

        private static string ResolveMedicalStatus(DashboardStats stats)
        {
            if (stats.TotalMedicalQuantity < 200)
                return "CRITICAL";
            if (stats.TotalMedicalQuantity < 500)
                return "LOW";
            return "ADEQUATE";
        }

        private static List<DashboardAlert> BuildAlerts(DashboardStats stats)
        {
            var alerts = new List<DashboardAlert>();

            if (stats.OccupancyPercent >= 90)
            {
                alerts.Add(new DashboardAlert
                {
                    Severity = "Critical",
                    Message = $"System-wide shelter occupancy at {stats.OccupancyPercent:F0}% — nearing maximum capacity."
                });
            }

            if (stats.ExpiringFoodCount > 0)
            {
                alerts.Add(new DashboardAlert
                {
                    Severity = "Critical",
                    Message = $"{stats.ExpiringFoodCount} food item(s) expiring within 7 days — review inventory immediately."
                });
            }

            if (stats.FoodSupplyStatus == "CRITICAL" || stats.FoodSupplyStatus == "LOW")
            {
                alerts.Add(new DashboardAlert
                {
                    Severity = stats.FoodSupplyStatus == "CRITICAL" ? "Critical" : "Warning",
                    Message = $"Food supply status: {stats.FoodSupplyStatus} ({stats.TotalFoodQuantity:N0} units on hand)."
                });
            }

            if (stats.MedicalSupplyStatus == "CRITICAL" || stats.MedicalSupplyStatus == "LOW")
            {
                alerts.Add(new DashboardAlert
                {
                    Severity = stats.MedicalSupplyStatus == "CRITICAL" ? "Critical" : "Warning",
                    Message = $"Medical supply status: {stats.MedicalSupplyStatus} ({stats.TotalMedicalQuantity:N0} units on hand)."
                });
            }

            if (alerts.Count == 0)
            {
                alerts.Add(new DashboardAlert
                {
                    Severity = "Info",
                    Message = "No critical logistics alerts at this time. All monitored systems within normal thresholds."
                });
            }

            return alerts;
        }

        private static List<double> BuildOccupancyTrend(double currentPercent)
        {
            // Until historical snapshots exist, show current occupancy for each day.
            var trend = new List<double>();
            for (int i = 0; i < 7; i++)
                trend.Add(currentPercent);
            return trend;
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
        public int TotalFoodQuantity { get; set; }
        public int TotalMedicalQuantity { get; set; }
        public double OccupancyPercent { get; set; }
        public string FoodSupplyStatus { get; set; } = "ADEQUATE";
        public string MedicalSupplyStatus { get; set; } = "ADEQUATE";
        public List<InventorySlice> InventoryBreakdown { get; set; } = new();
        public List<DashboardAlert> Alerts { get; set; } = new();
        public List<double> OccupancyTrend { get; set; } = new();
    }
}
