using Dapper;
using System;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.WebPages;
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
        public ActionResult Login(AccountModel acc )
        {
            DynamicParameters param = new DynamicParameters();
            var a = DapperORM.ReturnList<AccountModel>("sp_View", param).ToList();
            var result = a.Where(x => x.email == acc.email).FirstOrDefault().RoleId;
            var emailString = a.Where(x => x.email == acc.email).FirstOrDefault().email;
        
            if (emailString.ToLower().Contains("@gmail.com"))
            {
                if (result == "Admin" )
                {

                    Session["UserName"] = acc.username;
                    FormsAuthentication.SetAuthCookie(acc.username, true);

                    return RedirectToAction("Index", "Appear");
                }
                else if (result == "Analyst")
                {
                    //HttpCookie cookie = new HttpCookie("UserName");
                    //cookie["UserName"] = c;
                    //Response.Cookies.Add(cookie);
                    Session["UserName"] = acc.username;
                    FormsAuthentication.SetAuthCookie(acc.username, true, acc.username);

                    return RedirectToAction("Index", "Appear");
                }
                else if (result == "Production")
                {
                    Session["UserName"] = acc.username;
                    FormsAuthentication.SetAuthCookie(acc.username, true, acc.username);
                    return RedirectToAction("Profile", "Appear");
                }
                else if (result == "Editor")
                {
                    Session["UserName"] = acc.username;
                    FormsAuthentication.SetAuthCookie(acc.username, true, ".MyCookieName");
                    return RedirectToAction("Editor", "Appear");
                }
                else if (result == "Coordinator")
                {
                    Session["UserName"] = acc.username;
                    FormsAuthentication.SetAuthCookie(acc.username, true, ".MyCookieName");
                    return RedirectToAction("Profile", "Appear");
                }
                else if (result == "SA")
                {
                    Session["UserName"] = acc.username;
                    FormsAuthentication.SetAuthCookie(acc.username, true, acc.username);
                    return RedirectToAction("Profile", "Appear");
                }
                else if (result == "Publisher")
                {
                    Session["UserName"] = acc.username;
                    FormsAuthentication.SetAuthCookie(acc.username, true);
                    return RedirectToAction("Profile", "Appear");
                }
            }
            TempData["mail"] = "Email is Invalid";

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
            string msg = "Invalid Email Format";
            if (acc != null)
            {
                var a = DapperORM.ReturnList<AccountModel>("sp_View", param).ToList();
                var emailString = a.FirstOrDefault().email;
                
                param.Add("@Id", acc.Id);
                param.Add("@firstName", acc.firstName);
                param.Add("@lastName", acc.lastName);
                param.Add("@Gender", acc.Gender);
                param.Add("@age", acc.age);
                if (acc.email.ToLower().Contains("@gmail.com"))
                {
                    param.Add("@email", acc.email);
                }
                else
                {
                    TempData["mail"] = "Email is Invalid";
                    return RedirectToAction("Register");
                }
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
            if (emp.email.ToLower().Contains("@gmail.com"))
            {
                param.Add("@email", emp.email);
            }
            else
            {
                TempData["mail"] = "Email is Invalid";
                return RedirectToAction("Edit");

            }
            param.Add("@username", emp.username);
            param.Add("@password", emp.password);
            param.Add("@confirm_password", emp.confirm_password);
            param.Add("@Status",1);
            param.Add("@RoleId", emp.RoleId);
            DapperORM.ExecuteWithoutReturn("sp_Edit", param);
            return RedirectToAction("Index", "Appear");
        }

        public ActionResult Delete(int id)
        {
            
            DynamicParameters param = new DynamicParameters();
            DapperORM da = new DapperORM();
            var a = DapperORM.ReturnList<AccountModel>("sp_View", param).ToList();
          var  result = a.FirstOrDefault(x => x.Id == id);
            var b = result.RoleId.ToString();
            param.Add("@Id", id);
            if(b == "Admin")
            {
                ViewBag.Message = "You Cannot Delete The Admin";
                return RedirectToAction("Index", "Appear");
            }
            
          
     
            DapperORM.ExecuteWithoutReturn("sp_Delete", param);
            return RedirectToAction("Index", "Appear");
        }

        private ActionResult BadRequest(string v)
        {
            throw new System.NotImplementedException();
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
        public ActionResult IsNotValid()
        {
            return View();
        }
    }

}





  
