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
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.MVCAdmin.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRepository _appUser;
        AppUserProfileRepository _appProfile;

        public RegisterController()
        {
            _appUser = new AppUserRepository();
            _appProfile = new AppUserProfileRepository();
        }

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
          RegisterNowPageVM rpvm = new RegisterNowPageVM();
            rpvm.User = new AdminUserVM();
            rpvm.User.Roles = GetRoles();

            return View();

        }

        [HttpPost]
        public ActionResult RegisterNow(AdminUserVM user, AdminProfileVM profile)
        {
            // UserName alınmış mı 

            if (_appUser.Any(x => x.UserName == user.UserName))
            {
                ViewBag.Mevcut = "Farklı Bir İsim Seçiniz";
                return View();
            }
            // Passwordu şifreleme
            user.Password = CryptPassword.Crypt(user.Password);

            AppUser domainUser = new AppUser
            {
                UserName = user.UserName,
                Password = user.Password,
                Role = (UserRole)Enum.Parse(typeof(UserRole), user.Roles.First(r => r.Selected).Text)




            };
            _appUser.Add(domainUser);


            if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || !string.IsNullOrEmpty(profile.LastName.Trim()))
            {
                AppUserProfile domainProfile = new AppUserProfile
                {
                    ID = domainUser.ID,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                };
            }

            // Login ekranından sonra View Doldurulucak 
            return View();
        }

    }
}