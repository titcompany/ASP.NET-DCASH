using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Models;

namespace TIT.Datas.Services
{
    public class ThuChiService
    {
        const int PHIEU_THU = 0;
        const int PHIEU_CHI = 1;

        public static int Insert(ThuChiDataModel model, int PhieuThuHayChi)
        {
            var dbModel = new ThongTinThuChi()
            {
                Id_CuaHang = model.MaCuaHang,
                Id_NhanVien = model.MaNhanVien,
                KhachHang = model.KhachHang,
                LoaiPhieu = model.LoaiPhieu,
                NgayTao = DateTime.Now,
                PhieuThu = PhieuThuHayChi,
                GhiChu = model.GhiChu,
                SoTien = model.SoTien
            };
            
            using (var context = new TIT_Entities())
            {
                decimal QuyTienMat = 0;
                var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == model.MaCuaHang).OrderByDescending(p => p.Id).FirstOrDefault();
                if (lastestLichSuThaoTac != null)
                    QuyTienMat = lastestLichSuThaoTac.TongCongTon;

                var dbLichsu = new LichSuThaoTac()
                {
                    Id_CuaHang = model.MaCuaHang,
                    Id_HopDong = "",
                    Id_NhanVienThaoTac = model.MaNhanVien,
                    NgayThaoTac = DateTime.Now,
                    NoiDung = model.LoaiPhieu + ":" + model.GhiChu,
                    SoTien = model.SoTien,
                    ThuTien = PhieuThuHayChi == PHIEU_THU ? 1 : 0,
                    TongCongTon = QuyTienMat + model.SoTien * (PhieuThuHayChi == PHIEU_THU ? 1 : -1),
                    TenKhachHang = model.KhachHang
                };
                context.LichSuThaoTacs.Add(dbLichsu);
                var baocao = context.BaoCaoHangNgays.OrderByDescending(x => x.Ngay).FirstOrDefault(x => x.Id_CuaHang == model.MaCuaHang);
                if (baocao != null)
                {
                    if (baocao.Ngay == DateTime.Now.Date)
                    {
                        if (PhieuThuHayChi == PHIEU_THU)
                        {
                            baocao.SoTienVonConLai += model.SoTien;
                            baocao.TongThu += model.SoTien;
                            baocao.TongThuKhac += model.SoTien;
                        }
                        else
                        {
                            baocao.SoTienVonConLai -= model.SoTien;
                            baocao.TongChi += model.SoTien;
                            baocao.TongChiKhac += model.SoTien;
                        }
                    }
                    else
                    {
                        var bcModel = new BaoCaoHangNgay
                        {
                            Id_CuaHang = model.MaCuaHang,
                            Ngay = DateTime.Now.Date,
                            SoTienVonDauNgay = baocao.SoTienVonConLai,
                            SoTienVonConLai = baocao.SoTienVonConLai
                        };
                        if (PhieuThuHayChi == PHIEU_THU)
                        {
                            bcModel.SoTienVonConLai += model.SoTien;
                            bcModel.TongThu += model.SoTien;
                            bcModel.TongThuKhac += model.SoTien;
                        }
                        else
                        {
                            bcModel.SoTienVonConLai -= model.SoTien;
                            bcModel.TongChi += model.SoTien;
                            bcModel.TongChiKhac += model.SoTien;
                        }
                        context.BaoCaoHangNgays.Add(bcModel);
                    }
                }
                else
                {
                    var bcModel = new BaoCaoHangNgay
                    {
                        Id_CuaHang = model.MaCuaHang,
                        Ngay = DateTime.Now.Date
                    };
                    if (PhieuThuHayChi == PHIEU_THU)
                    {
                        bcModel.SoTienVonConLai = model.SoTien;
                        bcModel.TongThu = model.SoTien;
                        bcModel.TongThuKhac = model.SoTien;
                    }
                    else
                    {
                        bcModel.SoTienVonConLai = -model.SoTien;
                        bcModel.TongChi = model.SoTien;
                        bcModel.TongChiKhac = model.SoTien;
                    }
                    context.BaoCaoHangNgays.Add(bcModel);
                }





                context.ThongTinThuChis.Add(dbModel);
                try
                {

                    if (context.SaveChanges() > 0)
                    {
                        
                        return dbModel.Id;
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

        public static IEnumerable<ThuChiGridViewModel> LayDanhSachChi(IEnumerable<CuaHangDataModel> models)
        {
            IEnumerable<ThuChiGridViewModel> result;
            var DanhSachIDCuaHang = models.Select(x => x.MaCuaHang);
            using (var context = new TIT_Entities())
            {
                DateTime today = DateTime.Now.Date;
                DateTime tmr = DateTime.Now.Date.AddDays(1);
                result = (from tc in context.ThongTinThuChis
                          join ch in context.CuaHangs on tc.Id_CuaHang equals ch.MaCuaHang
                          join nv in context.AspNetUsers on tc.Id_NhanVien equals nv.Id
                          where DanhSachIDCuaHang.Contains(tc.Id_CuaHang) && tc.NgayTao >= today && tc.NgayTao < tmr && tc.PhieuThu == PHIEU_CHI
                          select new ThuChiGridViewModel()
                          {
                              KhachHang = tc.KhachHang,
                              GhiChu = tc.GhiChu,
                              LoaiPhieu = tc.LoaiPhieu,
                              SoTien = tc.SoTien,
                              Ngay = tc.NgayTao,
                              CuaHang = new CuaHangDataModel()
                              {
                                  MaCuaHang = ch.MaCuaHang,
                                  TenCuaHang = ch.TenCuaHang
                              },
                              NhanVien = nv.UserName
                          }).ToArray();
            }
            return result;
        }

        public static object LayThuChiTheoIDCuaHang(int maCuaHang)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<ThuChiGridViewModel> LayDanhSachThu(IEnumerable<CuaHangDataModel> models)
        {
            IEnumerable<ThuChiGridViewModel> result;
            var DanhSachIDCuaHang = models.Select(x => x.MaCuaHang);
            using (var context = new TIT_Entities())
            {
                DateTime today = DateTime.Now.Date;
                DateTime tmr = DateTime.Now.Date.AddDays(1);
                result = (from tc in context.ThongTinThuChis
                          join ch in context.CuaHangs on tc.Id_CuaHang equals ch.MaCuaHang
                          join nv in context.AspNetUsers on tc.Id_NhanVien equals nv.Id
                          where DanhSachIDCuaHang.Contains(tc.Id_CuaHang) && tc.NgayTao >= today && tc.NgayTao < tmr && tc.PhieuThu == PHIEU_THU
                          select new ThuChiGridViewModel()
                          {
                              KhachHang = tc.KhachHang,
                              GhiChu = tc.GhiChu,
                              LoaiPhieu = tc.LoaiPhieu,
                              SoTien = tc.SoTien,
                              Ngay = tc.NgayTao,
                              CuaHang = new CuaHangDataModel()
                              {
                                  MaCuaHang = ch.MaCuaHang,
                                  TenCuaHang = ch.TenCuaHang
                              },
                              NhanVien = nv.UserName
                          }).ToArray();
            }
            return result;
        }
    }
}
