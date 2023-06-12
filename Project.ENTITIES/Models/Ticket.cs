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
        public string ConfirmationCode { get; set; }
        //Silinme nedenini öğrenmek için
        public string CancellationReason { get; set; }
        public TicketType Type { get; set; }


        //Onay takip 
        public int? ApprovedByUserId { get; set; }
        public AppUser ApprovedByUser { get; set; }


        //Foreign Key

        public int? MovieID { get; set; }
        public int? BoxOfficeID { get; set; }

        //Relational Properties
        public virtual  MovieSession  MovieSession { get; set; }
        public virtual BoxOffice BoxOffice { get; set; }
       




    }
}
