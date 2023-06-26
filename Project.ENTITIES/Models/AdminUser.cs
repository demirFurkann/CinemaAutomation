using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AdminUser : BaseEntity
    {
        public AdminUser() { }

        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AdminRole AdminRole { get; set; }
    }
}
