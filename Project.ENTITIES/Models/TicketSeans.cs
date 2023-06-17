using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class TicketSeans:BaseEntity
    {
        public int TicketID { get; set; }
        public int SeansID { get; set; }

        //Relational Properties

        public virtual Ticket Ticket { get; set; }
        public virtual Seans Seans { get; set; }
    }
}
