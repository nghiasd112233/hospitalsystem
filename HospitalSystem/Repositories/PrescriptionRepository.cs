using HospitalSystem.Models;
       using MySql.Data.MySqlClient;
       using System;
       using System.Collections.Generic;

       namespace HospitalSystem.Repositories
       {
           public class PrescriptionRepository : BaseRepository
           {
               public PrescriptionRepository(MySqlConnection connection) : base(connection) { }

               public void Add(Prescription prescription)
               {
                   var query = @"INSERT INTO Prescriptions (AppointmentId, Medication, Dosage, Details)
                                 VALUES (@AppointmentId, @Medication, @Dosage, @Details)";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@AppointmentId", prescription.AppointmentId);
                   cmd.Parameters.AddWithValue("@Medication", prescription.Medication);
                   cmd.Parameters.AddWithValue("@Dosage", prescription.Dosage);
                   cmd.Parameters.AddWithValue("@Details", string.IsNullOrEmpty(prescription.Details) ? DBNull.Value : prescription.Details);
                   ExecuteNonQuery(cmd);
               }

               public void Update(Prescription prescription)
               {
                   var query = @"UPDATE Prescriptions
                                 SET AppointmentId = @AppointmentId,
                                     Medication = @Medication,
                                     Dosage = @Dosage,
                                     Details = @Details
                                 WHERE Id = @Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@AppointmentId", prescription.AppointmentId);
                   cmd.Parameters.AddWithValue("@Medication", prescription.Medication);
                   cmd.Parameters.AddWithValue("@Dosage", prescription.Dosage);
                   cmd.Parameters.AddWithValue("@Details", string.IsNullOrEmpty(prescription.Details) ? DBNull.Value : prescription.Details);
                   cmd.Parameters.AddWithValue("@Id", prescription.Id);
                   ExecuteNonQuery(cmd);
               }

               public void Delete(int id)
               {
                   var query = "DELETE FROM Prescriptions WHERE Id = @Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   ExecuteNonQuery(cmd);
               }

               public List<Prescription> GetAll()
               {
                   var query = "SELECT * FROM Prescriptions";
                   using var cmd = new MySqlCommand(query, _connection);
                   return ExecuteQuery(cmd, reader => new Prescription
                   {
                       Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32("Id"),
                       AppointmentId = reader.IsDBNull(reader.GetOrdinal("AppointmentId")) ? 0 : reader.GetInt32("AppointmentId"),
                       Medication = reader.IsDBNull(reader.GetOrdinal("Medication")) ? string.Empty : reader.GetString("Medication"),
                       Dosage = reader.IsDBNull(reader.GetOrdinal("Dosage")) ? string.Empty : reader.GetString("Dosage"),
                       Details = reader.IsDBNull(reader.GetOrdinal("Details")) ? string.Empty : reader.GetString("Details")
                   });
               }

               public bool Exists(int id)
               {
                   var query = "SELECT COUNT(*) FROM Prescriptions WHERE Id = @Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   return ExecuteScalar(cmd) > 0;
               }
           }
       }