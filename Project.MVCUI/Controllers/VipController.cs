using Microsoft.Ajax.Utilities;
using Project.BLL.Repositories.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class VipController : Controller
    {
        AppUserRepository _appUser;
        public VipController()
        {
            _appUser = new AppUserRepository();
        }

        [HttpGet]
        public ActionResult ActivationCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActivationCode(AppUserVM appUser)
        {
            // Aktivasyon kodu ile müşteriyi yakala
            AppUser user = _appUser.FirstOrDefault(x => x.ActivationCode == new Guid(appUser.ActivationCode));

            if (user == null)
            {
                return RedirectToAction("Error", "Activation");
            }

            string newVipCode = GenerateRandomCode(10);

            user.Role = ENTITIES.Enums.UserRole.VIP;
            user.VipCode = newVipCode;
            user.CreatedAt = DateTime.Now;

            _appUser.Update(user);

            string active = "Tebrikler ödemeniz alındı size özel giriş kodu şöyledir:" + Environment.NewLine + newVipCode;

            MailService.Send(user.Email, body: active, subject: "Vip Code");

            return RedirectToAction("LoginUser", "RegisterUser");
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rnd = new Random();
            return new string(Enumerable.Repeat(chars, length).
                Select(s => s[rnd.Next(s.Length)]).ToArray());

        }


    }
}