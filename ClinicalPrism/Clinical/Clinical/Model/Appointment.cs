using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinical.Model
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ClientDetailId { get; set; }
        public string Reason { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }


    }
}
