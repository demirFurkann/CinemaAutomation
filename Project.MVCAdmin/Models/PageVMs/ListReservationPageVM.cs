using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class ListReservationPageVM
    {
        public List<SeansVM> Seans { get; set; }
        public List<FilmVM> Films { get; set; }
        public List<ReservationVM> Reservations { get; set; }
        public ReservationVM Reservation { get; set; }
        public AppUserVM AppUser { get; set; }
       
        public ListReservationPageVM()
        {
            Reservations = new List<ReservationVM>();
            Reservation = new ReservationVM();
        }
    }
}