using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class ReportService
    {
        //Chi Phí Hoạt Động
        public static IEnumerable<ReportChiPhiHoatDongGridViewModel> LayDanhSachThuChi(IEnumerable<CuaHangDataModel> models, DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            IEnumerable<ReportChiPhiHoatDongGridViewModel> result;
            NgayKetThuc = NgayKetThuc.AddDays(1);
            var DanhSachIDCuaHang = models.Select(x => x.MaCuaHang);
            using (var context = new TIT_Entities())
            {
                result = (from tc in context.ThongTinThuChis
                          join ch in context.CuaHangs on tc.Id_CuaHang equals ch.MaCuaHang
                          join nv in context.AspNetUsers on tc.Id_NhanVien equals nv.Id
                          where DanhSachIDCuaHang.Contains(tc.Id_CuaHang)
                          select new ReportChiPhiHoatDongGridViewModel()
                          {
                              KhachHang = tc.KhachHang,
                              GhiChu = tc.GhiChu,
                              LoaiPhieu = tc.LoaiPhieu,
                              SoTien = tc.SoTien,
                              Ngay = tc.NgayTao,
                              PhieuThu = tc.PhieuThu,
                              CuaHang = new CuaHangDataModel()
                              {
                                  MaCuaHang = ch.MaCuaHang,
                                  TenCuaHang = ch.TenCuaHang
                              },
                              NhanVien = nv.UserName
                          }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }
            return result;
        }

        // Tổng Quỹ Tiền Mặt

        public static IEnumerable<ReportTongQuyTienMatGridViewModel> LayDanhSachTongQuyTienMatTheoCuaHang(DateTime NgayBatDau, DateTime NgayKetThuc, int cuaHang_Id)
        {
            List<ReportTongQuyTienMatGridViewModel> result = new List<ReportTongQuyTienMatGridViewModel>();
            ReportTongQuyTienMatGridViewModel r = new ReportTongQuyTienMatGridViewModel();
            NgayKetThuc = NgayKetThuc.AddDays(1);
            IEnumerable<BaoCaoCuaHangTempViewModel> resultCH;
            using (var context = new TIT_Entities())
            {
                resultCH = (from nv in context.BaoCaoHangNgays
                            where nv.Id_CuaHang == cuaHang_Id && nv.Ngay <= NgayKetThuc && nv.Ngay >=NgayBatDau
                            select new BaoCaoCuaHangTempViewModel
                            {
                                TongChi = nv.TongChi,
                                TongChiBatHo = nv.TongChiHopDongBatHo,
                                TongChiCamDo = nv.TongChiHopDongCamDo,
                                TongChiChoVay = nv.TongChiHopDongChoVay,
                                TongChiDNGD = nv.TongChiHopDongChoVayDNGD,
                                TongChiKhac = nv.TongChiKhac,
                                TongSoTienConLai = nv.SoTienVonConLai,
                                TongSoTienDauNgay = nv.SoTienVonDauNgay,
                                TongThu = nv.TongThu,
                                TongThuBatHo = nv.TongThuHopDongBatHo,
                                TongThuCamDo = nv.TongThuHopDongCamDo,
                                TongThuChoVay = nv.TongThuHopDongChoVay,
                                TongThuDNGD = nv.TongThuHopDongChoVayDNGD,
                                TongThuKhac = nv.TongThuKhac
                            }).ToArray();
            }
            Decimal SoTienDauNgay = 0, SoTienConLai = 0, TongThu = 0, TongChi = 0;

            foreach (BaoCaoCuaHangTempViewModel rs in resultCH)
            {
                SoTienDauNgay = SoTienDauNgay + rs.TongSoTienDauNgay;
                SoTienConLai = SoTienConLai + rs.TongSoTienConLai;
                TongThu = TongThu + rs.TongThu;
                TongChi = TongChi + rs.TongChi;
            }
            r.SoTienVonDauNgay = SoTienDauNgay;
            r.SoTienVonConLai = SoTienConLai;
            r.TongThu = TongThu;
            r.TongChi = TongChi;
            result.Add(r);
            return result;
        }
        //Sổ Quỹ Tiền Mặt
        public static IEnumerable<ReportSoQuyTienMatGridViewModel> LayDanhSachSoQuyTienMatTheoNhanVien(DateTime NgayBatDau, DateTime NgayKetThuc, string IdNhanVien)
        {

            List<ReportSoQuyTienMatGridViewModel> result = new List<ReportSoQuyTienMatGridViewModel>();
            NgayKetThuc = NgayKetThuc.AddDays(1);

            IEnumerable<ReportSoQuyTienMatGridViewModel> resultBH;
            using (var context = new TIT_Entities())
            {
                resultBH = (from lstt in context.LichSuThaoTacs
                            join bh in context.HD_BatHo on lstt.Id_HopDong equals bh.HD_BatHo_Id
                            join kh in context.KhachHangs on bh.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            where user.Id == IdNhanVien
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Bát Họ",
                                TaiSan = "Vay Họ",
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }
            foreach (ReportSoQuyTienMatGridViewModel rs in resultBH)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCD;
            using (var context = new TIT_Entities())
            {
                resultCD = (from lstt in context.LichSuThaoTacs
                            join cd in context.HD_CamDo on lstt.Id_HopDong equals cd.HD_CamDo_Id
                            join kh in context.KhachHangs on cd.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            where user.Id == IdNhanVien
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cầm Đồ",
                                TaiSan = cd.TaiSan,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCD)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from lstt in context.LichSuThaoTacs
                            join cv in context.HD_ChoVay on lstt.Id_HopDong equals cv.HD_ChoVay_Id
                            join kh in context.KhachHangs on cv.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            where user.Id == IdNhanVien
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cho Vay",
                                TaiSan = cv.TenTaiSanTheChap,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCD)
                result.Add(rs);

            return result;
        }

        public static IEnumerable<BaoCaoTongHop> LayBaoCaoTongHop(int id_CuaHang, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            IEnumerable<BaoCaoTongHop> result;
            using (var context = new TIT_Entities())
            {
                result = (from lstt in context.LichSuThaoTacs
                          join nv in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals nv.Id
                          where lstt.Id_CuaHang == id_CuaHang && lstt.NgayThaoTac >= ngayBatDau && lstt.NgayThaoTac <= ngayKetThuc
                          select new BaoCaoTongHop
                          {
                              NoiDung = lstt.NoiDung,
                              Ngay = lstt.NgayThaoTac,
                              Thu = lstt.ThuTien== 1? lstt.SoTien:0,
                              Chi = lstt.ThuTien == 0 ? lstt.SoTien : 0,
                              TongCongTon = lstt.TongCongTon,
                              Id_HopDong = lstt.Id_HopDong,
                              TenNhanVien = nv.UserName,
                              TenKhachHang = lstt.TenKhachHang
                          }).ToArray();
            }
            return result;
        }

        public static IEnumerable<ReportSoQuyTienMatGridViewModel> LayDanhSachSoQuyTienMatTheoCuaHang(DateTime NgayBatDau, DateTime NgayKetThuc, string IdQuanLy)
        {

            List<ReportSoQuyTienMatGridViewModel> result = new List<ReportSoQuyTienMatGridViewModel>();
            NgayKetThuc = NgayKetThuc.AddDays(1);

            IEnumerable<ReportSoQuyTienMatGridViewModel> resultBH;
            using (var context = new TIT_Entities())
            {
                resultBH = (from lstt in context.LichSuThaoTacs
                            join bh in context.HD_BatHo on lstt.Id_HopDong equals bh.HD_BatHo_Id
                            join kh in context.KhachHangs on bh.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            join ch in context.CuaHangs on bh.CuaHang_Id equals ch.MaCuaHang
                            where ch.QuanLyCuaHang == IdQuanLy
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Bát Họ",
                                TaiSan = "Vay Họ",
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }
            foreach (ReportSoQuyTienMatGridViewModel rs in resultBH)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCD;
            using (var context = new TIT_Entities())
            {
                resultCD = (from lstt in context.LichSuThaoTacs
                            join cd in context.HD_CamDo on lstt.Id_HopDong equals cd.HD_CamDo_Id
                            join kh in context.KhachHangs on cd.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            join ch in context.CuaHangs on cd.CuaHang_Id equals ch.MaCuaHang
                            where ch.QuanLyCuaHang == IdQuanLy
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cầm Đồ",
                                TaiSan = cd.TaiSan,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCD)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from lstt in context.LichSuThaoTacs
                            join cv in context.HD_ChoVay on lstt.Id_HopDong equals cv.HD_ChoVay_Id
                            join kh in context.KhachHangs on cv.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            join ch in context.CuaHangs on cv.CuaHang_Id equals ch.MaCuaHang
                            where ch.QuanLyCuaHang == IdQuanLy
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cho Vay",
                                TaiSan = cv.TenTaiSanTheChap,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCD)
                result.Add(rs);

            return result;
        }

        public static IEnumerable<ReportSoQuyTienMatGridViewModel> LayDanhSachSoQuyTienMat(DateTime NgayBatDau, DateTime NgayKetThuc)
        {

            List<ReportSoQuyTienMatGridViewModel> result = new List<ReportSoQuyTienMatGridViewModel>();
            NgayKetThuc = NgayKetThuc.AddDays(1);

            IEnumerable<ReportSoQuyTienMatGridViewModel> resultBH;            
            using (var context = new TIT_Entities())
            {
                resultBH = (from lstt in context.LichSuThaoTacs
                          join bh in context.HD_BatHo on lstt.Id_HopDong equals bh.HD_BatHo_Id
                          join kh in context.KhachHangs on bh.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            select new ReportSoQuyTienMatGridViewModel
                          {
                              MaHD = lstt.Id_HopDong,
                              Ngay = lstt.NgayThaoTac,
                              KhachHang = kh,
                              NhanVien = user.FullName,
                              LoaiGiaoDich = "Bát Họ",
                              TaiSan = "Vay Họ",
                              SoTien= lstt.SoTien,
                              ThuTien = lstt.ThuTien
                          }).Where(x=>(x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }
            foreach (ReportSoQuyTienMatGridViewModel rs in resultBH)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCD;
            using (var context = new TIT_Entities())
            {
                resultCD = (from lstt in context.LichSuThaoTacs
                            join cd in context.HD_CamDo on lstt.Id_HopDong equals cd.HD_CamDo_Id
                            join kh in context.KhachHangs on cd.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cầm Đồ",
                                TaiSan = cd.TaiSan,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCD)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from lstt in context.LichSuThaoTacs
                            join cv in context.HD_ChoVay on lstt.Id_HopDong equals cv.HD_ChoVay_Id
                            join kh in context.KhachHangs on cv.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cho Vay",
                                TaiSan = cv.TenTaiSanTheChap,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCV)
                result.Add(rs);

            // Vay Dư Nợ Giảm Dần
            IEnumerable<ReportSoQuyTienMatGridViewModel> resultDNGD;
            using (var context = new TIT_Entities())
            {
                resultDNGD = (from lstt in context.LichSuThaoTacs
                              join DNGN in context.HD_VayDuNoGiamDan on lstt.Id_HopDong equals DNGN.HD_Id
                              join kh in context.KhachHangs on DNGN.KhachHang_CMND equals kh.CMND
                              join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                              select new ReportSoQuyTienMatGridViewModel
                              {
                                  MaHD = lstt.Id_HopDong,
                                  Ngay =lstt.NgayThaoTac,
                                  KhachHang = kh,
                                  NhanVien = user.FullName,
                                  LoaiGiaoDich = "Vay_DNGD",
                                  TaiSan= "Vay DNGD",
                                  SoTien = lstt.SoTien,
                                  ThuTien = lstt.ThuTien
                              }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }
            foreach (ReportSoQuyTienMatGridViewModel rs in resultDNGD)
                result.Add(rs);

            return result;
        }

        // Số Quỹ Tiền Mặt theo cửa hàng
        public static IEnumerable<ReportSoQuyTienMatGridViewModel> LayDanhSachSoQuyTienMatTheoCuaHang(DateTime NgayBatDau, DateTime NgayKetThuc,int MaCuaHang)
        {

            List<ReportSoQuyTienMatGridViewModel> result = new List<ReportSoQuyTienMatGridViewModel>();
            NgayKetThuc = NgayKetThuc.AddDays(1);

            IEnumerable<ReportSoQuyTienMatGridViewModel> resultBH;
            using (var context = new TIT_Entities())
            {
                resultBH = (from lstt in context.LichSuThaoTacs
                            join bh in context.HD_BatHo on lstt.Id_HopDong equals bh.HD_BatHo_Id
                            join kh in context.KhachHangs on bh.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            where lstt.Id_CuaHang == MaCuaHang
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Bát Họ",
                                TaiSan = "Vay Họ",
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }
            foreach (ReportSoQuyTienMatGridViewModel rs in resultBH)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCD;
            using (var context = new TIT_Entities())
            {
                resultCD = (from lstt in context.LichSuThaoTacs
                            join cd in context.HD_CamDo on lstt.Id_HopDong equals cd.HD_CamDo_Id
                            join kh in context.KhachHangs on cd.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            where lstt.Id_CuaHang == MaCuaHang
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cầm Đồ",
                                TaiSan = cd.TaiSan,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCD)
                result.Add(rs);


            IEnumerable<ReportSoQuyTienMatGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from lstt in context.LichSuThaoTacs
                            join cv in context.HD_ChoVay on lstt.Id_HopDong equals cv.HD_ChoVay_Id
                            join kh in context.KhachHangs on cv.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                            where lstt.Id_CuaHang == MaCuaHang
                            select new ReportSoQuyTienMatGridViewModel
                            {
                                MaHD = lstt.Id_HopDong,
                                Ngay = lstt.NgayThaoTac,
                                KhachHang = kh,
                                NhanVien = user.FullName,
                                LoaiGiaoDich = "Cho Vay",
                                TaiSan = cv.TenTaiSanTheChap,
                                SoTien = lstt.SoTien,
                                ThuTien = lstt.ThuTien
                            }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportSoQuyTienMatGridViewModel rs in resultCV)
                result.Add(rs);

            // Vay Dư Nợ Giảm Dần
            IEnumerable<ReportSoQuyTienMatGridViewModel> resultDNGD;
            using (var context = new TIT_Entities())
            {
                resultDNGD = (from lstt in context.LichSuThaoTacs
                              join DNGN in context.HD_VayDuNoGiamDan on lstt.Id_HopDong equals DNGN.HD_Id
                              join kh in context.KhachHangs on DNGN.KhachHang_CMND equals kh.CMND
                              join user in context.AspNetUsers on lstt.Id_NhanVienThaoTac equals user.Id
                              where lstt.Id_CuaHang == MaCuaHang
                              select new ReportSoQuyTienMatGridViewModel
                              {
                                  MaHD = lstt.Id_HopDong,
                                  Ngay = lstt.NgayThaoTac,
                                  KhachHang = kh,
                                  NhanVien = user.FullName,
                                  LoaiGiaoDich = "Vay_DNGD",
                                  TaiSan = "Vay DNGD",
                                  SoTien = lstt.SoTien,
                                  ThuTien = lstt.ThuTien
                              }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }
            foreach (ReportSoQuyTienMatGridViewModel rs in resultDNGD)
                result.Add(rs);
            // Thu CHi
            IEnumerable<ReportChiPhiHoatDongGridViewModel> result_TC;            
            using (var context = new TIT_Entities())
            {
                result_TC = (from tc in context.ThongTinThuChis
                          join ch in context.CuaHangs on tc.Id_CuaHang equals ch.MaCuaHang
                          join nv in context.AspNetUsers on tc.Id_NhanVien equals nv.Id
                          where tc.Id_CuaHang == MaCuaHang
                          select new ReportChiPhiHoatDongGridViewModel()
                          {
                              KhachHang = tc.KhachHang,
                              GhiChu = tc.GhiChu,
                              LoaiPhieu = tc.LoaiPhieu,
                              SoTien = tc.SoTien,
                              Ngay = tc.NgayTao,
                              PhieuThu = tc.PhieuThu,
                              CuaHang = new CuaHangDataModel()
                              {
                                  MaCuaHang = ch.MaCuaHang,
                                  TenCuaHang = ch.TenCuaHang
                              },
                              NhanVien = nv.UserName
                          }).Where(x => (x.Ngay <= NgayKetThuc && x.Ngay >= NgayBatDau)).ToArray();
            }

            foreach (ReportChiPhiHoatDongGridViewModel tc in result_TC)
            {
                ReportSoQuyTienMatGridViewModel resultTC = new ReportSoQuyTienMatGridViewModel();
                KhachHang kh= new KhachHang();
                kh.TenKhachHang = tc.KhachHang;
                resultTC.KhachHang = kh;
                resultTC.MaHD = tc.LoaiPhieu;
                resultTC.Ngay = tc.Ngay;
                resultTC.NhanVien = tc.NhanVien;
                if (tc.PhieuThu == 0)
                {
                    resultTC.TaiSan = "Phiếu Thu";
                    resultTC.LoaiGiaoDich = "Phiếu Thu";
                }
                else
                {
                    resultTC.LoaiGiaoDich = "Phiếu Chi";
                    resultTC.TaiSan = "Phiếu Chi";
                }
                resultTC.SoTien = tc.SoTien;
                if (tc.PhieuThu == 0)
                {
                    resultTC.ThuTien = 1;
                }
                else
                {
                    resultTC.ThuTien = 0;
                }
                result.Add(resultTC);
            }

            return result;
        }

        // Chậm Đóng Tiền
        public static IEnumerable<ReportHopDongChamDongTienGridViewModel> LayHopDongChamDongTienTheoIdNhanVien(string IdNhanVien)
        {

            List<ReportHopDongChamDongTienGridViewModel> result = new List<ReportHopDongChamDongTienGridViewModel>();

            // lấy hợp đồng bát họ chậm tiền
            IEnumerable<HopDongBatHoGridViewModel> resultBH;
            using (var context = new TIT_Entities())
            {
                resultBH = (from hd in context.HD_BatHo
                            join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on hd.CuaHang_Id equals user.CuaHang_Id
                            where user.Id == IdNhanVien
                            select new HopDongBatHoGridViewModel
                            {
                                KhachHang = new KhachHangModel()
                                {
                                    TenKhachHang = kh.TenKhachHang,
                                    SoDienThoai = kh.SoDienThoai1
                                },
                                Id_HopDong = hd.HD_BatHo_Id,
                                NgayBoc = hd.NgayBoc,
                                NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayBoc,
                                NoCu = 0,
                                TienGiaoKhach = hd.TienDuaChoKhach,
                                BatHo = hd.BatHo,
                                BocTrongVong = hd.BocTrongVong,
                                SoNgayDongTien = hd.SoNgayDongTien
                                
                            }).Where(x => x.NgayDongTienCuoiCung < DateTime.Now).ToArray();
            }
            foreach (HopDongBatHoGridViewModel bh in resultBH)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = bh.Id_HopDong;
                CDT.KhachHang = bh.KhachHang;
                CDT.TaiSan = "Vay Họ";
                CDT.SoTienGoc = bh.BatHo;
                CDT.LaiDen = bh.TienMotNgay;
                CDT.NgayVay = bh.NgayBoc;
                CDT.NgayPhaiDongTien = bh.NgayPhaiDongTien;
                CDT.VND = bh.TienMotNgay.ToString();
                CDT.TrangThai = bh.TrangThai;
                CDT.MauTinhTrang = bh.MauTinhTrang;
                CDT.LoaiHinh = "Bát Họ";
                result.Add(CDT);
            }

          
            // lấy hợp đồng cho vay chậm trả tiền
            IEnumerable<HopDongChoVayGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from hd in context.HD_ChoVay                          
                            join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on hd.CuaHang_Id equals user.CuaHang_Id
                            where user.Id == IdNhanVien
                            select new HopDongChoVayGridViewModel
                            {
                                STT = 1,
                                KhachHang = new KhachHangModel()
                                {
                                    TenKhachHang = kh.TenKhachHang,
                                    SoDienThoai = kh.SoDienThoai1
                                },

                                MaHopDong = hd.HD_ChoVay_Id,
                                NgayVay = hd.NgayVay,
                                NgayCuoiCungDongLai = hd.NgayCuoiCungDongTienLai.HasValue ? hd.NgayCuoiCungDongTienLai.Value : hd.NgayVay,
                                TinhTrang = hd.TinhTrang.ToString(),
                                NoCu = 0,
                                SoTienVay = hd.SoTienVay,
                                TaiSan = hd.TenTaiSanTheChap,
                                HinhThucLai = hd.HinhThucLai,
                                Lai = (int)hd.Lai
                            }).Where(x => x.NgayCuoiCungDongLai < DateTime.Now).ToArray();

            }
            foreach (HopDongChoVayGridViewModel cv in resultCV)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = cv.MaHopDong;
                CDT.KhachHang = cv.KhachHang;
                CDT.TaiSan = cv.TaiSan;
                CDT.SoTienGoc = cv.SoTienVay;
                CDT.LaiDen = cv.LaiDen;
                CDT.NgayVay = cv.NgayVay;
                CDT.NgayPhaiDongTien = cv.NgayCuoiCungDongLai;
                CDT.VND = cv.LaiDen.ToString();
                CDT.TrangThai = cv.TrangThai;
                CDT.MauTinhTrang = cv.MauTinhTrang;
                CDT.LoaiHinh = "Cho Vay";
                result.Add(CDT);
            }
            
            // Hợp đồng cho vay DNGD
            IEnumerable<HopDongVayDNGDGridViewModel> resultDNGD;
            using (var context = new TIT_Entities())
            {
                resultDNGD = (from hd in context.HD_VayDuNoGiamDan
                            join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                            join user in context.AspNetUsers on hd.CuaHang_Id equals user.CuaHang_Id
                            where user.Id == IdNhanVien
                            select new HopDongVayDNGDGridViewModel
                            {
                                KhachHang = new KhachHangModel()
                                {
                                    TenKhachHang = kh.TenKhachHang,
                                    SoDienThoai = kh.SoDienThoai1
                                },
                                HoaHong = hd.HoaHong,
                                LaiSuat = hd.LaiSuat,
                                NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayVay,
                                NgayVay = hd.NgayVay,
                                SoTien = hd.SoTienVay,
                                ThoiHanVay = hd.ThoiHanVay,
                                Id_HopDong = hd.HD_Id,
                                TinhTrang = hd.TinhTrang,
                                DaDong = hd.TienLaiDaDong,
                                NgayBatDauThanhToan = hd.NgayBatDauDongLai
                                        
                            }).Where(x => x.NgayDongTienCuoiCung < DateTime.Now && x.TinhTrang != "Thanh Lý").ToArray();

            }
            foreach (HopDongVayDNGDGridViewModel DNGD in resultDNGD)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = DNGD.Id_HopDong;
                CDT.KhachHang = DNGD.KhachHang;
                CDT.TaiSan = "Cho vay DNGD";
                CDT.SoTienGoc = DNGD.SoTien;
                CDT.LaiDen = 0;
                CDT.NgayVay = DNGD.NgayVay;
                CDT.NgayPhaiDongTien = DNGD.NgayPhaiDongTien;
                CDT.VND = "";
                CDT.TrangThai = DNGD.TrangThai;
                CDT.MauTinhTrang = DNGD.MauTinhTrang;
                CDT.LoaiHinh = "Cho vay DNGD";
                result.Add(CDT);
            }
                       
            return result;
        }
        public static IEnumerable<ReportHopDongChamDongTienGridViewModel> LayHopDongChamDongTienTheoAdmin()
        {

            
            List<ReportHopDongChamDongTienGridViewModel> result= new List<ReportHopDongChamDongTienGridViewModel>();

            // lấy hợp đồng bát họ chậm tiền
            IEnumerable<HopDongBatHoGridViewModel> resultBH;
            using (var context = new TIT_Entities())
            {
                resultBH = (from hd in context.HD_BatHo
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND                            
                          select new HopDongBatHoGridViewModel
                          {
                              KhachHang = new KhachHangModel()
                              {
                                  TenKhachHang = kh.TenKhachHang,
                                  SoDienThoai = kh.SoDienThoai1
                              },
                              Id_HopDong = hd.HD_BatHo_Id,
                              NgayBoc = hd.NgayBoc,
                              NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayBoc,
                              NoCu = 0,
                              TienGiaoKhach = hd.TienDuaChoKhach,
                              BatHo = hd.BatHo,
                              BocTrongVong = hd.BocTrongVong,
                              SoNgayDongTien = hd.SoNgayDongTien
                          }).Where(x=>x.NgayDongTienCuoiCung<DateTime.Now).ToArray();              
            }
            foreach (HopDongBatHoGridViewModel bh in resultBH)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = bh.Id_HopDong;
                CDT.KhachHang = bh.KhachHang;
                CDT.TaiSan = "Vay Họ";
                CDT.SoTienGoc = bh.BatHo;
                CDT.LaiDen = bh.TienMotNgay;
                CDT.NgayVay = bh.NgayBoc;
                CDT.NgayPhaiDongTien = bh.NgayPhaiDongTien;
                CDT.VND = bh.TienMotNgay.ToString();
                CDT.TrangThai = bh.TrangThai;
                CDT.MauTinhTrang = bh.MauTinhTrang;
                CDT.LoaiHinh = "Bát Họ";
                result.Add(CDT);
            }

        
            // lấy hợp đồng cho vay chậm trả tiền
            IEnumerable<HopDongChoVayGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from hd in context.HD_ChoVay
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          select new HopDongChoVayGridViewModel
                          {
                              STT = 1,
                              KhachHang = new KhachHangModel()
                              {
                                  TenKhachHang = kh.TenKhachHang,
                                  SoDienThoai = kh.SoDienThoai1
                              },

                              MaHopDong = hd.HD_ChoVay_Id,
                              NgayVay = hd.NgayVay,
                              NgayCuoiCungDongLai = hd.NgayCuoiCungDongTienLai.HasValue ? hd.NgayCuoiCungDongTienLai.Value : hd.NgayVay,
                              TinhTrang = hd.TinhTrang.ToString(),
                              NoCu = 0,
                              SoTienVay = hd.SoTienVay,
                              TaiSan = hd.TenTaiSanTheChap,
                              HinhThucLai = hd.HinhThucLai,
                              Lai = (int)hd.Lai
                          }).Where(x => x.NgayCuoiCungDongLai < DateTime.Now).ToArray();

            }
            foreach (HopDongChoVayGridViewModel cv in resultCV)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = cv.MaHopDong;
                CDT.KhachHang = cv.KhachHang;
                CDT.TaiSan = cv.TaiSan;
                CDT.SoTienGoc = cv.SoTienVay;
                CDT.LaiDen = cv.LaiDen;
                CDT.NgayVay = cv.NgayVay;
                CDT.NgayPhaiDongTien = cv.NgayCuoiCungDongLai;
                CDT.VND = cv.LaiDen.ToString();
                CDT.TrangThai = cv.TrangThai;
                CDT.MauTinhTrang = cv.MauTinhTrang;
                CDT.LoaiHinh = "Cho Vay";
                result.Add(CDT);
            }
 
            // Hợp đồng cho vay DNGD
            IEnumerable<HopDongVayDNGDGridViewModel> resultDNGD;
            using (var context = new TIT_Entities())
            {
                resultDNGD = (from hd in context.HD_VayDuNoGiamDan
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          select new HopDongVayDNGDGridViewModel
                          {
                              KhachHang = new KhachHangModel()
                              {
                                  TenKhachHang = kh.TenKhachHang,
                                  SoDienThoai = kh.SoDienThoai1
                              },
                              HoaHong = hd.HoaHong,
                              LaiSuat = hd.LaiSuat,
                              NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayVay,
                              NgayVay = hd.NgayVay,
                              SoTien = hd.SoTienVay,
                              ThoiHanVay = hd.ThoiHanVay,
                              Id_HopDong = hd.HD_Id,
                              TinhTrang = hd.TinhTrang
                              
                          }).Where(x => x.NgayDongTienCuoiCung < DateTime.Now && x.TinhTrang != "Thanh Lý").ToArray();

            }
            foreach (HopDongVayDNGDGridViewModel DNGD in resultDNGD)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = DNGD.Id_HopDong;
                CDT.KhachHang = DNGD.KhachHang;
                CDT.TaiSan = "Cho vay DNGD";
                CDT.SoTienGoc = DNGD.SoTien;
                CDT.LaiDen = 0;
                CDT.NgayVay = DNGD.NgayVay;
                CDT.NgayPhaiDongTien = DNGD.NgayPhaiDongTien;
                CDT.VND = "";
                CDT.TrangThai = DNGD.TrangThai;
                CDT.MauTinhTrang = DNGD.MauTinhTrang;
                CDT.LoaiHinh = "Cho vay DNGD";
                result.Add(CDT);
            }
            
            return result;
        }
        public static IEnumerable<ReportHopDongChamDongTienGridViewModel> LayHopDongChamDongTienTheoIdQuanLy(string IdQuanLy)
        {

            List<ReportHopDongChamDongTienGridViewModel> result = new List<ReportHopDongChamDongTienGridViewModel>();

            // lấy hợp đồng bát họ chậm tiền
            IEnumerable<HopDongBatHoGridViewModel> resultBH;
            using (var context = new TIT_Entities())
            {
                resultBH = (from hd in context.HD_BatHo
                            join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                            join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                            where ch.QuanLyCuaHang == IdQuanLy
                            select new HopDongBatHoGridViewModel
                            {
                                KhachHang = new KhachHangModel()
                                {
                                    TenKhachHang = kh.TenKhachHang,
                                    SoDienThoai = kh.SoDienThoai1
                                },
                                Id_HopDong = hd.HD_BatHo_Id,
                                NgayBoc = hd.NgayBoc,
                                NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayBoc,
                                NoCu = 0,
                                TienGiaoKhach = hd.TienDuaChoKhach,
                                BatHo = hd.BatHo,
                                BocTrongVong = hd.BocTrongVong,
                                SoNgayDongTien = hd.SoNgayDongTien
                            }).Where(x => x.NgayDongTienCuoiCung < DateTime.Now).ToArray();
            }
            foreach (HopDongBatHoGridViewModel bh in resultBH)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = bh.Id_HopDong;
                CDT.KhachHang = bh.KhachHang;
                CDT.TaiSan = "Vay Họ";
                CDT.SoTienGoc = bh.BatHo;
                CDT.LaiDen = bh.TienMotNgay;
                CDT.NgayVay = bh.NgayBoc;
                CDT.NgayPhaiDongTien = bh.NgayPhaiDongTien;
                CDT.VND = bh.TienMotNgay.ToString();
                CDT.TrangThai = bh.TrangThai;
                CDT.MauTinhTrang = bh.MauTinhTrang;
                CDT.LoaiHinh = "Bát Họ";
                result.Add(CDT);
            }

        

            // lấy hợp đồng cho vay chậm trả tiền
            IEnumerable<HopDongChoVayGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from hd in context.HD_ChoVay
                            join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                            join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                            where ch.QuanLyCuaHang == IdQuanLy
                            select new HopDongChoVayGridViewModel
                            {
                                STT = 1,
                                KhachHang = new KhachHangModel()
                                {
                                    TenKhachHang = kh.TenKhachHang,
                                    SoDienThoai = kh.SoDienThoai1
                                },

                                MaHopDong = hd.HD_ChoVay_Id,
                                NgayVay = hd.NgayVay,
                                NgayCuoiCungDongLai = hd.NgayCuoiCungDongTienLai.HasValue ? hd.NgayCuoiCungDongTienLai.Value : hd.NgayVay,
                                TinhTrang = hd.TinhTrang.ToString(),
                                NoCu = 0,
                                SoTienVay = hd.SoTienVay,
                                TaiSan = hd.TenTaiSanTheChap,
                                HinhThucLai = hd.HinhThucLai,
                                Lai = (int)hd.Lai
                            }).Where(x => x.NgayCuoiCungDongLai < DateTime.Now).ToArray();

            }
            foreach (HopDongChoVayGridViewModel cv in resultCV)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = cv.MaHopDong;
                CDT.KhachHang = cv.KhachHang;
                CDT.TaiSan = cv.TaiSan;
                CDT.SoTienGoc = cv.SoTienVay;
                CDT.LaiDen = cv.LaiDen;
                CDT.NgayVay = cv.NgayVay;
                CDT.NgayPhaiDongTien = cv.NgayCuoiCungDongLai;
                CDT.VND = cv.LaiDen.ToString();
                CDT.TrangThai = cv.TrangThai;
                CDT.MauTinhTrang = cv.MauTinhTrang;
                CDT.LoaiHinh = "Cho Vay";
                result.Add(CDT);
            }
            
                       // Hợp đồng cho vay DNGD
                       IEnumerable<HopDongVayDNGDGridViewModel> resultDNGD;
                       using (var context = new TIT_Entities())
                       {
                           resultDNGD = (from hd in context.HD_VayDuNoGiamDan
                                     join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                                     join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                                     where ch.QuanLyCuaHang == IdQuanLy
                                     select new HopDongVayDNGDGridViewModel
                                     {
                                         KhachHang = new KhachHangModel()
                                         {
                                             TenKhachHang = kh.TenKhachHang,
                                             SoDienThoai = kh.SoDienThoai1
                                         },
                                         HoaHong = hd.HoaHong,
                                         LaiSuat = hd.LaiSuat,
                                         NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayVay,
                                         NgayVay = hd.NgayVay,
                                         SoTien = hd.SoTienVay,
                                         ThoiHanVay = hd.ThoiHanVay,
                                         Id_HopDong = hd.HD_Id,
                                         TinhTrang = hd.TinhTrang
                                     }).Where(x => x.NgayDongTienCuoiCung < DateTime.Now && x.TinhTrang != "Thanh Lý").ToArray();

                       }
                       foreach (HopDongVayDNGDGridViewModel DNGD in resultDNGD)
                       {
                           ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                           CDT.MaHD = DNGD.Id_HopDong;
                           CDT.KhachHang = DNGD.KhachHang;
                           CDT.TaiSan = "Cho vay DNGD";
                           CDT.SoTienGoc = DNGD.SoTien;
                           CDT.LaiDen = 0;
                           CDT.NgayVay = DNGD.NgayVay;
                           CDT.NgayPhaiDongTien = DNGD.NgayPhaiDongTien;
                           CDT.VND = "";
                           CDT.TrangThai = DNGD.TrangThai;
                           CDT.MauTinhTrang = DNGD.MauTinhTrang;
                           CDT.LoaiHinh = "Cho vay DNGD";
                           result.Add(CDT);
                       }
                       
            return result;
        }

        public static IEnumerable<ReportHopDongChamDongTienGridViewModel> LayHopDongChamDongTienTheoIdKeToan(string IdKeToan)
        {

            List<ReportHopDongChamDongTienGridViewModel> result = new List<ReportHopDongChamDongTienGridViewModel>();

            // lấy hợp đồng bát họ chậm tiền
            IEnumerable<HopDongBatHoGridViewModel> resultBH;
            using (var context = new TIT_Entities())
            {
                resultBH = (from hd in context.HD_BatHo
                            join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                            join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                            join user in context.AspNetUsers on IdKeToan equals user.Id
                            where ch.QuanLyCuaHang == user.ManagerId
                            select new HopDongBatHoGridViewModel
                            {
                                KhachHang = new KhachHangModel()
                                {
                                    TenKhachHang = kh.TenKhachHang,
                                    SoDienThoai = kh.SoDienThoai1
                                },
                                Id_HopDong = hd.HD_BatHo_Id,
                                NgayBoc = hd.NgayBoc,
                                NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayBoc,
                                NoCu = 0,
                                TienGiaoKhach = hd.TienDuaChoKhach,
                                BatHo = hd.BatHo,
                                BocTrongVong = hd.BocTrongVong,
                                SoNgayDongTien = hd.SoNgayDongTien
                            }).Where(x => x.NgayDongTienCuoiCung < DateTime.Now).ToArray();
            }
            foreach (HopDongBatHoGridViewModel bh in resultBH)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = bh.Id_HopDong;
                CDT.KhachHang = bh.KhachHang;
                CDT.TaiSan = "Vay Họ";
                CDT.SoTienGoc = bh.BatHo;
                CDT.LaiDen = bh.TienMotNgay;
                CDT.NgayVay = bh.NgayBoc;
                CDT.NgayPhaiDongTien = bh.NgayPhaiDongTien;
                CDT.VND = bh.TienMotNgay.ToString();
                CDT.TrangThai = bh.TrangThai;
                CDT.MauTinhTrang = bh.MauTinhTrang;
                CDT.LoaiHinh = "Bát Họ";
                result.Add(CDT);
            }

       
            // lấy hợp đồng cho vay chậm trả tiền
            IEnumerable<HopDongChoVayGridViewModel> resultCV;
            using (var context = new TIT_Entities())
            {
                resultCV = (from hd in context.HD_ChoVay
                            join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                            join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                            join user in context.AspNetUsers on IdKeToan equals user.Id
                            where ch.QuanLyCuaHang == user.ManagerId
                            select new HopDongChoVayGridViewModel
                            {
                                STT = 1,
                                KhachHang = new KhachHangModel()
                                {
                                    TenKhachHang = kh.TenKhachHang,
                                    SoDienThoai = kh.SoDienThoai1
                                },

                                MaHopDong = hd.HD_ChoVay_Id,
                                NgayVay = hd.NgayVay,
                                NgayCuoiCungDongLai = hd.NgayCuoiCungDongTienLai.HasValue ? hd.NgayCuoiCungDongTienLai.Value : hd.NgayVay,
                                TinhTrang = hd.TinhTrang.ToString(),
                                NoCu = 0,
                                SoTienVay = hd.SoTienVay,
                                TaiSan = hd.TenTaiSanTheChap,
                                HinhThucLai = hd.HinhThucLai,
                                Lai = (int)hd.Lai
                            }).Where(x => x.NgayCuoiCungDongLai < DateTime.Now).ToArray();

            }
            foreach (HopDongChoVayGridViewModel cv in resultCV)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = cv.MaHopDong;
                CDT.KhachHang = cv.KhachHang;
                CDT.TaiSan = cv.TaiSan;
                CDT.SoTienGoc = cv.SoTienVay;
                CDT.LaiDen = cv.LaiDen;
                CDT.NgayVay = cv.NgayVay;
                CDT.NgayPhaiDongTien = cv.NgayCuoiCungDongLai;
                CDT.VND = cv.LaiDen.ToString();
                CDT.TrangThai = cv.TrangThai;
                CDT.MauTinhTrang = cv.MauTinhTrang;
                CDT.LoaiHinh = "Cho Vay";
                result.Add(CDT);
            }

            // Hợp đồng cho vay DNGD
            IEnumerable<HopDongVayDNGDGridViewModel> resultDNGD;
            using (var context = new TIT_Entities())
            {
                resultDNGD = (from hd in context.HD_VayDuNoGiamDan
                              join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                              join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                              join user in context.AspNetUsers on IdKeToan equals user.Id
                              where ch.QuanLyCuaHang == user.ManagerId
                              select new HopDongVayDNGDGridViewModel
                              {
                                  KhachHang = new KhachHangModel()
                                  {
                                      TenKhachHang = kh.TenKhachHang,
                                      SoDienThoai = kh.SoDienThoai1
                                  },
                                  HoaHong = hd.HoaHong,
                                  LaiSuat = hd.LaiSuat,
                                  NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayVay,
                                  NgayVay = hd.NgayVay,
                                  SoTien = hd.SoTienVay,
                                  ThoiHanVay = hd.ThoiHanVay,
                                  Id_HopDong = hd.HD_Id,
                                  TinhTrang = hd.TinhTrang
                              }).Where(x => x.NgayDongTienCuoiCung < DateTime.Now && x.TinhTrang != "Thanh Lý").ToArray();

            }
            foreach (HopDongVayDNGDGridViewModel DNGD in resultDNGD)
            {
                ReportHopDongChamDongTienGridViewModel CDT = new ReportHopDongChamDongTienGridViewModel();
                CDT.MaHD = DNGD.Id_HopDong;
                CDT.KhachHang = DNGD.KhachHang;
                CDT.TaiSan = "Cho vay DNGD";
                CDT.SoTienGoc = DNGD.SoTien;
                CDT.LaiDen = 0;
                CDT.NgayVay = DNGD.NgayVay;
                CDT.NgayPhaiDongTien = DNGD.NgayPhaiDongTien;
                CDT.VND = "";
                CDT.TrangThai = DNGD.TrangThai;
                CDT.MauTinhTrang = DNGD.MauTinhTrang;
                CDT.LoaiHinh = "Cho vay DNGD";
                result.Add(CDT);
            }

            return result;
        }
    }
}
