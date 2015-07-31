using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MeBe.Models;

namespace MeBe.ViewModels
{
    public class ProcessMamaCheck
    {
        MamaCheck mama = new MamaCheck();

        public ProcessMamaCheck() { }

        public ProcessMamaCheck(int week,int old, double weight, double height)
        {
            mama.Week = week;
            mama.Old = old;
            mama.Weight = weight;
            mama.Height = height;
            mama.Datetime = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        }

        public double BMIIndex()
        {
            return mama.Weight / (mama.Height * mama.Height);
        }
        //smart method
        public string AdviceForMama()
        {
            bool checkWeek = true;
            PMamaCheck _check = new PMamaCheck();
            ObservableCollection<MamaCheck> _mList = _check.GetListObject();
            for (int i = 0; i < _mList.Count; ++i)
            {
                if (mama.Week == _mList[i].Week)
                {
                    checkWeek = false;
                }
            }
            if (checkWeek)
            {
                _check.InsertObject(mama);
            }
            else
            {
                _check.UpdateObject(mama);
            }
            if (_mList.Count == 0) // lan dau nhap so lieu
            {   
                return "Đây là lần nhập số liệu đầu tiên của bạn, chúng tôi lấy nó làm số liệu gốc để so sánh.";
            }
            else if (mama.Week == 0 || mama.Week == 1) // tuan dau mang thai
            {
                double j = BMIIndex();
                if (j < 18.5)
                {
                    return "Bạn có thể trạng gầy, nguy cơ sảy thai sẽ cao hơn bình thường 17%. Bạn cần tăng thêm ít nhất " + 
                        (18.5 * mama.Height * mama.Height - mama.Weight).ToString(".0") +
                        " Kg để chuẩn bị mang thai bé yêu của bạn. Chúng tôi khuyến cáo nên tăng thêm " +
                        (22 * mama.Height * mama.Height - mama.Weight).ToString(".0") + 
                        " Kg là hợp lý cho bà mẹ chuẩn bị mang thai.";
                }
                else if (j >= 18.5 && j < 35)
                {
                    return "Bạn có thể trạng bình thường. Chúc bạn khởi đầu thuận lợi.";
                }
                else
                {
                    return "Có vẻ bạn cần giảm cân, bà mẹ dư thừa cân nặng làm tăng nguy cơ xảy thai, chết lưu hoặc sinh" +
                        " nên hãy điều chỉnh về mức cân hợp lý nhé. Chúng tôi khuyến cáo nên giảm khoảng " + 
                        (mama.Weight - 22 * mama.Height * mama.Height) + " Kg để về mức hợp lý.";
                }
            }
            else // da co so lieu va k phai tuan dau tien
            {
                //lay du lieu cua tuan truoc gan do
                //so sanh can nang can tang
                if (mama.Week == _mList[0].Week)
                {
                    return "Trong thời gian quá ngắn, sự tư vấn của chúng tôi sẽ là không chính xác. Bạn hãy nhập lại vào tuần sau.";
                }
                else
                {
                    PMamaCheckWeight _checkW = new PMamaCheckWeight();
                    WeightForMama _weightPre = new WeightForMama();
                    WeightForMama _weightNow = new WeightForMama();
                    _weightPre = _checkW.GetObject(_mList[0].Week);
                    _weightNow = _checkW.GetObject(mama.Week);


                    double valueWeight = mama.Weight - _mList[0].Weight;//chenh lech muc can giua 2 tuan x va y
                    if (valueWeight <= 0)
                    {
                        //tut can
                        return "Bạn cần bổ sung thêm dinh dưỡng hằng ngày, tình trạng tụt cân khi mang thai gây"+
                            " nhiều biến chứng nguy hiểm cho bạn và bé. Số cân bạn cần tăng thêm trong tuần sau là " + 
                            (_checkW.GetObject(mama.Week + 1).OverWeight - _weightPre.UnderWeight - valueWeight) + " kg.";
                    }
                    else if (valueWeight < _weightNow.UnderWeight - _weightPre.UnderWeight)
                    {
                        return "Bạn cần tăng cân nhanh hơn nữa, số cân tăng thiếu so với khuyến cáo là " + 
                            (_weightNow.UnderWeight - _weightPre.UnderWeight - valueWeight) + 
                            " kg. Số cân tuần sau bạn cần bù là " +(_checkW.GetObject(mama.Week+1).OverWeight - _weightPre.UnderWeight - valueWeight)
                            + " kg.";
                    }
                    else if (valueWeight >= _weightNow.UnderWeight - _weightPre.UnderWeight && valueWeight <= _weightNow.OverWeight - _weightPre.OverWeight)
                    {
                        return "Bạn có một sự tăng cân hợp lý. Hãy duy trì điều đó để bạn và bé đều khỏe, tuần sau bạn nên tăng thêm " + 
                            (_checkW.GetObject(mama.Week + 1).OverWeight - _weightNow.OverWeight);
                    }
                    else
                    {
                        if (valueWeight > _checkW.GetObject(mama.Week + 1).OverWeight - _weightPre.OverWeight)
                        {
                            return "Có vẻ bạn tăng cân quá nhanh rồi, đừng lo lắng, chỉ cần kìm hãm lại tốc độ tăng cân là được."+
                                " Tuần sau bạn nên tăng khoảng " + (_checkW.GetObject(mama.Week + 1).OverWeight - _weightNow.OverWeight) / 2 + " kg.";
                        }
                        else
                        {
                            return "Có vẻ bạn tăng cân quá nhanh rồi, đừng lo lắng, chỉ cần kìm hãm lại tốc độ tăng cân là được."+
                                " Tuần sau bạn nên tăng khoảng " + (_checkW.GetObject(mama.Week + 1).OverWeight - _weightNow.OverWeight - valueWeight)
                                + " kg.";
                        }
                    }
                }
            }
        }

