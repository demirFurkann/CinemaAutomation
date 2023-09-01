using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUserProfile:BaseEntity
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        //Relational Properties

        public virtual AppUser AppUser { get; set; }
    }
}
