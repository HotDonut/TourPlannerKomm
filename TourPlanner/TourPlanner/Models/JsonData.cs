using System.Collections.Generic;

namespace TourPlanner.Models {
    public class JsonData {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<Log> Logs { get; set; }
        
        public JsonData()
        {
            this.Tours = new List<Tour>();
            this.Logs = new List<Log>();
        }

        public JsonData(IEnumerable<Tour> tours, IEnumerable<Log> logs)
        {
            this.Tours = tours;
            this.Logs = logs;
        }
    }
}
