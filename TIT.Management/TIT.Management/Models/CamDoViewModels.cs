using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIT.Management.Models
{
    public class CamDoGridViewModel
    {
        public string HopDong_Id{get; set;}
        public string TenKhachHang{get; set;}
        public string SoDienThoai{get; set;}
        public string TaiSan{get; set;}
        public string SoTien{get; set;}
        public string NgayTaoHopDong{get; set;}
        public string LaiDaDong{get; set;}
        public string NoCu{get; set;}
        public string LaiPhiDenHomNay{get; set;}
        public string TinhTrang{get; set;}
        public string NgayPhaiDongLai{get; set;}
    }

    public class CamDoInsertNewModel
    {
        public string CMND{get; set;}
        public string TenKhachHang{get; set;}
        public string SoDienThoai{get; set;}
        public string DiaChi{get; set;}
        public string LoaiTaiSan{get; set;}
        public string TenTaiSan { get; set; }
        public decimal SoTien{get; set;}
        public string HinhThucLai{get; set;}
        public int LaiPhi{get; set;}
        public int KyLai{get; set;}
        public DateTime NgayVay{get; set;}
        public string GhiChu{get; set;}
        public int Id_CuaHang { get; set; }
    }

    public class CamDoDetaiViewlModel
    {
        public string TenKhachHang{get; set;}
        public string SoDienThoai{get; set;}
        public string SoTien{get; set;}
        public string NgayVay{get; set;}
        public string NgayDongLaiCuoiCung{get; set;}
        public string LaiSuat{get; set;}
        public string TienLaiDaDong{get; set;}
        public string NoCu{get; set;}
        public string TrangThai{get; set;}
        
        public List<LichSuDongLaiCamDo> LichSuDongLai{get; set;}
        public List<LichSuThaoTacCamDo> LichSuThaoTac{get; set;}
    }
    public class LichSuDongLaiCamDo
    {
        public string TuNgay{get; set;}
        public string DenNgay{get; set;}
        public string SoNgay{get; set;}
        public string TienLaiPhi{get; set;}
        public string TienKhac{get; set;}
        public string TongTienTra{get; set;}
        public bool DaDong{get; set;}
    }
    public class LichSuThaoTacCamDo
    {
        public string Ngay{get; set;}
        public string GiaoDichVien{get; set;}
        public string SoTienGhiNo{get; set;}
        public string SoTienGhiCo{get; set;}
        public string NoiDung{get; set;}
    }
}