using HospitalSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HospitalSystem.Repositories
{
    public class MedicalRecordRepository : BaseRepository
    {
        public MedicalRecordRepository(MySqlConnection connection) : base(connection)
        {
        }

        public void Add(MedicalRecord record)
        {
            var query = @"INSERT INTO MedicalRecords (PatientId, DoctorId, Diagnosis, Treatment, RecordDate)
                          VALUES (@PatientId, @DoctorId, @Diagnosis, @Treatment, @RecordDate)";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@PatientId", record.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", record.DoctorId);
            cmd.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
            cmd.Parameters.AddWithValue("@Treatment", record.Treatment); // ✅ Thêm dòng này
            cmd.Parameters.AddWithValue("@RecordDate", record.RecordDate);

            ExecuteNonQuery(cmd);
        }

        public void Update(MedicalRecord record)
        {
            var query = @"UPDATE MedicalRecords 
                          SET PatientId=@PatientId, DoctorId=@DoctorId, Diagnosis=@Diagnosis, Treatment=@Treatment, RecordDate=@RecordDate 
                          WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", record.Id);
            cmd.Parameters.AddWithValue("@PatientId", record.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", record.DoctorId);
            cmd.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
            cmd.Parameters.AddWithValue("@Treatment", record.Treatment); // ✅ Thêm dòng này
            cmd.Parameters.AddWithValue("@RecordDate", record.RecordDate);

            ExecuteNonQuery(cmd);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM MedicalRecords WHERE Id=@Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            ExecuteNonQuery(cmd);
        }

        public List<MedicalRecord> GetAll()
        {
            var query = "SELECT * FROM MedicalRecords";
            using var cmd = new MySqlCommand(query, _connection);

            return ExecuteQuery(cmd, reader => new MedicalRecord
            {
                Id = reader.GetInt32("Id"),
                PatientId = reader.GetInt32("PatientId"),
                DoctorId = reader.GetInt32("DoctorId"),
                Diagnosis = reader.GetString("Diagnosis"),
                Treatment = reader.GetString("Treatment"), // ✅ BẮT BUỘC
                RecordDate = reader.GetDateTime("RecordDate")
            });
        }

        public bool Exists(int id)
        {
            var query = "SELECT COUNT(*) FROM MedicalRecords WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteScalar(cmd) > 0;
        }

        public bool PatientExists(int patientId)
        {
            var query = "SELECT COUNT(*) FROM Patients WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", patientId);
            return ExecuteScalar(cmd) > 0;
        }

        public bool DoctorExists(int doctorId)
        {
            var query = "SELECT COUNT(*) FROM Doctors WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@Id", doctorId);
            return ExecuteScalar(cmd) > 0;
        }
    }
}
