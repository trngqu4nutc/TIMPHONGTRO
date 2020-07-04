using MODEL.DAO;
using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TIMPHONGTRO.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Signup(string account)
        {
            var accountDTO = new JavaScriptSerializer().Deserialize<AccountDTO>(account);
            var check = new AccountDAO().Register(accountDTO);
            return Json(new
            {
                result = check
            });
        }
    }
}