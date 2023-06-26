using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.CustomTools;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.AdminPureVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCAdmin.Controllers
{
    public class UserController : Controller
    {
        AppUserRepository _appUserRep;
        public UserController()
        {
            _appUserRep = new AppUserRepository();
        }


        public ActionResult ListAppUser()
        {
            List<AdminUserVM> adminUsers= _appUserRep.Where(x=>x.Role != ENTITIES.Enums.UserRole.Admin).Select(x=> new AdminUserVM
            {
                UserName= x.UserName,
                Password= x.Password,
                Roles=x.Role
                

            }).ToList();


            ListAdminUserPageVM laup = new ListAdminUserPageVM()
            {
                AdminUsers = adminUsers,
            };

            return View(laup);
        }

        public ActionResult AddAppUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAppUser(AdminUserVM adminUser)
        {
            AppUser ap = new AppUser
            {
               ID= adminUser.ID,
               UserName= adminUser.UserName,
               Password= adminUser.Password,
               Role= adminUser.Roles,

            };
            _appUserRep.Add(ap);
            return View();
        }

        public ActionResult UpdateAppUser(int? id)
        {
            AddUpdateAdminUserPageVM apdp = new AddUpdateAdminUserPageVM()
            {
                 AdminUser = _appUserRep.Where(x => x.ID == id).Select(x => new AdminUserVM
                {
                    ID = x.ID,
                    UserName = x.UserName,
                    Password = x.Password,
                    Roles = x.Role
                }).FirstOrDefault()
            };

            return View(apdp);
        }


        [HttpPost]
        public ActionResult UpdateAppUser(AdminUserVM adminUser)
        {
            AppUser updated = _appUserRep.Find(adminUser.ID);
            updated.UserName= adminUser.UserName;
            updated.Password= adminUser.Password;
            updated.Role = adminUser.Roles;

            _appUserRep.Update(updated);
            return RedirectToAction("ListAppUser");
        }

        public ActionResult DeleteAppUser(int id)
        {
            _appUserRep.Delete(_appUserRep.Find(id));
            //_appUserRep.Delete(id);
            return RedirectToAction("ListAppUser");

        }

    }
}