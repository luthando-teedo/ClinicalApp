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
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }


    }
}
