using System;
using SQLite;


namespace MeBe.Models
{
    public class Eat
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Natrition { get; set; }
        public string Information { get; set; }
        public string UrlImage { get; set; }
    }
}
