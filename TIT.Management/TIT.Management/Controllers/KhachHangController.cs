using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIT.Datas.Models;

namespace TIT.Management.Controllers
{
    public class KhachHangController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        

        public JsonResult KiemTraKhachHangCoTonTaiHayKhong(string cmnd)
        {
            var khachhang = TIT.Datas.Services.KhachHangService.TimKiemKhachHangTheoCMND(cmnd);
            if (khachhang != null)
            {
                return Json(new
                {
                    status = 200,
                    khachhang = khachhang
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 404 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ThemMoiKhachHang()
        {
            return View();
        }
        public ActionResult ThemMoiKhachHangTemp()
        {
            List<DanhMucNganangModel> DSNganHang = new List<DanhMucNganangModel>();
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang ngoai thuong Viet Nam(VietcomBank)", NganHang = "1.Ngan hang ngoai thuong Viet Nam(VietcomBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang Dau tu va Phat trien VN (BIDV)", NganHang = "2.Ngan hang Dau tu va Phat trien VN (BIDV)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Chinh sach xa hoi (VBSP)", NganHang = "3.NH Chinh sach xa hoi (VBSP)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Cong thuong VN (Vietinbank)", NganHang = "4.NH Cong thuong VN (Vietinbank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Nong nghiep & PT Nong thon VN-AGribank", NganHang = "5.NH Nong nghiep & PT Nong thon VN-AGribank" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Phat trien Nha DBSCL (MHB)", NganHang = "6.NH Phat trien Nha DBSCL (MHB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Phat trien Viet Nam (VDB)", NganHang = "7.NH Phat trien Viet Nam (VDB)" });

            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Ban Viet", NganHang = "8.Ngan hang TMCP Ban Viet" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Dai A", NganHang = "9.Ngan hang TMCP Dai A" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Phat trien MeKong", NganHang = "10.Ngan hang TMCP Phat trien MeKong" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Quoc Dan(Nam Viet cu)", NganHang = "11.Ngan hang TMCP Quoc Dan(Nam Viet cu)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Viet A", NganHang = "12.Ngan hang TMCP Viet A" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Viet Nam Thuong Tin", NganHang = "13.Ngan hang TMCP Viet Nam Thuong Tin" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Xay dung VN(Dai Tin)", NganHang = "14.Ngan hang TMCP Xay dung VN(Dai Tin)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH BAO VIET(Bao Viet Bank)", NganHang = "15.NH BAO VIET(Bao Viet Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Tien Phong(Tien Phong Bank)", NganHang = "16.NH Tien Phong(Tien Phong Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH TMCP BUU DIEN LIEN VIET", NganHang = "17.NH TMCP BUU DIEN LIEN VIET" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP A Chau(ACB)", NganHang = "18.NHTMCP A Chau(ACB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP An Binh(ABBank)", NganHang = "19.NHTMCP An Binh(ABBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Bac A(Bac A bank)", NganHang = "20.NHTMCP Bac A(Bac A bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dai Duong(Oceanbank)", NganHang = "21.NHTMCP Dai Duong(Oceanbank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dau khi Toan cau(GPBank)", NganHang = "22.NHTMCP Dau khi Toan cau(GPBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dong A(Dong A bank)", NganHang = "23.NHTMCP Dong A(Dong A bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dong Nam A(Seabank)", NganHang = "24.NHTMCP Dong Nam A(Seabank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Hang Hai(Maritime Bank)", NganHang = "25.NHTMCP Hang Hai(Maritime Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Kien Long(Kien Long bank)", NganHang = "26.NHTMCP Kien Long(Kien Long bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Ky thuong VN(Techcombank)", NganHang = "27.NHTMCP Ky thuong VN(Techcombank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Nam A(Nam A Bank)", NganHang = "28.NHTMCP Nam A(Nam A Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP phat trien Tp HCM(HD Bank)", NganHang = "29.NHTMCP phat trien Tp HCM(HD Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Phuong Dong(OCB)", NganHang = "30.NHTMCP Phuong Dong(OCB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Phuong Nam(Southern Bank)", NganHang = "31.NHTMCP Phuong Nam(Southern Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Quan Doi(MB)", NganHang = "32.NHTMCP Quan Doi(MB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Quoc Te(VIB)", NganHang = "33.NHTMCP Quoc Te(VIB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Sai Gon(SCB)", NganHang = "34.NHTMCP Sai Gon(SCB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Sai gon -Ha Noi(SHB)", NganHang = "35.NHTMCP Sai gon -Ha Noi(SHB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Sai gon Thuong Tin(Sacombank)", NganHang = "36.NHTMCP Sai gon Thuong Tin(Sacombank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP SG Cong Thuong(SaigonBank)", NganHang = "37.NHTMCP SG Cong Thuong(SaigonBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Viet Hoa(Viet hoa JS bank)", NganHang = "38.NHTMCP Viet Hoa(Viet hoa JS bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP VN Thinh Vuong(VP Bank)", NganHang = "39.NHTMCP VN Thinh Vuong(VP Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Xang dau Petrolimex(PGBank)", NganHang = "40.NHTMCP Xang dau Petrolimex(PGBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Xuat Nhap Khau(Eximbank)", NganHang = "41.NHTMCP Xuat Nhap Khau(Eximbank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "PV com bank_NH Dai Chung(P.Tay+TCDK)", NganHang = "42.PV com bank_NH Dai Chung(P.Tay+TCDK)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Shinhan Bank", NganHang = "43.Shinhan Bank" });

            ViewBag.DSNganHang = DSNganHang;
            return View();
        }

        public ActionResult CapNhatKhachHangTemp(string CMND)
        {
            List<DanhMucTaiSanModel> LoaiTaiSan = new List<DanhMucTaiSanModel>();
            List<DanhMucNganangModel> DSNganHang = new List<DanhMucNganangModel>();
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang ngoai thuong Viet Nam(VietcomBank)", NganHang = "1.Ngan hang ngoai thuong Viet Nam(VietcomBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang Dau tu va Phat trien VN (BIDV)", NganHang = "2.Ngan hang Dau tu va Phat trien VN (BIDV)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Chinh sach xa hoi (VBSP)", NganHang = "3.NH Chinh sach xa hoi (VBSP)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Cong thuong VN (Vietinbank)", NganHang = "4.NH Cong thuong VN (Vietinbank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Nong nghiep & PT Nong thon VN-AGribank", NganHang = "5.NH Nong nghiep & PT Nong thon VN-AGribank" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Phat trien Nha DBSCL (MHB)", NganHang = "6.NH Phat trien Nha DBSCL (MHB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Phat trien Viet Nam (VDB)", NganHang = "7.NH Phat trien Viet Nam (VDB)" });

            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Ban Viet", NganHang = "8.Ngan hang TMCP Ban Viet" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Dai A", NganHang = "9.Ngan hang TMCP Dai A" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Phat trien MeKong", NganHang = "10.Ngan hang TMCP Phat trien MeKong" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Quoc Dan(Nam Viet cu)", NganHang = "11.Ngan hang TMCP Quoc Dan(Nam Viet cu)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Viet A", NganHang = "12.Ngan hang TMCP Viet A" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Viet Nam Thuong Tin", NganHang = "13.Ngan hang TMCP Viet Nam Thuong Tin" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Ngan hang TMCP Xay dung VN(Dai Tin)", NganHang = "14.Ngan hang TMCP Xay dung VN(Dai Tin)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH BAO VIET(Bao Viet Bank)", NganHang = "15.NH BAO VIET(Bao Viet Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH Tien Phong(Tien Phong Bank)", NganHang = "16.NH Tien Phong(Tien Phong Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NH TMCP BUU DIEN LIEN VIET", NganHang = "17.NH TMCP BUU DIEN LIEN VIET" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP A Chau(ACB)", NganHang = "18.NHTMCP A Chau(ACB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP An Binh(ABBank)", NganHang = "19.NHTMCP An Binh(ABBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Bac A(Bac A bank)", NganHang = "20.NHTMCP Bac A(Bac A bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dai Duong(Oceanbank)", NganHang = "21.NHTMCP Dai Duong(Oceanbank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dau khi Toan cau(GPBank)", NganHang = "22.NHTMCP Dau khi Toan cau(GPBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dong A(Dong A bank)", NganHang = "23.NHTMCP Dong A(Dong A bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Dong Nam A(Seabank)", NganHang = "24.NHTMCP Dong Nam A(Seabank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Hang Hai(Maritime Bank)", NganHang = "25.NHTMCP Hang Hai(Maritime Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Kien Long(Kien Long bank)", NganHang = "26.NHTMCP Kien Long(Kien Long bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Ky thuong VN(Techcombank)", NganHang = "27.NHTMCP Ky thuong VN(Techcombank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Nam A(Nam A Bank)", NganHang = "28.NHTMCP Nam A(Nam A Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP phat trien Tp HCM(HD Bank)", NganHang = "29.NHTMCP phat trien Tp HCM(HD Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Phuong Dong(OCB)", NganHang = "30.NHTMCP Phuong Dong(OCB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Phuong Nam(Southern Bank)", NganHang = "31.NHTMCP Phuong Nam(Southern Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Quan Doi(MB)", NganHang = "32.NHTMCP Quan Doi(MB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Quoc Te(VIB)", NganHang = "33.NHTMCP Quoc Te(VIB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Sai Gon(SCB)", NganHang = "34.NHTMCP Sai Gon(SCB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Sai gon -Ha Noi(SHB)", NganHang = "35.NHTMCP Sai gon -Ha Noi(SHB)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Sai gon Thuong Tin(Sacombank)", NganHang = "36.NHTMCP Sai gon Thuong Tin(Sacombank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP SG Cong Thuong(SaigonBank)", NganHang = "37.NHTMCP SG Cong Thuong(SaigonBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Viet Hoa(Viet hoa JS bank)", NganHang = "38.NHTMCP Viet Hoa(Viet hoa JS bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP VN Thinh Vuong(VP Bank)", NganHang = "39.NHTMCP VN Thinh Vuong(VP Bank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Xang dau Petrolimex(PGBank)", NganHang = "40.NHTMCP Xang dau Petrolimex(PGBank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "NHTMCP Xuat Nhap Khau(Eximbank)", NganHang = "41.NHTMCP Xuat Nhap Khau(Eximbank)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "PV com bank_NH Dai Chung(P.Tay+TCDK)", NganHang = "42.PV com bank_NH Dai Chung(P.Tay+TCDK)" });
            DSNganHang.Add(new DanhMucNganangModel { ID = "Shinhan Bank", NganHang = "43.Shinhan Bank" });

            ViewBag.DSNganHang = DSNganHang;
            var model = TIT.Datas.Services.KhachHangService.LayThongTinKhachHangDayDuTheoCMNDVaIdNhanVien(CMND, User.Identity.GetUserId());
            return View(model);
        }

        [HttpPost]
        public ActionResult ThemMoiKhachHangTemp(KhachHangDataModel model)
        {
            int result = -1;
            if (ModelState.IsValid)
            {
                result = TIT.Datas.Services.KhachHangService.ThemMoiHoacCapNhatKhachHangTheoNhanVienTemp(model, User.Identity.GetUserId());
            }
            if (result > 0)
                return RedirectToAction("CapNhatKhachHangTemp", "KhachHang", new { CMND = model.CMND });
            else
                return View(model);
        }

        [HttpPost]
        public ActionResult ThemMoiKhachHang(KhachHangModel model)
        {
            int result = -1;
            if (ModelState.IsValid)
            {
                result = TIT.Datas.Services.KhachHangService.ThemMoiHoacCapNhatKhachHangTheoNhanVien(model, User.Identity.GetUserId());
            }
            if (result > 0)
                return RedirectToAction("CapNhatKhachHangTemp", "KhachHang", new { CMND = model.CMND });
            else
                return View(model);
        }

        public ActionResult CapNhatKhachHang(string CMND)
        {
            KhachHangModel model;
            if (User.IsInRole("Admin"))
            {
                model = TIT.Datas.Services.KhachHangService.LayThongTinKhachHangTheoCMND_Admin(CMND);
            }
            else
            {
                model = TIT.Datas.Services.KhachHangService.LayThongTinKhachHangTheoCMNDVaIdQuanLy(CMND, User.Identity.GetUserId());
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CapNhatKhachHang(KhachHangModel model)
        {
            int result = -1;
            if (ModelState.IsValid)
            {
                result = TIT.Datas.Services.KhachHangService.ThemMoiHoacCapNhatKhachHangTheoNhanVien(model, User.Identity.GetUserId());
            }
            if (result > 0)
                return RedirectToAction("Index", "KhachHang");
            else
                return View(model);
        }

        public ActionResult Async_Save_CMND(IEnumerable<HttpPostedFileBase> cmnd1, string CMND)
        {
            if (string.IsNullOrEmpty(CMND))
                return Content("CMND không được để trống!!!!");
            // The Name of the Upload component is "files"
            if (cmnd1 != null && cmnd1.Count() > 0)
            {
                var file = cmnd1.FirstOrDefault();
                // Some browsers send file names with full path.
                // We are only interested in the file name.
                var fileName = Path.GetFileName(file.FileName);
                var path = Server.MapPath("~/Uploads") + "\\" + CMND;
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);
                var physicalPath = Path.Combine(path, "cmnd_mat_truoc" + Path.GetExtension(file.FileName));

                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);
                var NewPath = Url.Action(CMND, "Uploads", null, Request.Url.Scheme) + "/" + "cmnd_mat_truoc" + Path.GetExtension(file.FileName);
                return Json(new { status = "OK", newPath = NewPath }, "text/plain");
            }

            // Return an empty string to signify success
            return Content("Upload file thất bại");
        }

        public ActionResult Async_Save_HinhDaiDien(IEnumerable<HttpPostedFileBase> hinhdaidien, string CMND)
        {
            if (string.IsNullOrEmpty(CMND))
                return Content("CMND không được để trống!!!!");
            // The Name of the Upload component is "files"
            if (hinhdaidien != null && hinhdaidien.Count() > 0)
            {
                var file = hinhdaidien.FirstOrDefault();
                // Some browsers send file names with full path.
                // We are only interested in the file name.
                var fileName = Path.GetFileName(file.FileName);
                var path = Server.MapPath("~/Uploads") + "\\" + CMND;
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);
                var physicalPath = Path.Combine(path, "hinh_dai_dien" + Path.GetExtension(file.FileName));

                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);
                var NewPath = Url.Action(CMND, "Uploads", null, Request.Url.Scheme) + "/" + "hinh_dai_dien" + Path.GetExtension(file.FileName);
                return Json(new { status = "OK", newPath = NewPath }, "text/plain");
            }

            // Return an empty string to signify success
            return Content("Upload file thất bại");
        }

        public ActionResult Async_Save_CMND_2(IEnumerable<HttpPostedFileBase> cmnd2, string CMND)
        {
            if (string.IsNullOrEmpty(CMND))
                return Content("CMND không được để trống!!!!");
            // The Name of the Upload component is "files"
            if (cmnd2 != null && cmnd2.Count() > 0)
            {
                var file = cmnd2.FirstOrDefault();
                // Some browsers send file names with full path.
                // We are only interested in the file name.
                var fileName = Path.GetFileName(file.FileName);
                var path = Server.MapPath("~/Uploads") + "\\" + CMND;
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);
                var physicalPath = Path.Combine(path, "cmnd_mat_sau" + Path.GetExtension(file.FileName));

                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);
                var NewPath = Url.Action(CMND, "Uploads", null, Request.Url.Scheme) + "/" + "cmnd_mat_sau" + Path.GetExtension(file.FileName);
                return Json(new { status = "OK", newPath = NewPath }, "text/plain");
            }

            // Return an empty string to signify success
            return Content("Upload file thất bại");
        }
    }
}
