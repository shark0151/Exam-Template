using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MockExam;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private List<Measurement> Measurements;

        // GET: api/<MeasurementController>
        [HttpGet]
        public IEnumerable<Measurement> Get()
        {
            List<Measurement> DBList = new List<Measurement>();

            string SqlQuery = "Select * from SensorData";

            using (SqlConnection dataBaseConnection = new SqlConnection(ConnectionString))
            {
                dataBaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(SqlQuery, dataBaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            double temperature = reader.GetInt32(1);
                            double humidity = reader.GetInt32(2);
                            double pressure = reader.GetInt32(3);
                            DateTime dateTime = reader.GetDateTime(4);

                            DBList.Add(new Measurement(id, temperature, humidity, pressure, dateTime));
                        }
                    }
                }
            }
            
            return DBList;
        }

        // GET api/<MeasurementController>/5
        [HttpGet("{id}")]
        public Measurement Get(int id)
        {
            Measurement measurement = new Measurement();
            string SqlQuery = $"Select * from SensorData where Id = '{id}'";

            using (SqlConnection dataBaseConnection = new SqlConnection(ConnectionString))
            {
                dataBaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(SqlQuery, dataBaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int sid = reader.GetInt32(0);
                            double temperature = reader.GetInt32(1);
                            double humidity = reader.GetInt32(2);
                            double pressure = reader.GetInt32(3);
                            DateTime dateTime = reader.GetDateTime(4);

                            measurement = new Measurement(sid, temperature, humidity, pressure, dateTime);
                        }
                    }
                }
            }
            return measurement;
        }

        // POST api/<MeasurementController>
        [HttpPost]
        public void Post([FromBody] Measurement value)
        {
            string insertSql =
                "insert into SensorData(temperature, humidity,pressure, TimeStamp) values( @temperature, @humidity, @pressure, @dateTime)";

            using (SqlConnection dataBaseConnection = new SqlConnection(ConnectionString))
            {
                dataBaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertSql, dataBaseConnection))
                {
                    //insertCommand.Parameters.AddWithValue("@id", value.ID);
                    insertCommand.Parameters.AddWithValue("@temperature", value.Temperature);
                    insertCommand.Parameters.AddWithValue("@humidity", value.Humidity);
                    insertCommand.Parameters.AddWithValue("@pressure", value.Pressure);
                    insertCommand.Parameters.AddWithValue("@dateTime", value.TimeStamp);

                    var rowsAffected = insertCommand.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }

        // PUT api/<MeasurementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MeasurementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string SqlQuery = "delete from SensorData where Id=@id";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(SqlQuery, conn);
                command.Parameters.AddWithValue("@id", id);
                int affectedRows = command.ExecuteNonQuery();

            }
        }
    }
}
