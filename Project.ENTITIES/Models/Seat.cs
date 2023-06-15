using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Seat : BaseEntity
    {
        public string SeatNo { get; set; }
        public string Row { get; set; }

        public SeatStatus Status { get; set; }

        public Seat()
        {
            Status = SeatStatus.Empty;
        }

        //Foreign Key

        public int? SaloonID { get; set; }

        //Relational Properties

        public virtual Saloon Saloon { get; set; }

        public virtual List<ReservationSeat> ReservationSeats { get; set; }


        //bu olmaz değişcek 


        //Seat Rezervasyon
        //List tipinde



    }
}
