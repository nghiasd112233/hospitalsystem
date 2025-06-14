using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    /// <summary>
    /// Repository for managing TestRequest entities in the database.
    /// </summary>
    public class TestRequestRepository : BaseRepository
    {
        public TestRequestRepository(MySqlConnection connection) : base(connection)
        {
        }

        public void Add(TestRequest testRequest)
        {
            var query = @"INSERT INTO TestRequests (PatientId, DoctorId, TestName, Status, RequestDate)
                          VALUES (@PatientId, @DoctorId, @TestName, @Status, @RequestDate)";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@PatientId", testRequest.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", testRequest.DoctorId);
            cmd.Parameters.AddWithValue("@TestName", testRequest.TestName);
            cmd.Parameters.AddWithValue("@Status", testRequest.Status);
            cmd.Parameters.AddWithValue("@RequestDate", testRequest.RequestDate);
            ExecuteNonQuery(cmd);
        }

        public List<TestRequest> GetAll()
        {
            var query = "SELECT * FROM TestRequests";
            using var cmd = new MySqlCommand(query, _connection);
            return ExecuteQuery(cmd, reader => new TestRequest
            {
                Id = reader.GetInt32("Id"),
                PatientId = reader.GetInt32("PatientId"),
                DoctorId = reader.GetInt32("DoctorId"),
                TestName = reader.GetString("TestName"),
                Status = reader.GetString("Status"),
                RequestDate = reader.GetDateTime("RequestDate")
            });
        }

        public void Update(TestRequest testRequest)
        {
            var query = @"UPDATE TestRequests
                          SET PatientId=@PatientId, DoctorId=@DoctorId, TestName=@TestName, Status=@Status, RequestDate=@RequestDate
                          WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@PatientId", testRequest.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", testRequest.DoctorId);
            cmd.Parameters.AddWithValue("@TestName", testRequest.TestName);
            cmd.Parameters.AddWithValue("@Status", testRequest.Status);
            cmd.Parameters.AddWithValue("@RequestDate", testRequest.RequestDate);
            cmd.Parameters.AddWithValue("@Id", testRequest.Id);
            ExecuteNonQuery(cmd);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM TestRequests WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            ExecuteNonQuery(cmd);
        }

        public bool Exists(int id)
        {
            var query = "SELECT COUNT(*) FROM TestRequests WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }

        public bool PatientExists(int patientId)
        {
            var query = "SELECT COUNT(*) FROM Patients WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", patientId);
            return ExecuteScalar(cmd) > 0;
        }

        public bool DoctorExists(int doctorId)
        {
            var query = "SELECT COUNT(*) FROM Doctors WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", doctorId);
            return ExecuteScalar(cmd) > 0;
        }
    }
}