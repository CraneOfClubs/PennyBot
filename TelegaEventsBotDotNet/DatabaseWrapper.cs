using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.Common;

namespace TelegaEventsBotDotNet
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EventContext : DbContext
    {
        public virtual DbSet<RLEvent> Events { get; set; }
        public EventContext() : base()
        {

        }

        public EventContext(DbConnection existingConnection, bool contextOwnsConnection)
      : base(existingConnection, contextOwnsConnection)
        {

        }

    }

    [Table("events")]
    public class RLEvent
    {
        [Column(name: "markup_type")]
        public String MarkupType { get; set; }
        [Column(name: "label")]
        public String Label { get; set; }
        [Column(name: "description")]
        public String Description { get; set; }
        [Column(name: "location")]
        public String Location { get; set; }
        [Column(name: "optional_1")]
        public String Optional1 { get; set; }
        [Column(name: "optional_2")]
        public String Optional2 { get; set; }
        [Column(name: "optional_3")]
        public String Optional3 { get; set; }
        [Column(name: "optional_4")]
        public String Optional4 { get; set; }
        [Column(name: "optional_5")]
        public String Optional5 { get; set; }
        [Column(name: "optional_6")]
        public String Optional6 { get; set; }
        [Column(name: "tags")]
        public String Tags { get; set; }
        [Column(name: "type")]
        public String Type { get; set; }
        [Column(name: "date_time")]
        public DateTime dateTime { get; set; }
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "event_id")]
        public Int64 EventId { get; set; }
    }
    public class DatabaseWrapper
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        public DatabaseWrapper()
        {

        }

        public List<RLEvent> GetEventsBetween(DateTime start, DateTime finish)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NskEventsDB"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (var context = new EventContext(connection, false))
                {
                    var _events = from ev in context.Events
                                  where (ev.dateTime >= start && ev.dateTime <= finish)
                                  select ev;
                    return _events.ToList<RLEvent>();
                }
            }
        }

        public RLEvent GetEventById(Int64 id)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NskEventsDB"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (var context = new EventContext(connection, false))
                {
                    var _events = from ev in context.Events
                                  where (ev.EventId == id)
                                  select ev;
                    if (_events.Count() < 1)
                    {
                        return null;
                    } else 
                    return _events.ToList<RLEvent>()[0];
                }
            }
        }

        public List<RLEvent> GetEventIDsByKeyword(List<String> Keywords)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NskEventsDB"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (var context = new EventContext(connection, false))
                {
                    List<RLEvent> result = new List<RLEvent>();
                    foreach (var key in Keywords)
                    {
                        var matches = from ev in context.Events
                                      where ev.Label.Contains(key) || ev.Description.Contains(key)
                                      select ev;
                        foreach (var m in matches)
                        {
                            result.Add(m);
                        }
                    }
                    return result;
                }
            }
        } 

        public List<RLEvent> GetTodayEventsIds()
        {
            DateTime start = DateTime.Now;
            DateTime finish = DateTime.Today;
            finish = finish.AddDays(1);
            return GetEventsBetween(start, finish);
        }
        
    }
}
