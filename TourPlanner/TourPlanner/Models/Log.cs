using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string DateTime { get; set; }
        public string Report { get; set; }
        public int Distance { get; set; }
        public string TotalTime { get; set; }
        public int Rating { get; set; }
        public int Breaks { get; set; }
        public string Weather { get; set; }
        public int FuelConsumption { get; set; }
        public string Passenger { get; set; }
        public int Elevation { get; set; }
        public Log(int id, int tourId, string dateTime, string report, int distance, string totalTime, int rating, int breaks, string weather, int fuelConsumption, string passenger, int elevation)
        {
            this.Id = id;
            this.TourId = tourId;
            this.DateTime = dateTime;
            this.Report = report;
            this.Distance = distance;
            this.TotalTime = totalTime;
            this.Rating = rating;
            this.Breaks = breaks;
            this.Weather = weather;
            this.FuelConsumption = fuelConsumption;
            this.Passenger = passenger;
            this.Elevation = elevation;
        }

        public string GetFieldValue(string fieldName, bool caseSensitive = false)
        {
            var ergObj = this.GetType().GetProperty(fieldName)?.GetValue(this, null);

            if (ergObj == null)
                return "";

            string erg = ergObj.ToString();
            return caseSensitive ? erg : erg.ToLower();
        }
    }
}
