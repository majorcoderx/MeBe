using System;
using SQLite;

namespace MeBe.Models
{
    public class Food
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int Type { get; set; }
        public string Name { get; set; } //ten
        public string Information { get; set; }
        public string UrlImage { get; set; } 
    }
}
