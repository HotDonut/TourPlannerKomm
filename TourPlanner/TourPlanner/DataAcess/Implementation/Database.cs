using System;
using Npgsql;
using System.Data;
using System.Data.Common;
using TourPlanner.DataAccess.Interfaces;

namespace TourPlanner.DataAccess.Implementation {
    public class Database : IDatabase
    {

        private string _connectionString;

        public Database(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public DbCommand CreateCommand(string genericCommandText)
        {
            return new NpgsqlCommand(genericCommandText);
        }

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            if (!command.Parameters.Contains(name))
            {
                return command.Parameters.Add(new NpgsqlParameter(name, type));
            }

            throw new ArgumentException($"Parameter {name} already exists.");
        }

        public void DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            int index = DeclareParameter(command, name, type);
            command.Parameters[index].Value = value;
        }

        public void SetParameter(DbCommand command, string name, object value)
        {
            if (command.Parameters.Contains(name))
            {
                command.Parameters[name].Value = value;
            }

            throw new ArgumentException($"Parameter {name} does not exists.");
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            DbConnection connection = CreateConnection();
            command.Connection = connection;
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteScalar(DbCommand command)
        {
            DbConnection connection = CreateConnection();
            command.Connection = connection;
            return Convert.ToInt32(command.ExecuteScalar());
        }

        private DbConnection CreateConnection()
        {
            DbConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
    }
}
