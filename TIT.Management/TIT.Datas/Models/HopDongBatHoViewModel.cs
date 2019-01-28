using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIT.Datas.Helper;

namespace TIT.Datas.Models
{
    public class HopDongBatHoGridViewModel
    {
        [Display(Name = "Mã HĐ")]
        public string Id_HopDong { get; set; }

        public KhachHangModel KhachHang { get; set; }
        [Display(Name = "Tiền giao khách")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienGiaoKhach { get; set; }
        [Display(Name = "Bát họ")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal BatHo { get; set; }

        [Display(Name = "Tỷ lệ")]
        [DisplayFormat(DataFormatString = "{0: n1}")]
        public double TiLe { get {
                if (BatHo == 0)
                    return 0;
                return (double)TienGiaoKhach*10 / (double)BatHo;
            } }

        [Display(Name = "Ngày bốc")]
        public DateTime NgayBoc { get; set; }

        [Display(Name = "Nợ cũ")]
        public decimal NoCu { get; set; }

        [Display(Name = "Bốc trong vòng")]
        public int BocTrongVong { get; set; }
        [Display(Name = "Số ngày đóng tiền")]
        public int SoNgayDongTien { get; set; }

        [Display(Name = "Tiền một ngày")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienMotNgay { get {
                return HinhThucLaiHelper.TinhLaiMotNgayBatHo(BatHo, BocTrongVong);
            } }

        [Display(Name = "Tiền đã đóng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienDaDong {
            get {
                if (NgayDongTienCuoiCung == null || NgayDongTienCuoiCung == NgayBoc || NgayDongTienCuoiCung == DateTime.MinValue)
                    return 0;
                return (decimal)(NgayDongTienCuoiCung.Date.Subtract(NgayBoc.Date).TotalDays + 1) * TienMotNgay;
            }
        }

        [Display(Name = "Còn phải đóng")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal ConPhaiDong { get {
                return BatHo - TienDaDong;
            } }
        public string TrangThai {
            get {
                if (TinhTrang == "Thanh Lý")
                    return "Thanh lý";

                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongTien.Date).TotalDays;
                if (timeDiff < 0)
                    return "Đang vay";
                else if (timeDiff < 1)
                    return "Hôm nay";
                else if (timeDiff < 3)
                    return "Chậm họ";
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

                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongTien.Date).TotalDays;
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
        public DateTime NgayDongTienCuoiCung { get; set; }

        [Display(Name = "Ngày phải đóng tiền")]
        public DateTime NgayPhaiDongTien { get {
                if (NgayDongTienCuoiCung == null || NgayDongTienCuoiCung == NgayBoc)
                    return NgayBoc;
                return NgayDongTienCuoiCung.AddDays(1);
            } }

        public string TinhTrang { get; set; }

    }
    public class HopDongBatHoDataModel
    {
        [Key]
        [Display(Name = "Mã HĐ")]
        public string HD_BatHo_Id { get; set; }
       
        [Display(Name = "Bát Họ")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal BatHo { get; set; }
        [Display(Name = "Tiền Đưa Cho Khách")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienDuaChoKhach { get; set; }
        [Display(Name = "Bốc Trong Vòng")]
        public int BocTrongVong { get; set; }
        [Display(Name = "Số Ngày Đóng Tiền")]
        public int SoNgayDongTien { get; set; }
        [Display(Name = "Ngày Bốc")]
        public DateTime NgayBoc { get; set; }
        [Display(Name = "Thu Họ Trước")]
        public Boolean ThuHoTruoc { get; set; }
        [Display(Name = "Ghi Chú")]
        public string GhiChu { get; set; }
        [Display(Name = "CuaHang_Id")]
        public int CuaHang_Id { get; set; }
        [Display(Name = "NhanVien_Id")]
        public string NhanVien_Id { get; set; }
        [Display(Name = "Ngày Tạo")]
        public DateTime NgayTao { get; set; }

        public KhachHangModel KhachHang { get; set; }

        public decimal TienMotNgay
        {
            get
            {
                return HinhThucLaiHelper.TinhLaiMotNgayBatHo(BatHo, BocTrongVong);
            }
        }
        public decimal TienDaDong
        {
            get
            {
                if (NgayDongTienCuoiCung == null || NgayDongTienCuoiCung == NgayBoc || NgayDongTienCuoiCung == DateTime.MinValue)
                    return 0;
                return (decimal)(NgayDongTienCuoiCung.Date.Subtract(NgayBoc.Date).TotalDays + 1) * TienMotNgay;
            }
        }
        public decimal ConPhaiDong
        {
            get
            {
                return BatHo - TienDaDong;
            }
        }
        public string TinhTrang { get; set; }
        public string TrangThai
        {
            get
            {
                if (TinhTrang == "Thanh Lý")
                    return "Thanh lý";

                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongTien.Date).TotalDays;
                if (timeDiff < 0)
                    return "Đang vay";
                else if (timeDiff < 1)
                    return "Hôm nay";
                else if (timeDiff < 3)
                    return "Chậm họ";
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

                var timeDiff = DateTime.Now.Date.Subtract(NgayPhaiDongTien.Date).TotalDays;
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
      
        public DateTime NgayDongTienCuoiCung { get; set; }
        public DateTime NgayPhaiDongTien
        {
            get
            {
                if (NgayDongTienCuoiCung == null || NgayDongTienCuoiCung == NgayBoc)
                    return NgayBoc;
                return NgayDongTienCuoiCung.AddDays(1);
            }
        }

    }
    public class BatHo_ThongTinThanhLyModel
    {
        public DateTime NgayThanhLy { get; set; }
        public int SoKyConLaiPhaiDong { get; set; }
        public decimal SoTienConLaiPhaiDong { get; set; }
       

    }
    public class BatHo_ThongTinDongLaiModel
    {
        [Display(Name = "Ngày Họ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayHo { get; set; }
        
        [Display(Name = "Tiền Họ")]
        // [DisplayFormat(DataFormatString = "{0:0,0 vnđ}")]
        public decimal TienHo { get; set; }
        public List<BatHo_ThongTinChiTietDongTienLaiModel> DanhSachChiTietDongTienLai { get; set; }
    }
    public class BatHo_ThongTinChiTietDongTienLaiModel
    {
        public int ID { get; set; }
        [Display(Name = "Ngày Họ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayHo { get; set; }
        public DateTime NgayKetThuc { get; set; }
        [Display(Name = "Ngày GD")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayGiaoDich { get; set; }
        [Display(Name = "Tiền Họ")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienHo { get; set; }
        [Display(Name = "Nhân viên")]
        public string TenNhanVien { get; set; }
        public string MaNhanVien { get; set; }
        public int DaDong { get; set; }
    }
    public class BatHo_ThongTinThayDoiSoTienHopDongModel
    {
        public const int VAY_THEM = 1;
        public const int TRA_BOT_GOC = 2;
        public DateTime NgayThucHien { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTien { get; set; }
        public int Loai { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
    }

    public class BatHo_CapNhatThongTinDongTienViewModel
    {
        public HopDongBatHoDataModel HopDongBatHo { get; set; }
        public BatHo_ThongTinThanhLyModel ThongTinThanhLy { get; set; }
        public BatHo_ThongTinDongLaiModel ThongTinDongLai { get; set; }
        public BatHo_ThongTinThayDoiSoTienHopDongModel ThongTinThayDoiSoTienHopDong { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayCuoiCungDongLai
        {
            get
            {
                return HopDongBatHo.NgayDongTienCuoiCung;
            }
        }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TienLaiDaDong
        {
            get
            {
                return HopDongBatHo.TienDaDong;
            }
        }
    }

}
