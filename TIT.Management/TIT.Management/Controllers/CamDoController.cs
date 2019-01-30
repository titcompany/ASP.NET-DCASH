using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIT.Management.Models;
using Microsoft.AspNet.Identity;
using TIT.Datas.Models;

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
        [Authorize]
        // GET: CamDo
        public ActionResult Index()
        {
            IEnumerable<CamDoGridViewModel> models = TIT.Datas.Services.CamDoService.GetGridViewModel(User.Identity.GetUserId(), GetUserRole()).Select(
                x => new CamDoGridViewModel() {
                    HopDong_Id = x.HopDong_Id,
                    TenKhachHang = x.TenKhachHang,
                    LaiDaDong = x.LaiDaDong,
                    LaiPhiDenHomNay = x.LaiPhiDenHomNay,
                    NgayPhaiDongLai = x.NgayPhaiDongLai.ToString("dd/MM/yyyy"),
                    NgayTaoHopDong = x.NgayTaoHopDong.ToString("dd/MM/yyyy"),
                    NoCu = x.NoCu,
                    SoDienThoai = x.SoDienThoai,
                    SoTien = String.Format("{0:n0}", x.SoTien),
                    TaiSan = x.TaiSan,
                    TinhTrang = x.TinhTrang
                });
            List<DanhMucTaiSanModel> LoaiTaiSan = new List<DanhMucTaiSanModel>();
            LoaiTaiSan.Add(new DanhMucTaiSanModel { TaiSan = "Xe máy", MaTaiSan = "XM" });
            LoaiTaiSan.Add(new DanhMucTaiSanModel { TaiSan = "Ô tô", MaTaiSan = "OT" });
            LoaiTaiSan.Add(new DanhMucTaiSanModel { TaiSan = "Điện thoại", MaTaiSan = "DT" });
            LoaiTaiSan.Add(new DanhMucTaiSanModel { TaiSan = "Laptop", MaTaiSan = "LT" });
            LoaiTaiSan.Add(new DanhMucTaiSanModel { TaiSan = "Vàng", MaTaiSan = "VG" });
            ViewBag.LoaiTaiSan = LoaiTaiSan;

            List<DanhMucHinhThucLainModel> HinhThucLai = new List<DanhMucHinhThucLainModel>();
            HinhThucLai.Add(new DanhMucHinhThucLainModel { HinhThucLai = "Lãi ngày(k/triệu)", ID = "1" });
            HinhThucLai.Add(new DanhMucHinhThucLainModel { HinhThucLai = "Lãi ngày(k/ngày)", ID = "2" });
            HinhThucLai.Add(new DanhMucHinhThucLainModel { HinhThucLai = "Lãi tháng (%) (30 ngày)", ID = "3" });
            HinhThucLai.Add(new DanhMucHinhThucLainModel { HinhThucLai = "Lãi tuần (%)", ID = "4" });
            HinhThucLai.Add(new DanhMucHinhThucLainModel { HinhThucLai = "Lãi tuần (k)", ID = "5" });
            ViewBag.HinhThucLai = HinhThucLai;

            if (User.IsInRole("Employee"))
                ViewBag.CuaHang = TIT.Datas.Services.CuaHangService.GetListViewCuaHangTheoIdNhanVien(User.Identity.GetUserId());
            else if (User.IsInRole("BranchManager"))
                ViewBag.CuaHang = TIT.Datas.Services.CuaHangService.GetListViewCuaHangTheoIdQuanLy(User.Identity.GetUserId());

            return View(models);
        }

        [HttpPost]
        public ActionResult _TaoMoiHopDong(CamDoInsertNewModel model)
        {
            CamDoGridViewModel _returnModel = null;
            if (ModelState.IsValid)
            {
                string ID_NhanVien = User.Identity.GetUserId();
                KhachHangModel khachHang = new KhachHangModel();
                //Them Khach Hang
                int result1;
                if (User.IsInRole("BranchManager"))
                    result1 = TIT.Datas.Services.KhachHangService.ThemMoiHoacCapNhatKhachHangTheoQuanLy(khachHang, ID_NhanVien, model.Id_CuaHang);
                else
                    result1 = TIT.Datas.Services.KhachHangService.ThemMoiHoacCapNhatKhachHangTheoNhanVien(khachHang, ID_NhanVien);
                var returnModel = TIT.Datas.Services.CamDoService.Insert(new CamDoInsertNewDataModel() {
                     Id_CuaHang = model.Id_CuaHang,
                     CMND = model.CMND,
                     DiaChi = model.DiaChi,
                     GhiChu = model.GhiChu,
                     HinhThucLai = int.Parse(model.HinhThucLai),
                     KyLai = model.KyLai,
                     LaiPhi = model.LaiPhi,
                     LoaiTaiSan = model.LoaiTaiSan,
                     NgayVay = model.NgayVay,
                     SoDienThoai = model.SoDienThoai,
                     SoTien = model.SoTien,
                     TenKhachHang = model.TenKhachHang,
                     TenTaiSan = model.TenTaiSan
                }, User.Identity.GetUserId(), GetUserRole());
                _returnModel = new CamDoGridViewModel() {
                    TenKhachHang = model.TenKhachHang,
                    SoTien = String.Format("{0:n0}", returnModel.SoTien),
                    SoDienThoai = model.SoDienThoai,
                    HopDong_Id = returnModel.HopDong_Id,
                    LaiDaDong = returnModel.LaiDaDong,
                    LaiPhiDenHomNay = returnModel.LaiPhiDenHomNay,
                    NgayPhaiDongLai = returnModel.NgayPhaiDongLai.ToString("dd/MM/yyyy"),
                    NgayTaoHopDong = returnModel.NgayTaoHopDong.ToString("dd/MM/yyyy"),
                    NoCu = returnModel.NoCu,
                    TaiSan = returnModel.TaiSan,
                    TinhTrang = returnModel.TinhTrang,
                };
            }
            if (_returnModel != null)
                return Json(new
                {
                    result = "success",
                    data = _returnModel
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = "fail" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ThongTinChiTietCamDo(string HopDong_Id)
        {      
            var detail = TIT.Datas.Services.CamDoService.GetDetailViewModel(User.Identity.GetUserId(), GetUserRole(), HopDong_Id);
            CamDoDetaiViewlModel _returnModel = new CamDoDetaiViewlModel()
            {
                LaiSuat = detail.LaiSuat,
                NgayDongLaiCuoiCung = detail.NgayDongLaiCuoiCung.ToString("dd/MM/yyyy"),
                NgayVay = detail.NgayVay.ToString("dd/MM/yyyy"),
                NoCu = String.Format("{0:n0}", detail.NoCu),
                SoDienThoai = detail.SoDienThoai,
                SoTien = String.Format("{0:n0}", detail.SoTien),
                TenKhachHang = detail.TenKhachHang,
                TienLaiDaDong = String.Format("{0:n0}", detail.TienLaiDaDong),
                TrangThai = detail.TrangThai
            };

            _returnModel.LichSuDongLai = TIT.Datas.Services.CamDoService.GetHistoryPaymentViewModel(User.Identity.GetUserId(), GetUserRole(), HopDong_Id).Select(x => new LichSuDongLaiCamDo()
            {
                DaDong = x.DaDong,
                DenNgay = x.DenNgay,
                SoNgay = x.SoNgay,
                TienKhac = x.TienKhac,
                TienLaiPhi = x.TienLaiPhi,
                TongTienTra = x.TongTienTra,
                TuNgay = x.TuNgay
            });
            var lichSuThaoTac = TIT.Datas.Services.CamDoService.GetHistoryViewModel(User.Identity.GetUserId(), GetUserRole(), HopDong_Id);
            if (_returnModel != null)
                return Json(new
                {
                    result = "success",
                    data = _returnModel
                }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = "fail" }, JsonRequestBehavior.AllowGet);
        }
    }
}