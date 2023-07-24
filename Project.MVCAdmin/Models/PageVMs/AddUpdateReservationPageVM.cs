using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class AddUpdateReservationPageVM
    {
        public ReservationVM Reservation { get; set; }
        public List<ReservationVM> Reservations { get; set; }
        public List<FilmVM> Films { get; set; }
        public List<SaloonVM> Saloons { get; set; }
        public List<SeansVM> Seans { get; set; }
        public AppUserVM AppUser { get; set; }
        public AddUpdateReservationPageVM()
        {
            Films = new List<FilmVM>();
            Seans = new List<SeansVM>();
        }

    }
}