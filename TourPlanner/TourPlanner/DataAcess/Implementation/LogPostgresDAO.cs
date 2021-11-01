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
        private const string SqlFindByTour = "SELECT * FROM TourLogWHERE TourId=@TourId;";
        private const string SqlDeleteTourLog = "DELETE FROM TourLog WHERE Id=@Id;";
        private const string SqlEditTourLog = "UPDATE TourLog SET " +
                                              "DateTime=@DateTime, Report=@Report, Distance=@Distance, TotalTime=@TotalTime, Rating=@Rating, " +
                                              "Vehicle=@Vehicle, AvgSpeed=@AvgSpeed, People=@People, Breaks=@Breaks, LinearDistance=@LinearDistance " +
                                              "WHERE Id=@Id RETURNING Id;";
        private const string SqlInsertNewTourLog = "INSERT INTO TourLog (TourId, DateTime,Report,Distance,TotalTime, Rating, Vehicle, AvgSpeed, People, Breaks, LinearDistance) " +
                                                   "VALUES (@TourId, @DateTime, @Report, @Distance, @TotalTime, @Rating, @Vehicle, @AvgSpeed, @People, @Breaks, @LinearDistance) " +
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

        public Log AddNewTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance)
        {
            DbCommand insertCommand = _database.CreateCommand(SqlInsertNewTourLog);
            _database.DefineParameter(insertCommand, "@TourId", DbType.Int32, tour.Id);
            _database.DefineParameter(insertCommand, "@DateTime", DbType.String, dateTime);
            _database.DefineParameter(insertCommand, "@Report", DbType.String, report);
            _database.DefineParameter(insertCommand, "@Distance", DbType.Int32, distance);
            _database.DefineParameter(insertCommand, "@TotalTime", DbType.String, totalTime);
            _database.DefineParameter(insertCommand, "@Rating", DbType.Int32, rating);
            _database.DefineParameter(insertCommand, "@Vehicle", DbType.String, vehicle);
            _database.DefineParameter(insertCommand, "@AvgSpeed", DbType.Int32, avgSpeed);
            _database.DefineParameter(insertCommand, "@People", DbType.String, people);
            _database.DefineParameter(insertCommand, "@Breaks", DbType.Int32, breaks);
            _database.DefineParameter(insertCommand, "@LinearDistance", DbType.Int32, linearDistance);

            return FindById(_database.ExecuteScalar(insertCommand));
        }

        public void DeleteTourLog(Log tourLog)
        {
            DbCommand deleteCommand = _database.CreateCommand(SqlDeleteTourLog);
            _database.DefineParameter(deleteCommand, "@Id", DbType.Int32, tourLog.Id);

            _database.ExecuteScalar(deleteCommand);
        }

        public Log EditTourLog(Log tourLog, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance)
        {
            DbCommand editCommand = _database.CreateCommand(SqlEditTourLog);
            _database.DefineParameter(editCommand, "@DateTime", DbType.String, dateTime);
            _database.DefineParameter(editCommand, "@Report", DbType.String, report);
            _database.DefineParameter(editCommand, "@Distance", DbType.Int32, distance);
            _database.DefineParameter(editCommand, "@TotalTime", DbType.String, totalTime);
            _database.DefineParameter(editCommand, "@Rating", DbType.Int32, rating);
            _database.DefineParameter(editCommand, "@Vehicle", DbType.String, vehicle);
            _database.DefineParameter(editCommand, "@AvgSpeed", DbType.Int32, avgSpeed);
            _database.DefineParameter(editCommand, "@People", DbType.String, people);
            _database.DefineParameter(editCommand, "@Breaks", DbType.Int32, breaks);
            _database.DefineParameter(editCommand, "@LinearDistance", DbType.Int32, linearDistance);
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
                        (string)reader["Vehicle"],
                        (int)reader["AvgSpeed"],
                        (string)reader["People"],
                        (int)reader["Breaks"],
                        (int)reader["LinearDistance"]));
                }
            }

            return tourLogs;
        }
    }
}
