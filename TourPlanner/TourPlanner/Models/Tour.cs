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
                if (_imagePath == null || _imagePath.Equals(""))
                    return @".\..\..\..\Images\Icon\No_Image_Icon.png";

                if (File.Exists(_imagePath))
                    return _imagePath;

                return @".\..\..\..\Images\Icon\No_Image_Icon.png";
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
    }
}
