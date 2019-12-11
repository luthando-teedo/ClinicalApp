﻿using SQLite;
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


    }
}