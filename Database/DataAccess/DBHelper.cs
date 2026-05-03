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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection") ?? "";
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