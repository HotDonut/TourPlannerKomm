using System;
using NUnit.Framework;
using TourPlanner.Models;


namespace TourPlanner.Tests
{
    public class ModelsTest
    {
        [Test]
        public void TourHasImageTest_TourHasNullObjectAsPath_ShouldReturnFalse()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123, null);
            //act
            string nameValue = tour.GetFieldValue("Name", true);
            //assert
            Assert.True(nameValue.Equals(tour.Name));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetFieldValueNameLowerCase_ShouldReturnNameValueLowerCase()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123, "");
            //act
            string nameValue = tour.GetFieldValue("Name", false);
            //assert
            Assert.True(nameValue.Equals(tour.Name.ToLower()));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetFieldValueDistanceCaseSensitive_ShouldReturnDistanceValue()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123, "");
            //act
            string distanceValue = tour.GetFieldValue("Distance", true);
            //assert
            Assert.True(distanceValue.Equals(tour.Distance.ToString()));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetFieldValueDistanceLowerCase_ShouldReturnDistanceValue()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123, "");
            //act
            string distanceValue = tour.GetFieldValue("Distance", false);
            //assert
            Assert.True(distanceValue.Equals(tour.Distance.ToString()));
        }

        [Test]
        public void TourGetFieldValueTest_TourGetUnknownField_ShouldReturnEmptyString()
        {
            //arrange
            Tour tour = new Tour(1, "Name", "Description", "Wien", "Berlin", 123, "");
            //act
            string reportValue = tour.GetFieldValue("Report", true);
            //assert
            Assert.True(reportValue.Equals(""));
        }

        [Test]
        public void LogGetFieldValueTest_TourLogGetFieldValueReportCaseSensitive_ShouldReturnReportValue()
        {
            //arrange
            Log log = new Log(1, 1, "05-12-2021 18:23", "Report", 123, "11:03", 2, 3, "Clear", 23, "Rudi", 100);
            //act
            string reportValue = log.GetFieldValue("Report", true);
            //assert
            Assert.True(reportValue.Equals(log.Report));
        }

        [Test]
        public void LogGetFieldValueTest_TourLogGetFieldValueReportLowerCase_ShouldReturnReportValueLowerCase()
        {
            //arrange
            Log log = new Log(1, 1, "05-12-2021 18:23", "Report", 123, "11:03", 2, 3, "Clear", 23, "Rudi", 100);
            //act
            string reportValue = log.GetFieldValue("Report", false);
            //assert
            Assert.True(reportValue.Equals(log.Report.ToLower()));
        }

        [Test]
        public void LogGetFieldValueTest_TourLogGetFieldValueDistanceCaseSensitive_ShouldReturnDistanceValue()
        {
            //arrange
            Log log = new Log(1, 1, "05-12-2021 18:23", "Report", 123, "11:03", 2, 3, "Clear", 23, "Rudi", 100);
            //act
            string distanceValue = log.GetFieldValue("Distance", true);
            //assert
            Assert.True(distanceValue.Equals(log.Distance.ToString()));
        }

        [Test]
        public void LogGetFieldValueTest_TourLogGetFieldValueDistanceLowerCase_ShouldReturnDistanceValue()
        {
            //arrange
            Log log = new Log(1, 1, "05-12-2021 18:23", "Report", 123, "11:03", 2, 3, "Clear", 23, "Rudi", 100);
            //act
            string distanceValue = log.GetFieldValue("Distance", false);
            //assert
            Assert.True(distanceValue.Equals(log.Distance.ToString()));
        }

        [Test]
        public void LogGetFieldValueTest_TourLogGetUnknownField_ShouldReturnExceptionNullReference()
        {
            //arrange
            Log log = new Log(1, 1, "05-12-2021 18:23", "Report", 123, "11:03", 2, 3, "Clear", 23, "Rudi", 100);
            //act
            string nameValue = log.GetFieldValue("Name");
            //assert
            Assert.True(nameValue.Equals(""));
        }
    }
}
