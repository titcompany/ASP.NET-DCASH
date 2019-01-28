using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Helper;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class KhachHangService
    {
      
        public static KhachHangModel TimKiemKhachHangTheoCMND(string cmnd)
        {
            KhachHangModel result;
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangs
                          where kh.CMND == cmnd
                          select new KhachHangModel()
                          {
                               CMND = kh.CMND,
                               DiaChi = kh.DiaChi,
                               HinhDaiDien = kh.HinhDaiDien,
                               SoDienThoai = kh.SoDienThoai1,
                               TenKhachHang = kh.TenKhachHang
                          }).FirstOrDefault();
                if (result != null)
                {
                    ThongKeHopDongTheoKhachHang hopDongDNGD = TIT.Datas.Services.ChoVayDNGDService.LayThongKeHopDongTheoKhachHang(result.CMND);
                    result.ThongKe = hopDongDNGD;
                }
            }

            return result;
        }
        public static IEnumerable<KhachHangModel> LayDanhSachKhachHangTheoIDNhanVien(string IDNhanVien)
        {
            IEnumerable<KhachHangModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangThuocCuaHangs
                          join nv in context.AspNetUsers on  kh.Id_CuaHang equals nv.CuaHang_Id
                          where nv.Id == IDNhanVien
                          select new KhachHangModel()
                          {
                              CMND = kh.CMND,
                              DiaChi = kh.DiaChi,
                              HinhDaiDien = kh.HinhDaiDien,
                              SoDienThoai = kh.SoDienThoai1,
                              TenKhachHang = kh.TenKhachHang,
                              NgayTao = kh.NgayTao
                          }).ToArray();
            }
            return result;
        }
        public static int ThemMoiHoacCapNhatKhachHangTheoNhanVien(KhachHangModel model, string IdNhanVien)
        {
            using (var context = new TIT_Entities())
            {
                string Id_QuanLy = AccountService.LayIdNguoiQuanLyTheoIdNhanVien(IdNhanVien);
                int Id_CuaHang = CuaHangService.LayIdCuaHangTheoIdNhanVien(IdNhanVien);
                var result = context.KhachHangs.Where(x => x.CMND == model.CMND).FirstOrDefault();
                if (result == null)
                {
                    var dbmodelKhachHang = new KhachHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        SoDienThoai1 = model.SoDienThoai,
                        TenKhachHang = model.TenKhachHang,
                    };
                    var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        Id_CuaHang = Id_CuaHang,
                        Id_QuanLy = Id_QuanLy,
                        NgayTao = DateTime.Now,
                        SoDienThoai1 = model.SoDienThoai,
                        TenKhachHang = model.TenKhachHang
                    };
                    context.KhachHangs.Add(dbmodelKhachHang);
                    context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                        return 0;
                    }

                }
                else
                {
                    result.DiaChi = model.DiaChi;
                    result.HinhDaiDien = model.HinhDaiDien;
                    result.SoDienThoai1 = model.SoDienThoai;
                    result.TenKhachHang = model.TenKhachHang;

                    var result2 = context.KhachHangThuocCuaHangs.Where(x => x.CMND == model.CMND && x.Id_CuaHang == Id_CuaHang).FirstOrDefault();
                    if (result2 == null)
                    {
                        var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                        {
                            CMND = model.CMND,
                            DiaChi = model.DiaChi,
                            HinhDaiDien = model.HinhDaiDien,
                            Id_CuaHang = Id_CuaHang,
                            Id_QuanLy = Id_QuanLy,
                            NgayTao = DateTime.Now,
                            SoDienThoai1 = model.SoDienThoai,
                            TenKhachHang = model.TenKhachHang
                        };

                        context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                            return 0;
                        }
                    }
                    else
                    {
                        result2.DiaChi = model.DiaChi;
                        result2.HinhDaiDien = model.HinhDaiDien;
                        result2.Id_QuanLy = Id_QuanLy;
                        result2.NgayTao = DateTime.Now;
                        result2.SoDienThoai1 = model.SoDienThoai;
                        result2.TenKhachHang = model.TenKhachHang;
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
                            return 0;
                        }
                    }
                }
            }
        }
        public static int ThemMoiHoacCapNhatKhachHangTheoQuanLy(KhachHangModel model, string Id_QuanLy, int Id_CuaHang)
        {
            using (var context = new TIT_Entities())
            {
                var result = context.KhachHangs.Where(x => x.CMND == model.CMND).FirstOrDefault();
                if (result == null)
                {
                    var dbmodelKhachHang = new KhachHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        SoDienThoai1 = model.SoDienThoai,
                        TenKhachHang = model.TenKhachHang
                    };
                    var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        Id_CuaHang = Id_CuaHang,
                        Id_QuanLy = Id_QuanLy,
                        NgayTao = DateTime.Now,
                        SoDienThoai1 = model.SoDienThoai,
                        TenKhachHang = model.TenKhachHang
                    };
                    context.KhachHangs.Add(dbmodelKhachHang);
                    context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                        return 0;
                    }

                }
                else
                {
                    result.DiaChi = model.DiaChi;
                    result.HinhDaiDien = model.HinhDaiDien;
                    result.SoDienThoai1 = model.SoDienThoai;
                    result.TenKhachHang = model.TenKhachHang;

                    var result2 = context.KhachHangThuocCuaHangs.Where(x => x.CMND == model.CMND && x.Id_CuaHang == Id_CuaHang).FirstOrDefault();
                    if (result2 == null)
                    {
                        var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                        {
                            CMND = model.CMND,
                            DiaChi = model.DiaChi,
                            HinhDaiDien = model.HinhDaiDien,
                            Id_CuaHang = Id_CuaHang,
                            Id_QuanLy = Id_QuanLy,
                            NgayTao = DateTime.Now,
                            SoDienThoai1 = model.SoDienThoai,
                            TenKhachHang = model.TenKhachHang
                        };

                        context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                            return 0;
                        }
                    }
                    else
                    {
                        result2.DiaChi = model.DiaChi;
                        result2.HinhDaiDien = model.HinhDaiDien;
                        result2.Id_QuanLy = Id_QuanLy;
                        result2.NgayTao = DateTime.Now;
                        result2.SoDienThoai1 = model.SoDienThoai;
                        result2.TenKhachHang = model.TenKhachHang;
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
                            return 0;
                        }
                    }
                }
            }
        }

        public static KhachHangModel LayThongTinKhachHangTheoCMNDVaIdQuanLy(string CMND, string IdQuanLy)
        {
            KhachHangModel result = new KhachHangModel();
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangThuocCuaHangs
                            where IdQuanLy == kh.Id_QuanLy && kh.CMND == CMND
                            select new KhachHangModel()
                            {
                                CMND = kh.CMND,
                                DiaChi = kh.DiaChi,
                                HinhDaiDien = kh.HinhDaiDien,
                                SoDienThoai = kh.SoDienThoai1,
                                TenKhachHang = kh.TenKhachHang,
                                NgayTao = kh.NgayTao
                                
                            }).FirstOrDefault();
               
            }
            return result;
        }
        public static KhachHangModel LayThongTinKhachHangTheoCMNDVaIdNhanVien(string CMND, string IdNhanVien)
        {
            KhachHangModel result = new KhachHangModel();
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangThuocCuaHangs
                          join user in context.AspNetUsers on kh.Id_CuaHang equals user.CuaHang_Id
                          where IdNhanVien == user.Id && kh.CMND == CMND
                          select new KhachHangModel()
                          {
                              CMND = kh.CMND,
                              DiaChi = kh.DiaChi,
                              HinhDaiDien = kh.HinhDaiDien,
                              SoDienThoai = kh.SoDienThoai1,
                              TenKhachHang = kh.TenKhachHang,
                              NgayTao = kh.NgayTao

                          }).FirstOrDefault();

            }
            return result;
        }
        public static KhachHangModel LayThongTinKhachHangTheoCMND_Admin(string CMND)
        {
            KhachHangModel result;
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangs
                          where kh.CMND == CMND
                          select new KhachHangModel()
                          {
                              CMND = kh.CMND,
                              DiaChi = kh.DiaChi,
                              HinhDaiDien = kh.HinhDaiDien,
                              SoDienThoai = kh.SoDienThoai1,
                              TenKhachHang = kh.TenKhachHang
                          }).FirstOrDefault();
            }
            return result;
        }

        public static IEnumerable<KhachHangModel> LayDanhSachKhachHangTheoIDQuanLy(string IdQuanLy)
        {
            IEnumerable<KhachHangModel> result = new List<KhachHangModel>();
            using (var context = new TIT_Entities())
            {
                var temp = (from kh in context.KhachHangThuocCuaHangs
                            where IdQuanLy == kh.Id_QuanLy
                            select new KhachHangModel()
                            {
                                CMND = kh.CMND,
                                DiaChi = kh.DiaChi,
                                HinhDaiDien = kh.HinhDaiDien,
                                SoDienThoai = kh.SoDienThoai1,
                                TenKhachHang = kh.TenKhachHang,
                                NgayTao = kh.NgayTao
                            }).ToList() ;
                if (temp != null)
                {
                    result = from element in temp
                              group element by element.CMND
                                into groups
                              select groups.OrderBy(p => p.NgayTao).First();


                }
                    //result.Add(temp);
            }
            return result;
        }
        public static IEnumerable<KhachHangModel> LayDanhSachKhachHangTheoAdmin()
        {
            IEnumerable<KhachHangModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangs
                          select new KhachHangModel()
                          {
                              CMND = kh.CMND,
                              DiaChi = kh.DiaChi,
                              HinhDaiDien = kh.HinhDaiDien,
                              SoDienThoai = kh.SoDienThoai1,
                              TenKhachHang = kh.TenKhachHang
                          }).ToArray();
            }
            return result;
        }

        public static KhachHangDataModel LayThongTinKhachHangDayDuTheoCMNDVaIdQuanLy(string CMND, string IdQuanLy)
        {
            KhachHangDataModel result = new KhachHangDataModel();
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangThuocCuaHangs
                          where IdQuanLy == kh.Id_QuanLy && kh.CMND == CMND
                          select new KhachHangDataModel()
                          {
                              CMND = kh.CMND,
                              DiaChi = kh.DiaChi,
                              HinhDaiDien = kh.HinhDaiDien,
                              SoDienThoai1 = kh.SoDienThoai1,
                              TenKhachHang = kh.TenKhachHang,
                              NgayTao = kh.NgayTao,
                              BienSoXe = kh.BienSoXe,
                              ChucVu = kh.ChucVu,
                              TinhTrangHonNhan = kh.TinhTrangHonNhan,
                              HinhCMNDMatTruoc = kh.HinhCMNDMatTruoc,
                              HinhCMNDMatSau = kh.HinhCMNDMatSau,
                              ChuTaiKhoan = kh.ChuTaiKhoan,
                              CotLuongTienMat = kh.CotLuongTienMat.HasValue ? kh.CotLuongTienMat.Value : 0,
                              DiaChiCongTy = kh.DiaChiCongTy,
                              DiaChiLamViecNguoiBaoLanh = kh.DiaChiLamViecNguoiBaoLanh,
                              DiaChiNhaNguoiBaoLanh = kh.DiaChiNguoiBaoLanh,
                              KhuVuc = kh.KhuVuc,
                              LoaiNha = kh.LoaiNha,
                              LoaiXe = kh.LoaiXe,
                              LuongCoBan = kh.LuongCoBan.HasValue ? kh.LuongCoBan.Value : 0,
                              MaHoaThe = kh.MaHoaThe,
                              NgayLanhLuong = kh.NgayLanhLuong.HasValue ? kh.NgayLanhLuong.Value : 0,
                              NguyenQuan = kh.NguyenQuan,
                              NgayCapCMND = kh.NgayCapCMND.HasValue ? kh.NgayCapCMND.Value : DateTime.MinValue,
                              NoiCapCMND = kh.NoiCapCMND,
                              PasswordThe = MaHoaTheHelper.GiaiMaThe(kh.MaHoaThe),
                              SoDienThoai2 = kh.SoDienThoai2,
                              SoDienThoaiNguoiBaoLanh = kh.SoDienThoaiNguoiBaoLanh,
                              SoHuuNha = kh.SoHuuNha,
                              SoHuuXe = kh.SoHuuXe,
                              SoTaiKhoan = kh.SoTaiKhoan,
                              SoThe = kh.SoThe,
                              TenCongTy = kh.TenCongTy,
                              TenNganHang = kh.TenNganHang,
                              TenNguoiBaoLanh = kh.TenNguoiBaoLanh,
                              ThoiGianLamViec = kh.ThoiGianLamViecBaoLau
                          }).FirstOrDefault();

            }
            return result;
        }
        public static KhachHangDataModel LayThongTinKhachHangDayDuTheoCMNDVaIdNhanVien(string CMND, string IdNhanVien)
        {
            KhachHangDataModel result = new KhachHangDataModel();
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangThuocCuaHangs
                          join user in context.AspNetUsers on kh.Id_CuaHang equals user.CuaHang_Id
                          where IdNhanVien == user.Id && kh.CMND == CMND
                          select new KhachHangDataModel()
                          {
                              CMND = kh.CMND,
                              DiaChi = kh.DiaChi,
                              HinhDaiDien = kh.HinhDaiDien,
                              SoDienThoai1 = kh.SoDienThoai1,
                              TenKhachHang = kh.TenKhachHang,
                              NgayTao = kh.NgayTao,
                              BienSoXe = kh.BienSoXe,
                              ChucVu = kh.ChucVu,
                              TinhTrangHonNhan = kh.TinhTrangHonNhan,
                              HinhCMNDMatTruoc = kh.HinhCMNDMatTruoc,
                              HinhCMNDMatSau = kh.HinhCMNDMatSau,
                              ChuTaiKhoan = kh.ChuTaiKhoan,
                              CotLuongTienMat = kh.CotLuongTienMat.HasValue ? kh.CotLuongTienMat.Value : 0,
                              DiaChiCongTy = kh.DiaChiCongTy,
                              DiaChiLamViecNguoiBaoLanh = kh.DiaChiLamViecNguoiBaoLanh,
                              DiaChiNhaNguoiBaoLanh = kh.DiaChiNguoiBaoLanh,
                              KhuVuc = kh.KhuVuc,
                              LoaiNha = kh.LoaiNha,
                              LoaiXe = kh.LoaiXe,
                              LuongCoBan = kh.LuongCoBan.HasValue ? kh.LuongCoBan.Value : 0,
                              MaHoaThe = kh.MaHoaThe,
                              NgayLanhLuong = kh.NgayLanhLuong.HasValue ? kh.NgayLanhLuong.Value : 0,
                              NguyenQuan = kh.NguyenQuan,
                              NgayCapCMND = kh.NgayCapCMND.HasValue ? kh.NgayCapCMND.Value : DateTime.MinValue,
                              NoiCapCMND = kh.NoiCapCMND,
                              SoDienThoai2 = kh.SoDienThoai2,
                              SoDienThoaiNguoiBaoLanh = kh.SoDienThoaiNguoiBaoLanh,
                              SoHuuNha = kh.SoHuuNha,
                              SoHuuXe = kh.SoHuuXe,
                              SoTaiKhoan = kh.SoTaiKhoan,
                              SoThe = kh.SoThe,
                              TenCongTy = kh.TenCongTy,
                              TenNganHang = kh.TenNganHang,
                              TenNguoiBaoLanh = kh.TenNguoiBaoLanh,
                              ThoiGianLamViec = kh.ThoiGianLamViecBaoLau
                          }).FirstOrDefault();
                result.PasswordThe = MaHoaTheHelper.GiaiMaThe(result.MaHoaThe);
            }
            return result;
        }
        public static KhachHangDataModel LayThongTinKhachHangDayDuTheoCMND_Admin(string CMND)
        {
            KhachHangDataModel result;
            using (var context = new TIT_Entities())
            {
                result = (from kh in context.KhachHangs
                          where kh.CMND == CMND
                          select new KhachHangDataModel()
                          {
                              CMND = kh.CMND,
                              DiaChi = kh.DiaChi,
                              HinhDaiDien = kh.HinhDaiDien,
                              SoDienThoai1 = kh.SoDienThoai1,
                              TenKhachHang = kh.TenKhachHang,
                              NgayTao = DateTime.MinValue,
                              BienSoXe = kh.BienSoXe,
                              ChucVu = kh.ChucVu,
                              TinhTrangHonNhan = kh.TinhTrangHonNhan,
                              HinhCMNDMatTruoc = kh.HinhCMNDMatTruoc,
                              HinhCMNDMatSau = kh.HinhCMNDMatSau,
                              ChuTaiKhoan = kh.ChuTaiKhoan,
                              CotLuongTienMat = kh.CotLuongTienMat.HasValue ? kh.CotLuongTienMat.Value : 0,
                              DiaChiCongTy = kh.DiaChiCongTy,
                              DiaChiLamViecNguoiBaoLanh = kh.DiaChiLamViecNguoiBaoLanh,
                              DiaChiNhaNguoiBaoLanh = kh.DiaChiNguoiBaoLanh,
                              KhuVuc = kh.KhuVuc,
                              LoaiNha = kh.LoaiNha,
                              LoaiXe = kh.LoaiXe,
                              LuongCoBan = kh.LuongCoBan.HasValue ? kh.LuongCoBan.Value : 0,
                              MaHoaThe = kh.MaHoaThe,
                              NgayLanhLuong = kh.NgayLanhLuong.HasValue ? kh.NgayLanhLuong.Value : 0,
                              NguyenQuan = kh.NguyenQuan,
                              NgayCapCMND = kh.NgayCapCMND.HasValue ? kh.NgayCapCMND.Value : DateTime.MinValue,
                              NoiCapCMND = kh.NoiCapCMND,
                              SoDienThoai2 = kh.SoDienThoai2,
                              SoDienThoaiNguoiBaoLanh = kh.SoDienThoaiNguoiBaoLanh,
                              SoHuuNha = kh.SoHuuNha,
                              SoHuuXe = kh.SoHuuXe,
                              SoTaiKhoan = kh.SoTaiKhoan,
                              SoThe = kh.SoThe,
                              TenCongTy = kh.TenCongTy,
                              TenNganHang = kh.TenNganHang,
                              TenNguoiBaoLanh = kh.TenNguoiBaoLanh,
                              ThoiGianLamViec = kh.ThoiGianLamViecBaoLau
                          }).FirstOrDefault();
                result.PasswordThe = MaHoaTheHelper.GiaiMaThe(result.MaHoaThe);
            }
            return result;
        }

        public static int ThemMoiHoacCapNhatKhachHangTheoNhanVienTemp(KhachHangDataModel model, string IdNhanVien)
        {
            using (var context = new TIT_Entities())
            {
                string Id_QuanLy = AccountService.LayIdNguoiQuanLyTheoIdNhanVien(IdNhanVien);
                int Id_CuaHang = CuaHangService.LayIdCuaHangTheoIdNhanVien(IdNhanVien);
                var result = context.KhachHangs.Where(x => x.CMND == model.CMND).FirstOrDefault();
                if (result == null)
                {
                    var dbmodelKhachHang = new KhachHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        SoDienThoai1 = model.SoDienThoai1,
                        SoDienThoai2 = model.SoDienThoai2,
                        TenKhachHang = model.TenKhachHang,
                        BienSoXe = model.BienSoXe,
                        ChucVu = model.ChucVu,
                        ChuTaiKhoan = model.ChuTaiKhoan,
                        CotLuongTienMat = model.CotLuongTienMat,
                        DiaChiCongTy = model.DiaChiCongTy,
                        DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh,
                        DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh,
                        KhuVuc = model.KhuVuc,
                        LoaiNha = model.LoaiNha,
                        LoaiXe = model.LoaiXe,
                        LuongCoBan = model.LuongCoBan,
                        MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe),
                        NgayCapCMND = model.NgayCapCMND,
                        NgayLanhLuong = model.NgayLanhLuong,
                        NguyenQuan = model.NguyenQuan,
                        NoiCapCMND = model.NoiCapCMND,
                        SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh,
                        SoHuuNha = model.SoHuuNha,
                        SoHuuXe = model.SoHuuXe,
                        SoTaiKhoan = model.SoTaiKhoan,
                        SoThe = model.SoThe,
                        TenCongTy = model.TenCongTy,
                        TenNganHang = model.TenNganHang,
                        TenNguoiBaoLanh = model.TenNguoiBaoLanh,
                        ThoiGianLamViecBaoLau = model.ThoiGianLamViec,
                        TinhTrangHonNhan = model.TinhTrangHonNhan,
                        HinhCMNDMatSau = model.HinhCMNDMatSau,
                        HinhCMNDMatTruoc = model.HinhCMNDMatTruoc,
                        
                       
                    };
                    var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        Id_CuaHang = Id_CuaHang,
                        Id_QuanLy = Id_QuanLy,
                        NgayTao = DateTime.Now,
                        SoDienThoai1 = model.SoDienThoai1,
                        SoDienThoai2 = model.SoDienThoai2,
                        TenKhachHang = model.TenKhachHang,
                        BienSoXe = model.BienSoXe,
                        ChucVu = model.ChucVu,
                        ChuTaiKhoan = model.ChuTaiKhoan,
                        CotLuongTienMat = model.CotLuongTienMat,
                        DiaChiCongTy = model.DiaChiCongTy,
                        DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh,
                        DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh,
                        KhuVuc = model.KhuVuc,
                        LoaiNha = model.LoaiNha,
                        LoaiXe = model.LoaiXe,
                        LuongCoBan = model.LuongCoBan,
                        MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe),
                        NgayCapCMND = model.NgayCapCMND,
                        NgayLanhLuong = model.NgayLanhLuong,
                        NguyenQuan = model.NguyenQuan,
                        NoiCapCMND = model.NoiCapCMND,
                        SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh,
                        SoHuuNha = model.SoHuuNha,
                        SoHuuXe = model.SoHuuXe,
                        SoTaiKhoan = model.SoTaiKhoan,
                        SoThe = model.SoThe,
                        TenCongTy = model.TenCongTy,
                        TenNganHang = model.TenNganHang,
                        TenNguoiBaoLanh = model.TenNguoiBaoLanh,
                        ThoiGianLamViecBaoLau = model.ThoiGianLamViec,
                        TinhTrangHonNhan = model.TinhTrangHonNhan,
                        HinhCMNDMatSau = model.HinhCMNDMatSau,
                        HinhCMNDMatTruoc = model.HinhCMNDMatTruoc

                    };
                    context.KhachHangs.Add(dbmodelKhachHang);
                    context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                        return 0;
                    }

                }
                else
                {
                    result.DiaChi = model.DiaChi;
                    result.HinhDaiDien = model.HinhDaiDien;
                    result.SoDienThoai1 = model.SoDienThoai1;
                    result.SoDienThoai2 = model.SoDienThoai2;
                    result.TenKhachHang = model.TenKhachHang;
                    result.BienSoXe = model.BienSoXe;
                    result.ChucVu = model.ChucVu;
                    result.ChuTaiKhoan = model.ChuTaiKhoan;
                    result.CotLuongTienMat = model.CotLuongTienMat;
                    result.DiaChiCongTy = model.DiaChiCongTy;
                    result.DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh;
                    result.DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh;
                    result.KhuVuc = model.KhuVuc;
                    result.LoaiNha = model.LoaiNha;
                    result.LoaiXe = model.LoaiXe;
                    result.LuongCoBan = model.LuongCoBan;
                    result.MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe);
                    result.NgayCapCMND = model.NgayCapCMND;
                    result.NgayLanhLuong = model.NgayLanhLuong;
                    result.NguyenQuan = model.NguyenQuan;
                    result.NoiCapCMND = model.NoiCapCMND;
                    result.SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh;
                    result.SoHuuNha = model.SoHuuNha;
                    result.SoHuuXe = model.SoHuuXe;
                    result.SoTaiKhoan = model.SoTaiKhoan;
                    result.SoThe = model.SoThe;
                    result.TenCongTy = model.TenCongTy;
                    result.TenNganHang = model.TenNganHang;
                    result.TenNguoiBaoLanh = model.TenNguoiBaoLanh;
                    result.ThoiGianLamViecBaoLau = model.ThoiGianLamViec;
                    result.TinhTrangHonNhan = model.TinhTrangHonNhan;
                    result.HinhCMNDMatSau = model.HinhCMNDMatSau;
                    result.HinhCMNDMatTruoc = model.HinhCMNDMatTruoc;

                    var result2 = context.KhachHangThuocCuaHangs.Where(x => x.CMND == model.CMND && x.Id_CuaHang == Id_CuaHang).FirstOrDefault();
                    if (result2 == null)
                    {
                        var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                        {
                            CMND = model.CMND,
                            DiaChi = model.DiaChi,
                            HinhDaiDien = model.HinhDaiDien,
                            Id_CuaHang = Id_CuaHang,
                            Id_QuanLy = Id_QuanLy,
                            NgayTao = DateTime.Now,
                            SoDienThoai1 = model.SoDienThoai1,
                            SoDienThoai2 = model.SoDienThoai2,
                            TenKhachHang = model.TenKhachHang,
                            BienSoXe = model.BienSoXe,
                            ChucVu = model.ChucVu,
                            ChuTaiKhoan = model.ChuTaiKhoan,
                            CotLuongTienMat = model.CotLuongTienMat,
                            DiaChiCongTy = model.DiaChiCongTy,
                            DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh,
                            DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh,
                            KhuVuc = model.KhuVuc,
                            LoaiNha = model.LoaiNha,
                            LoaiXe = model.LoaiXe,
                            LuongCoBan = model.LuongCoBan,
                            MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe),
                            NgayCapCMND = model.NgayCapCMND,
                            NgayLanhLuong = model.NgayLanhLuong,
                            NguyenQuan = model.NguyenQuan,
                            NoiCapCMND = model.NoiCapCMND,
                            SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh,
                            SoHuuNha = model.SoHuuNha,
                            SoHuuXe = model.SoHuuXe,
                            SoTaiKhoan = model.SoTaiKhoan,
                            SoThe = model.SoThe,
                            TenCongTy = model.TenCongTy,
                            TenNganHang = model.TenNganHang,
                            TenNguoiBaoLanh = model.TenNguoiBaoLanh,
                            ThoiGianLamViecBaoLau = model.ThoiGianLamViec,
                            HinhCMNDMatSau = model.HinhCMNDMatSau,
                            HinhCMNDMatTruoc = model.HinhCMNDMatTruoc
                        };

                        context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                            return 0;
                        }
                    }
                    else
                    {
                        result2.DiaChi = model.DiaChi;
                        result2.HinhDaiDien = model.HinhDaiDien;
                        result2.Id_QuanLy = Id_QuanLy;
                        result2.NgayTao = DateTime.Now;
                        result2.SoDienThoai1 = model.SoDienThoai1;
                        result2.SoDienThoai2 = model.SoDienThoai2;
                        result2.TenKhachHang = model.TenKhachHang;
                        result2.BienSoXe = model.BienSoXe;
                        result2.ChucVu = model.ChucVu;
                        result2.ChuTaiKhoan = model.ChuTaiKhoan;
                        result2.CotLuongTienMat = model.CotLuongTienMat;
                        result2.DiaChiCongTy = model.DiaChiCongTy;
                        result2.DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh;
                        result2.DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh;
                        result2.KhuVuc = model.KhuVuc;
                        result2.LoaiNha = model.LoaiNha;
                        result2.LoaiXe = model.LoaiXe;
                        result2.LuongCoBan = model.LuongCoBan;
                        result2.MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe);
                        result2.NgayCapCMND = model.NgayCapCMND;
                        result2.NgayLanhLuong = model.NgayLanhLuong;
                        result2.NguyenQuan = model.NguyenQuan;
                        result2.NoiCapCMND = model.NoiCapCMND;
                        result2.SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh;
                        result2.SoHuuNha = model.SoHuuNha;
                        result2.SoHuuXe = model.SoHuuXe;
                        result2.SoTaiKhoan = model.SoTaiKhoan;
                        result2.SoThe = model.SoThe;
                        result2.TenCongTy = model.TenCongTy;
                        result2.TenNganHang = model.TenNganHang;
                        result2.TenNguoiBaoLanh = model.TenNguoiBaoLanh;
                        result2.ThoiGianLamViecBaoLau = model.ThoiGianLamViec;
                        result2.TinhTrangHonNhan = model.TinhTrangHonNhan;
                        result2.HinhCMNDMatSau = model.HinhCMNDMatSau;
                        result2.HinhCMNDMatTruoc = model.HinhCMNDMatTruoc;
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
                            return 0;
                        }
                    }
                }
            }
        }
        public static int ThemMoiHoacCapNhatKhachHangTheoQuanLyTemp(KhachHangDataModel model, string Id_QuanLy, int Id_CuaHang)
        {
            using (var context = new TIT_Entities())
            {
                var result = context.KhachHangs.Where(x => x.CMND == model.CMND).FirstOrDefault();
                if (result == null)
                {
                    var dbmodelKhachHang = new KhachHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        SoDienThoai1 = model.SoDienThoai1,
                        SoDienThoai2 = model.SoDienThoai2,
                        TenKhachHang = model.TenKhachHang,
                        BienSoXe = model.BienSoXe,
                        ChucVu = model.ChucVu,
                        ChuTaiKhoan = model.ChuTaiKhoan,
                        CotLuongTienMat = model.CotLuongTienMat,
                        DiaChiCongTy = model.DiaChiCongTy,
                        DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh,
                        DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh,
                        KhuVuc = model.KhuVuc,
                        LoaiNha = model.LoaiNha,
                        LoaiXe = model.LoaiXe,
                        LuongCoBan = model.LuongCoBan,
                        MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe),
                        NgayCapCMND = model.NgayCapCMND,
                        NgayLanhLuong = model.NgayLanhLuong,
                        NguyenQuan = model.NguyenQuan,
                        NoiCapCMND = model.NoiCapCMND,
                        SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh,
                        SoHuuNha = model.SoHuuNha,
                        SoHuuXe = model.SoHuuXe,
                        SoTaiKhoan = model.SoTaiKhoan,
                        SoThe = model.SoThe,
                        TenCongTy = model.TenCongTy,
                        TenNganHang = model.TenNganHang,
                        TenNguoiBaoLanh = model.TenNguoiBaoLanh,
                        ThoiGianLamViecBaoLau = model.ThoiGianLamViec,
                        TinhTrangHonNhan = model.TinhTrangHonNhan,
                        HinhCMNDMatSau = model.HinhCMNDMatSau,
                        HinhCMNDMatTruoc = model.HinhCMNDMatTruoc
                    };
                    var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                    {
                        CMND = model.CMND,
                        DiaChi = model.DiaChi,
                        HinhDaiDien = model.HinhDaiDien,
                        Id_CuaHang = Id_CuaHang,
                        Id_QuanLy = Id_QuanLy,
                        NgayTao = DateTime.Now,
                        SoDienThoai1 = model.SoDienThoai1,
                        SoDienThoai2 = model.SoDienThoai2,
                        TenKhachHang = model.TenKhachHang,
                        BienSoXe = model.BienSoXe,
                        ChucVu = model.ChucVu,
                        ChuTaiKhoan = model.ChuTaiKhoan,
                        CotLuongTienMat = model.CotLuongTienMat,
                        DiaChiCongTy = model.DiaChiCongTy,
                        DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh,
                        DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh,
                        KhuVuc = model.KhuVuc,
                        LoaiNha = model.LoaiNha,
                        LoaiXe = model.LoaiXe,
                        LuongCoBan = model.LuongCoBan,
                        MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe),
                        NgayCapCMND = model.NgayCapCMND,
                        NgayLanhLuong = model.NgayLanhLuong,
                        NguyenQuan = model.NguyenQuan,
                        NoiCapCMND = model.NoiCapCMND,
                        SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh,
                        SoHuuNha = model.SoHuuNha,
                        SoHuuXe = model.SoHuuXe,
                        SoTaiKhoan = model.SoTaiKhoan,
                        SoThe = model.SoThe,
                        TenCongTy = model.TenCongTy,
                        TenNganHang = model.TenNganHang,
                        TenNguoiBaoLanh = model.TenNguoiBaoLanh,
                        ThoiGianLamViecBaoLau = model.ThoiGianLamViec,
                        TinhTrangHonNhan = model.TinhTrangHonNhan,
                        HinhCMNDMatSau = model.HinhCMNDMatSau,
                        HinhCMNDMatTruoc = model.HinhCMNDMatTruoc
                    };
                    context.KhachHangs.Add(dbmodelKhachHang);
                    context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                        return 0;
                    }

                }
                else
                {
                    result.DiaChi = model.DiaChi;
                    result.HinhDaiDien = model.HinhDaiDien;
                    result.SoDienThoai1 = model.SoDienThoai1;
                    result.SoDienThoai2 = model.SoDienThoai2;
                    result.TenKhachHang = model.TenKhachHang;
                    result.BienSoXe = model.BienSoXe;
                    result.ChucVu = model.ChucVu;
                    result.ChuTaiKhoan = model.ChuTaiKhoan;
                    result.CotLuongTienMat = model.CotLuongTienMat;
                    result.DiaChiCongTy = model.DiaChiCongTy;
                    result.DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh;
                    result.DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh;
                    result.KhuVuc = model.KhuVuc;
                    result.LoaiNha = model.LoaiNha;
                    result.LoaiXe = model.LoaiXe;
                    result.LuongCoBan = model.LuongCoBan;
                    result.MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe);
                    result.NgayCapCMND = model.NgayCapCMND;
                    result.NgayLanhLuong = model.NgayLanhLuong;
                    result.NguyenQuan = model.NguyenQuan;
                    result.NoiCapCMND = model.NoiCapCMND;
                    result.SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh;
                    result.SoHuuNha = model.SoHuuNha;
                    result.SoHuuXe = model.SoHuuXe;
                    result.SoTaiKhoan = model.SoTaiKhoan;
                    result.SoThe = model.SoThe;
                    result.TenCongTy = model.TenCongTy;
                    result.TenNganHang = model.TenNganHang;
                    result.TenNguoiBaoLanh = model.TenNguoiBaoLanh;
                    result.ThoiGianLamViecBaoLau = model.ThoiGianLamViec;
                    result.TinhTrangHonNhan = model.TinhTrangHonNhan;
                    result.HinhCMNDMatSau = model.HinhCMNDMatSau;
                    result.HinhCMNDMatTruoc = model.HinhCMNDMatTruoc;
                    var result2 = context.KhachHangThuocCuaHangs.Where(x => x.CMND == model.CMND && x.Id_CuaHang == Id_CuaHang).FirstOrDefault();
                    if (result2 == null)
                    {
                        var dbModelKhachHangThuocCuaHang = new KhachHangThuocCuaHang()
                        {
                            CMND = model.CMND,
                            DiaChi = model.DiaChi,
                            HinhDaiDien = model.HinhDaiDien,
                            Id_CuaHang = Id_CuaHang,
                            Id_QuanLy = Id_QuanLy,
                            NgayTao = DateTime.Now,
                            SoDienThoai1 = model.SoDienThoai1,
                            SoDienThoai2 = model.SoDienThoai2,
                            TenKhachHang = model.TenKhachHang,
                            BienSoXe = model.BienSoXe,
                            ChucVu = model.ChucVu,
                            ChuTaiKhoan = model.ChuTaiKhoan,
                            CotLuongTienMat = model.CotLuongTienMat,
                            DiaChiCongTy = model.DiaChiCongTy,
                            DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh,
                            DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh,
                            KhuVuc = model.KhuVuc,
                            LoaiNha = model.LoaiNha,
                            LoaiXe = model.LoaiXe,
                            LuongCoBan = model.LuongCoBan,
                            MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe),
                            NgayCapCMND = model.NgayCapCMND,
                            NgayLanhLuong = model.NgayLanhLuong,
                            NguyenQuan = model.NguyenQuan,
                            NoiCapCMND = model.NoiCapCMND,
                            SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh,
                            SoHuuNha = model.SoHuuNha,
                            SoHuuXe = model.SoHuuXe,
                            SoTaiKhoan = model.SoTaiKhoan,
                            SoThe = model.SoThe,
                            TenCongTy = model.TenCongTy,
                            TenNganHang = model.TenNganHang,
                            TenNguoiBaoLanh = model.TenNguoiBaoLanh,
                            ThoiGianLamViecBaoLau = model.ThoiGianLamViec,
                            TinhTrangHonNhan = model.TinhTrangHonNhan,
                            HinhCMNDMatSau = model.HinhCMNDMatSau,
                            HinhCMNDMatTruoc = model.HinhCMNDMatTruoc
                        };

                        context.KhachHangThuocCuaHangs.Add(dbModelKhachHangThuocCuaHang);
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
                            return 0;
                        }
                    }
                    else
                    {
                        result2.DiaChi = model.DiaChi;
                        result2.HinhDaiDien = model.HinhDaiDien;
                        result2.Id_QuanLy = Id_QuanLy;
                        result2.NgayTao = DateTime.Now;
                        result2.SoDienThoai1 = model.SoDienThoai1;
                        result2.SoDienThoai2 = model.SoDienThoai2;
                        result2.TenKhachHang = model.TenKhachHang;
                        result2.BienSoXe = model.BienSoXe;
                        result2.ChucVu = model.ChucVu;
                        result2.ChuTaiKhoan = model.ChuTaiKhoan;
                        result2.CotLuongTienMat = model.CotLuongTienMat;
                        result2.DiaChiCongTy = model.DiaChiCongTy;
                        result2.DiaChiLamViecNguoiBaoLanh = model.DiaChiLamViecNguoiBaoLanh;
                        result2.DiaChiNguoiBaoLanh = model.DiaChiNhaNguoiBaoLanh;
                        result2.KhuVuc = model.KhuVuc;
                        result2.LoaiNha = model.LoaiNha;
                        result2.LoaiXe = model.LoaiXe;
                        result2.LuongCoBan = model.LuongCoBan;
                        result2.MaHoaThe = MaHoaTheHelper.MaHoaThe(model.PasswordThe);
                        result2.NgayCapCMND = model.NgayCapCMND;
                        result2.NgayLanhLuong = model.NgayLanhLuong;
                        result2.NguyenQuan = model.NguyenQuan;
                        result2.NoiCapCMND = model.NoiCapCMND;
                        result2.SoDienThoaiNguoiBaoLanh = model.SoDienThoaiNguoiBaoLanh;
                        result2.SoHuuNha = model.SoHuuNha;
                        result2.SoHuuXe = model.SoHuuXe;
                        result2.SoTaiKhoan = model.SoTaiKhoan;
                        result2.SoThe = model.SoThe;
                        result2.TenCongTy = model.TenCongTy;
                        result2.TenNganHang = model.TenNganHang;
                        result2.TenNguoiBaoLanh = model.TenNguoiBaoLanh;
                        result2.ThoiGianLamViecBaoLau = model.ThoiGianLamViec;
                        result2.TinhTrangHonNhan = model.TinhTrangHonNhan;
                        result2.HinhCMNDMatSau = model.HinhCMNDMatSau;
                        result2.HinhCMNDMatTruoc = model.HinhCMNDMatTruoc;
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
                            return 0;
                        }
                    }
                }
            }
        }

    }
}
