using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIMPHONGTRO.Models.DAO;

namespace TIMPHONGTRO.Controllers
{
    public class DetailNewsController : Controller
    {
        // GET: AllNews
        public ActionResult Index(int id)
        {
            return View(new NewsDao().GetById(id));
        }
    }
}