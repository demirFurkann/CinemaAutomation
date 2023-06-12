using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Saloon:BaseEntity
    {
        public string SaloonNumber { get; set; }
        public int Capaciyt { get; set; }



        //Relational Properties

        public virtual List<MovieSession> MovieSessions { get; set; }
    }
}
