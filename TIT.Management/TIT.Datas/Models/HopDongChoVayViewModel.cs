using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Helper;

namespace TIT.Datas.Models
{
    public class HopDongChoVayGridViewModel
    {
        public int STT { get; set; }

        [Display(Name = "Mã HĐ")]
        public string MaHopDong { get; set; }
        
        public KhachHangModel KhachHang { get; set; }

        [Display(Name = "Tài Sản")]
        public string TaiSan { get; set; }
        [Display(Name = "VNĐ")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTienVay { get; set; }
        [Display(Name = "Ngày vay")]
        public DateTime NgayVay { get; set; }
        [Display(Name = "Lãi đã đóng")]
        public int LaiDaDong { get; set; }
        [Display(Name = "Nợ cũ")]
        public int NoCu { get; set; }
        
        [Display(Name = "Tình trạng")]
        public string TinhTrang { get; set; }
        public int HinhThucLai { get; set; }
        public int Lai { get; set; }

        [Display(Name = "Lãi đến")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal LaiDen
        {
            get
            {
                decimal temp = LaiMotNgay * (int)(DateTime.Now.Date.Subtract(NgayPhaiDongLai.Date).TotalDays + 1);
                return temp > 0 ? temp : 0;
            }
        }

        [Display(Name = "Ngày phải đóng lãi")]
        public DateTime NgayPhaiDongLai
        {
            get
            {
                return NgayCuoiCungDongLai.AddDays(1);
            }
        }

        public DateTime NgayCuoiCungDongLai { get; set; }


        public string MauTinhTrang
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return "white";

                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongLai.Date).TotalDays;
                if (timeDiff < 0)
                    return "white";
                else if (timeDiff < 1)
                    return "green";
                else if (timeDiff < 3)
                    return "orange";
                else
                    return "red";
            }
        }

