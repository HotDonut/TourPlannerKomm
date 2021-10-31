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
        public string Vehicle { get; set; }
        public int AvgSpeed { get; set; }
        public string People { get; set; }
        public int Breaks { get; set; }
        public int LinearDistance { get; set; }
        public Log(int id, int tourId, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance)
        {
            this.Id = id;
            this.TourId = tourId;
            this.DateTime = dateTime;
            this.Report = report;
            this.Distance = distance;
            this.TotalTime = totalTime;
            this.Rating = rating;
            this.Vehicle = vehicle;
            this.AvgSpeed = avgSpeed;
            this.People = people;
            this.Breaks = breaks;
            this.LinearDistance = linearDistance;
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
