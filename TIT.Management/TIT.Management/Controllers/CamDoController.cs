using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIT.Management.Models;
using Microsoft.AspNet.Identity;
namespace TIT.Management.Controllers
{

    public class CamDoController : Controller
    {
        public string GetUserRole()
        {
            if (User.IsInRole("Admin"))
                return "Admin";
            else if (User.IsInRole("BranchManager"))
                return "BranchManager";
            else
                return "Employee";
        }
        // GET: CamDo
        public ActionResult Index()
        {
            IEnumerable<CamDoGridViewModel> models = TIT.Datas.Services.CamDoService.GetGridViewModel(User.Identity.GetUserId(), GetUserRole()).Select(
                x => new CamDoGridViewModel() {
                    HopDong_Id = x.HopDong_Id,
                    TenKhachHang = x.TenKhachHang,
                    LaiDaDong = x.LaiDaDong,
                    LaiPhiDenHomNay = x.LaiPhiDenHomNay,
                    NgayPhaiDongLai = x.NgayPhaiDongLai,
                    NgayTaoHopDong = x.NgayTaoHopDong,
                    NoCu= x.NoCu,
                    SoDienThoai = x.SoDienThoai,
                    SoTien = x.SoTien,
                    TaiSan = x.TaiSan,
                    TinhTrang = x.TinhTrang
                });

            return View(models);
        }
    }
}