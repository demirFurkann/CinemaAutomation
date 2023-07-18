using Project.BLL.Repositories.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterUserController : Controller
    {
        AppUserRepository _appUserRep;
        AppUserProfileRepository _appProfileRep;
        public RegisterUserController()
        {
            _appUserRep = new AppUserRepository();
            _appProfileRep = new AppUserProfileRepository();
        }


        public ActionResult Index()
        {

            return View();
        }


        public ActionResult RegisterNow()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegisterNow(AppUserVM appUser, AppUserProfile profile)
        {
            if (_appUserRep.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.Kayitli = "Kayıtlı Kullanıcı Adı";
                return View();
            }
            else if (_appUserRep.Any(x => x.Email == appUser.Email))
            {
                ViewBag.Kayitli = "Kayıtlı Email Adı";
                return View();
            }

            appUser.Password = CryptPassword.Crypt(appUser.Password);

            AppUser addUser = new AppUser
            {
                UserName = appUser.UserName,
                Password = appUser.Password,
                Email = appUser.Email,
            };

            _appUserRep.Add(addUser);

            string kayitMail = "Hoş Geldiniz Hesabınız oluşturuldu, Hesabı aktif etmek için Linke Tıklayınız \n  http://localhost:62653/RegisterUser/ActivationUser/" + addUser.ActivationCode;

            MailService.Send(appUser.Email, body: kayitMail, subject: "Hesap Aktivasyonu ! ! !");
            if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || !string.IsNullOrEmpty(profile.LastName.Trim()))
            {
                AppUserProfile addProfile = new AppUserProfile
                {
                    ID = addUser.ID,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Job = profile.Job,
                    Gender = profile.Gender,
                    BirthDate = profile.BirthDate,
                };
                _appProfileRep.Add(addProfile);

            }


            return View("RegisterOk");
        }


        public ActionResult LoginUser()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginUser(AppUserVM appUser)
        {
            AppUser login = _appUserRep.FirstOrDefault(x => x.UserName.Equals(appUser.UserName) || x.Email.Equals(appUser.Email));

            if (login != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult ActivationUser(Guid id)
        {
            AppUser isActive = _appUserRep.FirstOrDefault(x => x.ActivationCode == id);
            if (isActive != null)
            {
                isActive.Active = true;
                _appUserRep.Update(isActive);
                TempData["HesapAktifMi"] = "Hesap Aktif Hale Getirildi";
                return RedirectToAction("Index","Home");
            }
            TempData["HesapAktifMi"] = "Hesabınız bulunamadı";
            return RedirectToAction("LoginUser");
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

    }
}