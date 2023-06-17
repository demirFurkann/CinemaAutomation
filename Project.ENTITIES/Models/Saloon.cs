using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Saloon : BaseEntity
    {
        public string SaloonNumber { get; set; }
        public int Capacity { get; set; }



        //Relational Properties added
       
        public virtual List<Seat> Seats { get; set; }

        public virtual List<Seans> Seans { get; set; }
        // seat bağla
    }
}
