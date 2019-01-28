using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Device.Location;

namespace TIT.Datas.Models
{

    public class ReportChiPhiHoatDongGridViewModel
    {
        [Display(Name = "STT")]
        public int STT { get; set; }
        [Display(Name = "Ngày")]
        public DateTime Ngay { get; set; }
        [Display(Name = "Nhân viên")]
        public string NhanVien { get; set; }
        [Display(Name = "Khách hàng")]
        public string KhachHang { get; set; }
        [Display(Name = "Loại phiếu")]
        public string LoaiPhieu { get; set; }

        public int PhieuThu { get; set; }

        [Display(Name = "Số tiền Thu")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTienThu
        {
            get
            {
                if (PhieuThu == 0)
                    return SoTien;
                else
                    return 0;
            }
        }

        [Display(Name = "Số tiền Chi")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTienChi
        {
            get
            {
                if (PhieuThu == 1)
                    return SoTien;
                else
                    return 0;
            }
        }

        public decimal SoTien { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
        [Display(Name = "Cửa hàng")]
        public CuaHangDataModel CuaHang { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }

    public class ReportTongQuyTienMatGridViewModel
    {
        [Display(Name = "Số Tiền Vốn Đầu Ngày")]
        public decimal SoTienVonDauNgay { get; set; }

        [Display(Name = "Số Tiền Vốn Còn Lại")]
        public decimal SoTienVonConLai { get; set; }

        [Display(Name = "Tổng Thu")]
        public decimal TongThu { get; set; }

        [Display(Name = "Tổng Chi")]
        public decimal TongChi { get; set; }


       // [Display(Name = "Cửa hàng")]
       // public CuaHangDataModel CuaHang { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

    }
    public class ReportSoQuyTienMatGridViewModel
    {
        [Display(Name = "Ngày")]
        public DateTime Ngay { get; set; }
        [Display(Name = "Nhân Viên")]
        public string NhanVien { get; set; }
        [Display(Name = "Mã Hợp Đồng")]
        public string MaHD { get; set; }

        public KhachHang KhachHang { get; set; }
        [Display(Name = "Loại Giao Dịch")]
        public string LoaiGiaoDich { get; set; }
        
        public decimal SoTien { get; set; }
        public int ThuTien { get; set; }

        [Display(Name = "Cho Vay")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public Decimal ChoVay { get
            {
                if (ThuTien == 0)
                    return SoTien;
                else
                    return 0;
            }
        }
        [Display(Name = "Thu Về")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public Decimal ThuVe {
            get
            {
                if (ThuTien == 1)
                    return SoTien;
                else
                    return 0;
            }
        }
        [Display(Name = "Tài Sản")]
        public string TaiSan { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        [Display(Name = "Cửa hàng")]
        public int MaCuaHang { get; set; }
    }
    public class ReportHopDongChamDongTienGridViewModel
    {
        [Display(Name = "Mã HĐ")]
        public string MaHD { get; set; }

        public KhachHangModel KhachHang { get; set; }
        [Display(Name = "Tài Sản")]
        public string TaiSan { get; set; }
        [Display(Name = "Tiền Gốc")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTienGoc { get; set; }
        [Display(Name = "Số Tiền Nợ")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal LaiDen { get; set; }

        [Display(Name = "Ngày Vay")]
        public DateTime NgayVay { get; set; }

        [Display(Name = "Ngày Phải Đóng Tiền")]
        public DateTime NgayPhaiDongTien { get; set; }

        public string VND { get; set; }

        [Display(Name = "Loại Hình")]
        public string LoaiHinh { get; set; }

        public int MaCuaHang { get; set; }

        public string TrangThai { get; set; }
        public string MauTinhTrang { get; set; }


    }
    public class ReportStopModel
    {
        public int STT { get; set; }
        public int DeviceId { get; set; }
        [Display(Name = "Tên thiết bị")]
        public string DeviceName { get; set; }
        [Display(Name = "Vĩ độ (Lat)")]
        public double Latitude { get; set; }
        [Display(Name = "Kinh độ (Lng)")]
        public double Longitude { get; set; }
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        public double AvgSpeed { get; set; }
        [Display(Name = "Từ lúc")]
        public DateTime FromTime { get; set; }
        [Display(Name = "Đến lúc")]
        public DateTime ToTime { get; set; }
        [Display(Name = "Tổng TG (giây)")]
        public double TotalSeconds { get; set; }
        [Display(Name = "Pin (%)")]
        public double Battery { get; set; }
        [Display(Name = "Điểm gần nhất")]
        public string StationNearest { get; set; }
        [Display(Name = "Khoảng cách (m)")]
        public int DistanceToStation  { get; set; }
    }

    public class ReportTransactionModel
    {
        [Key]
        public decimal Id { get; set; }
        [Display(Name = "Khu vực")]
        public int ProvinceId { get; set; }

        [Display(Name = "NV giao dịch")]
        public int DeviceId { get; set; }
        [Display(Name = "Điểm bán hàng")]
        public int StationId { get; set; }
        [Display(Name = "Sản phẩm (SKU)")]
        public int ProductId { get; set; }
        [Display(Name = "Số lượng bán")]
        public int Quantity { get; set; }
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }
        [Display(Name = "Khuyến mãi")]
        public int PromotionId { get; set; }
        [Display(Name = "Số lượng (KM)")]
        public int PromotionQuantity { get; set; }
      
        [Display(Name = "Số lượng tồn")]
        public int InventoryQuantity { get; set; }
        [Display(Name = "Giao dịch lúc")]
        public DateTime SellTime { get; set; }
        [Display(Name = "Vĩ độ (Lat)")]
        public double Latitude { get; set; }
        [Display(Name = "Kinh độ (Lng)")]
        public double Longitude { get; set; }
        [Display(Name = "Vị trí giao dịch (Lat,Lng)")]
        public string Location
        {
            get{ return Latitude.ToString()+","+Longitude.ToString(); }
        }
        public double StationLatitude { get; set; }
        [Display(Name = "Kinh độ (Lng)")]
        public double StationLongitude { get; set; }
        [Display(Name = "Vị trí điểm bán hàng (Lat,Lng)")]
        public string StationLocation
        {
            get { return StationLatitude.ToString() + "," + StationLongitude.ToString(); }
        }
        [Display(Name = "Khoảng cách (m)")]
        public double Distance
        {
            get { return Math.Round(GetDistance(Latitude, Longitude, StationLatitude, StationLongitude)); }
        }
        [Display(Name = "Số dòng SP (SKU)")]
        public int Success
        {
            get
            {
                if (Quantity > 0)
                    return 1;
                else
                    return 0;
            }
        }
        public string CreatedBy { get; set; }

        private double GetDistance(double sLatitude, double sLongitude, double eLatitude, double eLongitude)
        {
            if (sLatitude == 0 || sLongitude == 0 || eLatitude == 0 || eLongitude == 0)
                return 9999;
            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(eLatitude, eLongitude);

            return sCoord.GetDistanceTo(eCoord);
        }


    }

    public class ReportTransactionCompetitorModel
    {
        [Key]
        public decimal Id { get; set; }
        [Display(Name = "Khu vực")]
        public int ProvinceId { get; set; }
        [Display(Name = "NV giao dịch")]
        public int DeviceId { get; set; }
        [Display(Name = "Điểm bán hàng")]
        public int StationId { get; set; }
        [Display(Name = "Sản phẩm")]
        public int ProductId { get; set; }        
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }        
        [Display(Name = "Số lượng tồn")]
        public int InventoryQuantity { get; set; }
        [Display(Name = "Thăm viếng lúc")]
        public DateTime VisitTime { get; set; }
        [Display(Name = "Vĩ độ (Lat)")]
        public double Latitude { get; set; }
        [Display(Name = "Kinh độ (Lng)")]
        public double Longitude { get; set; }
        [Display(Name = "Vị trí thăm viếng (Lat,Lng)")]
        public string Location
        {
            get { return Latitude.ToString() + "," + Longitude.ToString(); }
        }
        public double StationLatitude { get; set; }
        [Display(Name = "Kinh độ (Lng)")]
        public double StationLongitude { get; set; }
        [Display(Name = "Vị trí điểm bán hàng (Lat,Lng)")]
        public string StationLocation
        {
            get { return StationLatitude.ToString() + "," + StationLongitude.ToString(); }
        }
        public string CreatedBy { get; set; }
        private double GetDistance(double sLatitude, double sLongitude, double eLatitude, double eLongitude)
        {
            if (sLatitude == 0 || sLongitude == 0 || eLatitude == 0 || eLongitude == 0)
                return 9999;
            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(eLatitude, eLongitude);

            return sCoord.GetDistanceTo(eCoord);
        }


    }

    public class ReportEmployeeModel
    {
        [Display(Name = "NV giao dịch")]
        public int DeviceId { get; set; }
      
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }
        [Display(Name = "Xuất kho lúc")]
        public DateTime ExportAt { get; set; }

        [Display(Name = "GD đầu")]
        public DateTime StartTime { get; set; }
        [Display(Name = "GD cuối")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Xuất kho")]
        public string ProductExport { get; set; }
        [Display(Name = "Đã bán")]
        public string ProductSell { get; set; }
        [Display(Name = "Số điểm đi được")]
        public int TotalStation { get; set; }
        [Display(Name = "Quãng đường đi được (km)")]
        public double TotalDistance { get; set; }
    }

    public class ChartTransactionModel
    {
        public string Month { get; set; }
        public int Year { get; set; }
        public int TotalTransaction { get; set; }
        public int TotalSuccessTransaction { get; set; }

    }

    public class ChartProductTransactionModel
    {
        public string Month { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int DepositQuantity { get; set; }

    }

    public class CustomerAnalyzeViewModel
    {
        [Key]
        public int StationId { get; set; }
        [Display(Name = "Khu vực")]
        [Required]
        public int ProvinceId { get; set; }
        [Display(Name = "Mã KH")]
        public string StationCode { get; set; }
        [Display(Name = "Tên KH")]
        [Required]
        public string StationName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
      
        [Display(Name = "Số ĐT")]
        [Required]
        public string Phone { get; set; }
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }

        [Display(Name = "Tạo lúc")]
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Cập nhật lúc")]
        public DateTime UpdatedTime { get; set; }
        [Display(Name = "Cập nhật bởi")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Quản lý bởi")]
        public int DeviceId { get; set; }
        [Display(Name = "Ngày")]
        public string Day { get; set; }

        [Display(Name = "Phân loại")]
        public string Grade { get; set; }

        [Display(Name = "Loại hình")]
        public string StationType { get; set; }

        [Display(Name = "Tần suất")]
        public string Frequency { get; set; }
        [Display(Name = "Cấp phân phối")]
        public string ImportSource { get; set; }

        public int? TotalTransaction3M { get; set; }
        public int? TotalTransaction2M { get; set; }
        public int? TotalTransaction1M { get; set; }
        public DateTime AnalyzeTime { get; set; }


        [Display(Name = "Đánh giá")]
        public string Analyze
        {
            get
            {
                if (CreatedTime> AnalyzeTime)
                    return "Mở mới";
                else if (TotalTransaction3M > 0 && TotalTransaction2M > 0 && TotalTransaction1M > 0)
                    return "Năng động";
                else if (TotalTransaction3M == 0 && TotalTransaction2M == 0 && TotalTransaction1M == 0)
                    return "Bị mất";
                else if (TotalTransaction3M == 0 && TotalTransaction2M == 0 && TotalTransaction1M > 0)
                    return "Mua lại";
                
                return "Bán chậm";
            }
        }


    }

    public class ReportCustomerModel
    {
        public int DeviceId { get; set; }
        public int STT { get; set; }
        [Display(Name = "Nhân viên")]
        public string DriverName { get; set; }

        [Display(Name = "Tổng KH hiện có")]
        public int TotalCustomer { get; set; }
        [Display(Name = "KH năng động (2)")]
        public int ActiveCustomer { get; set; }
        [Display(Name = "Tỉ lệ (%)")]
        public string PercentActiveCustomer
        {
            get
            {
                if (TotalCustomer == 0)
                    return "n/a";
                double _kq = (ActiveCustomer * 100 / TotalCustomer);
                return _kq.ToString();
            }
        }
        [Display(Name = "Chỉ tiêu")]
        public int CustomerGoal { get; set; }
        [Display(Name = "KQ thực hiện (%)")]
        public string ResultActiveCustomer
        {
            get
            {
                if (CustomerGoal == 0)
                    return "n/a";
                double _kq = (ActiveCustomer * 100 / CustomerGoal);
                return _kq.ToString();
            }
        }


        [Display(Name = "KH bị mất")]
        public int DeactiveCustomer { get; set; }
        [Display(Name = "Tỉ lệ (%)")]
        public string PercentDeactiveCustomer
        {
            get
            {
                if (TotalCustomer == 0)
                    return "n/a";
                double _kq = (DeactiveCustomer * 100 / TotalCustomer);
                return _kq.ToString();
            }
        }

        [Display(Name = "KH mới")]
        public int NewCustomer { get; set; }
        [Display(Name = "Tỉ lệ (%)")]
        public string PercentNewCustomer
        {
            get
            {
                if (TotalCustomer == 0)
                    return "n/a";
                double _kq = (NewCustomer * 100 / TotalCustomer);
                return _kq.ToString();
                    //((NewCustomer / TotalCustomer) * 100).ToString() + "%";
            }
        }
        [Display(Name = "Chỉ tiêu")]
        public int NewCustomerGoal { get; set; }
        [Display(Name = "KQ thực hiện (%)")]
        public string ResultNewCustomer
        {
            get
            {
                if (NewCustomerGoal == 0)
                    return "n/a";
                double _kq = (NewCustomer * 100 / NewCustomerGoal);
                return _kq.ToString();
            }
        }


    }
    public class ReportSalesEffectivenessModel
    {
        public int DeviceId { get; set; }
        public int STT { get; set; }
        [Display(Name = "Nhân viên")]
        public string DriverName { get; set; }
        [Display(Name = "Khu vực")]
        public int ProvinceId { get; set; }

        [Display(Name = "Tổng KH hiện có")]
        public int TotalCustomer { get; set; }
        [Display(Name = "Tổng số lần thăm viếng")]
        public int TotalVisit { get; set; }
        [Display(Name = "Tổng số đơn hàng")]
        public int TotalTransaction { get; set; }
        [Display(Name = "Hiệu quả bán hàng")]
        public string SalesEffectiveness
        {
            get
            {
                if (TotalVisit == 0)
                    return "n/a";
                return 
                    ((TotalTransaction*100)/TotalVisit).ToString();
            }
        }
        [Display(Name = "Chỉ tiêu hiệu quả bán hàng")]
        public double SalesEffectivenessGoal { get; set; }

        [Display(Name = "Kết quả thực hiện chỉ tiêu hiệu quả bán hàng")]
        public string SalesEffectivenessResult
        {
            get
            {
                if (SalesEffectivenessGoal == 0|| TotalVisit==0)
                    return "n/a";
                return
                    Math.Round(((TotalTransaction * 100) / TotalVisit)/SalesEffectivenessGoal,2).ToString();
            }
        }

    }

    public class BaoCaoTongHop
    {
        public DateTime Ngay { get; set; }
        public string Id_HopDong { get; set; }
        public string NoiDung { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal Thu { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal Chi { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TongCongTon { get; set; }
        public string TenNhanVien { get; set; }
        public string TenKhachHang { get; set; }
    }

}