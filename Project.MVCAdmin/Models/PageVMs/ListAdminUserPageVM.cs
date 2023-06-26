using Project.VM.AdminPureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class ListAdminUserPageVM
    {
        public List<AdminUserVM> AdminUsers { get; set; }
    }
}