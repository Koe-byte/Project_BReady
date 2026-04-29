using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectBReady.Data
{
    public class DBHelper
    {
        // Pinagsama at nilinis na connection string gamit ang |DataDirectory| 
        // para gumana ito sa lahat ng laptop ng mga ka-team mo.
        private static string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\BReadyDB.mdf;Integrated Security=True;Connect Timeout=30";

        // Para sa SELECT (Pagkuha ng data para i-display sa DataGridView)
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
                            da.Fill(dt);
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

        // Para sa INSERT, UPDATE, DELETE
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
                        return rowsAffected > 0;
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