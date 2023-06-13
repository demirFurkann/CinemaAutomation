using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class FilmSaloon : BaseEntity
    {
        public int SaloonID { get; set; }
        public int FilmID { get; set; }


        //Relational Properties
        public virtual Film Film { get; set; }
        public virtual Saloon Salon { get; set; }


    }
}
