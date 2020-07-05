using MODEL.DAO;
using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TIMPHONGTRO.Common;

namespace TIMPHONGTRO.Controllers
{
    public class ProfileController : AuthUserController
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UpdatePassword(string data)
        {
            var passwords = new JavaScriptSerializer().Deserialize<List<string>>(data);
            string phoneNumber = ((AccountDTO)Session[Constants.USER_SESSION]).PhoneNum;
            int result = new AccountDAO().UpdatePassword(phoneNumber, passwords);
            return Json(new
            {
                data = result
            });
        }

        [HttpPost]
        public JsonResult UpdateAccount(string data)
        {
            var accountDTO = new JavaScriptSerializer().Deserialize<AccountDTO>(data);
            int result = new AccountDAO().UpdateAccount(accountDTO);
            if(result == 1)
            {
                var userSession = (AccountDTO)Session[Constants.USER_SESSION];
                userSession.Fullname = accountDTO.Fullname;
                userSession.Email = accountDTO.Email;
                Session.Add(Constants.USER_SESSION, userSession);
            }
            return Json(new
            {
                data = result
            });
        }
    }
}