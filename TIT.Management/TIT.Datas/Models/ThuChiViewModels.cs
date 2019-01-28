using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIT.Datas.Models
{
    public class ThuChiGridViewModel
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

        [Display(Name = "Số tiền")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal SoTien { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
        [Display(Name = "Cửa hàng")]
        public CuaHangDataModel CuaHang { get; set; }
    }

    public class ThuChiDataModel
    {
        [Key]
        public string ID_ThuChi { get; set; }

        [Display(Name = "Cửa hàng")]
        public int MaCuaHang { get; set; }

        public string MaNhanVien { get; set; }

        [Display(Name = "Khách hàng")]
        public string KhachHang { get; set; }

        [Display(Name = "Số tiền")]
        public int SoTien { get; set; }

        [Display(Name = "Loại phiếu")]
        public string LoaiPhieu { get; set; }

        [Display(Name = "Lý do")]
        public string GhiChu { get; set; }
    }
}
