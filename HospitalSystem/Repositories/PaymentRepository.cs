using System;
       using System.Collections.Generic;
       using MySql.Data.MySqlClient;
       using HospitalSystem.Models;

       namespace HospitalSystem.Repositories
       {
           /// <summary>
           /// Repository for managing Payment entities in the database.
           /// </summary>
           public class PaymentRepository : BaseRepository
           {
               public PaymentRepository(MySqlConnection connection) : base(connection)
               {
               }

               public void Add(Payment payment)
               {
                   var query = @"INSERT INTO Payments (PatientId, Amount, PaymentDate, PaymentMethod)
                                 VALUES (@PatientId, @Amount, @PaymentDate, @PaymentMethod)";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@PatientId", payment.PatientId);
                   cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                   cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                   cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                   ExecuteNonQuery(cmd);
               }

               public List<Payment> GetAll()
               {
                   var query = "SELECT * FROM Payments";
                   using var cmd = new MySqlCommand(query, _connection);
                   return ExecuteQuery(cmd, reader => new Payment
                   {
                       Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32("Id"),
                       PatientId = reader.IsDBNull(reader.GetOrdinal("PatientId")) ? 0 : reader.GetInt32("PatientId"),
                       Amount = reader.IsDBNull(reader.GetOrdinal("Amount")) ? 0m : reader.GetDecimal("Amount"),
                       PaymentDate = reader.IsDBNull(reader.GetOrdinal("PaymentDate")) ? DateTime.MinValue : reader.GetDateTime("PaymentDate"),
                       PaymentMethod = reader.IsDBNull(reader.GetOrdinal("PaymentMethod")) ? string.Empty : reader.GetString("PaymentMethod")
                   });
               }

               public void Update(Payment payment)
               {
                   var query = @"UPDATE Payments
                                 SET PatientId=@PatientId, Amount=@Amount, PaymentDate=@PaymentDate, PaymentMethod=@PaymentMethod
                                 WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@PatientId", payment.PatientId);
                   cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                   cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                   cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                   cmd.Parameters.AddWithValue("@Id", payment.Id);
                   ExecuteNonQuery(cmd);
               }

               public void Delete(int id)
               {
                   var query = "DELETE FROM Payments WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   ExecuteNonQuery(cmd);
               }

               public bool Exists(int id)
               {
                   var query = "SELECT COUNT(*) FROM Payments WHERE Id=@Id";
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
           }
       }    