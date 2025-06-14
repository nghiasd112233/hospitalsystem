using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    /// <summary>
    /// Repository for managing Patient entities in the database.
    /// </summary>
    public class PatientRepository : BaseRepository
    {
        public PatientRepository(MySqlConnection connection) : base(connection) { }

        public void Add(Patient patient)
        {
            var query = @"INSERT INTO Patients (FullName, BirthDate, Gender, Phone)
                          VALUES (@FullName, @BirthDate, @Gender, @Phone)";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@FullName", patient.FullName);
            cmd.Parameters.AddWithValue("@BirthDate", patient.BirthDate);
            cmd.Parameters.AddWithValue("@Gender", patient.Gender);
            cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(patient.Phone) ? DBNull.Value : patient.Phone);
            ExecuteNonQuery(cmd);
        }

        public List<Patient> GetAll()
{
    var query = "SELECT * FROM Patients";
    using var cmd = new MySqlCommand(query, _connection);
    return ExecuteQuery(cmd, reader => new Patient
    {
        Id = Convert.ToInt32(reader["Id"]), // hoặc reader.GetInt32("Id") nếu chắc chắn không null
        FullName = reader.GetString("FullName"),
        BirthDate = reader.GetDateTime("BirthDate"),
        Gender = reader.GetString("Gender"),
        Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString("Phone") // ✅ Sửa đúng
    });
}


        public void Update(Patient patient)
        {
            var query = @"UPDATE Patients
                          SET FullName=@FullName, BirthDate=@BirthDate, Gender=@Gender, Phone=@Phone
                          WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@FullName", patient.FullName);
            cmd.Parameters.AddWithValue("@BirthDate", patient.BirthDate);
            cmd.Parameters.AddWithValue("@Gender", patient.Gender);
            cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(patient.Phone) ? DBNull.Value : patient.Phone);
            cmd.Parameters.AddWithValue("@Id", patient.Id);
            ExecuteNonQuery(cmd);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Patients WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            ExecuteNonQuery(cmd);
        }

        public bool Exists(int id)
        {
            var query = "SELECT COUNT(*) FROM Patients WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }

        public bool ExistsByPhone(string phone)
        {
            var query = "SELECT COUNT(*) FROM Patients WHERE Phone = @Phone";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Phone", phone);
            return ExecuteScalar(cmd) > 0;
        }

        public bool ExistsByPhoneForOther(string phone, int id)
        {
            var query = "SELECT COUNT(*) FROM Patients WHERE Phone = @Phone AND Id != @Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }
    }
}