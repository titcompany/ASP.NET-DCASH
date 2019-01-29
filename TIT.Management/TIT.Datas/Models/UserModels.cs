using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Helper;

namespace TIT.Datas.Models
{
    public class UserModel
    {
        public string _userId;
        public string _role;
        public static void CapNhatBaoCao_Chi(TIT_Entities context, int MaCuaHang, decimal SoTien)
        {
            var BaoCaoTongKet = context.BaoCaoTongKets.Where(x => x.Id_CuaHang == MaCuaHang).OrderByDescending(p => p.Ngay).FirstOrDefault();
            if (BaoCaoTongKet != null)
            {
                if (BaoCaoTongKet.Ngay == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    BaoCaoTongKet.CamDo_Chi += SoTien;
                }
                else
                {
                    var baoCao = new BaoCaoTongKetGiaoDichModel()
                    {
                        SoTienDauNgay = BaoCaoTongKet.SoTienDauNgay,
                        CamDo_Thu = BaoCaoTongKet.CamDo_Thu,
                        CamDo_Chi = BaoCaoTongKet.CamDo_Chi,
                        VayLai_Thu = BaoCaoTongKet.VayLai_Thu,
                        VayLai_Chi = BaoCaoTongKet.VayLai_Chi,
                        BatHo_Thu = BaoCaoTongKet.BatHo_Thu,
                        BatHo_Chi = BaoCaoTongKet.BatHo_Chi,
                        HoatDong_Thu = BaoCaoTongKet.HoatDong_Thu,
                        HoatDong_Chi = BaoCaoTongKet.HoatDong_Chi,
                        Ngay_ToString = BaoCaoTongKet.Ngay
                    };
                    var baoCaoHomNay = new BaoCaoTongKet()
                    {
                        Ngay = DateTime.Now.ToString("yyyy-MM-dd"),
                        SoTienDauNgay = baoCao.SoTienCuoiNgay,
                        CamDo_Chi = SoTien,
                        Id_CuaHang = MaCuaHang,
                    };
                    context.BaoCaoTongKets.Add(baoCaoHomNay);
                }
            }
            else
            {
                var baoCaoHomNay = new BaoCaoTongKet()
                {
                    Ngay = DateTime.Now.ToString("yyyy-MM-dd"),
                    CamDo_Chi = SoTien,
                    Id_CuaHang = MaCuaHang,
                };
                context.BaoCaoTongKets.Add(baoCaoHomNay);
            }
        }

