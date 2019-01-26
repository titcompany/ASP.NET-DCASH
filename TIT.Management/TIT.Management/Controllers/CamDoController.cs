using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIT.Management.Models;
namespace TIT.Management.Controllers
{
    public class CamDoController : Controller
    {
        // GET: CamDo
        public ActionResult Index()
        {
            List<CamDoGridViewModel> models = new List<CamDoGridViewModel>();
            models.Add(new CamDoGridViewModel() {
                HopDong_Id = "CD-1",
                TenKhachHang = "Trần Văn A",
                SoTien = "100000000"
            });
            models.Add(new CamDoGridViewModel()
            {
                HopDong_Id = "CD-2",
                TenKhachHang = "Trần Văn B",
                SoTien = "10000000"
            });
            return View(models);
        }
    }
}