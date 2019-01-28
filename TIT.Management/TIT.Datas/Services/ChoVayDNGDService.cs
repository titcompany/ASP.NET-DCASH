using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class ChoVayDNGDService
    {
        public static string Insert(HopDongChoVayDNGDDataModel model)
        {
            using (var context = new TIT_Entities())
            {
                var result = context.ThemHopDongDNGD(model.KhachHang.CMND, model.SoTienVay, model.HoaHong, model.Lai, model.ThoiHanVay,
                     model.NgayVay, model.GhiChu, model.CuaHang_Id, model.NhanVien_Id, DateTime.Now, model.NgayDongLaiCuoiCung, model.TinhTrang,
                     model.NgayBatDauThanhToan, DateTime.MinValue, "", 0, "", "", "", model.TongSoTienLaiDaDong, model.LaiThangDau, model.LaiGocMotThang);
                return result.ToString();
            }
        }

        public static ThongKeHopDongTheoKhachHang LayThongKeHopDongTheoKhachHang(string cMND)
        {
            IEnumerable<ChiTietThongKeHopDongTheoKhachHang> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_VayDuNoGiamDan
                          where hd.KhachHang_CMND == cMND
                          select new ChiTietThongKeHopDongTheoKhachHang
                          {
                              CMND = hd.KhachHang_CMND,
                              TinhTrang = hd.TinhTrang
                          }).ToArray();
                if (result != null)
                {
                    ThongKeHopDongTheoKhachHang temp = new ThongKeHopDongTheoKhachHang();
                    temp.TongSoHopDong = result.Count();
                    temp.SoHopDongMo = result.Where(x => x.TinhTrang != "Thanh Lý").Count();
                    temp.SoHopDongDong = temp.TongSoHopDong - temp.SoHopDongMo;
                    return temp;
                }
            }
            return new ThongKeHopDongTheoKhachHang();
        }

        public static IEnumerable<HopDongVayDNGDGridViewModel> LayHopDongTheoIdNhanVien(string IdNhanVien)
        {
            IEnumerable<HopDongVayDNGDGridViewModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_VayDuNoGiamDan
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          join user in context.AspNetUsers on hd.CuaHang_Id equals user.CuaHang_Id
                          join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                          where user.Id == IdNhanVien && hd.TinhTrang != "Hủy hợp đồng"
                          select new HopDongVayDNGDGridViewModel
                          {
                              KhachHang = new KhachHangModel()
                              {
                                  TenKhachHang = kh.TenKhachHang,
                                  SoDienThoai = kh.SoDienThoai1
                              },
                              HoaHong = hd.HoaHong,
                              LaiSuat = hd.LaiSuat,
                              NgayDongTienCuoiCung = hd.NgayCuoiCungDongTien.HasValue? hd.NgayCuoiCungDongTien.Value: hd.NgayVay,
                              NgayVay = hd.NgayVay,
                              SoTien = hd.SoTienVay,
                              ThoiHanVay = hd.ThoiHanVay,
                              Id_HopDong = hd.HD_Id,
                              TinhTrang = hd.TinhTrang,
                              NgayHen = hd.NgayHenGio.HasValue ? hd.NgayHenGio.Value : DateTime.MinValue,
                              NoiDungHen = hd.GhiChuHenGio,
                              IsHenGio = hd.TrangThaiHenGio == 1 ? true : false,
                              DaDong = hd.TienLaiDaDong,
                              NgayBatDauThanhToan = hd.NgayBatDauDongLai,
                              CuaHang = ch.TenCuaHang
                          }).ToArray();

            }
            return result;
        }

        public static int CapNhatThongTinGhiChu(string id_HopDong, string ghiChu)
        {
            using (var context = new TIT_Entities())
            {
                

                var hd_update = context.HD_VayDuNoGiamDan.SingleOrDefault(x => x.HD_Id == id_HopDong);
                hd_update.GhiChu = ghiChu;
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

        public static int HuyHopDong(string id_HopDong, string ghiChu, decimal soTienThuLai, string Id_NhanVien, string TenKhachHang)
        {

            using (var context = new TIT_Entities())
            {
                //update
                var item_update = context.HD_VayDuNoGiamDan.SingleOrDefault(x => x.HD_Id == id_HopDong);
                item_update.TinhTrang = "Hủy hợp đồng";

                var item_delete = context.ThongTinDongLais.Where(x => x.HopDong_Id == id_HopDong).ToArray();
                foreach (var item in item_delete)
                    context.ThongTinDongLais.Remove(item);

                decimal QuyTienMat = 0;
                var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == item_update.CuaHang_Id).OrderByDescending(p => p.Id).FirstOrDefault();
                if (lastestLichSuThaoTac != null)
                    QuyTienMat = lastestLichSuThaoTac.TongCongTon;

                QuyTienMat += soTienThuLai;
                var model = new LichSuThaoTac()
                {
                    Id_CuaHang = item_update.CuaHang_Id,
                    Id_HopDong = id_HopDong,
                    Id_NhanVienThaoTac = Id_NhanVien,
                    NgayThaoTac = DateTime.Now,
                    NoiDung = "Hủy hợp đồng: " + ghiChu,
                    SoTien = soTienThuLai,
                    ThuTien = 1,
                    TongCongTon = QuyTienMat,
                    TenKhachHang = TenKhachHang
                };
                context.LichSuThaoTacs.Add(model);
              


                var baocao = context.BaoCaoHangNgays.OrderByDescending(x => x.Ngay).FirstOrDefault(x => x.Id_CuaHang == item_update.CuaHang_Id);
                if (baocao != null)
                {
                    if (baocao.Ngay == DateTime.Now.Date)
                    {
                        baocao.SoTienVonConLai += soTienThuLai;
                        baocao.TongThu += soTienThuLai;
                        baocao.TongThuHopDongChoVayDNGD += soTienThuLai;
                    }
                    else
                    {
                        var bcModel = new BaoCaoHangNgay
                        {
                            Id_CuaHang = item_update.CuaHang_Id,
                            Ngay = DateTime.Now.Date,
                            SoTienVonDauNgay = baocao.SoTienVonConLai,
                            SoTienVonConLai = baocao.SoTienVonConLai,
                        };
                        bcModel.SoTienVonConLai += soTienThuLai;
                        bcModel.TongThu += soTienThuLai;
                        bcModel.TongThuHopDongChoVayDNGD += soTienThuLai;

                        context.BaoCaoHangNgays.Add(bcModel);
                    }
                }
                else
                {
                    var bcModel = new BaoCaoHangNgay
                    {
                        Id_CuaHang = item_update.CuaHang_Id,
                        Ngay = DateTime.Now.Date
                    };
                    bcModel.SoTienVonConLai += soTienThuLai;
                    bcModel.TongThu += soTienThuLai;
                    bcModel.TongThuHopDongChoVayDNGD += soTienThuLai;
                    context.BaoCaoHangNgays.Add(bcModel);
                }
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

        public static IEnumerable<HopDongVayDNGDGridViewModel> LayHopDongTheoAdmin()
        {
            IEnumerable<HopDongVayDNGDGridViewModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_VayDuNoGiamDan
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                          where hd.TinhTrang != "Hủy hợp đồng"
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
                              NgayHen = hd.NgayHenGio.HasValue ? hd.NgayHenGio.Value : DateTime.MinValue,
                              NoiDungHen = hd.GhiChuHenGio,
                              IsHenGio = hd.TrangThaiHenGio == 1 ? true : false,
                              DaDong = hd.TienLaiDaDong,
                              NgayBatDauThanhToan = hd.NgayBatDauDongLai,
                              CuaHang = ch.TenCuaHang
                          }).ToArray();

            }
            return result;
        }



        public static IEnumerable<ChoVayDNGD_ThongTinChiTietDongTienLaiModel> LayDanhSachDongTienLai(string maHopDong)
        {
            IEnumerable<ChoVayDNGD_ThongTinChiTietDongTienLaiModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.ThongTinDongLais
                          join nv in context.AspNetUsers on hd.NhanVienThuTien equals nv.Id into gj
                          from subpet in gj.DefaultIfEmpty()
                          where hd.HopDong_Id == maHopDong
                          select new ChoVayDNGD_ThongTinChiTietDongTienLaiModel()
                          {
                              NgayBatDau = hd.NgayBatDau,
                              NgayKetThuc = hd.NgayKetThuc,
                              DaDong = hd.DaDong,
                              TienKhac = hd.TienKhac.HasValue ? hd.TienKhac.Value : 0,
                              SoNgay = hd.SoNgay,
                              TienLai = hd.TienLai,
                              MaNhanVien = hd.NhanVienThuTien,
                              TenNhanVien = subpet.UserName ?? String.Empty,
                              ID = hd.Id,
                              SoTienDaDong = hd.SoTienDaDong
                          }).ToArray();

            }

            return result;
        }


        public static IEnumerable<HopDongVayDNGDGridViewModel> LayHopDongTheoIdQuanLy(string IdQuanLy, int MaCuaHang)
        {
            IEnumerable<HopDongVayDNGDGridViewModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_VayDuNoGiamDan
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                          where ch.QuanLyCuaHang == IdQuanLy && ch.MaCuaHang == MaCuaHang && hd.TinhTrang != "Hủy hợp đồng"
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
                              NgayHen = hd.NgayHenGio.HasValue ? hd.NgayHenGio.Value : DateTime.MinValue,
                              NoiDungHen = hd.GhiChuHenGio,
                              IsHenGio = hd.TrangThaiHenGio == 1 ? true: false,
                              DaDong = hd.TienLaiDaDong,
                              NgayBatDauThanhToan = hd.NgayBatDauDongLai,
                              CuaHang = ch.TenCuaHang
                          }).ToArray();

            }
            return result;
        }

        public static IEnumerable<HopDongVayDNGDGridViewModel> LayHopDongTheoIdQuanLy(string IdQuanLy)
        {
            IEnumerable<HopDongVayDNGDGridViewModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_VayDuNoGiamDan
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                          where ch.QuanLyCuaHang == IdQuanLy && hd.TinhTrang != "Hủy hợp đồng"
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
                              NgayHen = hd.NgayHenGio.HasValue ? hd.NgayHenGio.Value : DateTime.MinValue,
                              NoiDungHen = hd.GhiChuHenGio,
                              IsHenGio = hd.TrangThaiHenGio == 1 ? true : false,
                              DaDong = hd.TienLaiDaDong,
                              NgayBatDauThanhToan = hd.NgayBatDauDongLai,
                              CuaHang = ch.TenCuaHang
                          }).ToArray();

            }
            return result;
        }

        public static int CapNhatUrl(string idHopDong, string newPath, int STT)
        {
            using (var context = new TIT_Entities())
            {
                var hopdong = context.HD_VayDuNoGiamDan.Where(x => x.HD_Id == idHopDong).FirstOrDefault();
                if (STT == 1)
                    hopdong.UrlHinh1 = newPath;
                else if (STT == 2)
                    hopdong.UrlHinh2 = newPath;
                else
                    hopdong.UrlHinh3 = newPath;
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

        public static int DongTienLai(string hD_Id ,List<ChoVayDNGD_ThongTinChiTietDongTienLaiModel> update, string Id_NhanVien, decimal SoTien, decimal TienPhat, string Ghichu, string GhiChuTong, string TenKhachHang)
        {
            using (var context = new TIT_Entities())
            {
                decimal TienDongHangThang = update[0].TienLai;
                decimal SoTienDong = SoTien;
                int IDX = 0;
                DateTime NgayCuoiCung = DateTime.MinValue;
                DateTime NgayDongLaiCuoiCung = DateTime.MinValue;
                int SoKyDongLaiBoSung = 0;
                bool IsTienPhat = true;
                while (SoTienDong > 0 && IDX < update.Count)
                {
                    if (SoTienDong + update[IDX].SoTienDaDong < TienDongHangThang)
                    {
                        int ID = update[IDX].ID;
                        var item_update = context.ThongTinDongLais.SingleOrDefault(x => x.Id == ID);
                        item_update.SoTienDaDong = SoTienDong + update[IDX].SoTienDaDong;
                        NgayCuoiCung = item_update.NgayKetThuc;
                        SoTienDong = SoTienDong + update[IDX].SoTienDaDong - TienDongHangThang;
                        if (IsTienPhat)
                        {
                            item_update.TienKhac += TienPhat;
                            item_update.GhiChu = Ghichu;
                            IsTienPhat = false;
                        }
                    }
                    else
                    {
                        int ID = update[IDX].ID;
                        SoTienDong = SoTienDong + update[IDX].SoTienDaDong - TienDongHangThang;
                        var item_update = context.ThongTinDongLais.SingleOrDefault(x => x.Id == ID);
                        item_update.SoTienDaDong = TienDongHangThang;
                        NgayDongLaiCuoiCung = item_update.NgayKetThuc;
                        item_update.DaDong = 1;
                        SoKyDongLaiBoSung++;
                        if (IsTienPhat)
                        {
                            item_update.TienKhac += TienPhat;
                            item_update.GhiChu = Ghichu;
                            IsTienPhat = false;
                        }
                    }
                    IDX++;
                }
                if (IsTienPhat)
                {
                    int ID = update[IDX].ID;
                    var item_update = context.ThongTinDongLais.SingleOrDefault(x => x.Id == ID);
                    item_update.TienKhac = TienPhat;
                    item_update.GhiChu = Ghichu;
                    IsTienPhat = false;
                }





                ////update
                //var item_update = context.ThongTinDongLais.SingleOrDefault(x => x.Id == update.ID);
                //item_update.DaDong = update.DaDong;
                //item_update.TienKhac = update.TienKhac;
                //item_update.NgayDongLai = DateTime.Now;

                var hd_update = context.HD_VayDuNoGiamDan.SingleOrDefault(x => x.HD_Id == hD_Id);
                if(NgayDongLaiCuoiCung != DateTime.MinValue)
                    hd_update.NgayCuoiCungDongTien = NgayDongLaiCuoiCung;
                hd_update.TienLaiDaDong += SoTien;
                hd_update.GhiChu = GhiChuTong;
                //int desMonth = hd_update.NgayCuoiCungDongTien.Value.Month;
                //int srcMonth = hd_update.NgayVay.Month;
                //int totalMonth = desMonth > srcMonth ? desMonth - srcMonth : desMonth + 13 - srcMonth;
                //if (totalMonth - 1 >= hd_update.ThoiHanVay)
                //    hd_update.TinhTrang = "Thanh Lý";
                if(SoKyDongLaiBoSung == update.Count)
                    hd_update.TinhTrang = "Thanh Lý";


                decimal QuyTienMat = 0;
                var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == hd_update.CuaHang_Id).OrderByDescending(p => p.Id).FirstOrDefault();
                if (lastestLichSuThaoTac != null)
                    QuyTienMat = lastestLichSuThaoTac.TongCongTon;
                QuyTienMat = QuyTienMat + SoTien;
                var lichsu = new LichSuThaoTac()
                {
                    Id_CuaHang = hd_update.CuaHang_Id,
                    Id_HopDong = hd_update.HD_Id,
                    Id_NhanVienThaoTac = Id_NhanVien,
                    NgayThaoTac = DateTime.Now,
                    NoiDung = "Đóng lãi",
                    SoTien = SoTien,
                    ThuTien = 1,
                    TongCongTon = QuyTienMat,
                    TenKhachHang = TenKhachHang
                };

                context.LichSuThaoTacs.Add(lichsu);
                if (TienPhat > 0)
                {
                    QuyTienMat = QuyTienMat + TienPhat;
                    var lichsu2 = new LichSuThaoTac()
                    {
                        Id_CuaHang = hd_update.CuaHang_Id,
                        Id_HopDong = hd_update.HD_Id,
                        Id_NhanVienThaoTac = Id_NhanVien,
                        NgayThaoTac = DateTime.Now,
                        NoiDung = "Tiền khác",
                        SoTien = TienPhat,
                        ThuTien = 1,
                        TongCongTon = QuyTienMat,
                        TenKhachHang = TenKhachHang
                    };
                    context.LichSuThaoTacs.Add(lichsu2);
                }

                var baocao = context.BaoCaoHangNgays.OrderByDescending(x => x.Ngay).FirstOrDefault(x => x.Id_CuaHang == hd_update.CuaHang_Id);
                if (baocao != null)
                {
                    if (baocao.Ngay == DateTime.Now.Date)
                    {
                        baocao.SoTienVonConLai += SoTien;
                        baocao.TongThu += SoTien;
                        baocao.TongThuHopDongChoVayDNGD += SoTien;
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
                        bcModel.SoTienVonConLai += SoTien;
                        bcModel.TongThu += SoTien;
                        bcModel.TongThuHopDongChoVayDNGD += SoTien;

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
                    bcModel.SoTienVonConLai += SoTien;
                    bcModel.TongThu += SoTien;
                    bcModel.TongThuHopDongChoVayDNGD += SoTien;
                    context.BaoCaoHangNgays.Add(bcModel);
                }



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

        public static int HuyBoHenGio(string id_HopDong, DateTime ngayHen, string ghiChu)
        {
            using (var context = new TIT_Entities())
            {
                var HD =context.HD_VayDuNoGiamDan.Where(x => x.HD_Id == id_HopDong).FirstOrDefault();
                if (HD != null)
                {
                    HD.TrangThaiHenGio = 0;
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
                        
                    }
                }
            }
            return 0;
        }

        public static int HenGio(string id_HopDong, DateTime ngayHen, string ghiChu)
        {
            using (var context = new TIT_Entities())
            {
                var HD = context.HD_VayDuNoGiamDan.Where(x => x.HD_Id == id_HopDong).FirstOrDefault();
                if (HD != null)
                {
                    HD.TrangThaiHenGio = 1;
                    HD.NgayHenGio = ngayHen;
                    HD.GhiChuHenGio = ghiChu;
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

                    }
                }
            }
            return 0;
        }

        public static ChoVayDNGD_CapNhatThongTinDongTienViewModel LayThongTinDongTienHopDong(string maHopDong)
        {
            ChoVayDNGD_CapNhatThongTinDongTienViewModel result = new ChoVayDNGD_CapNhatThongTinDongTienViewModel();
            using (var context = new TIT_Entities())
            {
                result.HopDongChoVay = (from hd in context.HD_VayDuNoGiamDan
                                        join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                                        where hd.HD_Id == maHopDong
                                        select new HopDongChoVayDNGDDataModel()
                                        {
                                            Id_HopDong = hd.HD_Id,
                                            GhiChu = hd.GhiChu,
                                            KhachHang = new KhachHangModel()
                                            {
                                                TenKhachHang = kh.TenKhachHang,
                                                CMND = kh.CMND,
                                                DiaChi = kh.DiaChi != null ? kh.DiaChi : "",
                                                HinhDaiDien = kh.HinhDaiDien != null ? kh.HinhDaiDien : "",
                                                SoDienThoai = kh.SoDienThoai1
                                            },
                                            Lai = hd.LaiSuat,
                                            NgayVay = hd.NgayVay,
                                            SoTienVay = hd.SoTienVay,
                                            TinhTrang = hd.TinhTrang,
                                            CuaHang_Id = hd.CuaHang_Id,
                                            HoaHong = hd.HoaHong,
                                            NhanVien_Id = hd.NhanVien_Id,
                                            ThoiHanVay = hd.ThoiHanVay,
                                            NgayBatDauThanhToan = hd.NgayBatDauDongLai,
                                            NgayDongLaiCuoiCung = hd.NgayCuoiCungDongTien.HasValue ? hd.NgayCuoiCungDongTien.Value : hd.NgayVay,
                                            ThongTinHenGio = new ChoVayDNGD_HenGio() {
                                               NgayHen = hd.NgayHenGio.HasValue? hd.NgayHenGio.Value: DateTime.Now,
                                               GhiChu = hd.GhiChuHenGio,
                                               IsNhacHen = hd.TrangThaiHenGio == 1? true: false
                                            },
                                            UrlHinh1 = hd.UrlHinh1,
                                            UrlHinh2 = hd.UrlHinh2,
                                            UrlHinh3 = hd.UrlHinh3,
                                            TongSoTienLaiDaDong = hd.TienLaiDaDong

                                        }).FirstOrDefault();
                result.ThongTinDongLai = new ChoVayDNGD_ThongTinDongLaiModel();
                result.ThongTinDongLai.DanhSachChiTietDongTienLai = (from dl in context.ThongTinDongLais
                                                                     where dl.HopDong_Id == maHopDong
                                                                     select new ChoVayDNGD_ThongTinChiTietDongTienLaiModel()
                                                                     {
                                                                         ID = dl.Id,
                                                                         NgayBatDau = dl.NgayBatDau,
                                                                         NgayKetThuc = dl.NgayKetThuc,
                                                                         DaDong = dl.DaDong,
                                                                         TienKhac = dl.TienKhac.HasValue ? dl.TienKhac.Value : 0,
                                                                         TienLai = dl.TienLai,
                                                                         SoNgay = dl.SoNgay,
                                                                         SoTienDaDong = dl.SoTienDaDong
                                                                     }).ToList();
             
            }
            return result;
        }
        public static int ThanhToanHopDong(string Id_HopDong, int Id_CuaHang, string Id_NhanVien, decimal SoTien, decimal TienPhat, string NoiDung,
            DateTime NgayBatDau, DateTime NgayKetThuc, string TenKhachHang)
        {
            using (var context = new TIT_Entities())
            {
                //update
                var item_update = context.HD_VayDuNoGiamDan.SingleOrDefault(x => x.HD_Id == Id_HopDong);
                item_update.TinhTrang = "Thanh Lý";
                item_update.TienLaiDaDong += SoTien;
                var item_delete = context.ThongTinDongLais.Where(x => x.DaDong == 0 && x.HopDong_Id == Id_HopDong).ToArray();
                var start = item_delete.First();
                foreach (var item in item_delete)
                    context.ThongTinDongLais.Remove(item);
                var item_donglai = new ThongTinDongLai() {
                    HopDong_Id = Id_HopDong,
                    GhiChu = "Thanh lý",
                    DaDong = 1,
                    NgayBatDau = start.NgayBatDau,
                    NgayKetThuc = NgayKetThuc,
                    NgayDongLai = DateTime.Now,
                    NhanVienThuTien = Id_NhanVien,
                    TienLai = SoTien,
                    SoTienDaDong = SoTien,
                    TienKhac = TienPhat,
                    SoNgay = 0
                };
                context.ThongTinDongLais.Add(item_donglai);

                decimal QuyTienMat = 0;
                var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == Id_CuaHang).OrderByDescending(p => p.Id).FirstOrDefault();
                if (lastestLichSuThaoTac != null)
                    QuyTienMat = lastestLichSuThaoTac.TongCongTon;

                QuyTienMat += SoTien;
                var model = new LichSuThaoTac()
                {
                    Id_CuaHang = Id_CuaHang,
                    Id_HopDong = Id_HopDong,
                    Id_NhanVienThaoTac = Id_NhanVien,
                    NgayThaoTac = DateTime.Now,
                    NoiDung = "Thanh lý hợp đồng",
                    SoTien = SoTien,
                    ThuTien = 1,
                    TongCongTon = QuyTienMat,
                    TenKhachHang = TenKhachHang
                };
                context.LichSuThaoTacs.Add(model);
                if (TienPhat > 0)
                {
                    QuyTienMat += TienPhat;
                    var model2 = new LichSuThaoTac()
                    {
                        Id_CuaHang = Id_CuaHang,
                        Id_HopDong = Id_HopDong,
                        Id_NhanVienThaoTac = Id_NhanVien,
                        NgayThaoTac = DateTime.Now,
                        NoiDung = NoiDung,
                        SoTien = TienPhat,
                        ThuTien = 1,
                        TongCongTon = QuyTienMat,
                        TenKhachHang = TenKhachHang
                    };
                    context.LichSuThaoTacs.Add(model2);
                }


                var baocao = context.BaoCaoHangNgays.OrderByDescending(x => x.Ngay).FirstOrDefault(x => x.Id_CuaHang == Id_CuaHang);
                if (baocao != null)
                {
                    if (baocao.Ngay == DateTime.Now.Date)
                    {
                        baocao.SoTienVonConLai += SoTien + TienPhat;
                        baocao.TongThu += SoTien + TienPhat;
                        baocao.TongThuHopDongChoVayDNGD += SoTien + TienPhat;
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
                        bcModel.SoTienVonConLai += SoTien + TienPhat;
                        bcModel.TongThu += SoTien + TienPhat;
                        bcModel.TongThuHopDongChoVayDNGD += SoTien + TienPhat;

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
                    bcModel.SoTienVonConLai += SoTien + TienPhat;
                    bcModel.TongThu += SoTien + TienPhat;
                    bcModel.TongThuHopDongChoVayDNGD += SoTien + TienPhat;
                    context.BaoCaoHangNgays.Add(bcModel);
                }



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
    }
}
