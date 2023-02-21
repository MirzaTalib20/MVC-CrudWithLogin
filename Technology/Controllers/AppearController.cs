using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Technology.DapperRepository;
using Technology.Models;

namespace Technology.Controllers
{
    public class AppearController : Controller
    {
        // GET: Appear
        [Authorize]
        public ActionResult Index()
        {

            DynamicParameters param = new DynamicParameters();
            var a = DapperORM.ReturnList<AccountModel>("sp_View", param).ToList();

            try
            {
                if (User.Identity.Name == null)
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }



            return View(a);
        }
        [Authorize]
        public ActionResult Profile()
        {
            DynamicParameters param = new DynamicParameters();
            var a = DapperORM.ReturnList<AccountModel>("sp_View", param).ToList();

            try
            {
                if (User.Identity.Name == null)
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }



            return View(a);
        }


        [Authorize]
        public ActionResult Editor()
        {

            DynamicParameters param = new DynamicParameters();
            var a = DapperORM.ReturnList<AccountModel>("sp_View", param).ToList();

            try
            {
                if (User.Identity.Name == null)
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }



            return View(a);
        }




       
    }
}