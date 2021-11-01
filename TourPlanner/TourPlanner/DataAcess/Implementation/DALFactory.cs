using System;
using System.Configuration;
using System.Reflection;
using TourPlanner.DataAccess.Interfaces;

namespace TourPlanner.DataAccess.Implementation
{
    public class DALFactory
    {
        private static IDatabase _database;


        static DALFactory()
        {

        }

        public static IDatabase GetDatabase()
        {
            if (_database == null)
            {
                _database = CreateDatabase();
            }

            return _database;
        }

        private static IDatabase CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgresSqlConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        private static IDatabase CreateDatabase(string connectionString)
        {
            IDatabase DataBase = new Database(connectionString);

            return DataBase;
        }

        public static ITourDAO CreateTourDAO()
        {
            ITourDAO TourDAO = new TourPostgresDAO();
            return TourDAO;
        }

        public static ILogDAO CreateTourLogDAO()
        {
            ILogDAO LogDAO = new LogPostgresDAO();
            return LogDAO;
        }
    }
}
