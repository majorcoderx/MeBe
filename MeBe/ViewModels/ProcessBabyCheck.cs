using System;
using MeBe.Models;

namespace MeBe.ViewModels
{
    class ProcessBabyCheck
    {
        public BabyCheck baby = new BabyCheck();

        public ProcessBabyCheck(int month, string sex,double weight,double height)
        {
            baby.Month = month;
            baby.Sex = sex;
            baby.Weight = weight;
            baby.Height = height;
            baby.Datetime = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        }

        public int ProcessHeightBabyCheck()
        {
            HeightFor _height = new HeightFor();
            PBabyCheckHeight _checkH = new PBabyCheckHeight();
            if (baby.Sex.Equals("Girls"))
            {
                _height = _checkH.GetObject(baby.Month, "HeightForGirl");
            }
            else _height = _checkH.GetObject(baby.Month, "HeightForBoy");
            if (baby.Height < _height.UnderStand3)
            {
                return -3;
            }
            else if (baby.Height >= _height.UnderStand3 && baby.Height < _height.UnderStand2)
            {
                return -2;
            }
            else if (baby.Height >= _height.UnderStand2 && baby.Height < _height.UnderStand1)
            {
                return -1;
            }
            else if (baby.Height >= _height.UnderStand1 && baby.Height < _height.OverStand1)
            {
                return 0;
            }
            else if (baby.Height >= _height.OverStand1 && baby.Height < _height.OverStand2)
            {
                return 1;
            }
            else if (baby.Height >= _height.OverStand2 && baby.Height < _height.OverStand1)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        
        public int ProcessWeightBabyCheck()
        {
            WeightFor _weight = new WeightFor();
            PBabyCheckWeight _checkW = new PBabyCheckWeight();
            if (baby.Sex.Equals("Girls"))
            {
                _weight = _checkW.GetObject(baby.Month, "WeightForGirl");
            }
            else _weight = _checkW.GetObject(baby.Month, "WeightForBoy");

            if (baby.Weight < _weight.UnderWeight3)
            {
                return -3;
            }
            else if (baby.Weight >= _weight.UnderWeight3 && baby.Weight < _weight.UnderWeight2)
            {
                return -2;
            }
            else if (baby.Weight >= _weight.UnderWeight2 && baby.Weight < _weight.UnderWeight1)
            {
                return -1;
            }
            else if (baby.Weight >= _weight.UnderWeight1 && baby.Weight < _weight.OverWeight1)
            {
                return 0;
            }
            else if (baby.Weight >= _weight.OverWeight1 && baby.Weight < _weight.OverWeight2)
            {
                return 1;
            }
            else if (baby.Weight >= _weight.OverWeight2 && baby.Weight < _weight.OverWeight3)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private string Advance = "";

        public string SmartCheck()
        {
            int _w = ProcessWeightBabyCheck();
            int _h = ProcessHeightBabyCheck();
            PBabyCheck pbaby = new PBabyCheck();
            pbaby.InsertObject(baby);

            if (_w == -3)
            {
                Advance += "Bé bị suy dinh dưỡng trầm trọng\r\n";
            }
            else if (_w == -2)
            {
                Advance += "Bé suy dinh dưỡng\r\n";
            }
            else if (_w == -1)
            {
                Advance += "Bé thiếu cân, có nguy cơ suy dinh dưỡng\r\n";
            }
            else if (_w == 0)
            {
                Advance += "Cân nặng đạt mức phát triển bình thường\r\n";
            }
            else if (_w == 1)
            {
                Advance += "Bé tăng cân nhanh hơn khuyến cáo\r\n";
            }
            else if (_w == 2)
            {
                Advance += "Bé có nguy cơ béo phì\r\n";
            }
            else
            {
                Advance += "Bé đang trong tình trạng béo phì\r\n";
            }

            if (_h == -3)
            {
                Advance += "Chiều cao của bé phát triển quá chậm\r\n";
            }
            else if (_h == -2)
            {
                Advance += "Chiều cao phát triển chậm\r\n";
            }
            else if (_h == -1)
            {
                Advance += "Chiều cao phát triển hơi chậm\r\n";
            }
            else if (_h == 0)
            {
                Advance += "Chiều cao phát triển bình thường\r\n";
            }
            else if (_h == 1)
            {
                Advance += "Chiều cao bé phát triển tốt\r\n";
            }
            else if (_h == 2)
            {
                Advance += "Chiều cao bé phát triển rất tốt\r\n";
            }
            else
            {
                Advance += "Chiều cao bé phát triển cực tốt\r\n";
            }

            if ((_w==-3&&_h==-3)||(_w == -3 && _h == -2) || (_w == -2 && _h == -3))
            {    //vừa thiếu cân, vừa thiếu chiều cao
                Advance += "Bạn cần chú ý vấn đề dinh dưỡng của trẻ. Bé đang ở mức kém phát triển.";
            }
            if ((_w == -2 && _h == -2) || (_w == -2 && _h == -1) || (_w == -1 && _h == -2))
            {
                //thiếu cân, chiều cao độ 2
                Advance += "Bạn cần bổ sung thêm dinh dưỡng cho trẻ, bé đang có nguy cơ suy dinh dưỡng và chiều cao kém phát triển";
            }
            if(_w == -1 && _h == -1)
            {
                Advance += "Bạn cần bổ sung thêm dinh dưỡng cho trẻ như : tăng cường độ ăn dặm, đảm bảo chất dinh dưỡng trong các bữa ăn...Bé của bạn đang có nguy cơ suy dinh dưỡng.";
            }
            return Advance;
        }
    }
}
