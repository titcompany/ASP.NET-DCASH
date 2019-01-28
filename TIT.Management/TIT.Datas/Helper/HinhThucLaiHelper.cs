using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIT.Datas.Helper
{
    public class HinhThucLaiHelper
    {
        public static decimal TinhLaiMotNgay(int HinhThucLai, decimal SoTien, decimal Lai)
        {
            switch (HinhThucLai)
            {
                case 1: return SoTien * Lai / 1000;
                case 2: return Lai * 1000;
                case 3: return SoTien * Lai / 3000;
                case 4: return SoTien * Lai / 700;
                case 5: return Lai * 1000 / 7;
            }
            return 0;
        }

        public static decimal TinhLaiMotNgayBatHo(decimal SoTien, int SoNgay)
        {
            if (SoNgay == 0)
                return 0;
            return SoTien / SoNgay;
        }
    }
    
}
