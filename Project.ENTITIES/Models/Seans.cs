﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Seans : BaseEntity
    {
        //Seanslar
        public string SeansNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        //Deneme
        //Foreign key
        public int? FilmSaloonID { get; set; }
        


        //Relational Properties
        public virtual FilmSaloon FilmSaloon { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
        public virtual List<TicketSeans> TicketSeans { get; set; }



    }
}
