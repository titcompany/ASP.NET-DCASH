using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{

    public class BaseService
    {
        protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }

}
