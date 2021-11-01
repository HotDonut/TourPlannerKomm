using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TourPlanner.DataAccess.Interfaces;
using TourPlanner.DataAccess.Implementation;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    internal class TourPlannerFactoryImpl : ITourPlannerFactory
    {

        public IEnumerable<Tour> GetTours()
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            return tourDao.GetTours();
        }

        public IEnumerable<Log> GetTourLogs(Tour tour)
        {
            ILogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.GetTourLogs(tour);
        }

        public IEnumerable<Tour> FindTour(IEnumerable<Tour> tours, IEnumerable<Tour> found, string fieldName, string searchArg, bool caseSensitive = false)
        {
            searchArg = caseSensitive ? searchArg : searchArg.ToLower();
            return found.Concat(tours.Where(x => x.GetFieldValue(fieldName, caseSensitive).Contains(searchArg)));
        }

        public IEnumerable<Log> FindTourLog(IEnumerable<Log> tourLogs, IEnumerable<Log> found, string fieldName, string searchArg, bool caseSensitive = false)
        {
            searchArg = caseSensitive ? searchArg : searchArg.ToLower();
            return found.Concat(tourLogs.Where(x => x.GetFieldValue(fieldName, caseSensitive).Contains(searchArg)));
        }

        public IEnumerable<Tour> Search(string searchArg, bool caseSensitive = false)
        {
            IEnumerable<Tour> tours = GetTours();
            IEnumerable<Tour> found = new List<Tour>();

            if (searchArg != null)
            {
                var enumerable = tours.ToList();
                found = FindTour(enumerable, found, "Name", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "FromLocation", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "ToLocation", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "Description", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "Distance", searchArg, caseSensitive);
            }

            return found.Distinct();

        }

        public IEnumerable<Log> SearchTourLog(Tour tour, string searchArg, bool caseSensitive = false)
        {
            IEnumerable<Log> tourLogs = GetTourLogs(tour);
            IEnumerable<Log> found = new List<Log>();

            if (searchArg != null)
            {
                var enumerable = tourLogs.ToList();
                found = FindTourLog(enumerable, found, "DateTime", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Report", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Distance", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "TotalTime", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Rating", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Vehicle", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "AvgSpeed", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "People", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Breaks", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "LinearDistance", searchArg, caseSensitive);
            }

            return found.Distinct();
        }

        public Tour AddTour(string tourName, string tourDescription, string tourFromLocation, string tourToLocation)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            MapQuest mapQuest = new MapQuest(tourFromLocation, tourToLocation);
            string imagePath = mapQuest.LoadImage();
            int tourDistance = mapQuest.GetRouteDistance();
            return tourDao.AddNewTour(tourName, tourFromLocation, tourToLocation, tourDescription, tourDistance, imagePath);
        }

        public Tour EditTour(Tour tour, string tourName, string tourDescription, string tourFromLocation, string tourToLocation)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            MapQuest mapQuest = new MapQuest(tourFromLocation, tourToLocation);
            string imagePath = mapQuest.LoadImage();
            int tourDistance = mapQuest.GetRouteDistance();
            return tourDao.EditTour(tour, tourName, tourDescription, tourFromLocation, tourToLocation, tourDistance, imagePath);
        }

        public void DeleteTour(Tour tour, string imagePath)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();

            tourDao.DeleteTour(tour);
        }

        public Tour CopyTour(Tour tour)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            MapQuest mapQuest = new MapQuest(tour.FromLocation, tour.ToLocation);
            return tourDao.AddNewTour(tour.Name, tour.FromLocation, tour.ToLocation, tour.Description, tour.Distance, mapQuest.LoadImage());
        }

        public Log AddTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating, int breaks, string weather, int fuelConsumption, string passenger, int elevation)
        {
            ILogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.AddNewTourLog(tour, dateTime, report, distance, totalTime, rating, breaks, weather, fuelConsumption, passenger, elevation);
        }

        public Log EditTourLog(Log tourLog, string dateTime, string report, int distance, string totalTime, int rating, int breaks, string weather, int fuelConsumption, string passenger, int elevation)
        {
            ILogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.EditTourLog(tourLog, dateTime, report, distance, totalTime, rating, breaks, weather, fuelConsumption, passenger, elevation);
        }

        public void DeleteTourLog(Log tourLog)
        {
            ILogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            tourLogDao.DeleteTourLog(tourLog);
        }

        public Log CopyTourLog(Tour tour, Log tourLog)
        {
            ILogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.AddNewTourLog(tour, tourLog.DateTime, tourLog.Report, tourLog.Distance, tourLog.TotalTime, tourLog.Rating, tourLog.Breaks, tourLog.Weather, tourLog.FuelConsumption, tourLog.Passenger, tourLog.Elevation);
        }
    }
}
