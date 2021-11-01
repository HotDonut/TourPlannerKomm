using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccess.Interfaces {
    public interface ILogDAO {
        Log FindById(int tourLogId);
        Log AddNewTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating, int breaks, string weather, int fuelConsumption, string passenger, int elevation);
        void DeleteTourLog(Log tourLog);
        Log EditTourLog(Log tourLog, string dateTime, string report, int distance, string totalTime, int rating, int breaks, string weather, int fuelConsumption, string passenger, int elevation);
        IEnumerable<Log> GetTourLogs(Tour tour);
        IEnumerable<Log> GetAllLogs();
    }
}
