using Microsoft.Ajax.Utilities;
using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.CustomTools;
using Project.MVCAdmin.Models.PageVMs;
using Project.MVCUI.Models.PageVMs;
using Project.MVCUI.Models.ReservationTools;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        FilmRepository _filmRep;
        SeansRepository _seansRep;
        SeatRepository _seatRep;
        TicketSeatRepository _ticketSeatRep;
        TicketRepository _ticketRep;
        AppUserRepository _appRep;
        public HomeController()
        {
            _filmRep = new FilmRepository();
            _seansRep = new SeansRepository();
            _seatRep = new SeatRepository();
            _ticketSeatRep = new TicketSeatRepository();
            _ticketRep = new TicketRepository();
            _appRep = new AppUserRepository();
        }


        private List<TicketVM> GetTickets()
        {
            return _ticketRep.Select(ticket => new TicketVM
            {
                ID = ticket.ID,
                TotalPrice = ticket.Price,
                Type = ticket.Type,

            }).ToList();
        }

        private List<SeatVM> GetSeats(int? seansId)
        {
            List<Seat> seat = _seatRep.Where(x => x.SeansID == seansId).ToList();

            List<SeatVM> seats = seat.Where(x => x.Status != DataStatus.Deleted).Select(x => new SeatVM
            {
                ID = x.ID,
                SeatNo = x.SeatNo,
                SeatStatus = x.SeatStatus,
                Row = x.Row,
                SaloonNumber = x.Saloon.SaloonNumber,
                SeatPrice = x.SeatPrice,
                SaloonID = x.Saloon.ID,
                SeansID = x.SeansID.Value,

            }).ToList();
            return seats;
        }




        private List<FilmVM> GetFilms()
        {



            return _filmRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new FilmVM
            {
                ID = x.ID,
                MovieName = x.MovieName,
                Duration = x.Duration,
                Type = x.Type,
                Info = x.Info,
                ImagePath = x.ImagePath 


            }).ToList();


        }




        private List<SeansVM> GetSeans(int filmId)
        {
            List<Seans> seanslar = _seansRep.Where(x => x.FilmId == filmId).ToList();

            List<SeansVM> seansVMs = seanslar.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(seans => new SeansVM
            {
                ID = seans.ID,
                SeansNumber = seans.SeansNumber,
                StartTime = seans.StartTime,
                EndTime = seans.EndTime,
                SaloonID = seans.Saloon.ID,
                SaloonNumber = seans.Saloon.SaloonNumber,
                MovieName = seans.Film.MovieName,
                Duration = seans.Film.Duration,
                FilmID = seans.Film.ID,


            }).ToList();

            return seansVMs;
        }

        public ActionResult Index()
        {
            List<FilmVM> films = GetFilms();

            foreach (FilmVM film in films)
            {
                List<SeansVM> seanslar = GetSeans(film.ID);
                film.Seanslar = seanslar;

            }

            ListSeansPageVM lpvm = new ListSeansPageVM
            {
                Films = films,

            };
            return View(lpvm);
        }
        public ActionResult StudentControl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentControl(TicketVM ticket)
        {
            int currentYear = DateTime.Now.Year;
            int age = currentYear - ticket.BirthYear;

            if (age < 25)
            {
                Session["Student"] = age;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Student = "Öğrenci için uygun değildir";
            }
            return View();
        }

        public ActionResult VipControl()
        {
            return View();
        }

        [HttpPost]

        public ActionResult VipControl(TicketVM ticket)
        {
            AppUser vipMi = _appRep.FirstOrDefault(x => x.VipCode == ticket.VipCode);

            if (vipMi != null)
            {

                Session["VipUser"] = vipMi;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Gecersiz = "Geçersiz Kod";
            }

            return View();
        }

        public ActionResult SeansSeats(int? seansId, int filmId)
        {


            FilmVM film = GetFilms().FirstOrDefault(f => f.ID == filmId);

            if (film == null)
            {
                return RedirectToAction("Index");
            }

            SeansVM seans = GetSeans(filmId).FirstOrDefault(s => s.ID == seansId);

            if (seans == null)
            {
                return RedirectToAction("Index");
            }

            List<TicketVM> ticket = GetTickets();
            if (ticket == null)
            {
                return RedirectToAction("Index");
            }




            List<SeatVM> seats = GetSeats(seansId);

            ListSeansPageVM seansSeatsVM = new ListSeansPageVM
            {
                Film = film,
                Seans = new List<SeansVM> { seans },
                Seats = seats,
                Tickets = ticket

            };

            if (seansId != null)
            {
                TempData["seansId"] = seansId;
            }

            TempData["FilmAdi"] = film.MovieName;

            return View(seansSeatsVM);
        }



    }

}
