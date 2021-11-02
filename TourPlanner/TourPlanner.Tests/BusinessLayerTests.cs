using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using TourPlanner.Businesslayer;
using TourPlanner.Models;
using System.Configuration;

namespace TourPlanner.Tests
{
    public class BusinessLayerTests
    {
        [Test]
        public void JsonHandler_Export1TourAnd1Log_True()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\";

            IJsonHandler handler = new JsonHandler(expectedFileName);

            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123, "path");
            Log log = new Log(1, 1, "05-12-2021 18:23", "Report", 123, "11:03", 2, 3, "Clear", 23, "Rudi", 100);

            List<Tour> tourList = new List<Tour>() { tour };
            List<Log> logList = new List<Log>() { log };

            //act
            bool erg = handler.ExportData(tourList, logList);

            //assert
            Assert.True(erg);
            Assert.True(File.Exists(expectedFileName + "export.json"));

            //cleanup
            if (File.Exists(expectedFileName + "export.json"))
                File.Delete(expectedFileName + "export.json");
        }

        [Test]
        public void JsonHandler_ImportJsonWith1Tour1Log_ShouldReturnJsonDataObjectContainingToursAndLogs()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\save.json";

            IJsonHandler handler = new JsonHandler(expectedFileName);

            //act
            JsonData erg = handler.ImportData();

            //assert
            Assert.True(erg.Tours.ToList().Count == 1);
            Assert.True(erg.Logs.ToList().Count == 1); ;
        }

        [Test]
        public void JsonHandler_ImportJsonWith5Tours0Log_ShouldReturnJsonDataObjectContainingToursAndLogs()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\5Tours0Logs.json";

            IJsonHandler handler = new JsonHandler(expectedFileName);

            //act
            JsonData erg = handler.ImportData();

            //assert
            Assert.True(erg.Tours.ToList().Count == 5);
            Assert.IsEmpty(erg.Logs.ToList());
        }

        [Test]
        public void JsonHandler_ExportJsonWith5Tours0Log_ShouldReturnJsonDataObjectContainingToursAndLogs()
        {
            //arrange
            var expectedFileName = @"..\..\..\TestFiles\";
            List<Tour> tourList = new List<Tour>();
            List<Log> logList = new List<Log>();

            for (int i = 1; i <= 5; i++)
            {
                tourList.Add(new Tour(i, "Name", "Description", "Wien", "Berlin", 123, "path"));
            }

            IJsonHandler handler = new JsonHandler(expectedFileName);

            //act
            bool erg = handler.ExportData(tourList, logList);

            //assert
            Assert.True(erg);
            Assert.True(File.Exists(expectedFileName + "export.json"));

            //cleanup
            if (File.Exists(expectedFileName + "export.json"))
                File.Delete(expectedFileName + "export.json");
        }

        [Test]
        public void JsonHandler_ExporttJsonWithInvalidPath_False()
        {
            //arrange
            var expectedFileName = @"..\..\..\WrongPath\";

            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123, "path");
            Log log = new Log(1, 1, "05-12-2021 18:23", "Report", 123, "11:03", 2, 3, "Clear", 23, "Rudi", 100);

            IJsonHandler handler = new JsonHandler(expectedFileName);

            List<Tour> tourList = new List<Tour>() { tour };
            List<Log> logList = new List<Log>() { log };

            //act
            bool erg = handler.ExportData(tourList, logList);

            //assert
            Assert.False(File.Exists(expectedFileName + "export.json"));
        }

        [Test]
        public void JsonHandler_ImportJsonWithInvalidPath_EmptyJsonData()
        {
            //arrange
            var expectedFileName = @"..\..\..\WrongPath\save.json";

            IJsonHandler handler = new JsonHandler(expectedFileName);

            //act
            JsonData erg = handler.ImportData();

            //assert
            Assert.IsEmpty(erg.Tours.ToList());
            Assert.IsEmpty(erg.Logs.ToList());
        }

        [Test]
        public void PDFGenerator__PDFGenerationEmptyDataWithValidPath_ShouldGenerate1PdfFile()
        {
            //arrange
            string expectedFileName = @"..\..\..\TestFiles\";

            IPDFGenerator generator = new PDFGenerator(expectedFileName);

            List<Tour> tour = new List<Tour>();

            //act
            bool erg = generator.GenerateReport(tour);

            //assert
            Assert.True(Directory.GetFiles(expectedFileName, "*.pdf").Count() == 1);
            Assert.True(erg);

            //cleanup
            foreach (string fileName in Directory.GetFiles(expectedFileName, "*.pdf"))
            {
                File.Delete(fileName);
            }
        }

        [Test]
        public void PDFGenerator__PDFGenerationEmptyDataWithInValidPath_ShouldGenerateNoPdfFile()
        {
            //arrange
            string expectedFileName = @"..\..\..\WrongPath\";

            IPDFGenerator generator = new PDFGenerator(expectedFileName);

            List<Tour> tour = new List<Tour>();

            //act
            bool erg = generator.GenerateReport(tour);

            //assert
            Assert.False(erg);

        }
    }
}