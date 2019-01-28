using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIT.Datas.Models
{
    public class DanhMucTaiSanModel
    {
        public string MaTaiSan { get; set; }
        public string TaiSan { get; set; }
    }

    public class DanhMucHinhThucLainModel
    {
        public string ID { get; set; }
        public string HinhThucLai { get; set; }
    }

    public class DanhMucNganangModel
    {
        public string ID { get; set; }
        public string NganHang { get; set; }
    }
}
