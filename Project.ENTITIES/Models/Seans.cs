using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Seans : BaseEntity
    {
        //Seanslar
        public string SeansNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int SaloonId { get; set; }
        public int FilmId { get; set; }

        //Relational Properties
        public virtual List<Reservation> Reservations   { get; set; }

        public virtual Saloon Saloon { get; set; }
        public virtual Film Film { get; set; }
    }
}
