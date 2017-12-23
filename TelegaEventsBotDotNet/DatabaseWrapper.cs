using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace TelegaEventsBotDotNet
{
    public class RLEvent
    {
        public String MarkupType { get; set; }
        public String Label { get; set; }
        public String Description { get; set; }
        public String Location { get; set; }
        public String Optional1 { get; set; }
        public String Optional2 { get; set; }
        public String Optional3 { get; set; }
        public String Optional4 { get; set; }
        public String Optional5 { get; set; }
        public String Optional6 { get; set; }
        public DateTime dateTime { get; set; }
        public Int64 EventId { get; set; }
    }
    public class DatabaseWrapper
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        public DatabaseWrapper()
        {

        }

        public List<int> GetTodayEventsIds()
        {
            List<int> ids = new List<int>();
            using (SqlConnection connection = new SqlConnection(
            "Data Source=telegram.crczform3iip.eu-central-1.rds.amazonaws.com,1433;Network Library=DBMSSOCN; Initial Catalog = Events; User ID = administrator; Password = pt1cagrach; "))
            {
                string test1 = "";
                connection.Open();
                DateTime localDate = DateTime.Now.Date;
                localDate = localDate.AddHours(23);
                localDate = localDate.AddMinutes(59);
                localDate = localDate.AddSeconds(59);
                String query = "SELECT event_id from [Events].[dbo].[events] where datetime <= '" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                SqlCommand command = new SqlCommand(query, connection);
                int test = command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add((int)reader[0]);
                    }
                }
                // Pool B is created because the connection strings differ.
            }
            return ids;
        }

        public RLEvent GetEventById(Int64 ID)
        {
            RLEvent rlevent = new RLEvent();
            using (SqlConnection connection = new SqlConnection(
            "Data Source=telegram.crczform3iip.eu-central-1.rds.amazonaws.com,1433;Network Library=DBMSSOCN; Initial Catalog = Events; User ID = administrator; Password = pt1cagrach; "))
            {
                connection.Open();
                String query = "SELECT * from [Events].[dbo].[events] where event_id = " + ID;

                SqlCommand command = new SqlCommand(query, connection);
                int test = command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                            rlevent.EventId = (int)reader[0];
                        if (!reader.IsDBNull(1))
                            rlevent.MarkupType = (string)reader[1];
                        if (!reader.IsDBNull(2))
                            rlevent.Label = (string)reader[2];
                        if (!reader.IsDBNull(3))
                            rlevent.Description = (string)reader[3];
                        if (!reader.IsDBNull(4))
                            rlevent.Location = (string)reader[4];
                        if (!reader.IsDBNull(5))
                            rlevent.dateTime = (DateTime)reader[5];
                        if (!reader.IsDBNull(6))
                        {
                            rlevent.Optional1 = (string)reader[6];
                        }
                        if (!reader.IsDBNull(7))
                        {
                            rlevent.Optional2 = (string)reader[7];
                        }
                        if (!reader.IsDBNull(8))
                        {
                            rlevent.Optional3 = (string)reader[8];
                        }
                        if (!reader.IsDBNull(9))
                        {
                            rlevent.Optional4 = (string)reader[9];
                        }
                        if (!reader.IsDBNull(10))
                        {
                            rlevent.Optional5 = (string)reader[10];
                        }
                        if (!reader.IsDBNull(11))
                        {
                            rlevent.Optional6 = (string)reader[11];
                        }
                    }
                }
                // Pool B is created because the connection strings differ.
            }

            return rlevent;
        }
        
    }
}
