using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIT.Datas.Models
{
    public class KhachHangModel
    {
        [Key]
        [Display(Name = "CMND/ Hộ chiếu")]
        public string CMND { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string TenKhachHang { get; set; }

        public string HinhDaiDien { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        public DateTime NgayTao { get; set; }

        public ThongKeHopDongTheoKhachHang ThongKe { get; set; }
    }

    public class ThongTinChiTietKhachHang
    {
        public int TongSoHopDong { get; set; }
        public int TongSoHopDongDong { get; set; }
        public int TongSoHopDongMo { get; set; }
        public decimal TongTienDangNo { get; set; }
    }

    public class KhachHangDataModel
    {
        [Display(Name = "Tên khách hàng")]
        public string TenKhachHang { get; set; }

        
        [Display(Name = "CMND/ Hộ chiếu")]
        public string CMND { get; set; }
        [Display(Name = "CMND/ Hộ chiếu")]
        public DateTime NgayCapCMND { get; set; }
        [Display(Name = "CMND/ Hộ chiếu")]
        public string NoiCapCMND { get; set; }

        [Display(Name = "Số điện thoại 1")]
        public string SoDienThoai1 { get; set; }
        [Display(Name = "Số điện thoại 2")]
        public string SoDienThoai2 { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Loại nhà")]
        public string LoaiNha { get; set; }
        [Display(Name = "Sở hữu")]
        public string SoHuuNha { get; set; }

        [Display(Name = "Nguyên quán")]
        public string NguyenQuan { get; set; }
        [Display(Name = "Tình trạng hôn nhân")]
        public string TinhTrangHonNhan { get; set; }

        [Display(Name = "Khu vực")]
        public string KhuVuc { get; set; }

        [Display(Name = "Biển số xe")]
        public string BienSoXe { get; set; }
        [Display(Name = "Loại xe")]
        public string LoaiXe { get; set; }
        [Display(Name = "Sở hữu")]
        public string SoHuuXe { get; set; }

        [Display(Name = "Tên công ty/ Nơi làm việc")]
        public string TenCongTy { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChiCongTy { get; set; }
        [Display(Name = "Vị trí làm việc/ Chức vụ")]
        public string ChucVu { get; set; }
        [Display(Name = "Thời gian làm việc bao lâu")]
        public string ThoiGianLamViec { get; set; }

        [Display(Name = "Tên người bảo lãnh")]
        public string TenNguoiBaoLanh { get; set; }
        [Display(Name = "Địa chỉ nhà")]
        public string DiaChiNhaNguoiBaoLanh { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SoDienThoaiNguoiBaoLanh { get; set; }
        [Display(Name = "Địa chỉ làm việc")]
        public string DiaChiLamViecNguoiBaoLanh { get; set; }

      

        [Display(Name = "Lương cơ bản")]
        public decimal LuongCoBan { get; set; }
        [Display(Name = "Lương cơ bản")]
        public int NgayLanhLuong { get; set; }
        [Display(Name = "Cột lương tiền mặt")]
        public decimal CotLuongTienMat { get; set; }
        [Display(Name = "Tên ngân hàng")]
        public string TenNganHang { get; set; }
        [Display(Name = "Chủ tài khoản")]
        public string ChuTaiKhoan { get; set; }
        [Display(Name = "Số tài khoản")]
        public string SoTaiKhoan { get; set; }
        [Display(Name = "Số thẻ")]
        public string SoThe { get; set; }
        [Display(Name = "Password")]
        public string PasswordThe { get; set; }
        [Display(Name = "Mã hóa thẻ")]
        public string MaHoaThe { get; set; }

        public DateTime NgayTao { get; set; }

        public string HinhDaiDien { get; set; }
        public string HinhCMNDMatTruoc { get; set; }
        public string HinhCMNDMatSau { get; set; }
    }

    public class ThongKeHopDongTheoKhachHang
    {
        public int TongSoHopDong { get; set; }
        public int SoHopDongDong { get; set; }
        public int SoHopDongMo { get; set; }
    }

    public class ChiTietThongKeHopDongTheoKhachHang
    {
        public string CMND { get; set; }
        public string TinhTrang { get; set; }
    }
}
