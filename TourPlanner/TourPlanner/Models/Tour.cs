using System.IO;

namespace TourPlanner.Models
{
    public class Tour
    {
        private string _imagePath;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public int Distance { get; set; }
        public string ImagePath
        {
            get
            {
                if (File.Exists(_imagePath))
                    return _imagePath;

                return "";
            }
            set => _imagePath = value;
        }

        public Tour(int id, string name, string description, string from, string to, int distance, string imagePath)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.FromLocation = from;
            this.ToLocation = to;
            this.Distance = distance;
            this.ImagePath = imagePath;
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
