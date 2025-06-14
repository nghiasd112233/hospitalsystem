using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    /// <summary>
    /// Repository for managing Doctor entities in the database.
    /// </summary>
    public class DoctorRepository : BaseRepository
    {
        public DoctorRepository(MySqlConnection connection) : base(connection)
        {
        }

        public void Add(Doctor doctor)
        {
            var query = @"INSERT INTO Doctors (FullName, Specialty, Phone, Email)
                          VALUES (@FullName, @Specialty, @Phone, @Email)";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@FullName", doctor.FullName);
            cmd.Parameters.AddWithValue("@Specialty", string.IsNullOrEmpty(doctor.Specialty) ? DBNull.Value : doctor.Specialty);
            cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(doctor.Phone) ? DBNull.Value : doctor.Phone);
            cmd.Parameters.AddWithValue("@Email", doctor.Email); // ✅ Thêm dòng này
            ExecuteNonQuery(cmd);
        }

        public List<Doctor> GetAll()
        {
            var query = "SELECT * FROM Doctors";
            using var cmd = new MySqlCommand(query, _connection);
            return ExecuteQuery(cmd, reader => new Doctor
            {
                Id = reader.GetInt32("Id"),
                FullName = reader.GetString("FullName"),
                Specialty = reader.GetString("Specialty"),
                Phone = reader.GetString("Phone"),
                Email = reader.GetString("Email")
            });
        }

        public void Update(Doctor doctor)
        {
            var query = @"UPDATE Doctors
                          SET FullName=@FullName, Specialty=@Specialty, Phone=@Phone, Email=@Email
                          WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@FullName", doctor.FullName);
            cmd.Parameters.AddWithValue("@Specialty", string.IsNullOrEmpty(doctor.Specialty) ? DBNull.Value : doctor.Specialty);
            cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(doctor.Phone) ? DBNull.Value : doctor.Phone);
            cmd.Parameters.AddWithValue("@Email", doctor.Email); // ✅ Bổ sung dòng này
            cmd.Parameters.AddWithValue("@Id", doctor.Id);
            ExecuteNonQuery(cmd);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Doctors WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            ExecuteNonQuery(cmd);
        }

        public bool Exists(int id)
        {
            var query = "SELECT COUNT(*) FROM Doctors WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }

        public bool ExistsByPhone(string phone)
        {
            var query = "SELECT COUNT(*) FROM Doctors WHERE Phone=@Phone";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Phone", phone);
            return ExecuteScalar(cmd) > 0;
        }

        public bool ExistsByPhoneForOther(string phone, int id)
        {
            var query = "SELECT COUNT(*) FROM Doctors WHERE Phone=@Phone AND Id!=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }
    }
}
