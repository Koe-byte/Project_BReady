using System;
using System.IO;
using System.Windows;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace ProjectBReadyWPF.Database.DataAccess
{
    public class DBHelper
    {
        private readonly string connectionString = "";

        public DBHelper()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration config = builder.Build();
                connectionString = config.GetConnectionString("DefaultConnection") ?? "";
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Configuration file 'appsettings.json' is missing!\nPlease ensure you have this file in the project folder.", "Database Setup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    MessageBox.Show("Connected!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}