using System;
namespace MeBe.ViewModels
{
    public class CountTime
    {
        public CountTime(DateTimeOffset dateapp)
        {
            dateNow = DateTime.Now;
            this.dateApp = dateapp;
        }

        DateTimeOffset dateApp;
        public DateTime dateNow;

        public int HaveMonth()
        {
            //duoc 1 thang
            int day = (dateNow.Date - dateApp.Date).Days;
            if (day % 30 >= 20)
            {
                return (int)day / 30 + 1;
            }
            else
            {
                return (int)day / 30;
            }
        }

        public int CountWeek()
        {
            int day = (dateNow.Date - dateApp.Date).Days;
            if (day % 7 >= 6)
            {
                return (int)day / 7 + 1;
            }
            else
            {
                return (int)day / 7;
            }
        }
    }
}
