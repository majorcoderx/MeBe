using System;
using SQLite;

namespace MeBe.Models
{
    public class TestMama
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Type { get; set; }
        public int Month { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Result { get; set; }
        public int Answers { get; set; }
        public string UriImage { get; set; }
    }
}
