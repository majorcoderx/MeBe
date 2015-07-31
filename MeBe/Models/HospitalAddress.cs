using System;
using SQLite;

namespace MeBe.Models
{
    public class HospitalAddress
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string XY { get; set; }
        //chua xong, can them your location
    }
}
