using System;
using SQLite;

namespace MeBe.Models
{
    public class BabyCheck
    {
        public BabyCheck() { }

        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int Month { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Sex { get; set; }
        public string Datetime { get; set; }
    }

    public class HeightFor
    {
        [PrimaryKey]
        public int Month { get; set; }
        public double UnderStand3 { get; set; }
        public double UnderStand2 { get; set; }
        public double UnderStand1 { get; set; }
        public double Normal { get; set; }
        public double OverStand1 { get; set; }
        public double OverStand2 { get; set; }
        public double OverStand3 { get; set; }
    }

    public class WeightFor
    {
        [PrimaryKey]
        public int Month { get; set; }
        public double UnderWeight3 { get; set; }
        public double UnderWeight2 { get; set; }
        public double UnderWeight1 { get; set; }
        public double Normal { get; set; }
        public double OverWeight1 { get; set; }
        public double OverWeight2 { get; set; }
        public double OverWeight3 { get; set; }
    }
}
