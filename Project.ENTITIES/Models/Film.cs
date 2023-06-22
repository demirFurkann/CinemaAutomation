using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Film : BaseEntity
    {
        public string MovieName { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
        public string ImagePath { get; set; }




        //Relational Properties 
        public List<Seans> Seans { get; set; }
        

    }
}
