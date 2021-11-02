using System;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using TourPlanner.Models;
using TourPlanner.DataAccess.Interfaces;
using TourPlanner.DataAccess.Implementation;
using IronPdf;

namespace TourPlanner.Businesslayer
{
    public class PDFGenerator : IPDFGenerator
    {
        private static string _filepath;

        public PDFGenerator()
        {
            _filepath = ConfigurationManager.AppSettings["PdfExportPath"];
        }

        public PDFGenerator(string filepath)
        {
            _filepath = filepath;
        }

        public bool GenerateReport(IEnumerable<Tour> tours)
        {
            string buildPDF = "";
            int logSumDistance = 0;

            foreach (Tour tour in tours)
            {
                buildPDF += $"<h1 style=\"font-family:Courier;\"> Report of Tour: {tour.Name}</h1>" +
                                  $"<h2 style=\"font-family:Courier;\"> Tour description: {tour.Description}</h2>" +
                                  $"<h2 style=\"font-family:Courier;\">Route information: {tour.Distance}</h2>" +
                                  $"<h2 style=\"font-family:Courier;\">Start: {tour.FromLocation}</h2>" +
                                  $"<h2 style=\"font-family:Courier;\">End: {tour.ToLocation}</h2>" +
                                  $"<img src='{tour.ImagePath}'>" +
                                  $"<h1>LOGS:</h1>" +
                                  $"<ol>";


                ILogDAO tourLogDao = DALFactory.CreateTourLogDAO();
                List<Log> logs = (List<Log>)tourLogDao.GetTourLogs(tour);

                foreach (var log in logs)
                {
                    buildPDF += $"<li>" +
                                $"<h3> Log date: {log.DateTime}</h3>" +
                                $"<p> Report: {log.Report}</p>" +
                                $"<p> Distance: {log.Distance}</p>" +
                                $"<p> TotalTime: {log.TotalTime}</p>" +
                                $"<p> Rating: {log.Rating}</p>" +
                                $"<p> Breaks: {log.Breaks}</p>" +
                                $"<p> Weather: {log.Weather}</p>" +
                                $"<p> FuelConsumption: {log.FuelConsumption}</p>" +
                                $"<p> Passenger: {log.Passenger}</p>" +
                                $"<p> Elevation: {log.Elevation }</p>" +
                                $"</li>";

                    logSumDistance += log.Distance;
                }

                buildPDF += "</ol>";

            }

            buildPDF += $"<h3>Insgesamt zurückgelegte Distanz: {logSumDistance}km<h3>";

            var renderer = new IronPdf.HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(buildPDF);


            string uniqueFileName = GetUniqueFilename();
            string fullFileName = _filepath + uniqueFileName;

            if (pdf.TrySaveAs(fullFileName))
            {
                return true;
            }

            return false;
        }

        private string GetUniqueFilename()
        {
            Random rand = new Random();
            var imageName = Convert.ToString(rand.Next(999999999));
            imageName += ".pdf";

            while (File.Exists(_filepath + @"\" + imageName) == true)
            {
                imageName = Convert.ToString(rand.Next(999999999));
                imageName += ".pdf";
            }

            return imageName;
        }

    }

}
