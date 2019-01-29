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

        public static CamDoGridDataModel Insert(CamDoInsertNewDataModel model, string userId, string role)
        {
            UserModel userModel = UserModel.getUserModel(userId, role);
            return userModel.InsertCamDo(model);
        }

        public static object GetDetailViewModel(string userId, string role, string Id_HopDong)
        {
            UserModel userModel = UserModel.getUserModel(userId, role);
            return userModel.GetDetailViewModel(Id_HopDong);
        }

        public static IEnumerable<CamDoHistoryDataModel> GetHistoryViewModel(string userId, string role, string Id_HopDong)
        {
            UserModel userModel = UserModel.getUserModel(userId, role);
            return userModel.GetHistoryViewModel(Id_HopDong);
        }

        public static IEnumerable<CamDoPaymentHistoryDataModel> GetHistoryPaymentViewModel(string userId, string role, string Id_HopDong)
        {
            UserModel userModel = UserModel.getUserModel(userId, role);
            return userModel.GetHistoryPaymentViewModel(Id_HopDong);
        }
    }
}
