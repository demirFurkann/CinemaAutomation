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

        // Onaylama kodu
        public string ConfirmationCode { get; set; }
        //Silinme nedenini öğrenmek için
        public string CancellationReason { get; set; }



        //Foreign Key

        public int? SeansID { get; set; }
        public int? AppUserID { get; set; }

        //Relational Properties
        public virtual Seans Seans { get; set; }
        public virtual AppUser AppUser{ get; set; }


    }
}
