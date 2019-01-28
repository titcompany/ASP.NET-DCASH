using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class AccountService
    {
        public static IEnumerable<AccountViewModel> GetList()
        {
            IEnumerable<AccountViewModel> result;

            using (var context = new TIT_Entities())
            {


                result = (from user in context.AspNetUsers
                          join ur in context.AspNetUserRoles on user.Id equals ur.UserId
                          select new AccountViewModel()
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              FullName = user.FullName,
                              Email = user.Email,
                              UserRole = ur.AspNetRole.Name

                          }).ToArray();
              
            }
            return result;
        }
        public static IEnumerable<NhanVienViewModel> GetListByManagerId(string managerId)
        {
            IEnumerable<NhanVienViewModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from user in context.AspNetUsers
                          join  ch in context.CuaHangs on user.CuaHang_Id equals ch.MaCuaHang
                          where ch.QuanLyCuaHang == managerId
                          select new NhanVienViewModel()
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              FullName = user.FullName,
                              Email = user.Email,
                              TenCuaHang = ch.TenCuaHang

                          }).ToArray();
            }
            return result;
        }


        public static IEnumerable<UserWithRole> LayDanhSachBranchManager()
        {
            List<UserWithRole> result;
            using (var context = new TIT_Entities())
            {
                result = (from user in context.AspNetUsers
                          join ur in context.AspNetUserRoles on user.Id equals ur.UserId
                          join role in context.AspNetRoles on ur.RoleId equals role.Id
                          where role.Name == "BranchManager"
                          select new UserWithRole()
                          {
                              UserId = user.Id,
                              RoleName = role.Name,
                              UserName = user.UserName
                          }).ToList();
              
            }
            return result;
        }

        public static string LayIdNguoiQuanLyTheoIdNhanVien(string idNhanVien)
        {
            string result;
            using (var context = new TIT_Entities())
            {
                result = (from user in context.AspNetUsers
                          where user.Id == idNhanVien
                          select  user.ManagerId).SingleOrDefault();
            }
            return result;
        }

        public static int Insert(CuaHangAddNewModel model)
        {
            var dbModel = new CuaHang
            {
                TenCuaHang = model.TenCuaHang,
                DiaChi = model.DiaChi,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                NgayTao = model.NgayTao,
                NguoiTao = model.NguoiTao,
                QuyTienMat = model.QuyTienMat,
                VonDauTu = model.VonDauTu,
                TinhTrang = model.TinhTrang ? 1 : 0 
            };
         

            using (var context = new TIT_Entities())
            {
                context.CuaHangs.Add(dbModel);
                try
                {
                    return context.SaveChanges();                    
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    //_logger.Error(ex.ToString());
                    return -1;
                }
            }
        }

        public static List<AspNetRole> LayDanhSachPhanQuyen()
        {
            using (var context = new TIT_Entities())
            {
                return context.AspNetRoles.ToList();
            }
        }
    }
}
