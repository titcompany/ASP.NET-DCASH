using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIT.Datas.Helper
{
    public class MaHoaTheHelper
    {
        internal static string MaHoaThe(string passwordThe)
        {
            if (string.IsNullOrEmpty(passwordThe))
                return null;
            string result = passwordThe;
            result=result.Replace('1','I');
            result=result.Replace('2', 'S');
            result=result.Replace('3', 'D');
            result = result.Replace('4', 'G');
            result = result.Replace('5', 'J');
            result = result.Replace('6', 'H');
            result = result.Replace('7', 'L');
            result = result.Replace('8', 'A');
            result = result.Replace('9', 'P');
            return result;
        }

        internal static string GiaiMaThe(string maHoaThe)
        {
            if (string.IsNullOrEmpty(maHoaThe))
                return null;
            string result = maHoaThe;
            result = result.Replace('I', '1');
            result = result.Replace('S', '2');
            result = result.Replace('D', '3');
            result = result.Replace('G', '4');
            result = result.Replace('J', '5');
            result = result.Replace('H', '6');
            result = result.Replace('L', '7');
            result = result.Replace('A', '8');
            result = result.Replace('P', '9');
            return result;
        }
    }
}
