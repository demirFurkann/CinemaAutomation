using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Reservation : BaseEntity
    {
        //Rezervasyon işlemleri
        public DateTime ReservationTime { get; set; }
        public bool IsApproved { get; set; }
        public decimal TotalPrice { get; set; }





        //Foreign Key

        public int? MovieSessionID { get; set; }

        //Relational Properties
        public virtual MovieSession MovieSession { get; set; }

    }
}
