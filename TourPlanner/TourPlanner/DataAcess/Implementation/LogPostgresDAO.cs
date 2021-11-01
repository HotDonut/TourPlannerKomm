using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TourPlanner.Models;
using TourPlanner.DataAccess.Interfaces;

namespace TourPlanner.DataAccess.Implementation {
    class LogPostgresDAO : ILogDAO {

        private const string SqlFindById = "SELECT * FROM TourLog WHERE Id=@Id;";
        private const string SqlGetAllTourLogs = "SELECT * FROM TourLog;";
        private const string SqlFindByTour = "SELECT * FROM TourLog WHERE TourId=@TourId;";
        private const string SqlDeleteTourLog = "DELETE FROM TourLog WHERE Id=@Id;";
        private const string SqlEditTourLog = "UPDATE TourLog SET " +
                                              "DateTime=@DateTime, Report=@Report, Distance=@Distance, TotalTime=@TotalTime, Rating=@Rating, " +
                                              "Breaks=@Breaks, Weather=@Weather, FuelConsumption=@FuelConsumption, Passenger=@Passenger, Elevation=@Elevation " +
                                              "WHERE Id=@Id RETURNING Id;";
        private const string SqlInsertNewTourLog = "INSERT INTO TourLog (TourId, DateTime,Report,Distance,TotalTime, Rating, Breaks, Weather, FuelConsumption, Passenger, Elevation) " +
                                                   "VALUES (@TourId, @DateTime, @Report, @Distance, @TotalTime, @Rating, @Breaks, @Weather, @FuelConsumption, @Passenger, @Elevation) " +
                                                   "RETURNING Id;";

        private IDatabase _database;
        private ITourDAO _tourDAO;

        public LogPostgresDAO()
        {
            this._database = DALFactory.GetDatabase();
            this._tourDAO = DALFactory.CreateTourDAO();
        }

        public LogPostgresDAO(IDatabase database, ITourDAO tourDAO)
        {
            this._database = database;
            this._tourDAO = tourDAO;
        }

        public Log FindById(int tourLogId)
        {
            DbCommand findCommand = _database.CreateCommand(SqlFindById);
            _database.DefineParameter(findCommand, "@Id", DbType.Int32, tourLogId);

            IEnumerable<Log> logs = QueryTourLogsFromDatabase(findCommand);
            return logs.FirstOrDefault();
        }

        public Log AddNewTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating, int breaks, string weather, int fuelConsumption, string passenger, int elevation)
        {
            DbCommand insertCommand = _database.CreateCommand(SqlInsertNewTourLog);
            _database.DefineParameter(insertCommand, "@TourId", DbType.Int32, tour.Id);
            _database.DefineParameter(insertCommand, "@DateTime", DbType.String, dateTime);
            _database.DefineParameter(insertCommand, "@Report", DbType.String, report);
            _database.DefineParameter(insertCommand, "@Distance", DbType.Int32, distance);
            _database.DefineParameter(insertCommand, "@TotalTime", DbType.String, totalTime);
            _database.DefineParameter(insertCommand, "@Rating", DbType.Int32, rating);
            _database.DefineParameter(insertCommand, "@Breaks", DbType.Int32, breaks);
            _database.DefineParameter(insertCommand, "@Weather", DbType.String, weather);
            _database.DefineParameter(insertCommand, "@FuelConsumption", DbType.Int32, fuelConsumption);
            _database.DefineParameter(insertCommand, "@Passenger", DbType.String, passenger);
            _database.DefineParameter(insertCommand, "@Elevation", DbType.Int32, elevation);

            return FindById(_database.ExecuteScalar(insertCommand));
        }

        public void DeleteTourLog(Log tourLog)
        {
            DbCommand deleteCommand = _database.CreateCommand(SqlDeleteTourLog);
            _database.DefineParameter(deleteCommand, "@Id", DbType.Int32, tourLog.Id);

            _database.ExecuteScalar(deleteCommand);
        }

        public Log EditTourLog(Log tourLog, string dateTime, string report, int distance, string totalTime, int rating, int breaks, string weather, int fuelConsumption, string passenger, int elevation)
        {
            DbCommand editCommand = _database.CreateCommand(SqlEditTourLog);
            _database.DefineParameter(editCommand, "@DateTime", DbType.String, dateTime);
            _database.DefineParameter(editCommand, "@Report", DbType.String, report);
            _database.DefineParameter(editCommand, "@Distance", DbType.Int32, distance);
            _database.DefineParameter(editCommand, "@TotalTime", DbType.String, totalTime);
            _database.DefineParameter(editCommand, "@Rating", DbType.Int32, rating);
            _database.DefineParameter(editCommand, "@Breaks", DbType.Int32, breaks);
            _database.DefineParameter(editCommand, "@Weather", DbType.String, weather);
            _database.DefineParameter(editCommand, "@FuelConsumption", DbType.Int32, fuelConsumption);
            _database.DefineParameter(editCommand, "@Passenger", DbType.String, passenger);
            _database.DefineParameter(editCommand, "@Elevation", DbType.Int32, elevation);
            _database.DefineParameter(editCommand, "@Id", DbType.Int32, tourLog.Id);

            return FindById(_database.ExecuteScalar(editCommand));
        }

        public IEnumerable<Log> GetTourLogs(Tour tour)
        {
            return QueryTourLogsFromTour(tour);
        }

        public IEnumerable<Log> GetAllLogs()
        {
            DbCommand getLogsCommand = _database.CreateCommand(SqlGetAllTourLogs);
            return QueryTourLogsFromDatabase(getLogsCommand);
        }

        private IEnumerable<Log> QueryTourLogsFromTour(Tour tour)
        {
            DbCommand getLogsCommand = _database.CreateCommand(SqlFindByTour);
            _database.DefineParameter(getLogsCommand, "@TourId", DbType.Int32, tour.Id);

            return QueryTourLogsFromDatabase(getLogsCommand);
        }

        private IEnumerable<Log> QueryTourLogsFromDatabase(DbCommand command)
        {
            List<Log> tourLogs = new List<Log>();

            using (IDataReader reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourLogs.Add(new Log(
                        (int)reader["Id"],
                        (int)reader["TourId"],
                        (string)reader["DateTime"],
                        (string)reader["Report"],
                        (int)reader["Distance"],
                        (string)reader["TotalTime"],
                        (int)reader["Rating"],
                        (int)reader["Breaks"],
                        (string)reader["Weather"],
                        (int)reader["FuelConsumption"],
                        (string)reader["Passenger"],
                        (int)reader["Elevation"]));
                }
            }

            return tourLogs;
        }
    }
}
