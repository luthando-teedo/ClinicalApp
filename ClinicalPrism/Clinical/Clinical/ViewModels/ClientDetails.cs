using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinical.ViewModels
{
    public class ClientDetails
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Passrd { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PostalAddress { get; set; }


    }
}
