using MODEL.DAO;
using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TIMPHONGTRO.Common;

namespace TIMPHONGTRO.Controllers
{
    public class NewsController : AuthUserController
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadNews(int newid)
        {
            var result = new NewsDAO().FindById(newid);
            return Json(new
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadProvincial()
        {
            var provincials = new NewsDAO().GetProvincials();
            return Json(new
            {
                data = provincials
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadDistrict(int keyLoad)
        {
            var districts = new NewsDAO().GetDistricts(keyLoad);
            return Json(new
            {
                data = districts
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadWard(int keyLoad)
        {
            var newsDAO = new NewsDAO();
            var wards = newsDAO.GetWards(keyLoad);
            var streets = newsDAO.GetStreets(keyLoad);
            return Json(new
            {
                wards = wards,
                streets = streets
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveNews(string news)
        {
            var serializer = new JavaScriptSerializer();
            var newDTO = serializer.Deserialize<NewDTO>(news);
            for (int i=0; i<newDTO.baseImages.Count; i++)
            {
                byte[] bytes = Convert.FromBase64String(newDTO.baseImages[i]);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                string generateFileName = GenerateName.doGenerate(((AccountDTO)Session[Constants.USER_SESSION]).PhoneNum);
                var fullPath = Path.Combine(Server.MapPath("~/Public/img/"), generateFileName);
                try
                {
                    new Bitmap(image).Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    newDTO.baseImages[i] = "~/Public/img/" + generateFileName;
                }
                catch
                {
                    return Json(new
                    {
                        data = false
                    });
                }
            }
            var check = newDTO.newId > 0 ? new NewsDAO().UpdateNew(newDTO) : new NewsDAO().AddNew(newDTO);
            return Json(new
            {
                data = check
            });
        }
    }
}