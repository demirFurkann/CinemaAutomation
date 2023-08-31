using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Ticket : BaseEntity
    {
        // Bilet işlemleri için alim tarihi ve onaylımı kontrolu
        public decimal  Price { get; set; }
        public DateTime PurchaseDate { get; set; }  
        public TicketType Type { get; set; }

        public Ticket()
        {
            PurchaseDate = DateTime.Now;
        }


        //Foreign Key
        public int? BoxOfficeID { get; set; }
        public int? ReservationID { get; set; }
        public int? SeansID { get; set; }
        public int? AdminUserID { get; set; }


        //Relational Properties
        public virtual BoxOffice BoxOffice { get; set; }
        public virtual Seans Seans { get; set; }
        public virtual AdminUser AdminUser { get; set; }

    }
}
