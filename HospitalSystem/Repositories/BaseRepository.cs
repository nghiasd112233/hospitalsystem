using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HospitalSystem.Repositories
{
    /// <summary>
    /// Base class for database operations, handling connection and query execution.
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly MySqlConnection _connection;

        protected BaseRepository(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        protected void ExecuteNonQuery(MySqlCommand cmd)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu: {ex.Message}", ex);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }
        }

        protected List<T> ExecuteQuery<T>(MySqlCommand cmd, Func<MySqlDataReader, T> mapper)
        {
            var list = new List<T>();
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(mapper(reader));
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu: {ex.Message}", ex);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }
            return list;
        }

        protected int ExecuteScalar(MySqlCommand cmd)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu: {ex.Message}", ex);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }
        }
    }
}