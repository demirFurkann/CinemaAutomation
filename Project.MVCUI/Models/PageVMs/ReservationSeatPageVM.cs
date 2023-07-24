using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class ReservationSeatPageVM
    {
        public List<SeansVM> Seans { get; set; }
        public FilmVM Film { get; set; }
        public List<SeatVM> Seats { get; set; }
        public ReservationSeatVM ReservationSeat { get; set; }
    }
}