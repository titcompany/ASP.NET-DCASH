using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIT.Datas.Models
{
    public class HopDongVayDNGDGridViewModel
    {
        [Display(Name = "Mã HĐ")]
        public string Id_HopDong { get; set; }
        public KhachHangModel KhachHang { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        [Display(Name = "Tiền vay")]
        public decimal SoTien { get; set; }

        [DisplayFormat(DataFormatString = "{0: n1}")]
        [Display(Name = "Hoa hồng")]
        public double HoaHong{ get;set; }

        [Display(Name = "Khách thực nhận")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public double KhachThucNhan
        {
            get
            {
                return Math.Round((double)SoTien * (100-HoaHong) / 100, 0);
            }
        }
        [DisplayFormat(DataFormatString = "{0: n1}")]
        [Display(Name = "Lãi suất")]
        public double LaiSuat { get; set; }
        [Display(Name = "Ngày vay")]
        public DateTime NgayVay { get; set; }
        //Month
        [Display(Name = "Thời hạn vay")]
        public int ThoiHanVay { get; set; }
        public string TrangThai
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return "Thanh Lý";
                if (TinhTrang == "Hủy hợp đồng")
                    return "Hủy hợp đồng";
                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongTien.Date).TotalDays;
                if (timeDiff < 0)
                    return "Đang vay";
                else if (timeDiff < 1)
                    return "Hôm nay";
                else if (timeDiff < 3)
                    return "Chậm lãi";
                else
                    return "Trễ hạn";
            }
        }

        public DateTime NgayHen { get; set; }
        public string NoiDungHen { get; set; }
        public bool IsHenGio { get; set; }


        public DateTime NgayBatDauThanhToan { get; set; }
        public DateTime NgayKetThucThanhToan { get { return NgayBatDauThanhToan.AddMonths(ThoiHanVay - 1); } }
        public string MauTinhTrang
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return "grey";
                if (TinhTrang == "Hủy hợp đồng")
                    return "grey";
                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongTien).TotalDays;
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
        [Display(Name = "Ngày đóng tiền cuối cùng")]
        public DateTime NgayDongTienCuoiCung { get; set; }
        [Display(Name = "Ngày phải đóng tiền")]
        public DateTime NgayPhaiDongTien
        {
            get
            {
                DateTime _NgayPhaiDongTien = DateTime.MinValue;
                if (DaDong < LaiGocMotThang)
                    _NgayPhaiDongTien = NgayBatDauThanhToan;
                else
                    _NgayPhaiDongTien = NgayDongTienCuoiCung.AddMonths(1);
                return _NgayPhaiDongTien;
            }
        }
        [Display(Name = "Tình trạng")]
        public string TinhTrang { get; set; }
        [Display(Name = "Hoa hồng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal PhiDichVu { get {
                return Math.Round((SoTien * (decimal)(HoaHong) / 100) / 1000, 0) * 1000;
            } }

        [Display(Name = "Đã đóng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal DaDong { get; set; }
        [Display(Name = "Phải đóng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal PhaiDong { get {
                return LaiGocMotThang * ThoiHanVay - DaDong;
            } }

        [Display(Name = "Mỗi tháng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal LaiGocMotThang
        {
            get
            {
                return Math.Round((SoTien * (decimal)(LaiSuat) / 100 + SoTien / ThoiHanVay) / 1000, 0) * 1000;
            }
        }

        [Display(Name = "Cửa hàng")]
        public string CuaHang { get; set; }
    }

    public class HopDongChoVayDNGDDataModel
    {

        [Key]
        [Display(Name = "Mã hợp đồng")]
        public string Id_HopDong { get; set; }
        
        [Display(Name = "Số tiền vay")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTienVay { get; set; }

        [Display(Name = "Ngày vay")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayVay { get; set; }

        [Display(Name = "Ngày thanh toán")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayBatDauThanhToan { get; set; }
        
        public DateTime NgayKetThucThanhToan { get {
                return NgayBatDauThanhToan.AddMonths(ThoiHanVay);
            } }

        [Display(Name = "Lãi")]
        public double Lai { get; set; }

        [Display(Name = "Thời hạn vay")]
        public int ThoiHanVay { get; set; }

        [Display(Name = "Tình trạng")]
        public string TinhTrang { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public double HoaHong { get; set; }

        public KhachHangModel KhachHang { get; set; }

        // Khoa ngoai tham chieu den bang Cua Hang
        public int CuaHang_Id { get; set; }

        // Khoa ngoai tham chieu den bang Nhan Vien
        public string NhanVien_Id { get; set; }

        public DateTime NgayDongLaiCuoiCung { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayPhaiDongLai
        {
            get
            {
                DateTime _NgayPhaiDongTien = DateTime.MinValue;
                if (TongSoTienLaiDaDong < LaiGocMotThang)
                    _NgayPhaiDongTien = NgayBatDauThanhToan;
                else
                    _NgayPhaiDongTien = NgayDongLaiCuoiCung.AddMonths(1);
                return _NgayPhaiDongTien;
            }
        }

        public string TrangThai
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return "Thanh Lý";
                if (TinhTrang == "Hủy hợp đồng")
                    return "Hủy hợp đồng";
                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongLai.Date).TotalDays;
                if (timeDiff < 0)
                    return "Đang vay";
                else if (timeDiff < 1)
                    return "Hôm nay";
                else if (timeDiff < 3)
                    return "Chậm lãi";
                else
                    return "Trễ hạn";
            }
        }

        public string MauTinhTrang
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return "grey";
                if (TinhTrang == "Hủy hợp đồng")
                    return "grey";
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

        public decimal LaiGocMotNgay {
            get {
                return Math.Round((SoTienVay / ThoiHanVay + SoTienVay * (decimal)(Lai) / 100) / 30000,0)*1000;
            }
        }

        public decimal LaiMotThang
        {
            get
            {
                return Math.Round((SoTienVay * (decimal)(Lai) / 100) / 1000, 0) * 1000;
            }
        }

        public decimal LaiGocMotThang
        {
            get
            {
                return Math.Round((SoTienVay * (decimal)(Lai) / 100 + SoTienVay/ThoiHanVay) / 1000, 0) * 1000;
            }
        }
        public decimal GocMotThang
        {
            get
            {
                return Math.Round((SoTienVay / ThoiHanVay) / 1000, 0) * 1000;
            }
        }
        
        public ChoVayDNGD_HenGio ThongTinHenGio { get; set; }
        public string UrlHinh1 { get; set; }
        public string UrlHinh2 { get; set; }
        public string UrlHinh3 { get; set; }

        [Display(Name = "Tổng số tiền lãi đã đóng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TongSoTienLaiDaDong { get; set; }
        public decimal LaiThangDau { get
            {
                int daydiff = (int)(NgayBatDauThanhToan.Subtract(NgayVay).TotalDays);
                if (daydiff < 15)
                {
                    return Math.Round(((SoTienVay) * ((decimal)Lai / 3000) * daydiff)/1000, 0)*1000;
                }
                else
                    return 0;
            } }
    }
    public class ChoVayDNGD_ThongTinDongLaiModel
    {
        [Display(Name = "Ngày giao dịch")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgayGiaoDich { get; set; }

        [Display(Name = "Tiền lãi")]
        // [DisplayFormat(DataFormatString = "{0:0,0 vnđ}")]
        public decimal TienLai { get; set; }

        [Display(Name = "Tiền khác")]
        // [DisplayFormat(DataFormatString = "{0:0,0 vnđ}")]
        public decimal TienKhac { get; set; }

        [Display(Name = "Ghi chú")]
        // [DisplayFormat(DataFormatString = "{0:0,0 vnđ}")]
        public string GhiChu { get; set; }

        public List<ChoVayDNGD_ThongTinChiTietDongTienLaiModel> DanhSachChiTietDongTienLai { get; set; }
    }
    public class ChoVayDNGD_ThongTinChiTietDongTienLaiModel
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
        public decimal TongTienTra { get { return SoTienDaDong + TienKhac; } }

        [Display(Name = "Số tiền đã đóng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTienDaDong { get; set; }

        [Display(Name = "Nhân viên")]
        public string TenNhanVien { get; set; }

        public string MaNhanVien { get; set; }

        public int DaDong { get; set; }
    }

    public class ChoVayDNGD_ThongTinThayDoiSoTienHopDongModel
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

    public class ChoVayDNGD_ThongTinThanhLyHopDongModel
    {
        public DateTime NgayThanhLy { get; set; }
        public int Thang { get; set; }
        public decimal TongTienTraTheoDuNoGiamDan { get; set; }
        public decimal TongTienTra { get; set; }
        public decimal TienKhac { get; set; }
        public bool IsDuNoGiamDan { get; set; }
        public string NoiDung { get; set; }
        public string GhiChu { get; set; }
    }

    public class ChoVayDNGD_HenGio
    {
        public DateTime NgayHen { get; set; }
        public string GhiChu { get; set; }
        public bool IsNhacHen { get; set; }
    }

    public class ChoVayDNGD_HuyHopDong
    {
        public string GhiChu { get; set; }
        public decimal SoTienThuLai { get; set; }
    }

    public class ChoVayDNGD_CapNhatThongTinDongTienViewModel
    {
        public HopDongChoVayDNGDDataModel HopDongChoVay { get; set; }
        public ChoVayDNGD_ThongTinThanhLyHopDongModel ThongTinThanhLyHopDongModel { get; set; }
        public ChoVayDNGD_ThongTinDongLaiModel ThongTinDongLai { get; set; }
        public ChoVayDNGD_HuyHopDong ThongTinHuyHopDong { get; set; }
        public List<string> UrlChungTu { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayCuoiCungDongLai
        {
            get
            {
                return HopDongChoVay.NgayDongLaiCuoiCung;
            }
        }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienLaiDaDong
        {
            get
            {
                return HopDongChoVay.TongSoTienLaiDaDong;
            }
        }
    }

    
}
