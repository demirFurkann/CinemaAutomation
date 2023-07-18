using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.CustomTools;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        FilmRepository _filmRep;
        SeansRepository _seansRep;
        SeatRepository _seatRep;
        public HomeController()
        {
            _filmRep = new FilmRepository();
            _seansRep = new SeansRepository();
            _seatRep = new SeatRepository();
        }

        private List<SeatVM> GetSeats(int saloonID)
        {
            return _seatRep.Where(x => x.Saloon.ID == saloonID).Select(x => new SeatVM
            {
                ID = x.ID,
                SeatNo = x.SeatNo,
                SeatStatus = x.SeatStatus,
                Row = x.Row,
                SaloonNumber = x.Saloon.SaloonNumber
            }).ToList();
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
                FilmID = seans.Film.ID

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

        public ActionResult SeansSeats(int seansId, int filmId)
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

            List<SeatVM> seats = GetSeats(seans.SaloonID);

            ListSeansPageVM seansSeatsVM = new ListSeansPageVM
            {
                Film = film,
                Seans = new List<SeansVM> { seans },
                Seats = seats
            };

            return View(seansSeatsVM);
        }



    }
}