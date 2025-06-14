using System;
       using System.Collections.Generic;
       using MySql.Data.MySqlClient;
       using HospitalSystem.Models;

       namespace HospitalSystem.Repositories
       {
           /// <summary>
           /// Repository for managing Appointment entities in the database.
           /// </summary>
           public class AppointmentRepository : BaseRepository
           {
               public AppointmentRepository(MySqlConnection connection) : base(connection)
               {
               }

               public void Add(Appointment appointment)
               {
                   var query = @"INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Status)
                                 VALUES (@PatientId, @DoctorId, @AppointmentDate, @Status)";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                   cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                   cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                   cmd.Parameters.AddWithValue("@Status", appointment.Status);
                   ExecuteNonQuery(cmd);
               }

               public List<Appointment> GetAll()
               {
                   var query = "SELECT * FROM Appointments";
                   using var cmd = new MySqlCommand(query, _connection);
                   return ExecuteQuery(cmd, reader => new Appointment
                   {
                       Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32("Id"),
                       PatientId = reader.IsDBNull(reader.GetOrdinal("PatientId")) ? 0 : reader.GetInt32("PatientId"),
                       DoctorId = reader.IsDBNull(reader.GetOrdinal("DoctorId")) ? 0 : reader.GetInt32("DoctorId"),
                       AppointmentDate = reader.IsDBNull(reader.GetOrdinal("AppointmentDate")) ? DateTime.MinValue : reader.GetDateTime("AppointmentDate"),
                       Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? string.Empty : reader.GetString("Status")
                   });
               }

               public void Update(Appointment appointment)
               {
                   var query = @"UPDATE Appointments
                                 SET PatientId=@PatientId, DoctorId=@DoctorId, AppointmentDate=@AppointmentDate, Status=@Status
                                 WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                   cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                   cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                   cmd.Parameters.AddWithValue("@Status", appointment.Status);
                   cmd.Parameters.AddWithValue("@Id", appointment.Id);
                   ExecuteNonQuery(cmd);
               }

               public void Delete(int id)
               {
                   var query = "DELETE FROM Appointments WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   ExecuteNonQuery(cmd);
               }

               public bool Exists(int id)
               {
                   var query = "SELECT COUNT(*) FROM Appointments WHERE Id=@Id";
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

               public bool AppointmentExists(int appointmentId)
               {
                   var query = "SELECT COUNT(*) FROM Appointments WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", appointmentId);
                   return ExecuteScalar(cmd) > 0;
               }
           }
       }