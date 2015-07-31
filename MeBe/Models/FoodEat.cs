using System;
using SQLite;

namespace MeBe.Models
{
    public class FoodEat
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int IDFood { get; set; }
        public int IDEat { get; set; }
    }
}
