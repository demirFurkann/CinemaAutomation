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
        public decimal SeatPrice { get; set; }

        public SeatStatus SeatStatus { get; set; }
        public TicketType TicketType { get; set; }

        public Seat()
        {
            SeatStatus = SeatStatus.Empty;
        }

        //Foreign Key

        public int? SaloonID { get; set; }

        // koltukları seanslara gore listelemek için
        public int? SeansID { get; set; }

        //Relational Properties

        public virtual Saloon Saloon { get; set; }

        // koltukları seanslara gore listelemek için
        public virtual Seans Seans { get; set; }

        public virtual List<ReservationSeat> ReservationSeats { get; set; }


        //bu olmaz değişcek 


        //Seat Rezervasyon
        //List tipinde



    }
}