        public string TrangThai
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return TinhTrang;
                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongLai.Date).TotalDays;
                if (timeDiff < 0)
                    return "Cho vay";
                else if (timeDiff < 1)
                    return "Hôm nay";
                else if (timeDiff < 3)
                    return "Chậm lãi";
                else
                    return "Trễ hạn";
            }
        }

        public decimal LaiMotNgay
        {
            get
            {
                return HinhThucLaiHelper.TinhLaiMotNgay(HinhThucLai, SoTienVay, Lai);
            }
        }

    }
    public class HopDongChoVayViewModel
    {
        [Display(Name = "Mã HĐ")]
        public int MaHopDong { get; set; }
        [Display(Name = "Tên khách hàng")]
        public int TenKhachHang { get; set; }
        [Display(Name = "Tài Sản")]
        public int TaiSan { get; set; }
        [Display(Name = "VNĐ")]
        public int SoTienCam { get; set; }
        [Display(Name = "Ngày vay")]
        public int NgayVay { get; set; }
        [Display(Name = "Lãi đã đóng")]
        public int LaiDaDong { get; set; }
        [Display(Name = "Nợ cũ")]
        public int NoCu { get; set; }
        [Display(Name = "Lãi đến")]
        public int LaiDen { get; set; }
        [Display(Name = "Tình trạng")]
        public int TinhTrang { get; set; }
        [Display(Name = "Ngày phải đóng lãi")]
        public int NgayPhaiDongLai { get; set; }

    }

    public class HopDongChoVayDataModel
    {

        [Key]
        [Display(Name = "Mã hợp đồng")]
        public string MaHopDong { get; set; }
        
        [Display(Name = "Tên tài sản thế chấp")]
        public string TenTaiSan { get; set; }

        [Display(Name = "Số tiền vay")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTienVay { get; set; }

        [Display(Name = "Ngày vay")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime NgayVay { get; set; }

        [Display(Name = "Hình thức lãi")]
        public int HinhThucLai { get; set; }

        [Display(Name = "Lãi")]
        public double Lai { get; set; }

        [Display(Name = "Kỳ lãi")]
        public int KyLai { get; set; }

        [Display(Name = "Số ngày vay")]
        public int SoNgayVay { get; set; }

        [Display(Name = "Tình trạng")]
        public string TinhTrang { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public decimal LaiMotNgay { get; set; }

        public KhachHangModel KhachHang { get; set; }

        // Khoa ngoai tham chieu den bang Cua Hang
        public int MaCuaHang { get; set; }

        // Khoa ngoai tham chieu den bang Nhan Vien
        public string MaNhanVien { get; set; }

        public DateTime NgayDongLaiCuoiCung { get; set; }

        public DateTime NgayPhaiDongLai
        {
            get
            {
                return NgayDongLaiCuoiCung.AddDays(1);
            }
        }

        public string MauTinhTrang
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return "white";

                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongLai.Date).TotalDays;
                if (timeDiff < 0)
                    return "white";
                else if (timeDiff < 1)
                    return "green";
                else if (timeDiff < 3)
                    return "orange";
                else
                    return "red";
            }
        }

        public string TrangThai
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return TinhTrang;
                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongLai.Date).TotalDays;
                if (timeDiff < 0)
                    return "Cho vay";
                else if (timeDiff < 1)
                    return "Hôm nay";
                else if (timeDiff < 3)
                    return "Chậm lãi";
                else
                    return "Trễ hạn";
            }
        }
    }

    public class ChoVay_ThongTinDongLaiModel
    {
        [Display(Name = "Lãi từ ngày")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LaiTuNgay { get; set; }

        [Display(Name = "Lãi đến ngày")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LaiDenNgay { get; set; }

        [Display(Name = "Số ngày")]
        public int SoNgay { get; set; }

        [Display(Name = "Tiền lãi")]
        // [DisplayFormat(DataFormatString = "{0:0,0 vnđ}")]
        public decimal TienLai { get; set; }

        [Display(Name = "Tiền khác")]
        // [DisplayFormat(DataFormatString = "{0:0,0 vnđ}")]
        public decimal TienKhac { get; set; }

        [Display(Name = "Tổng tiền")]
        // [DisplayFormat(DataFormatString = "{0:0,0 vnđ}")]
        public decimal TongTien { get; set; }
        public List<ChoVay_ThongTinChiTietDongTienLaiModel> DanhSachChiTietDongTienLai { get; set; }
    }
    public class ChoVay_ThongTinChiTietDongTienLaiModel
    {
        public int ID { get; set; }
        [Display(Name = "Ngày BĐ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgayBatDau { get; set; }

        [Display(Name = "Ngày KT")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgayKetThuc { get; set; }

        [Display(Name = "Số ngày")]
        public int SoNgay { get; set; }

        [Display(Name = "Lãi")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienLai { get; set; }

        [Display(Name = "Khác")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienKhac { get; set; }

        [Display(Name = "Tổng tiền")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TongTienTra { get { return TienLai + TienKhac; } }

        [Display(Name = "Nhân viên")]
        public string TenNhanVien { get; set; }

        public string MaNhanVien { get; set; }

        public int DaDong { get; set; }
    }

    public class ChoVay_ThongTinThayDoiSoTienHopDongModel
    {
        public const int VAY_THEM = 1;
        public const int TRA_BOT_GOC = 2;
        public DateTime NgayThucHien { get; set; }
        public decimal SoTien { get; set; }
        public int Loai { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
    }

    public class ChoVay_ThongTinThanhLyHopDongModel
    {
        public DateTime NgayThanhLy { get; set; }
        public decimal SoTienChoVay { get; set; }
        public int NoCu { get; set; }
        public decimal TienLai { get; set; }
        public decimal TienKhac { get; set; }
        public decimal TongTienTra { get { return TienLai + TienKhac + SoTienChoVay; } }
        public int SoNgay { get; set; }
    }


    public class ChoVay_CapNhatThongTinDongTienViewModel
    {
        public HopDongChoVayDataModel HopDongChoVay { get; set; }
        public ChoVay_ThongTinThanhLyHopDongModel ThongTinThanhLyHopDongModel { get; set; }
        public ChoVay_ThongTinDongLaiModel ThongTinDongLai { get; set; }
        public ChoVay_ThongTinThayDoiSoTienHopDongModel ThongTinThayDoiSoTienHopDong { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgayCuoiCungDongLai
        {
            get
            {
                if (ThongTinDongLai != null && ThongTinDongLai.DanhSachChiTietDongTienLai != null && ThongTinDongLai.DanhSachChiTietDongTienLai.Count > 1)
                {
                    return ThongTinDongLai.DanhSachChiTietDongTienLai[ThongTinDongLai.DanhSachChiTietDongTienLai.Count - 2].NgayKetThuc;
                }
                return HopDongChoVay.NgayVay;
            }
        }

        public decimal TienLaiDaDong
        {
            get
            {
                decimal Sum = 0;

                if (ThongTinDongLai != null && ThongTinDongLai.DanhSachChiTietDongTienLai != null && ThongTinDongLai.DanhSachChiTietDongTienLai.Count > 0)
                {
                    foreach (var item in ThongTinDongLai.DanhSachChiTietDongTienLai)
                    {
                        if (item.DaDong == 1)
                            Sum += item.TongTienTra;
                    }
                }
                return Sum;
            }
        }
    }
}
