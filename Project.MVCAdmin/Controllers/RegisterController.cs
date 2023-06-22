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
        AppUserRepository _appUser;
        AppUserProfileRepository _appProfile;

        public RegisterController()
        {
            _appUser = new AppUserRepository();
            _appProfile = new AppUserProfileRepository();
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
        public ActionResult RegisterNow(AdminUserVM user, AdminProfileVM profile)
        {
            // UserName alınmış mı 


            if (_appUser.Any(x => x.UserName.Equals( user.UserName)))
            {
                ViewBag.Mevcut = "Farklı Bir İsim Seçiniz";
                return View();
            }
            // Passwordu şifreleme
            //user.Password = CryptPassword.Crypt(user.Password);
           

            AppUser domainUser = new AppUser
            {
                UserName = user.UserName,
                Password = user.Password,
               /* Role = (UserRole)Enum.Parse(typeof(UserRole), user.Roles)*/
                //Role = (UserRole)user.Roles // int kontrol için
                Role=user.Roles


            };
            _appUser.Add(domainUser);


            if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || !string.IsNullOrEmpty(profile.LastName.Trim()))
            {
                AppUserProfile domainProfile = new AppUserProfile
                {
                    ID = domainUser.ID,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    BirthDate = profile.BirthDate
                };
                _appProfile.Add(domainProfile);

            }

            // Login ekranından sonra View Doldurulucak 
            return RedirectToAction("LoginOK");
        }
        public ActionResult LoginOK()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginOk(AdminUserVM user)
        {
            AppUser userLogin = _appUser.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if(userLogin != null)
            {
                if (userLogin.Role.Equals(UserRole.Admin))
                {
                    // Admin girişi
                    return RedirectToAction("AdminScreen", "AdminPanel");
                }
               
            }

            return View();

        }

      }
 }
