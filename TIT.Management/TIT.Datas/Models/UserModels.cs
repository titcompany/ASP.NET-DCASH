using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIT.Datas.Models
{
    public class UserModel
    {
        public string _userId;
        public string _role;
        internal static UserModel getUserModel(string userId, string role)
        {
            UserModel userModel = null;
            if (role == "Admin")
                userModel = new Admin();
            else if (role == "BranchManager")
                userModel = new BranchManager();
            else if (role == "Employee")
                userModel = new Employee();
            if (userModel != null)
            {
                userModel._userId = userId;
                userModel._role = role;
            }
            return userModel;
        }

        public virtual IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            throw new NotImplementedException();
        }
    }

    public class Admin : UserModel
    {
        public override IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            return base.GetGridViewCamDoModel();
        }
    }
    public class BranchManager : UserModel
    {
        public override IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            return base.GetGridViewCamDoModel();
        }
    }
    public class Employee : UserModel
    {
        public override IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            return base.GetGridViewCamDoModel();
        }
    }
}
