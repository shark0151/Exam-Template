using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestService
{
    public class Measurement
    {
        public int ID { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public DateTime TimeStamp { get; set; }

        public Measurement()
        { }

        public Measurement(int id,double temp, double hum, double press, DateTime time)
        {
            ID = id;
            Temperature = temp;
            Humidity = hum;
            Pressure = press;
            TimeStamp = time;
        }
    }
}
