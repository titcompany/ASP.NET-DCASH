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
        public string SoTien { get; set; }
        public string NgayTaoHopDong { get; set; }
        public string LaiDaDong { get; set; }
        public string NoCu { get; set; }
        public string LaiPhiDenHomNay { get; set; }
        public string TinhTrang { get; set; }
        public string NgayPhaiDongLai { get; set; }
    }
}
