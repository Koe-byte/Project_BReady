using System;
using System.Collections.Generic;
using ProjectBReadyWPF.Backend.Models.Facilities;
using ProjectBReadyWPF.Database.DataAccess;
using Npgsql;

using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Backend.Services
{
    public class ShelterService : IShelterService
    {
        private readonly DBHelper _dbHelper;

        public ShelterService()
        {
            _dbHelper = new DBHelper();
        }

        // ── READ: Kunin lahat ng shelter ──────────────────────────────
        public List<Shelter> GetAllShelters()
        {
            var shelters = new List<Shelter>();

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "SELECT shelter_id, shelter_name, max_capacity, current_occupancy, status FROM shelters ORDER BY shelter_id",
                    conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    shelters.Add(new Shelter
                    {
                        ShelterID = reader.GetInt32(0),
                        ShelterName = reader.GetString(1),
                        MaxCapacity = reader.GetInt32(2),
                        CurrentOccupancy = reader.GetInt32(3),
                        Status = reader.GetString(4)
                    });
                }
            }

            return shelters;
        }

        // ── READ: Kunin ang isang shelter by ID ──────────────────────
        public Shelter? GetShelterById(int shelterId)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new NpgsqlCommand(
                "SELECT shelter_id, shelter_name, max_capacity, current_occupancy, status FROM shelters WHERE shelter_id = @id",
                conn);

            cmd.Parameters.AddWithValue("@id", shelterId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Shelter
                {
                    ShelterID = reader.GetInt32(0),
                    ShelterName = reader.GetString(1),
                    MaxCapacity = reader.GetInt32(2),
                    CurrentOccupancy = reader.GetInt32(3),
                    Status = reader.GetString(4)
                };
            }

            return null;
        }

        // ── CREATE: Magdagdag ng bagong shelter ──────────────────────
        public bool AddShelter(Shelter shelter)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"INSERT INTO shelters (shelter_name, max_capacity, current_occupancy, status)
                      VALUES (@name, @maxCap, @curOcc, @status)",
                    conn);

                cmd.Parameters.AddWithValue("@name", shelter.ShelterName);
                cmd.Parameters.AddWithValue("@maxCap", shelter.MaxCapacity);
                cmd.Parameters.AddWithValue("@curOcc", shelter.CurrentOccupancy);
                cmd.Parameters.AddWithValue("@status", shelter.Status);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding shelter: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        // ── UPDATE: I-update ang occupancy ng shelter ────────────────
        public bool UpdateOccupancy(int shelterId, int newOccupancy)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "UPDATE shelters SET current_occupancy = @occ WHERE shelter_id = @id",
                    conn);

                cmd.Parameters.AddWithValue("@occ", newOccupancy);
                cmd.Parameters.AddWithValue("@id", shelterId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error updating occupancy: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        // ── UPDATE: I-update ang status ng shelter ───────────────────
        public bool UpdateStatus(int shelterId, string status)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "UPDATE shelters SET status = @status WHERE shelter_id = @id",
                    conn);

                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", shelterId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error updating status: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        // ── DELETE: Tanggalin ang shelter ─────────────────────────────
        public bool DeleteShelter(int shelterId)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "DELETE FROM shelters WHERE shelter_id = @id",
                    conn);

                cmd.Parameters.AddWithValue("@id", shelterId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error deleting shelter: {ex.Message}",
                    "Database Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }
    }
}