using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectBReady.Data
{
    public class DBHelper
    {
        // 1. Ang nag-iisang Connection String! Dito lang tayo magpapalit kung kailangan.
<<<<<<< Updated upstream
        private static string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\BReadyDB.mdf;Integrated Security=True;Connect Timeout=30";

=======
        private static string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Prog Projects\AOOP - Final Project\Project_BReady\Data\BReadyDB.mdf"";Integrated Security=True;Connect Timeout=30";
>>>>>>> Stashed changes
        // 2. Para sa SELECT (Pagkuha ng data para i-display sa DataGridView)
        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt); // Auto-open at auto-close na ito ng connection!
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Fetch Error: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // 3. Para sa INSERT, UPDATE, DELETE (Pang dagdag/bawas inventory o update occupancy)
        public static bool ExecuteQuery(string query)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // True kapag naging successful ang command
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Action Error: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}