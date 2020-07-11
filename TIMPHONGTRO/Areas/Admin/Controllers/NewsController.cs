using MODEL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TIMPHONGTRO.Areas.Admin.Controllers
{
    public class NewsController : AuthController
    {
        // GET: Admin/News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Images()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(string fullname, string sex, int status, int page, int pageSize)
        {
            var result = new NewsDAO().GetAllPaging(fullname, sex, status, page, pageSize);
            return Json(new
            {
                totalRow = result.TotalRecord,
                data = result.Items
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult UpdateStatus(int newid, int status)
        {
            var result = new NewsDAO().UpdateStatus(newid, status);
            return Json(new
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDetail(int newid)
        {
            var result = new NewsDAO().GetDetail(newid);
            return Json(new
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadImages(int newid)
        {
            var images = new NewsDAO().GetImagesByNewId(newid);
            var status = new NewsDAO().GetStatusByNewId(newid);
            return Json(new
            {
                data = images,
                status = status
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteImage(string picture)
        {
            var result = new NewsDAO().DeleteImage(picture);
            return Json(new
            {
                data = result
            });
        }
    }
}