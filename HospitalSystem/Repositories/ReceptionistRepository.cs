using System;
       using System.Collections.Generic;
       using MySql.Data.MySqlClient;
       using HospitalSystem.Models;

       namespace HospitalSystem.Repositories
       {
           public class ReceptionistRepository : BaseRepository
           {
               public ReceptionistRepository(MySqlConnection connection) : base(connection) { }

               public void Add(Receptionist receptionist)
               {
                   var query = @"INSERT INTO Receptionists (FullName, Phone, Email)
                                 VALUES (@FullName, @Phone, @Email)";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@FullName", receptionist.FullName);
                   cmd.Parameters.AddWithValue("@Phone", receptionist.Phone);
                   cmd.Parameters.AddWithValue("@Email", receptionist.Email);
                   ExecuteNonQuery(cmd);
               }

               public List<Receptionist> GetAll()
               {
                   var query = "SELECT * FROM Receptionists";
                   using var cmd = new MySqlCommand(query, _connection);
                   return ExecuteQuery(cmd, reader => new Receptionist
                   {
                       Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32("Id"),
                       FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString("FullName"),
                       Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString("Phone"),
                       Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString("Email")
                   });
               }

               public void Update(Receptionist receptionist)
               {
                   var query = @"UPDATE Receptionists
                                 SET FullName = @FullName,
                                     Phone = @Phone,
                                     Email = @Email
                                 WHERE Id = @Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@FullName", receptionist.FullName);
                   cmd.Parameters.AddWithValue("@Phone", receptionist.Phone);
                   cmd.Parameters.AddWithValue("@Email", receptionist.Email);
                   cmd.Parameters.AddWithValue("@Id", receptionist.Id);
                   ExecuteNonQuery(cmd);
               }

               public void Delete(int id)
               {
                   var query = "DELETE FROM Receptionists WHERE Id = @Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   ExecuteNonQuery(cmd);
               }

               public bool Exists(int id)
               {
                   var query = "SELECT COUNT(*) FROM Receptionists WHERE Id = @Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   return ExecuteScalar(cmd) > 0;
               }

               public bool ExistsByPhone(string phone)
               {
                   var query = "SELECT COUNT(*) FROM Receptionists WHERE Phone = @Phone";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Phone", phone);
                   return ExecuteScalar(cmd) > 0;
               }

               public bool ExistsByPhoneForOther(string phone, int id)
               {
                   var query = "SELECT COUNT(*) FROM Receptionists WHERE Phone = @Phone AND Id != @Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Phone", phone);
                   cmd.Parameters.AddWithValue("@Id", id);
                   return ExecuteScalar(cmd) > 0;
               }
           }
       }