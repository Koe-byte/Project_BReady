using System.Data;
using ProjectBReady.Data;

namespace ProjectBReady.Services
{
    public static class UserService
    {
        /// <summary>
        /// Checks the system AdminPIN stored in SETTINGS.
        /// Returns true if the entered PIN matches.
        /// </summary>
        public static bool ValidatePIN(string enteredPIN)
        {
            DataTable dt = DBHelper.GetData(
                "SELECT SettingValue FROM SETTINGS WHERE SettingKey = 'AdminPIN'");
            if (dt.Rows.Count == 0) return false;
            return dt.Rows[0]["SettingValue"].ToString() == enteredPIN;
        }

        /// <summary>Returns the first official's UserID and FullName for the dashboard greeting.</summary>
        public static DataRow GetDefaultOfficial()
        {
            DataTable dt = DBHelper.GetData(
                "SELECT UserID, FullName FROM USERS WHERE Role = 'Official' LIMIT 1");
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static DataTable GetAllUsers()
        {
            return DBHelper.GetData(
                "SELECT UserID, FullName, Role, IsReadOnly FROM dbo.USERS ORDER BY Role, FullName");
        }

        public static bool UpdatePIN(string newPIN)
        {
            return DBHelper.ExecuteQuery(
                $"UPDATE SETTINGS SET SettingValue = '{newPIN}' WHERE SettingKey = 'AdminPIN'");
        }
    }
}