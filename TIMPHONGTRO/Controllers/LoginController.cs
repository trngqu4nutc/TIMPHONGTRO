using MODEL.DAO;
using MODEL.DTO;
using MODEL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TIMPHONGTRO.Common;

namespace TIMPHONGTRO.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Signin(string account)
        {
            var accountDTO = new JavaScriptSerializer().Deserialize<AccountDTO>(account);
            var accountDAO = new AccountDAO();
            var result = accountDAO.Login(accountDTO.PhoneNum, accountDTO.Password);
            if(result == 1)
            {
                accountDTO = accountDAO.FindByPhoneNumber(accountDTO.PhoneNum);
                Session.Add(Constants.USER_SESSION, accountDTO);
            }
            return Json(new
            {
                result = result,
                role = accountDTO.RoleId
            });
        }
    }
}