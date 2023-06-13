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
        public decimal TotalPrice { get; set; }

        // Onaylama işlemleri
        public bool IsApproved { get; set; }
        public string ConfirmationCode { get; set; }
        
        //Silinme nedenini öğrenmek için
        public string CancellationReason { get; set; }



        //Foreign Key

        public int? AppUserID { get; set; }
        public int? BoxOfficeID { get; set; }
        public int? FilmID { get; set; }


        //Relational Properties

        public virtual AppUser AppUser { get; set; }
        public virtual BoxOffice BoxOffice { get; set; }
        public virtual Film Film { get; set; }
        public virtual List<Ticket> Ticket { get; set; }
        public virtual List<ReportItem> ReportItems { get; set; }

    }
}
