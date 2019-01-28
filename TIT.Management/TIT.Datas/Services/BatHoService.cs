using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class BatHoService
    {
        public static string Insert(HopDongBatHoDataModel model)
        {
            var dbModel = new HD_BatHo
            {
                CuaHang_Id = model.CuaHang_Id,
                GhiChu = model.GhiChu,
                BatHo = model.BatHo,
                BocTrongVong = model.BocTrongVong,
                KhachHang_CMND = model.KhachHang.CMND,
                NgayBoc = model.NgayBoc,
                NgayCuoiCungDongTien = model.NgayBoc,
                NgayTao = DateTime.Now,
                NhanVien_Id = model.NhanVien_Id,
                SoNgayDongTien = model.SoNgayDongTien,
                SoTienKhachDaDong = 0,
                ThuHoTruoc = false,
                TienDuaChoKhach = model.TienDuaChoKhach,
                TinhTrang = "Đang vay"

            };

            


            using (var context = new TIT_Entities())
            {
                context.HD_BatHo.Add(dbModel);

                try
                {
                    if (context.SaveChanges() > 0)
                    {
                        var baocao = context.BaoCaoHangNgays.OrderByDescending(x => x.Ngay).FirstOrDefault(x => x.Id_CuaHang == model.CuaHang_Id);
                        if (baocao != null)
                        {
                            if (baocao.Ngay == DateTime.Now.Date)
                            {
                                baocao.SoTienVonConLai -= model.TienDuaChoKhach;
                                baocao.TongChi += model.TienDuaChoKhach;
                                baocao.TongChiHopDongBatHo += model.TienDuaChoKhach;
                            }
                            else
                            {
                                var bcModel = new BaoCaoHangNgay
                                {
                                    Id_CuaHang = model.CuaHang_Id,
                                    Ngay = DateTime.Now.Date,
                                    SoTienVonDauNgay = baocao.SoTienVonConLai,
                                    SoTienVonConLai = baocao.SoTienVonConLai,
                                };
                                bcModel.SoTienVonConLai -= model.TienDuaChoKhach;
                                bcModel.TongChi += model.TienDuaChoKhach;
                                bcModel.TongChiHopDongBatHo += model.TienDuaChoKhach;

                                context.BaoCaoHangNgays.Add(bcModel);
                            }
                        }
                        else
                        {
                            var bcModel = new BaoCaoHangNgay
                            {
                                Id_CuaHang = model.CuaHang_Id,
                                Ngay = DateTime.Now.Date
                            };
                            bcModel.SoTienVonConLai -= model.TienDuaChoKhach;
                            bcModel.TongChi += model.TienDuaChoKhach;
                            bcModel.TongChiHopDongBatHo += model.TienDuaChoKhach;
                            context.BaoCaoHangNgays.Add(bcModel);
                        }
                        decimal QuyTienMat = 0;
                        var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == model.CuaHang_Id).OrderByDescending(p => p.Id).FirstOrDefault();
                        if (lastestLichSuThaoTac != null)
                            QuyTienMat = lastestLichSuThaoTac.TongCongTon;
                        var dbModel2 = new LichSuThaoTac()
                        {
                            Id_CuaHang = model.CuaHang_Id,
                            Id_HopDong = dbModel.HD_BatHo_Id,
                            Id_NhanVienThaoTac = model.NhanVien_Id,
                            NgayThaoTac = DateTime.Now,
                            NoiDung = "Tạo hợp đồng",
                            SoTien = model.TienDuaChoKhach,
                            ThuTien = 0,
                            TenKhachHang = model.KhachHang.TenKhachHang,
                            TongCongTon = QuyTienMat - model.TienDuaChoKhach
                        };
                        context.LichSuThaoTacs.Add(dbModel2);

                        List<ThongTinDongLai> listModels = new List<ThongTinDongLai>();
                        int SoNgay = model.BocTrongVong / model.SoNgayDongTien;
                        DateTime ngayBatDau = model.NgayBoc;
                        for (int i = 0; i < SoNgay; i++)
                        {

                            ThongTinDongLai item = new ThongTinDongLai()
                            {
                                DaDong = 0,
                                HopDong_Id = dbModel.HD_BatHo_Id,
                                NgayBatDau = ngayBatDau,
                                NgayDongLai = DateTime.Now,
                                NgayKetThuc = ngayBatDau.AddDays(model.SoNgayDongTien - 1),
                                SoNgay = model.SoNgayDongTien,
                                TienLai = model.TienMotNgay * model.SoNgayDongTien
                            };
                            context.ThongTinDongLais.Add(item);
                            ngayBatDau = ngayBatDau.AddDays(model.SoNgayDongTien);
                        }
                        if (context.SaveChanges() > 0)
                        {
                            return dbModel.HD_BatHo_Id;
                        }
                        else
                            return null;

                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    //_logger.Error(ex.ToString());
                    return null;
                }
            }
        }
        public static IEnumerable<HopDongBatHoGridViewModel> LayHopDongTheoIdNhanVien(string IdNhanVien)
        {
            IEnumerable<HopDongBatHoGridViewModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_BatHo
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
                              SoNgayDongTien = hd.SoNgayDongTien,
                              TinhTrang = hd.TinhTrang
                          }).ToArray();

            }
            return result;
        }

        public static BatHo_CapNhatThongTinDongTienViewModel LayThongTinDongTienHopDong(string maHopDong)
        {
            BatHo_CapNhatThongTinDongTienViewModel result = new BatHo_CapNhatThongTinDongTienViewModel();
            using (var context = new TIT_Entities())
            {
                result.HopDongBatHo = (from hd in context.HD_BatHo
                                        join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                                        where hd.HD_BatHo_Id == maHopDong
                                        select new HopDongBatHoDataModel()
                                        {
                                            HD_BatHo_Id = hd.HD_BatHo_Id,
                                            GhiChu = hd.GhiChu,
                                            KhachHang = new KhachHangModel()
                                            {
                                                TenKhachHang = kh.TenKhachHang,
                                                CMND = kh.CMND,
                                                DiaChi = kh.DiaChi != null ? kh.DiaChi : "",
                                                HinhDaiDien = kh.HinhDaiDien != null ? kh.HinhDaiDien : "",
                                                SoDienThoai = kh.SoDienThoai1
                                            },
                                            BatHo = hd.BatHo,
                                            BocTrongVong = hd.BocTrongVong,
                                            NgayBoc = hd.NgayBoc,
                                            NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue? hd.NgayCuoiCungDongTien.Value: hd.NgayBoc,
                                            NgayTao = hd.NgayTao,
                                            NhanVien_Id = hd.NhanVien_Id,
                                            SoNgayDongTien = hd.SoNgayDongTien,
                                            ThuHoTruoc = hd.ThuHoTruoc,
                                            CuaHang_Id = hd.CuaHang_Id,
                                            TienDuaChoKhach = hd.TienDuaChoKhach,
                                            TinhTrang = hd.TinhTrang
                                        }).FirstOrDefault();
                result.ThongTinDongLai = new BatHo_ThongTinDongLaiModel();
                result.ThongTinDongLai.DanhSachChiTietDongTienLai = (from dl in context.ThongTinDongLais
                                                                     where dl.HopDong_Id == maHopDong
                                                                     select new BatHo_ThongTinChiTietDongTienLaiModel()
                                                                     {
                                                                       NgayHo = dl.NgayBatDau,
                                                                       TienHo = dl.TienLai,
                                                                       ID = dl.Id,
                                                                       DaDong = dl.DaDong
                                                                     }).ToList();
            }
            return result;
        }

        public static int ThanhToanHopDong(string Id_HopDong, int Id_CuaHang, string Id_NhanVien, decimal SoTien)
        {
            using (var context = new TIT_Entities())
            {
                var item_update2 = context.ThongTinDongLais.Where(x => x.HopDong_Id == Id_HopDong).ToList();
                for (int i = 0; i < item_update2.Count; i++)
                {
                    item_update2[i].DaDong = 1;
                }
                //update
                var item_update = context.HD_BatHo.SingleOrDefault(x => x.HD_BatHo_Id == Id_HopDong);
                item_update.TinhTrang = "Thanh Lý";
                item_update.NgayCuoiCungDongTien = item_update2[item_update2.Count - 1].NgayKetThuc;

                var baocao = context.BaoCaoHangNgays.OrderByDescending(x => x.Ngay).FirstOrDefault(x => x.Id_CuaHang == Id_CuaHang);
                if (baocao != null)
                {
                    if (baocao.Ngay == DateTime.Now.Date)
                    {
                        baocao.SoTienVonConLai += SoTien;
                        baocao.TongThu += SoTien;
                        baocao.TongThuHopDongBatHo += SoTien;
                    }
                    else
                    {
                        var bcModel = new BaoCaoHangNgay
                        {
                            Id_CuaHang = Id_CuaHang,
                            Ngay = DateTime.Now.Date,
                            SoTienVonDauNgay = baocao.SoTienVonConLai,
                            SoTienVonConLai = baocao.SoTienVonConLai,
                        };
                        bcModel.SoTienVonConLai += SoTien;
                        bcModel.TongThu += SoTien;
                        bcModel.TongThuHopDongBatHo += SoTien;

                        context.BaoCaoHangNgays.Add(bcModel);
                    }
                }
                else
                {
                    var bcModel = new BaoCaoHangNgay
                    {
                        Id_CuaHang = Id_CuaHang,
                        Ngay = DateTime.Now.Date
                    };
                    bcModel.SoTienVonConLai += SoTien;
                    bcModel.TongThu += SoTien;
                    bcModel.TongThuHopDongBatHo += SoTien;
                    context.BaoCaoHangNgays.Add(bcModel);
                }
                decimal QuyTienMat = 0;
                var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == Id_CuaHang).OrderByDescending(p => p.Id).FirstOrDefault();
                if (lastestLichSuThaoTac != null)
                    QuyTienMat = lastestLichSuThaoTac.TongCongTon;
                
                var model = new LichSuThaoTac()
                {
                    Id_CuaHang = Id_CuaHang,
                    Id_HopDong = Id_HopDong,
                    Id_NhanVienThaoTac = Id_NhanVien,
                    NgayThaoTac = DateTime.Now,
                    NoiDung = "Thanh lý hợp đồng",
                    SoTien = SoTien,
                    ThuTien = 1,
                    TongCongTon = QuyTienMat + SoTien,
                    TenKhachHang = ""
                };
                context.LichSuThaoTacs.Add(model);

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



        public static IEnumerable<BatHo_ThongTinChiTietDongTienLaiModel> LayDanhSachDongTienLai(string maHopDong)
        {
            IEnumerable<BatHo_ThongTinChiTietDongTienLaiModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.ThongTinDongLais
                          join nv in context.AspNetUsers on hd.NhanVienThuTien equals nv.Id into gj
                          from subpet in gj.DefaultIfEmpty()
                          where hd.HopDong_Id == maHopDong
                          select new BatHo_ThongTinChiTietDongTienLaiModel()
                          {
                              NgayHo = hd.NgayBatDau,
                              NgayKetThuc = hd.NgayKetThuc,
                              DaDong = hd.DaDong,
                              TienHo = hd.TienLai,
                              MaNhanVien = hd.NhanVienThuTien,
                              TenNhanVien = subpet.UserName ?? String.Empty,
                              ID = hd.Id,
                              NgayGiaoDich = hd.NgayDongLai.HasValue? hd.NgayDongLai.Value: hd.NgayBatDau
                          }).ToArray();

            }

            return result;
        }

        public static int DongTienLai(string hD_BatHo_Id, BatHo_ThongTinChiTietDongTienLaiModel update, string Id_NhanVien)
        {
            using (var context = new TIT_Entities())
            {
                //update
                var item_update = context.ThongTinDongLais.SingleOrDefault(x => x.Id == update.ID);
                item_update.DaDong = update.DaDong;
                item_update.NgayDongLai = DateTime.Now;

                var hd_update = context.HD_BatHo.SingleOrDefault(x => x.HD_BatHo_Id == hD_BatHo_Id);
                hd_update.NgayCuoiCungDongTien = item_update.NgayKetThuc;

                var baocao = context.BaoCaoHangNgays.OrderByDescending(x => x.Ngay).FirstOrDefault(x => x.Id_CuaHang == hd_update.CuaHang_Id);
                if (baocao != null)
                {
                    if (baocao.Ngay == DateTime.Now.Date)
                    {
                        baocao.SoTienVonConLai += update.TienHo;
                        baocao.TongThu += update.TienHo;
                        baocao.TongThuHopDongBatHo += update.TienHo;
                    }
                    else
                    {
                        var bcModel = new BaoCaoHangNgay
                        {
                            Id_CuaHang = hd_update.CuaHang_Id,
                            Ngay = DateTime.Now.Date,
                            SoTienVonDauNgay = baocao.SoTienVonConLai,
                            SoTienVonConLai = baocao.SoTienVonConLai,
                        };
                        bcModel.SoTienVonConLai += update.TienHo;
                        bcModel.TongThu += update.TienHo;
                        bcModel.TongThuHopDongBatHo += update.TienHo;

                        context.BaoCaoHangNgays.Add(bcModel);
                    }
                }
                else
                {
                    var bcModel = new BaoCaoHangNgay
                    {
                        Id_CuaHang = hd_update.CuaHang_Id,
                        Ngay = DateTime.Now.Date
                    };
                    bcModel.SoTienVonConLai += update.TienHo;
                    bcModel.TongThu += update.TienHo;
                    bcModel.TongThuHopDongBatHo += update.TienHo;
                    context.BaoCaoHangNgays.Add(bcModel);
                }
                decimal QuyTienMat = 0;
                var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == hd_update.CuaHang_Id).OrderByDescending(p => p.Id).FirstOrDefault();
                if (lastestLichSuThaoTac != null)
                    QuyTienMat = lastestLichSuThaoTac.TongCongTon;


                var lichsu = new LichSuThaoTac()
                {
                    Id_CuaHang = hd_update.CuaHang_Id,
                    Id_HopDong = hd_update.HD_BatHo_Id,
                    Id_NhanVienThaoTac = Id_NhanVien,
                    NgayThaoTac = DateTime.Now,
                    NoiDung = "Đóng họ",
                    SoTien = item_update.TienLai,
                    ThuTien = 1,
                    TongCongTon = QuyTienMat + item_update.TienLai,
                    TenKhachHang = ""
                };

                context.LichSuThaoTacs.Add(lichsu);

                
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

        public static IEnumerable<HopDongBatHoGridViewModel> LayHopDongTheoAdmin()
        {
            IEnumerable<HopDongBatHoGridViewModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_BatHo
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
                              SoNgayDongTien = hd.SoNgayDongTien,
                              TinhTrang = hd.TinhTrang
                          }).ToArray();

            }
            return result;
        }
        public static IEnumerable<HopDongBatHoGridViewModel> LayHopDongTheoIdQuanLy(string IdQuanLy)
        {
            IEnumerable<HopDongBatHoGridViewModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_BatHo
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
                              SoNgayDongTien = hd.SoNgayDongTien,
                              TinhTrang = hd.TinhTrang
                          }).ToArray();
            }
            return result;
        }
        

    }
}
