using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Helper;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class CamDoService
    {
        public static IEnumerable<CamDoGridDataModel> GetGridViewModel(string userId, string role)
        {
            UserModel userModel = UserModel.getUserModel(userId, role);
            return userModel.GetGridViewCamDoModel();
        }
    }
}
