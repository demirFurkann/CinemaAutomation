using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class TicketVM
    {
        public int ID { get; set; }
        public decimal TotalPrice { get; set; }
        public TicketType Type { get; set; }
        public DateTime PurchaseDate { get; set; }

        //Gişe işlemleri için
        public int BoxOfficeID { get; set; }
        public string BoxOfficeNumber { get; set; }
        //Koltuk işlemleri için
        public int MyProperty { get; set; }


        //Seans işlemleri için
        public int SeansID { get; set; }
        public string SeansNumber { get; set; }

        public int AdminUserID { get; set; }
        public AdminRole EmployeeRole { get; set; }



    }
}
