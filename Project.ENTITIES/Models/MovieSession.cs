using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class MovieSession : BaseEntity
    {
        //Seanslar
        public string SessionNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }



        //Foreign key

        public int? SaloonID { get; set; }
        public int? FilmID { get; set; }

        //Relational Properties

        public virtual Saloon Saloon { get; set; }
        public virtual Film Film { get; set; }
        public virtual List<Ticket> Ticket { get; set; }
        public virtual List<Reservation> Reservations { get; set; }



    }
}
