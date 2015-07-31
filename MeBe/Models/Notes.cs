using System;
using SQLite;

namespace MeBe.Models
{
    public class Notes
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string TimeCreate { get; set; }
    }
}
