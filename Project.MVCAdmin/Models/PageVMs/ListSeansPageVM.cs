using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class ListSeansPageVM
    {
        public List<SeansVM> Seans { get; set; }
        public List<FilmVM> Films { get; set; }
        public List<ReservationSeatVM> Reservations { get; set; }
        public FilmVM Film { get; set; }
        public List<SeatVM> Seats { get; set; }
        public SeatVM Seat { get; set; }

    }
}