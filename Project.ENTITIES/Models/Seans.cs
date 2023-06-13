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
        public string SessionNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }



        //Foreign key

        public int? FilmID { get; set; }

        //Relational Properties
        public virtual Film Film { get; set; }





    }
}
