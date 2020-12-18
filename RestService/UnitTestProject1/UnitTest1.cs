using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestService;
using RestService.Controllers;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateEmptyMeasure()
        {
            Measurement x = new Measurement();
            Assert.AreNotEqual(null, x);
        }

        [TestMethod]
        public void CreateMeasureWithData()
        {
            Measurement x = new Measurement(1,22,30,502, DateTime.Today);

            Assert.IsTrue(x.ID == 1 && x.Humidity == 30 && x.Pressure ==502 && x.Temperature ==22 && x.TimeStamp == DateTime.Today);
        }

        [TestMethod]
        public void GetDatefromDB()
        {
            List<Measurement> Measurements;
            MeasurementController x = new MeasurementController();
            Measurements = (List<Measurement>)x.Get();
            Assert.IsTrue(Measurements.Count == 5);
        }

        [TestMethod]
        public void GetDatefromDBbyID()
        {
            
            MeasurementController x = new MeasurementController();
            Measurement n = x.Get(1);
            Assert.IsTrue(n.Temperature == 22);
        }

        [TestMethod]
        public void PostToDB()
        {

            MeasurementController x = new MeasurementController();
            Measurement n = new Measurement(0,99,99,99,DateTime.Now);
            x.Post(n);
            Assert.IsTrue(x.Get(6).Temperature == 99);
        }
    }
}
