using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Project.VM.AdminPureVMs
{
    public class AdminUserVM
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Roles { get; set; }

    }
}
