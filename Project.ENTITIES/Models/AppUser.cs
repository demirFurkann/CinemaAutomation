using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid ActivationCode { get; set; }
        public bool Active { get; set; }

        public UserRole Role { get; set; }

        public AppUser()
        {
            ActivationCode = Guid.NewGuid();
        }

        //Foreign Key


        //Relational Properties
        public virtual AppUserProfile AppUserProfile { get; set; }
        public virtual List<Reservation> Reservations { get; set; }



    }
}
