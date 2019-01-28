using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class CuaHangService
    {
        public static IEnumerable<CuaHangDataModel> GetListViewCuaHangTheoIdQuanLy(string IdQuanLy)
        {
            IEnumerable<CuaHangDataModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from ch in context.CuaHangs
                          where ch.QuanLyCuaHang == IdQuanLy
                          select new CuaHangDataModel()
                          {
                              MaCuaHang = ch.MaCuaHang,
                              TenCuaHang = ch.TenCuaHang,
                              QuyTienMat = ch.QuyTienMat.HasValue ? ch.QuyTienMat.Value : 0,
                              VonDauTu = ch.VonDauTu.HasValue ? ch.VonDauTu.Value : 0,
                              DiaChi = ch.DiaChi
                          }).ToArray();
            }
            return result;
        }

        public static IEnumerable<CuaHangDataModel> GetListViewCuaHangTheoIdKeToan(string IdKeToan)
        {
            IEnumerable<CuaHangDataModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from ch in context.CuaHangs
                          join user in context.AspNetUsers on IdKeToan equals user.Id
                          where ch.QuanLyCuaHang == user.ManagerId
                          select new CuaHangDataModel()
                          {
                              MaCuaHang = ch.MaCuaHang,
                              TenCuaHang = ch.TenCuaHang,
                              QuyTienMat = ch.QuyTienMat.HasValue ? ch.QuyTienMat.Value : 0,
                              VonDauTu = ch.VonDauTu.HasValue ? ch.VonDauTu.Value : 0,
                              DiaChi = ch.DiaChi
                          }).ToArray();
            }
            return result;
        }
        public static IEnumerable<CuaHangDataModel> GetListViewCuaHangTheoIdNhanVien(string maNhanVien)
        {
            IEnumerable<CuaHangDataModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from nv in context.AspNetUsers
                          join ch in context.CuaHangs on nv.CuaHang_Id equals ch.MaCuaHang
                          where nv.Id == maNhanVien
                          select new CuaHangDataModel()
                          {
                              MaCuaHang = ch.MaCuaHang,
                              TenCuaHang = ch.TenCuaHang,
                              QuyTienMat = ch.QuyTienMat.HasValue ? ch.QuyTienMat.Value : 0,
                              VonDauTu = ch.VonDauTu.HasValue ? ch.VonDauTu.Value : 0,
                              DiaChi = ch.DiaChi

                          }).ToArray();
            }
            return result;
        }

        public static IEnumerable<CuaHangDataModel> GetListViewCuaHangTheoAdmin()
        {
            IEnumerable<CuaHangDataModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from  ch in context.CuaHangs
                          select new CuaHangDataModel()
                          {
                              MaCuaHang = ch.MaCuaHang,
                              TenCuaHang = ch.TenCuaHang,
                              QuyTienMat = ch.QuyTienMat.HasValue? ch.QuyTienMat.Value : 0,
                              VonDauTu = ch.VonDauTu.HasValue? ch.VonDauTu.Value: 0,
                              DiaChi = ch.DiaChi
                          }).ToArray();
            }
            return result;
        }

        internal static int LayIdCuaHangTheoIdNhanVien(string idNhanVien)
        {
            int? result;
            using (var context = new TIT_Entities())
            {
                result = (from user in context.AspNetUsers
                          where user.Id == idNhanVien
                          select user.CuaHang_Id).SingleOrDefault();
            }
            return result.HasValue? result.Value : -1;
        }

        public static IEnumerable<CuaHangGridViewModel> GetGridViewCuaHangTheoIdNhanVien(string MaNhanVien)
        {
            IEnumerable<CuaHangGridViewModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from nv in context.AspNetUsers
                          join ch in context.CuaHangs on nv.CuaHang_Id equals ch.MaCuaHang
                          where nv.Id == MaNhanVien
                          select new CuaHangGridViewModel()
                          {
                              MaCuaHang = ch.MaCuaHang,
                              TenCuaHang = ch.TenCuaHang,
                              DiaChi = ch.DiaChi,
                              DienThoai = "Không có",
                              NgayTao = ch.NgayTao,
                              NguoiTao = ch.NguoiTao,
                              QuyTienMat = ch.QuyTienMat.HasValue? ch.QuyTienMat.Value: 0,
                              VonDauTu = ch.VonDauTu.HasValue? ch.VonDauTu.Value: 0,
                              TinhTrang = ch.TinhTrang
                          }).ToArray();
            }
            return result;
        }

        public static int LayCuaHangIdTheoIdNhanVien(string id_NhanVien)
        {
            int result = -1;
            using (var context = new TIT_Entities())
            {
                result = (from nv in context.AspNetUsers
                          where nv.Id == id_NhanVien
                          select nv.CuaHang_Id).FirstOrDefault().Value;
            }
            return result;
        }

        public static BaoCaoCuaHangTempViewModel LayBaoCaoCuaHang(int cuaHang_Id)
        {
            BaoCaoCuaHangTempViewModel result;

            using (var context = new TIT_Entities())
            {
                var date = DateTime.Now.Date;
                result = (from nv in context.BaoCaoHangNgays
                          where nv.Id_CuaHang == cuaHang_Id && nv.Ngay == date
                          select new BaoCaoCuaHangTempViewModel()
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
                          }).FirstOrDefault();
                if (result == null)
                    return new BaoCaoCuaHangTempViewModel();
            }
            return result;
        }

        public static int CapNhatCuaHangTheoQuanLy(CuaHangUpdateModel model)
        {
            using (var context = new TIT_Entities())
            {
                //update
                var item_update = context.CuaHangs.SingleOrDefault(x => x.MaCuaHang == model.MaCuaHang);
                if (item_update != null)
                {
                    item_update.DiaChi = model.DiaChi;
                    item_update.Latitude = model.Latitude;
                    item_update.Longitude = model.Longitude;
                    item_update.QuyTienMat = model.QuyTienMat;
                    item_update.TenCuaHang = model.TenCuaHang;
                    item_update.TinhTrang = model.TinhTrang ? 1 : 0;
                    item_update.VonDauTu = model.VonDauTu;

                    try
                    {
                        if (context.SaveChanges() > 0)
                        {
                            return 1;
                        }
                        else
                            return 0;
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        //_logger.Error(ex.ToString());
                        return 0;
                    }
                }
                
            }
            return 0;
        }

        public static int CapNhatCuaHangTheoAdmin(CuaHangUpdateModel model)
        {
            using (var context = new TIT_Entities())
            {
                //update
                var item_update = context.CuaHangs.SingleOrDefault(x => x.MaCuaHang == model.MaCuaHang);
                if (item_update != null)
                {
                    item_update.DiaChi = model.DiaChi;
                    item_update.Latitude = model.Latitude;
                    item_update.Longitude = model.Longitude;
                    item_update.QuyTienMat = model.QuyTienMat;
                    item_update.TenCuaHang = model.TenCuaHang;
                    item_update.TinhTrang = model.TinhTrang ? 1 : 0;
                    item_update.VonDauTu = model.VonDauTu;
                    item_update.QuanLyCuaHang = model.QuanLyCuaHang;

                    try
                    {
                        if (context.SaveChanges() > 0)
                        {
                            return 1;
                        }
                        else
                            return 0;
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        //_logger.Error(ex.ToString());
                        return 0;
                    }
                }

            }
            return 0;
        }

        public static CuaHangDetailModel LayCuaHangTheoIDCuaHang(int maCuaHang)
        {
            CuaHangDetailModel result = null;
            using (var context = new TIT_Entities())
            {
                result = (from ch in context.CuaHangs 
                          where ch.MaCuaHang == maCuaHang
                          select new CuaHangDetailModel()
                          {
                              MaCuaHang = ch.MaCuaHang,
                              TenCuaHang = ch.TenCuaHang,
                              DiaChi = ch.DiaChi,
                              DienThoai = "Không có",
                              NgayTao = ch.NgayTao,
                              NguoiTao = ch.NguoiTao,
                              QuyTienMat = ch.QuyTienMat.HasValue ? ch.QuyTienMat.Value : 0,
                              VonDauTu = ch.VonDauTu.HasValue ? ch.VonDauTu.Value : 0,
                              Latitude = ch.Latitude.HasValue? ch.Latitude.Value: 0,
                              Longitude = ch.Longitude.HasValue? ch.Longitude.Value: 0,
                              TinhTrang = ch.TinhTrang== 1? true: false
                          }).FirstOrDefault();
            }

            return result;
        }
        public static CuaHangUpdateModel LayCuaHangUpdateTheoIDCuaHang(int maCuaHang)
        {
            CuaHangUpdateModel result = null;
            using (var context = new TIT_Entities())
            {
                result = (from ch in context.CuaHangs
                          where ch.MaCuaHang == maCuaHang
                          select new CuaHangUpdateModel()
                          {
                              MaCuaHang = ch.MaCuaHang,
                              TenCuaHang = ch.TenCuaHang,
                              DiaChi = ch.DiaChi,
                              DienThoai = "Không có",
                              NgayTao = ch.NgayTao,
                              NguoiTao = ch.NguoiTao,
                              QuyTienMat = ch.QuyTienMat.HasValue ? ch.QuyTienMat.Value : 0,
                              VonDauTu = ch.VonDauTu.HasValue ? ch.VonDauTu.Value : 0,
                              Latitude = ch.Latitude.HasValue ? ch.Latitude.Value : 0,
                              Longitude = ch.Longitude.HasValue ? ch.Longitude.Value : 0,
                              TinhTrang = ch.TinhTrang == 1 ? true : false,
                              TKQuanLy=ch.QuanLyCuaHang
                          }).FirstOrDefault();
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
                TinhTrang = model.TinhTrang ? 1 : 0,
                QuanLyCuaHang = model.TKQuanLy
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

        public static IEnumerable<NhanVienModel> LayDanhSachNhanVienTheoIdCuaHang(int idCuaHang)
        {
            IEnumerable<NhanVienModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from nv in context.AspNetUsers
                          where nv.CuaHang_Id == idCuaHang
                          select new NhanVienModel()
                          {
                              MaNhanVien = nv.Id,
                              TenNhanVien = nv.FullName
                          }).ToArray();
            }
            return result;
        }
        public static IEnumerable<NhanVienModel> LayDanhSachNhanVienTheoIDQuanLy(string idQuanLy)
        {
            IEnumerable<NhanVienModel> result;

            using (var context = new TIT_Entities())
            {
                result = (from nv in context.AspNetUsers                        
                          where nv.ManagerId == idQuanLy
                          select new NhanVienModel()
                          {
                              MaNhanVien = nv.Id,
                              TenNhanVien = nv.FullName
                          }).ToArray();
            }
            return result;
        }

    }
}
