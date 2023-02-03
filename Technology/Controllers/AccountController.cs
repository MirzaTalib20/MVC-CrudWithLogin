using Dapper;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Technology.DapperRepository;
using Technology.Models;

namespace Technology.Controllers
{
    public class AccountController : Controller
    {
        public AccountModel RoleId { get; private set; }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountModel acc)
        {
            DynamicParameters param = new DynamicParameters();
            var a = DapperORM.ReturnList<AccountModel>("sp_View", param).ToList();
            var result = a.Where(x => x.username == acc.username).FirstOrDefault().RoleId;

            if (result == "Admin")
            {
               
               
                FormsAuthentication.SetAuthCookie(acc.username, true);
                return RedirectToAction("Index", "Appear");
            }
            else if(result == "Analyst")
            {
                Session["UserName"] = acc.username;
                FormsAuthentication.SetAuthCookie(acc.username, true);
                return RedirectToAction("Profile", "Appear");
            }
            else if(result == "Production")
            {
                Session["UserName"] = acc.username;
                FormsAuthentication.SetAuthCookie(acc.username, true);
                return RedirectToAction("Profile", "Appear");
            }
            else if (result == "Editor")
            {
                Session["UserName"] = acc.username;
                FormsAuthentication.SetAuthCookie(acc.username, true);
                return RedirectToAction("Profile", "Appear");
            }
            else if (result == "IRC")
            {
                Session["UserName"] = acc.username;
                FormsAuthentication.SetAuthCookie(acc.username, true);
                return RedirectToAction("Profile", "Appear");
            }
            else if (result == "SA")
            {
                Session["UserName"] = acc.username;
                FormsAuthentication.SetAuthCookie(acc.username, true);
                return RedirectToAction("Profile", "Appear");
            }
            else if(result == "Publisher")
            {
                Session["UserName"] = acc.username;
                FormsAuthentication.SetAuthCookie(acc.username, true);
                return RedirectToAction("Profile", "Appear");
            }
            TempData["msg"] = "Incorrect UserName and Password";
            return RedirectToAction("Login");
        }
        

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }




        [HttpPost]
        public ActionResult Register(AccountModel acc)
        {
            DynamicParameters param = new DynamicParameters();
            if (acc != null)
            {

                param.Add("@Id", acc.Id);
                param.Add("@firstName", acc.firstName);
                param.Add("@lastName", acc.lastName);
                param.Add("@Gender", acc.Gender);
                param.Add("@age", acc.age);
                param.Add("@email", acc.email);
                param.Add("@username", acc.username);
                param.Add("@password", acc.password);
                param.Add("@confirm_password", acc.confirm_password);
                param.Add("@Status", 1);
                param.Add("@RoleId", acc.RoleId);
                DapperORM.ExecuteWithoutReturn("sp_add", param);
            }
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }



        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
            {
                return View();
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                var a = DapperORM.ReturnList<AccountModel>("sp_GetuserDetails", param).FirstOrDefault();
                return View(a);
            }
        }




        [HttpPost]
        public ActionResult Edit(AccountModel emp)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", emp.Id);
            param.Add("@firstName", emp.firstName);
            param.Add("@lastName", emp.lastName);
            param.Add("@Gender", emp.Gender);
            param.Add("@age", emp.age);
            param.Add("@email", emp.email);
            param.Add("@username", emp.username);
            param.Add("@password", emp.password);
            param.Add("@confirm_password", emp.confirm_password);
            param.Add("@Status", emp.Status);
            param.Add("@RoleId", emp.RoleId);
            DapperORM.ExecuteWithoutReturn("sp_Edit", param);
            return RedirectToAction("Index", "Appear");
        }

        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", id);

            DapperORM.ExecuteWithoutReturn("sp_Delete", param);
            return RedirectToAction("Index", "Appear");
        }


        public ActionResult ViewDelete()
        {

            return View(DapperORM.ReturnList<AccountModel>("sp_View_Delete"));
        }

        public ActionResult Restore(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            DapperORM.ExecuteWithoutReturn("sp_restore", param);
            return RedirectToAction("Index", "Appear");
        }

    }

}





  
