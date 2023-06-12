﻿using System;
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

        //Relational Properties 

        public virtual List<Seans> Seans { get; set; }
        public virtual List<FilmSaloon> FilmSaloons { get; set; }


    }
}
