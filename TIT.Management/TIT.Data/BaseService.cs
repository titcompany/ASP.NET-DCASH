using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace TIT.Data
{
    public class BaseService
    {
        public static string _masterDBCon = ConfigurationManager.ConnectionStrings["MasterDatabase"].ToString();
        public static string _subDBCon = ConfigurationManager.ConnectionStrings["SubDatabase"].ToString();
    }
}
