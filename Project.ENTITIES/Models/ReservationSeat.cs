using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class ReservationSeat : BaseEntity
    {
        public int ReservationID { get; set; }
        public int SeatID { get; set; }



        //Relational Properties

        public virtual Reservation Reservation { get; set; }
        public virtual Seat Seat { get; set; }
    }
}
