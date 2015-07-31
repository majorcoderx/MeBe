using System;
using SQLite;
namespace MeBe.Models
{
    public class Knowledges
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int Week { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ExContent { get; set; }
        public int Like { get; set; }
        public int State { get; set; }
    }
}
