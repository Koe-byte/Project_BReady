using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows.Forms;

namespace ProjectBReady.Data
{
    public class DBHelper
    {
        // Ang .db file ay nasa same folder ng .exe — kasama sa repo
        private static string dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "BReadyDB.db");

        private static string connectionString =>
            $"Data Source={dbPath}";

        // ── Initialize DB — creates tables + seeds data if DB is new ──────
        public static void InitializeDB()
        {
            bool isNew = !File.Exists(dbPath);

            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            // Create tables
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS SHELTERS (
                    ShelterID        TEXT PRIMARY KEY,
                    ShelterName      TEXT NOT NULL,
                    MaxCapacity      INTEGER NOT NULL,
                    CurrentOccupancy INTEGER NOT NULL DEFAULT 0,
                    Status           TEXT NOT NULL DEFAULT 'Open',
                    DateAdded        TEXT NOT NULL DEFAULT (datetime('now'))
                );

                CREATE TABLE IF NOT EXISTS USERS (
                    UserID     TEXT PRIMARY KEY,
                    FullName   TEXT NOT NULL,
                    Role       TEXT NOT NULL,
                    IsReadOnly INTEGER NOT NULL DEFAULT 1
                );

                CREATE TABLE IF NOT EXISTS INVENTORY (
                    ItemID                 TEXT PRIMARY KEY,
                    ItemName               TEXT NOT NULL,
                    Quantity               INTEGER NOT NULL DEFAULT 0,
                    ItemType               TEXT NOT NULL,
                    ExpirationDate         TEXT NULL,
                    Dosage                 TEXT NULL,
                    IsPrescriptionRequired INTEGER NULL
                );

                CREATE TABLE IF NOT EXISTS DISPATCH_LOGS (
                    LogID        INTEGER PRIMARY KEY AUTOINCREMENT,
                    ItemID       TEXT NOT NULL REFERENCES INVENTORY(ItemID),
                    ShelterID    TEXT NOT NULL REFERENCES SHELTERS(ShelterID),
                    Quantity     INTEGER NOT NULL,
                    DispatchedAt TEXT NOT NULL DEFAULT (datetime('now')),
                    DispatchedBy TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS SETTINGS (
                    SettingKey   TEXT PRIMARY KEY,
                    SettingValue TEXT NOT NULL
                );
            ";
            cmd.ExecuteNonQuery();

            // Seed only if DB is brand new
            if (isNew)
            {
                cmd.CommandText = @"
                    INSERT OR IGNORE INTO SHELTERS (ShelterID, ShelterName, MaxCapacity, CurrentOccupancy, Status)
                    VALUES
                        ('SH-001', 'Barangay Hall Gymnasium', 200, 0, 'Open'),
                        ('SH-002', 'San Isidro Elementary School', 150, 0, 'Open'),
                        ('SH-003', 'Community Center Bldg A', 100, 0, 'Open');

                    INSERT OR IGNORE INTO USERS (UserID, FullName, Role, IsReadOnly)
                    VALUES
                        ('OFF-001', 'Barangay Captain', 'Official', 0),
                        ('OFF-002', 'Relief Coordinator', 'Official', 0),
                        ('RES-001', 'Juan dela Cruz', 'Resident', 1);

                    INSERT OR IGNORE INTO INVENTORY (ItemID, ItemName, Quantity, ItemType, ExpirationDate)
                    VALUES
                        ('FOOD-001', 'Canned Goods (Sardines)', 500, 'Food', '2026-12-31'),
                        ('FOOD-002', 'Rice (50kg sacks)', 100, 'Food', '2025-06-30');

                    INSERT OR IGNORE INTO INVENTORY (ItemID, ItemName, Quantity, ItemType, Dosage, IsPrescriptionRequired)
                    VALUES
                        ('MED-001', 'Paracetamol 500mg', 1000, 'Medical', '500mg', 0),
                        ('MED-002', 'First Aid Kit', 50, 'Medical', 'N/A', 0);

                    INSERT OR IGNORE INTO SETTINGS (SettingKey, SettingValue)
                    VALUES ('AdminPIN', '1234');
                ";
                cmd.ExecuteNonQuery();
            }
        }

        // ── SELECT — returns DataTable ─────────────────────────────────────
        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using var conn = new SqliteConnection(connectionString);
                conn.Open();
                using var cmd = new SqliteCommand(query, conn);
                using var reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Fetch Error: " + ex.Message,
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // ── INSERT / UPDATE / DELETE ───────────────────────────────────────
        public static bool ExecuteQuery(string query)
        {
            try
            {
                using var conn = new SqliteConnection(connectionString);
                conn.Open();
                using var cmd = new SqliteCommand(query, conn);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Action Error: " + ex.Message,
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}