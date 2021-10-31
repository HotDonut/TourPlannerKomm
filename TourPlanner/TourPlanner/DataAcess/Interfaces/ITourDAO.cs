using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccess.Interfaces {
    public interface ITourDAO
    {
        Tour FindById(int tourId);
        Tour AddNewTour(string tourName, string tourFromLocation, string tourToLocation, string tourDescription, int tourDistance, string tourImagePath);
        void DeleteTour(Tour tour);
        Tour EditTour(Tour tour, string tourName, string tourFromLocation, string tourToLocation, string tourDescription, int tourDistance, string tourImagePath);
        IEnumerable<Tour> GetTours();
    }
}
