using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class ReportItem:BaseEntity
    {
        public string Name { get; set; }
        public decimal BoxOfficeSales { get; set; }
        public decimal OnlineSales { get; set; }
        public decimal TotalSales => BoxOfficeSales + OnlineSales;

        //Foreign Key
        public int? AppUserID { get; set; }
        public int? ReservationID { get; set; }
        public int? ReportID { get; set; }

        //Relational Properties
        public virtual AppUser AppUser { get; set; }
        public virtual Reservation Reservation { get; set; }
        public virtual Report Report { get; set; }
    }
}
