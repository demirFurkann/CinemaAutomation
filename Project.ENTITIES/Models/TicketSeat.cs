using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class TicketSeat:BaseEntity
    {
        public int TicketID { get; set; }
        public int SeatID { get; set; }

        //Relational Properties
        public virtual Seat Seats { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
