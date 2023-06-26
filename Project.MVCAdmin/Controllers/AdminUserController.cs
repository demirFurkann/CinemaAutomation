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
    public class AdminUserController : Controller
    {
        AdminUserRepository _adminUserRep;
        public AdminUserController()
        {
            _adminUserRep = new AdminUserRepository();
        }


        public ActionResult Index()
        {
            List<AdminUserVM> adminUsers= _adminUserRep.Where(x=>x.AdminRole != ENTITIES.Enums.AdminRole.Admin).Select(x=> new AdminUserVM
            {
                ID=x.ID,
                UserName= x.UserName,
                Password= x.Password,
                AdminRole=x.AdminRole
                

            }).ToList();


            ListAdminUserPageVM adminUserListPageVM = new ListAdminUserPageVM()
            {
                AdminUsers = adminUsers,
            };

            return View(adminUserListPageVM);
        }

        public ActionResult AddAdminUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdminUser(AdminUserVM adminUser)
        {
            AdminUser ap = new AdminUser
            {
               ID= adminUser.ID,
               UserName= adminUser.UserName,
               Password= adminUser.Password,
               AdminRole= adminUser.AdminRole,

            };
            _adminUserRep.Add(ap);
            return View();
        }

        public ActionResult UpdateAdminUser(int? id)
        {
            AddUpdateAdminUserPageVM adminUserVM = new AddUpdateAdminUserPageVM()
            {
                 AdminUser = _adminUserRep.Where(x => x.ID == id).Select(x => new AdminUserVM
                {
                    ID = x.ID,
                    UserName = x.UserName,
                    Password = x.Password,
                    AdminRole = x.AdminRole
                }).FirstOrDefault()
            };

            return View(adminUserVM);
        }


        [HttpPost]
        public ActionResult UpdateAdminUser(AdminUserVM adminUser)
        {
            AdminUser updated = _adminUserRep.Find(adminUser.ID);
            updated.UserName= adminUser.UserName;
            updated.Password= adminUser.Password;
            updated.AdminRole = adminUser.AdminRole;

            _adminUserRep.Update(updated);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAppUser(int id)
        {
            var adminUserToDelete = _adminUserRep.Find(id);

            _adminUserRep.Delete(adminUserToDelete);
            //_appUserRep.Delete(id);
            return RedirectToAction("Index");

        }

    }
}