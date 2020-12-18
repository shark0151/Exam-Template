using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP_Broadcast_sender
{
    public class Measurement
    {
        
        public int ID { get; set; }
       
        public int Temperature { get; set; }
        
        public int Humidity { get; set; }
        public int Pressure { get; set; }

        public DateTime DateTime { get; set; }



        
        public Measurement(int id, int temperature, int humidity,int pressure, DateTime dateTime)
        {
            ID = id;
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
            DateTime = dateTime;
        }
       
        public Measurement()
        {
        }

        
        public override string ToString()
        {
            return $"ID:{ID}, Temperature:{Temperature}, Humidity:{Humidity}, Pressure:{Pressure}, Date:{DateTime.ToString("f")}";
        }
    }
    class Program
    {
        private const int Port = 55555;
        static void Main(string[] args)
        {
            
            Measurement x = new Measurement(0, 0, 0, 0, DateTime.Now);
            var rand = new Random();
            int no = 1;
            //Using remote endpoint needs UdpClient (0) for broadcasting
            UdpClient udpServer = new UdpClient(0);  //or empty
            udpServer.EnableBroadcast = true; //not necessary
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, Port);
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 7000); in client

            Console.WriteLine("Broadcast started: Press Enter");
            Console.ReadLine();

            while (true)
            {
                x.ID = no;
                x.Temperature = rand.Next(1, 40);
                x.Humidity = rand.Next(0, 100);
                x.Pressure = rand.Next(400, 800);
                x.DateTime = DateTime.Now;
                Byte[] sendBytes = Encoding.ASCII.GetBytes(x.ToString());
                try
                {
                    udpServer.Send(sendBytes, sendBytes.Length, endPoint); //, endPoint

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                no++;
                Console.WriteLine(" " + x);
                Thread.Sleep(1000);
            }
        }
    }
}
