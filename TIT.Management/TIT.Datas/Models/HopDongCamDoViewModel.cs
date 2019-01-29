using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Helper;

namespace TIT.Datas.Models
{
    public class CamDoGridDataModel
    {
        public string HopDong_Id { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string TaiSan { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayTaoHopDong { get; set; }
        public string LaiDaDong { get; set; }
        public string NoCu { get; set; }
        public string LaiPhiDenHomNay { get; set; }
        public string TinhTrang { get; set; }
        public DateTime NgayPhaiDongLai { get; set; }
    }

    public class CamDoInsertNewDataModel
    {
        public string CMND { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string LoaiTaiSan { get; set; }
        public string TenTaiSan { get; set; }
        public decimal SoTien { get; set; }
        public int HinhThucLai { get; set; }
        public int LaiPhi { get; set; }
        public int KyLai { get; set; }
        public DateTime NgayVay { get; set; }
        public string GhiChu { get; set; }
        public int Id_CuaHang { get; set; }
    }

    public class CamDoDetailDataModel {
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string SoTien { get; set; }
        public string NgayVay { get; set; }
        public string NgayDongLaiCuoiCung { get; set; }
        public string LaiSuat { get; set; }
        public string TienLaiDaDong { get; set; }
        public string NoCu { get; set; }
        public string TrangThai { get; set; }
    }
    public class CamDoPaymentHistoryDataModel {
        public int Id { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public int SoNgay { get; set; }
        public decimal TienLaiPhi { get; set; }
        public decimal TienKhac { get; set; }
        public string TongTienTra { get; set; }
        public int DaDong { get; set; }
        public string TenNhanVien { get; set; }
    }
    public class CamDoHistoryDataModel
    {
        public DateTime Ngay { get; set; }
        public string GiaoDichVien { get; set; }
        public decimal SoTienGhiNo { get; set; }
        public decimal SoTienGhiCo { get; set; }
        public string NoiDung { get; set; }
    }
}
