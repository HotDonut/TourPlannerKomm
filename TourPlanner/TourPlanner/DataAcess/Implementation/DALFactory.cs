using System;
using System.Configuration;
using System.Reflection;
using TourPlanner.DataAccess.Interfaces;

namespace TourPlanner.DataAccess.Implementation
{
    public class DALFactory
    {
        private static string _assemblyName;
        private static Assembly _dalAssembly;
        private static IDatabase _database;

        static DALFactory()
        {
            _assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            _dalAssembly = Assembly.Load(_assemblyName);
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
            string dataBaseClassName = _assemblyName + ".Database";
            Type dbClass = _dalAssembly.GetType(dataBaseClassName);

            return Activator.CreateInstance(dbClass, new object[] {connectionString}) as IDatabase;
        }

        public static ITourDAO CreateTourDAO()
        {
            string className = _assemblyName + ".TourPostgresDAO";
            Type tourType = _dalAssembly.GetType(className);
            return Activator.CreateInstance(tourType) as ITourDAO;
        }

        public static ILogDAO CreateTourLogDAO()
        {
            string className = _assemblyName + ".TourLogPostgresDAO";
            Type tourLogType = _dalAssembly.GetType(className);
            return Activator.CreateInstance(tourLogType) as ILogDAO;
        }
    }
}
