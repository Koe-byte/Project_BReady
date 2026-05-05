using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace ProjectBReady.Data
{
    public class DBHelper
    {
        // .db file nasa Data\ folder ng project — visible sa git at Solution Explorer
        private static string dbPath = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                     .Parent.Parent.Parent.FullName,
            "Data", "BReadyDB.db");

        private static string connectionString => $"Data Source={dbPath}";

        // ── INITIALIZE — creates tables + seeds data kung bagong DB ──────
        public static void InitializeDB()
        {
            bool isNew = !File.Exists(dbPath);

            using var conn = new SqliteConnection(connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS SHELTERS (
                    ShelterID        INTEGER PRIMARY KEY AUTOINCREMENT,
                    ShelterName      TEXT    NOT NULL,
                    MaxCapacity      INTEGER NOT NULL,
                    CurrentOccupancy INTEGER NOT NULL DEFAULT 0,
                    Status           TEXT    NOT NULL DEFAULT 'Open',
                    DateAdded        TEXT    NOT NULL DEFAULT (datetime('now'))
                );

                CREATE TABLE IF NOT EXISTS INVENTORY (
                    ItemID                 INTEGER PRIMARY KEY AUTOINCREMENT,
                    ItemName               TEXT    NOT NULL,
                    Quantity               INTEGER NOT NULL DEFAULT 0,
                    ItemType               TEXT    NOT NULL,
                    ExpirationDate         TEXT    NULL,
                    Dosage                 TEXT    NULL,
                    IsPrescriptionRequired INTEGER NULL
                );

                CREATE TABLE IF NOT EXISTS DISPATCH_LOGS (
                    LogID        INTEGER PRIMARY KEY AUTOINCREMENT,
                    ItemID       INTEGER NOT NULL REFERENCES INVENTORY(ItemID),
                    ShelterID    INTEGER NOT NULL REFERENCES SHELTERS(ShelterID),
                    Quantity     INTEGER NOT NULL,
                    DispatchedAt TEXT    NOT NULL DEFAULT (datetime('now')),
                    DispatchedBy TEXT    NOT NULL
                );

                CREATE TABLE IF NOT EXISTS SETTINGS (
                    SettingKey   TEXT PRIMARY KEY,
                    SettingValue TEXT NOT NULL
                );
            ";
            cmd.ExecuteNonQuery();

            if (isNew)
            {
                cmd.CommandText = @"
                    INSERT OR IGNORE INTO SHELTERS (ShelterName, MaxCapacity, CurrentOccupancy, Status)
                    VALUES
                        ('Barangay Hall Gymnasium', 200, 0, 'Open'),
                        ('San Isidro Elementary School', 150, 0, 'Open'),
                        ('Community Center Bldg A', 100, 0, 'Open');

                    INSERT OR IGNORE INTO INVENTORY (ItemName, Quantity, ItemType, ExpirationDate)
                    VALUES
                        ('Canned Goods (Sardines)', 500, 'Food', '2026-12-31'),
                        ('Rice (50kg sacks)', 100, 'Food', '2025-06-30');

                    INSERT OR IGNORE INTO INVENTORY (ItemName, Quantity, ItemType, Dosage, IsPrescriptionRequired)
                    VALUES
                        ('Paracetamol 500mg', 1000, 'Medical', '500mg', 0),
                        ('First Aid Kit', 50, 'Medical', 'N/A', 0);

                    INSERT OR IGNORE INTO SETTINGS (SettingKey, SettingValue)
                    VALUES ('AdminPIN', '1234');
                ";
                cmd.ExecuteNonQuery();
            }
        }

        // ── SELECT — walang parameters ────────────────────────────────────
        public static DataTable GetData(string query)
        {
            return GetData(query, null);
        }

        // ── SELECT — may parameters ───────────────────────────────────────
        public static DataTable GetData(string query, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using var conn = new SqliteConnection(connectionString);
                conn.Open();
                using var cmd = new SqliteCommand(query, conn);

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);

                using var reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Fetch Error: {ex.Message}",
                    "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return dt;
        }

        // ── INSERT / UPDATE / DELETE — walang parameters ──────────────────
        public static bool ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, null);
        }

        // ── INSERT / UPDATE / DELETE — may parameters ─────────────────────
        public static bool ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using var conn = new SqliteConnection(connectionString);
                conn.Open();
                using var cmd = new SqliteCommand(query, conn);

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Action Error: {ex.Message}",
                    "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}