using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TourPlanner.Models;


namespace TourPlanner.Businesslayer
{
    public class JsonHandler : IJsonHandler
    {
        private static string _filepath;
        private static string _importFilePath;

        public JsonHandler()
        {
            _filepath = ConfigurationManager.AppSettings["JsonExportPath"];
            _importFilePath = ConfigurationManager.AppSettings["JsonImportFile"];
        }

        public JsonHandler(string filepath)
        {
            _filepath = filepath;
            _importFilePath = filepath;
        }

        public bool ExportData(IEnumerable<Tour> tours, IEnumerable<Log> logs)
        {

            JsonData data = new JsonData(tours, logs);
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            string finalPath = _filepath + "export.json";

            if(Directory.Exists(_filepath))
                File.WriteAllText(finalPath, json);

            return File.Exists(finalPath);
        }

        public JsonData ImportData()
        {
            if (File.Exists(_importFilePath))
            {
                StreamReader sr = new StreamReader(_importFilePath);
                string json = sr.ReadToEnd();
                sr.Close();
                return JsonConvert.DeserializeObject<JsonData>(json);
            }
            return new JsonData();
        }
    }
}
