using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    /// <summary>
    /// Repository for managing Admin entities in the database.
    /// </summary>
    public class AdminRepository : BaseRepository
    {
        public AdminRepository(MySqlConnection connection) : base(connection)
        {
        }

        public void Add(Admin admin)
        {
            var query = @"INSERT INTO Admins (FullName, Phone, Email)
                          VALUES (@FullName, @Phone, @Email)";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@FullName", admin.FullName);
            cmd.Parameters.AddWithValue("@Phone", admin.Phone);
            cmd.Parameters.AddWithValue("@Email", admin.Email); // ✅ Bổ sung Email
            ExecuteNonQuery(cmd);
        }

        public List<Admin> GetAll()
        {
            var query = "SELECT * FROM Admins";
            using var cmd = new MySqlCommand(query, _connection);
            return ExecuteQuery(cmd, reader => new Admin
            {
                Id = reader.GetInt32("Id"),
                FullName = reader.GetString("FullName"),
                Phone = reader.GetString("Phone"),
                Email = reader.GetString("Email") // ✅ Bắt buộc
            });
        }

        public void Update(Admin admin)
        {
            var query = @"UPDATE Admins
                          SET FullName=@FullName, Phone=@Phone, Email=@Email
                          WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@FullName", admin.FullName);
            cmd.Parameters.AddWithValue("@Phone", admin.Phone);
            cmd.Parameters.AddWithValue("@Email", admin.Email); // ✅ Bổ sung Email
            cmd.Parameters.AddWithValue("@Id", admin.Id);
            ExecuteNonQuery(cmd);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Admins WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            ExecuteNonQuery(cmd);
        }

        public bool Exists(int id)
        {
            var query = "SELECT COUNT(*) FROM Admins WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }

        public bool ExistsByPhone(string phone)
        {
            var query = "SELECT COUNT(*) FROM Admins WHERE Phone=@Phone";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Phone", phone);
            return ExecuteScalar(cmd) > 0;
        }

        public bool ExistsByPhoneForOther(string phone, int id)
        {
            var query = "SELECT COUNT(*) FROM Admins WHERE Phone=@Phone AND Id!=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }
    }
}
