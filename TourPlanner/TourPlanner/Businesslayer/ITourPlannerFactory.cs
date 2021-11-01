using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourPlannerFactory
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<Log> GetTourLogs(Tour tour);
        IEnumerable<Tour> Search(string searchArg, bool caseSensitive = false);
        IEnumerable<Log> SearchTourLog(Tour tour, string searchArg, bool caseSensitive = false);
        Tour AddTour(string tourName, string tourDescription, string tourFromLocation, string tourToLocation);
        Log AddTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
        void DeleteTour(Tour tour, string imagePath);
        void DeleteTourLog(Log tourLog);
        Tour EditTour(Tour tour, string tourName, string tourDescription, string tourFromLocation, string tourToLocation);
        Log EditTourLog(Log tourLog, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
    }
}
