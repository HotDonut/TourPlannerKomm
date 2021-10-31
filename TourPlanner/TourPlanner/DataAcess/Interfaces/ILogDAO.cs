using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccess.Interfaces {
    public interface ILogDAO {
        Log FindById(int tourLogId);
        Log AddNewTourLog(Tour logTour,string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
        void DeleteTourLog(Log tourLog);
        Log EditTourLog(Log tourLog, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
        IEnumerable<Log> GetTourLogs(Tour tour);
        IEnumerable<Log> GetAllLogs();
    }
}
