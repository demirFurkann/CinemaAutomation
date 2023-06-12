﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class BoxOffice:BaseEntity 
    {
        // Gişe işlemleri
        public string OfficeNumber { get; set; }



        //Relational Properties
        public virtual List<Ticket> Ticket { get; set; }
        public virtual List<AppUser> AppUsers { get; set; }

    }
}
