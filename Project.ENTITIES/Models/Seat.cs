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
        public string SeatNumber { get; set; }

        public SeatStatus Status { get; set; }

        public Seat()
        {
            Status = SeatStatus.Empty;
        }

        //Foreign Key

        public int? SaloonID { get; set; }

        //Relational Properties
        public virtual Saloon Saloon { get; set; }



    }
}
