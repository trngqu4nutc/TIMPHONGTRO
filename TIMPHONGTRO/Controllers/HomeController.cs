using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TIMPHONGTRO.Models.DAO;
using TIMPHONGTRO.Models.DTO;

namespace TIMPHONGTRO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new NewsDao().GetAll());
        }

        [HttpGet]
        public JsonResult LoadData(string model, int page = 1, int pageSize = 2)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var filterDTO = serializer.Deserialize<FilterDTO>(model);
            var newsDTOs = new NewsDao().FilterNews(filterDTO).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int totalRow = newsDTOs.Count();
            return Json(new
            {
                data = newsDTOs,
                total = totalRow
            }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult SearchBar()
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult LoadQuan()
        {
            var lstQuan = new QuanDuongDao().GetDistricts();
            return Json(new
            {
                data = lstQuan,
                total = lstQuan.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadDuong(int DistrictId)
        {
            var lstDuong = new QuanDuongDao().GetStreets(DistrictId);
            return Json(new
            {
                data = lstDuong,
                total = lstDuong.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadLoai()
        {
            var lstLoai = new NewsDao().GetCategories();
            return Json(new
            {
                data = lstLoai,
                total = lstLoai.Count()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}