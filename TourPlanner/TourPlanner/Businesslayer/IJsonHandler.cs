using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface IJsonHandler
    {
        public bool ExportData(IEnumerable<Tour> tours, IEnumerable<Log> logs);
        public JsonData ImportData();
    }
}
