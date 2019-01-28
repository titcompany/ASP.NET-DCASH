using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIT.Datas.Models
{
    public class ResultCode
    {
        public readonly static int Success = 200;
        public readonly static int Valid = 601;
        public readonly static int Invalid = 600;
    }
    public class ProvinceSelectItem
    {
        public int id { get; set; }
        public string text { get; set; }
    }
    public class ProvinceModel
    {
        [Key]
        [Display(Name = "ID tỉnh")]
        public int ProvinceId { get; set; }

        [Display(Name = "Tên tỉnh")]
        public string Name { get; set; }

        [Display(Name = "Mã tỉnh")]
        public string Code { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class AccountViewModel
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "Vai trò")]
        public string UserRole { get; set; }
    }
    public class NhanVienViewModel: AccountViewModel
    {
        [Display(Name = "Thuộc cửa hàng")]
        public string TenCuaHang { get; set; }

    }
    public class UserWithRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }

}