        internal IEnumerable<CamDoPaymentHistoryDataModel> GetHistoryPaymentViewModel(string Id_HopDong)
        {
            IEnumerable<CamDoPaymentHistoryDataModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.ThongTinDongLais
                          join nv in context.AspNetUsers on hd.NhanVienThuTien equals nv.Id into gj
                          from subpet in gj.DefaultIfEmpty()
                          where hd.HopDong_Id == Id_HopDong
                          select new CamDoPaymentHistoryDataModel()
                          {
                              TuNgay = hd.NgayBatDau,
                              DenNgay = hd.NgayKetThuc,
                              DaDong = hd.DaDong,
                              TienKhac = hd.TienKhac.HasValue ? hd.TienKhac.Value : 0,
                              SoNgay = hd.SoNgay,
                              TienLaiPhi = hd.TienLai,
                              TenNhanVien = subpet.UserName ?? String.Empty,
                              Id = hd.Id
                          }).ToArray();
            }
            return result;
        }

        internal IEnumerable<CamDoHistoryDataModel> GetHistoryViewModel(string id_HopDong)
        {
            IEnumerable<CamDoHistoryDataModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.LichSuThaoTacs
                          join user in context.AspNetUsers on hd.Id_NhanVienThaoTac equals user.Id
                          where hd.Id_HopDong == id_HopDong
                          select new CamDoHistoryDataModel
                          {
                              Ngay = hd.NgayThaoTac,
                              GiaoDichVien = user.UserName,
                              SoTienGhiCo = hd.ThuTien == 1 ? hd.SoTien : 0,
                              SoTienGhiNo = hd.ThuTien == 0 ? hd.SoTien : 0,
                              NoiDung = hd.NoiDung
                          }).ToArray();
            }
            return result;
        }

        

        public static void CapNhatBaoCao_Thu(TIT_Entities context, int MaCuaHang, decimal SoTien)
        {
            var BaoCaoTongKet = context.BaoCaoTongKets.Where(x => x.Id_CuaHang == MaCuaHang).OrderByDescending(p => p.Ngay).FirstOrDefault();
            if (BaoCaoTongKet != null)
            {
                if (BaoCaoTongKet.Ngay == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    BaoCaoTongKet.CamDo_Thu += SoTien;
                }
                else
                {
                    var baoCao = new BaoCaoTongKetGiaoDichModel()
                    {
                        SoTienDauNgay = BaoCaoTongKet.SoTienDauNgay,
                        CamDo_Thu = BaoCaoTongKet.CamDo_Thu,
                        CamDo_Chi = BaoCaoTongKet.CamDo_Chi,
                        VayLai_Thu = BaoCaoTongKet.VayLai_Thu,
                        VayLai_Chi = BaoCaoTongKet.VayLai_Chi,
                        BatHo_Thu = BaoCaoTongKet.BatHo_Thu,
                        BatHo_Chi = BaoCaoTongKet.BatHo_Chi,
                        HoatDong_Thu = BaoCaoTongKet.HoatDong_Thu,
                        HoatDong_Chi = BaoCaoTongKet.HoatDong_Chi,
                        Ngay_ToString = BaoCaoTongKet.Ngay
                    };
                    var baoCaoHomNay = new BaoCaoTongKet()
                    {
                        Ngay = DateTime.Now.ToString("yyyy-MM-dd"),
                        SoTienDauNgay = baoCao.SoTienCuoiNgay,
                        CamDo_Thu = SoTien,
                        Id_CuaHang = MaCuaHang,
                    };
                    context.BaoCaoTongKets.Add(baoCaoHomNay);
                }
            }
            else
            {
                var baoCaoHomNay = new BaoCaoTongKet()
                {
                    Ngay = DateTime.Now.ToString("yyyy-MM-dd"),
                    CamDo_Thu = SoTien,
                    Id_CuaHang = MaCuaHang,
                };
                context.BaoCaoTongKets.Add(baoCaoHomNay);
            }
        }
        internal static UserModel getUserModel(string userId, string role)
        {
            UserModel userModel = null;
            if (role == "Admin")
                userModel = new Admin();
            else if (role == "BranchManager")
                userModel = new BranchManager();
            else if (role == "Employee")
                userModel = new Employee();
            if (userModel != null)
            {
                userModel._userId = userId;
                userModel._role = role;
            }
            return userModel;
        }

        internal CamDoGridDataModel InsertCamDo(CamDoInsertNewDataModel model)
        {
            var dbModel = new HD_CamDo
            {
                CuaHang_Id = model.Id_CuaHang,
                MaTaiSan = model.LoaiTaiSan,
                TaiSan = model.TenTaiSan,
                NgayCuoiCungDongTienLai = model.NgayVay,
                HinhThucLai = model.HinhThucLai,
                GhiChu = model.GhiChu,
                KyLai = model.KyLai,
                Lai = model.LaiPhi,
                SoTienCam = model.SoTien,
                NgayCam = model.NgayVay,
                NgayThanhLyHopDong = null,
                NhanVien_Id = _userId,
                TinhTrang = "Đang cầm",
                KhachHang_CMND = model.CMND,
                KhachHang_HoTen = model.TenKhachHang,
                NgayTao = DateTime.Now,
            };

            using (var context = new TIT_Entities())
            {
                context.HD_CamDo.Add(dbModel);

                try
                {
                    if (context.SaveChanges() > 0)
                    {
                        var dbDongLai = new ThongTinDongLai()
                        {
                            HopDong_Id = dbModel.HD_CamDo_Id,
                            TienLai = HinhThucLaiHelper.TinhLaiMotNgay(model.HinhThucLai, model.SoTien, model.LaiPhi) * model.KyLai,
                            TienKhac = 0,
                            DaDong = 0,
                            NgayBatDau = model.NgayVay.Date,
                            NgayKetThuc = model.NgayVay.Date.Add(new TimeSpan(model.KyLai - 1, 0, 0, 0)),
                            NgayDongLai = DateTime.Now
                        };

                        CapNhatBaoCao_Chi(context, model.Id_CuaHang, model.SoTien);

                        decimal QuyTienMat = 0;
                        var lastestLichSuThaoTac = context.LichSuThaoTacs.Where(x => x.Id_CuaHang == model.Id_CuaHang).OrderByDescending(p => p.Id).FirstOrDefault();
                        if (lastestLichSuThaoTac != null)
                            QuyTienMat = lastestLichSuThaoTac.TongCongTon;

                        var dbModel2 = new LichSuThaoTac()
                        {
                            Id_CuaHang = model.Id_CuaHang,
                            Id_HopDong = dbModel.HD_CamDo_Id,
                            Id_NhanVienThaoTac = _userId,
                            NgayThaoTac = DateTime.Now,
                            NoiDung = "Tạo hợp đồng",
                            SoTien = model.SoTien,
                            ThuTien = 0,
                            TongCongTon = QuyTienMat - model.SoTien,
                            TenKhachHang = model.TenKhachHang
                        };
                        context.ThongTinDongLais.Add(dbDongLai);
                        context.LichSuThaoTacs.Add(dbModel2);
                        if (context.SaveChanges() > 0)
                        {
                            return new CamDoGridDataModel()
                            {
                                HopDong_Id = dbModel.HD_CamDo_Id,
                                SoTien = dbModel.SoTienCam,
                                LaiDaDong = "0",
                                LaiPhiDenHomNay ="0",
                                NgayPhaiDongLai = dbModel.NgayCuoiCungDongTienLai.Value.AddDays(1),
                                NgayTaoHopDong = dbModel.NgayCam,
                                NoCu = "0",
                                TaiSan = dbModel.TaiSan,
                                TinhTrang = dbModel.TinhTrang
                            };
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

        public virtual IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            throw new NotImplementedException();
        }
    }

    public class Admin : UserModel
    {
        public override IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            IEnumerable<CamDoGridDataModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_CamDo
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          select new CamDoGridDataModel
                          {
                              HopDong_Id = hd.HD_CamDo_Id,
                              TenKhachHang = kh.TenKhachHang,
                              SoDienThoai = kh.SoDienThoai1,
                              NgayPhaiDongLai = hd.NgayCuoiCungDongTienLai.HasValue ? hd.NgayCuoiCungDongTienLai.Value : hd.NgayCam,
                              TinhTrang = hd.TinhTrang.ToString(),
                              NoCu = "",
                              SoTien = hd.SoTienCam,
                              TaiSan = hd.TaiSan,
                              NgayTaoHopDong = hd.NgayCam
                          }).ToArray();

            }
            return result;
        }
    }
    public class BranchManager : UserModel
    {
        public override IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            IEnumerable<CamDoGridDataModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_CamDo
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          join ch in context.CuaHangs on hd.CuaHang_Id equals ch.MaCuaHang
                          where ch.QuanLyCuaHang == _userId
                          select new CamDoGridDataModel
                          {
                              HopDong_Id = hd.HD_CamDo_Id,
                              TenKhachHang = kh.TenKhachHang,
                              SoDienThoai = kh.SoDienThoai1,
                              NgayPhaiDongLai = hd.NgayCuoiCungDongTienLai.HasValue ? hd.NgayCuoiCungDongTienLai.Value : hd.NgayCam,
                              TinhTrang = hd.TinhTrang.ToString(),
                              NoCu = "",
                              SoTien = hd.SoTienCam,
                              TaiSan = hd.TaiSan,
                              NgayTaoHopDong = hd.NgayCam
                          }).ToArray();
            }
            return result;
        }
    }
    public class Employee : UserModel
    {
        public override IEnumerable<CamDoGridDataModel> GetGridViewCamDoModel()
        {
            IEnumerable<CamDoGridDataModel> result;
            using (var context = new TIT_Entities())
            {
                result = (from hd in context.HD_CamDo
                          join kh in context.KhachHangs on hd.KhachHang_CMND equals kh.CMND
                          join user in context.AspNetUsers on hd.CuaHang_Id equals user.CuaHang_Id
                          where user.Id == _userId
                          select new CamDoGridDataModel
                          {
                              HopDong_Id = hd.HD_CamDo_Id,
                              TenKhachHang = kh.TenKhachHang,
                              SoDienThoai = kh.SoDienThoai1,
                              NgayPhaiDongLai = hd.NgayCuoiCungDongTienLai.HasValue ? hd.NgayCuoiCungDongTienLai.Value : hd.NgayCam,
                              TinhTrang = hd.TinhTrang.ToString(),
                              NoCu = "",
                              SoTien = hd.SoTienCam,
                              TaiSan = hd.TaiSan,
                              NgayTaoHopDong = hd.NgayCam
                          }).ToArray();

            }
            return result;
        }
    }
}
