using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ProjectBReady.Data
{
    public class DBHelper
    {
        private static readonly string connectionString =
            //alternative db path for jd: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Prog Projects\AOOP - Final Project\Project_BReady\Data\BReadyDB.mdf"";Integrated Security=True;Connect Timeout=30";
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\BReadyDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        // 1. Para sa SELECT (Pagkuha ng data) - Standard version
        public static DataTable GetData(string query)
        {
            return GetData(query, null); // Tinatawag nito yung overload sa baba
        }

        // 2. ITO ANG SOLUSYON SA ERROR MO: GetData na tumatanggap ng Parameters
        public static DataTable GetData(string query, Dictionary<string, object> parameters)
        {
            DataTable dt = new(); // Simplified 'new'
            try
            {
                using var conn = new SqlConnection(connectionString); // Simplified 'using'
                using var cmd = new SqlCommand(query, conn);

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                using var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Fetch Error: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // 3. Para sa INSERT, UPDATE, DELETE
        public static bool ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(query, conn);

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Action Error: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}