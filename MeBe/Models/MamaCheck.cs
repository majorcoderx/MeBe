using System;
using SQLite;

namespace MeBe.Models
{
    public class MamaCheck
    {

        public MamaCheck() { }

        public MamaCheck(int week, double weight, double height,int old)
        {
            this.Week = week;
            this.Weight = weight;
            this.Height = height;
            this.Old = old;
            this.Datetime = DateTime.Now.ToString("dd/MM/yyyy              hh:mm tt");
        }

        [PrimaryKey]
        public int Week { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Old { get; set; }
        public string Datetime { get; set; }
    }
    //cai nay cho ba me cho con bu,ghi lai lich su tra cuu BMI, BMR
    public class WeightMamaBMI
    {
        public WeightMamaBMI() { }

        public WeightMamaBMI(double weight, double height, int old)
        {
            this.Old = old;
            this.Weight = weight;
            this.Height = height;
            this.Datetime = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Old { get; set; }
        public string Datetime { get; set; }
    }

    public class WeightForMama
    {
        [PrimaryKey, AutoIncrement]
        public int Week { get; set; }
        public double UnderWeight { get; set; }
        public double OverWeight { get; set; }
    }
}
