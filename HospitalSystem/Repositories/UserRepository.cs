using System;
       using System.Collections.Generic;
       using MySql.Data.MySqlClient;
       using HospitalSystem.Models;

       namespace HospitalSystem.Repositories
       {
           /// <summary>
           /// Repository for managing User entities (e.g., Nurse) in the database.
           /// </summary>
           public class UserRepository : BaseRepository
           {
               public UserRepository(MySqlConnection connection) : base(connection)
               {
               }

               public void Add(User user)
               {
                   var query = @"INSERT INTO Users (FullName, Role, Email)
                                 VALUES (@FullName, @Role, @Email)";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@FullName", user.FullName);
                   cmd.Parameters.AddWithValue("@Role", user.Role);
                   cmd.Parameters.AddWithValue("@Email", user.Email);
                   ExecuteNonQuery(cmd);
               }

               public List<User> GetAll()
               {
                   var query = "SELECT * FROM Users";
                   using var cmd = new MySqlCommand(query, _connection);
                   return ExecuteQuery(cmd, reader => new User
                   {
                       Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32("Id"),
                       FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString("FullName"),
                       Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? string.Empty : reader.GetString("Role"),
                       Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString("Email")
                   });
               }

               public void Update(User user)
               {
                   var query = @"UPDATE Users
                                 SET FullName=@FullName, Role=@Role, Email=@Email
                                 WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@FullName", user.FullName);
                   cmd.Parameters.AddWithValue("@Role", user.Role);
                   cmd.Parameters.AddWithValue("@Email", user.Email);
                   cmd.Parameters.AddWithValue("@Id", user.Id);
                   ExecuteNonQuery(cmd);
               }

               public void Delete(int id)
               {
                   var query = "DELETE FROM Users WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   ExecuteNonQuery(cmd);
               }

               public bool Exists(int id)
               {
                   var query = "SELECT COUNT(*) FROM Users WHERE Id=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Id", id);
                   return ExecuteScalar(cmd) > 0;
               }

               public bool ExistsByEmail(string email)
               {
                   var query = "SELECT COUNT(*) FROM Users WHERE Email=@Email";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Email", email);
                   return ExecuteScalar(cmd) > 0;
               }

               public bool ExistsByEmailForOther(string email, int id)
               {
                   var query = "SELECT COUNT(*) FROM Users WHERE Email=@Email AND Id!=@Id";
                   using var cmd = new MySqlCommand(query, _connection);
                   cmd.Parameters.AddWithValue("@Email", email);
                   cmd.Parameters.AddWithValue("@Id", id);
                   return ExecuteScalar(cmd) > 0;
               }
           }
       }