using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP_Broadcast_reciever
{
    public class Measurement
    {
        public int ID { get; set; }

        public int Temperature { get; set; }

        public int Humidity { get; set; }
        public int Pressure { get; set; }

        public DateTime DateTime { get; set; }

        public Measurement(int id, int temperature, int humidity, int pressure, DateTime dateTime)
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
            return $"ID:{ID}, Temperature:{Temperature}, Humidity:{Humidity}, Date:{DateTime.ToString("f")}";
        }
    }
    class Program
    {
        private const int Port = 55555;
        static void Main(string[] args)
        {
            //Creates a UdpClient for reading incoming data.

            UdpClient udpReceiver = new UdpClient(Port);

            


            //BROADCASTING RECEIVER
            //This IPEndPoint will allow you to read datagrams sent from any ip-source on port 7000
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, Port);

            //IPEndPoint object will allow us to read datagrams sent from any source.
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                while (true)
                {
                    Byte[] receiveBytes = udpReceiver.Receive(ref RemoteIpEndPoint);

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);

                    Console.WriteLine("Sender: " + receivedData.ToString());

                    string[] textLines = receivedData.Split(',');
                    string[] list1 = textLines[0].Split(':');
                    int id = Convert.ToInt32(double.Parse(list1[1].Trim()));
                    string[] list2 = textLines[1].Split(':');
                    int Temp = Convert.ToInt32(double.Parse(list2[1].Trim()));
                    string[] list3 = textLines[2].Split(':');
                    int Hum = Convert.ToInt32(double.Parse(list3[1].Trim()));
                    string[] list4 = textLines[3].Split(':');
                    int Press = Convert.ToInt32(double.Parse(list4[1].Trim()));
                    string[] list5 = textLines[4].Split(':');
                    DateTime Date = Convert.ToDateTime(list5[1] + ":" + list5[2]);

                    Measurement x = new Measurement(id, Temp, Hum,Press, Date);

                    Thread.Sleep(200);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
