using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;

namespace HospitalSystem
{
    /// <summary>
    /// Provides a MySQL database connection based on configuration.
    /// </summary>
    public class DbConnection
    {
        private readonly IConfiguration _configuration;

        public DbConnection(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public MySqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Chuỗi kết nối không được tìm thấy trong cấu hình.");
            }
            return new MySqlConnection(connectionString);
        }
    }
}