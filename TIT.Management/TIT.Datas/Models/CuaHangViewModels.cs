using System;
using System.ComponentModel.DataAnnotations;

namespace TIT.Datas.Models
{
    public class CuaHangGridViewModel
    {
        public int MaCuaHang { get; set; }
        [Display(Name = "Tên cửa hàng")]
        public string TenCuaHang { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Số ĐT")]
        public string DienThoai { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        [Display(Name = "Vốn đầu tư")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal VonDauTu { get; set; }

        [Display(Name = "Quỹ tiền mặt")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal QuyTienMat { get; set; }
        public int TinhTrang { get; set; }

    }
    public class CuaHangDataModel
    {
        public int MaCuaHang { get; set; }
        public string TenCuaHang { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal VonDauTu { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal QuyTienMat { get; set; }
        public int TinhTrang { get; set; }
        public string TKQuanLy { get; set; }
    }

    public class CuaHangDetailModel
    {
        public int MaCuaHang { get; set; }
        [Display(Name = "Cửa hàng")]
        public string TenCuaHang { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public decimal VonDauTu { get; set; }
        public decimal QuyTienMat { get; set; }
        public bool TinhTrang { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TKQuanLy { get; set; }
    }
    public class CuaHangAddNewModel
    {
        public int MaCuaHang { get; set; }
        public string TenCuaHang { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public decimal VonDauTu { get; set; }
        public decimal QuyTienMat { get; set; }
        public bool TinhTrang { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TKQuanLy { get; set; }
        public string QuanLyCuaHang { get; set; }

    }
    public class CuaHangUpdateModel
    {
        public int MaCuaHang { get; set; }
        public string TenCuaHang { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public decimal VonDauTu { get; set; }
        public decimal QuyTienMat { get; set; }
        public bool TinhTrang { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string QuanLyCuaHang { get; set; }
        public string TKQuanLy { get; set; }

    }

    public class BaoCaoCuaHangTempViewModel
    {
        public decimal TongSoTienDauNgay { get; set; }
        public decimal TongSoTienConLai { get; set; }
        public decimal TongThu { get; set; }
        public decimal TongThuKhac { get; set; }
        public decimal TongThuCamDo { get; set; }
        public decimal TongThuChoVay { get; set; }
        public decimal TongThuBatHo { get; set; }
        public decimal TongThuDNGD { get; set; }
        public decimal TongChi { get; set; }
        public decimal TongChiKhac { get; set; }
        public decimal TongChiCamDo { get; set; }
        public decimal TongChiChoVay { get; set; }
        public decimal TongChiBatHo { get; set; }
        public decimal TongChiDNGD { get; set; }
    }


    public class BaoCaoCuaHangViewModel
    {

        public CuaHangDetailModel chitiet { get; set; }

        public decimal TienDangChoVay { get; set; }

        //Thong tin lãi
        public decimal LaiDuKien { get; set; }
        public decimal LaiDaThu { get; set; }

        //Thong tin hợp đồng
        public int HopDongMo { get; set; }
        public int HopDongDong { get; set; }
        public int TongSoHopDong { get; set; }
        public int TongSoHopDongNoLai { get; set; }

        //Thong tin thu chi
        public decimal ChiTieu { get; set; }
        public decimal ThuBatThuong { get; set; }
        public decimal SoTienRutTuLai { get; set; }
        public decimal TongSoTienKhachNo { get; set; }

        public CuaHang_ThongKeHopDong ThongKeCamDo;
        public CuaHang_ThongKeHopDong ThongKeBatHo;
        public CuaHang_ThongKeHopDong ThongKeChoVay;
    }

    public class CuaHang_ThongKeHopDong
    {
        public int TongSoHopDong { get; set; }
        public int HopDongDong { get; set; }
        public int HopDongMo { get; set; }
        public decimal TienChoVay { get; set; }
        public decimal LaiDuKien { get; set; }
        public decimal LaiDaThu { get; set; }
        public decimal TienKhachNo { get; set; }
    }

    public class NhanVienModel
    {
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
    }

}
