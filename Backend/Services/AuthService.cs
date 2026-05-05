using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _storedHash;

        public AuthService()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _storedHash = config.GetSection("Security")["AdminPinHash"] ?? "";
        }

        public bool ValidatePin(string inputPin)
        {
            if (string.IsNullOrEmpty(inputPin) || string.IsNullOrEmpty(_storedHash))
                return false;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPin));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                string inputHash = builder.ToString();

                return inputHash.Equals(_storedHash, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}