        //method nay danh cho ba me cho con bu
        public string AdviceMamaBeautiful()
        {
            double bmi = BMIIndex();

            WeightMamaBMI wBmi = new WeightMamaBMI(mama.Weight, mama.Height, mama.Old);
            PMamaBMI mbmi = new PMamaBMI();
            mbmi.InsertObject(wBmi);

            if (bmi < 18.5)
            {
                return "Bạn có thể trạng gầy, có thể gây thiếu chất và thiếu sữa cho con bú. Bạn cần tăng thêm ít nhất " +
                        (18.5 * mama.Height * mama.Height - mama.Weight).ToString(".0") +
                        " kg. Chúng tôi khuyến cáo nên tăng thêm " +
                        (20.5 * mama.Height * mama.Height - mama.Weight).ToString(".0") +
                        " kg là hợp lý cho bà mẹ chuẩn bị mang thai.";
            }
            else if (bmi >= 18.5 && bmi <= 24.5)
            {
                if (bmi <= 21.5 && bmi >= 19.5)
                {
                    return "Bạn có mức cân hợp lí. Hãy cố gắng duy trì điều này để có thể chất khỏe mạnh, vóc dáng đẹp sau sinh";
                }
                else
                {
                    return "Bạn có mức cân khá hợp lí. Chúng tôi khuyến cáo nên ở mức cân " 
                        + (20.5 * mama.Height * mama.Height).ToString(".0") + " để đạt tỉ lệ vàng.";
                }
            }
            else
            {
                return "Bạn có số cân quá mức. Điều này gây nhiều nguy cơ về tim mạch, hàm lượng mỡ trong máu cao. Bạn nên điều chỉnh về số cân về "
                    + (20.5 * mama.Height * mama.Height - mama.Weight).ToString(".0") + " kg";
            }
            
        }

        //method nay danh cho tu van thuc pham
        public double BMRIndex(int week)
        {
            MamaCheck _mamacheck = new MamaCheck();
            PMamaCheck _check = new PMamaCheck();
            if(week >= 1)
            {
                _mamacheck = _check.GetObject(week);
            }
            else
            {
                return 0;
            }
            return 0.00;
        }
    }
}
