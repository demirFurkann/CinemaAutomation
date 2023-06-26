using Microsoft.Ajax.Utilities;
using Project.BLL.Repositories.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.AdminPureVMs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.MVCAdmin.Controllers
{
    public class RegisterController : Controller
    {
        AdminUserRepository _adminUser;

        public RegisterController()
        {
            _adminUser = new AdminUserRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        // Enum'lari listelemek için

        private List<SelectListItem> GetRoles()
        {
            var roles = Enum.GetValues(typeof(UserRole))
                       .Cast<UserRole>()
                       .Select(r => new SelectListItem
                       {
                           Value = r.ToString(),
                           Text = r.ToString()
                       })
                       .ToList();

            return roles;
        }

        public ActionResult RegisterNow()
        {
            //Todo : Burayi Tekrar Kontrol et
            RegisterNowPageVM rpvm = new RegisterNowPageVM();
            rpvm.User = new AdminUserVM();

            rpvm.Roles = GetRoles();

            return View(rpvm);

        }

        [HttpPost]
        public ActionResult RegisterNow(AdminUserVM user)
        {
            // UserName alınmış mı 


            if (_adminUser.Any(x => x.UserName.Equals( user.UserName)))
            {
                ViewBag.Mevcut = "Farklı Bir İsim Seçiniz";
                return View();
            }
            // Passwordu şifreleme
            //user.Password = CryptPassword.Crypt(user.Password);
           

            AdminUser domainUser = new AdminUser
            {
                UserName = user.UserName,
                Password = user.Password,
               /* Role = (UserRole)Enum.Parse(typeof(UserRole), user.Roles)*/
                //Role = (UserRole)user.Roles // int kontrol için
                AdminRole=user.AdminRole


            };
            _adminUser.Add(domainUser);


            // Login ekranından sonra View Doldurulucak 
            return RedirectToAction("LoginOK");
        }
     
        [HttpPost]
        public ActionResult LoginOk(AdminUserVM user)
        {
            AdminUser adminUser = _adminUser.FirstOrDefault
                (x => x.UserName == user.UserName
                && x.Password == user.Password);

            if(adminUser != null)
            {
                if (adminUser.AdminRole.Equals(AdminRole.Admin))
                {
                    // Admin girişi
                    return RedirectToAction("AdminScreen", "AdminPanel");
                }
               
            }

            return View();

        }

      }
 }
