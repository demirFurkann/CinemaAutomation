using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCAdmin.Controllers
{
    public class SeansController : Controller
    {
        SeansRepository _seansRep;
        FilmRepository _filmRep;
        SaloonRepository _saloonRep;
        public SeansController()
        {
            _seansRep = new SeansRepository();
            _filmRep = new FilmRepository();
            _saloonRep = new SaloonRepository();
        }

        private List<FilmVM> GetFilms()
        {
            return _filmRep.Where(x=> x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new FilmVM
            {
                ID = x.ID,
                MovieName = x.MovieName,
            }).ToList();
        }
        private List<SaloonVM> GetSaloons()
        {
            return _saloonRep.Select(x => new SaloonVM
            {
                ID = x.ID,
                SaloonNumber = x.SaloonNumber,
            }).ToList();
        }
        private List<SeansVM> GetSeans(int filmID)
        {
            List<Seans> seanslar = _seansRep.Where(x => x.FilmId == filmID).ToList();

            List<SeansVM> seansVMs = seanslar.Where(x=> x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(seans => new SeansVM
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

            ListSeansPageVM model = new ListSeansPageVM
            {
                Films = films
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ListSeansPageVM model)
        {
            if (model.Film != null && model.Film.ID != 0)
            {
                List<SeansVM> seansListesi = GetSeans(model.Film.ID);

                model.Seans = seansListesi;
            }
            else
            {
                model.Seans = null;
            }

            model.Films = GetFilms();

            return View(model);
        }

        public ActionResult AddSeans()
        {
            List<SaloonVM> saloons = GetSaloons();
            List<FilmVM> films = GetFilms();
            AddUpdateSeansPageVM seans = new AddUpdateSeansPageVM
            {
                Saloons = saloons,
                Films = films,
                Seans = new SeansVM()
            };

            return View(seans);
        }

        [HttpPost]
        public ActionResult AddSeans(SeansVM seans)
        {
            Film film = _filmRep.Find(seans.FilmID);
            Saloon saloon = _saloonRep.Find(seans.SaloonID);

            DateTime startTime = seans.StartTime;
            double filmDuration = film.Duration;

            DateTime endTime = startTime.AddMinutes(filmDuration + 10);

            Seans s = new Seans
            {
                Saloon = saloon,
                Film = film,
                SeansNumber = seans.SeansNumber,
                StartTime = startTime,
                EndTime = endTime
            };
            _seansRep.Add(s);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateSeans(int id)
        {
            List<SaloonVM> saloons = GetSaloons();
            List<FilmVM> films = GetFilms();

            SeansVM seans = _seansRep.Where(x => x.ID == id).Select(x => new SeansVM
            {
                ID = x.ID,
                SeansNumber = x.SeansNumber,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                SaloonNumber = x.Saloon.SaloonNumber,
                FilmID = x.FilmId

            }).FirstOrDefault();
            AddUpdateSeansPageVM apvm = new AddUpdateSeansPageVM
            {
                Seans = seans,
                Saloons = saloons,
                Films = films,
            };

            return View(apvm);
        }
        [HttpPost]
        public ActionResult UpdateSeans(SeansVM seans)
        {
            Saloon saloon = _saloonRep.Find(seans.SaloonID);
            Film film = _filmRep.Find(seans.FilmID);
            DateTime startTime = seans.StartTime;
            double filmDuration = film.Duration;

            DateTime endTime = startTime.AddMinutes(filmDuration + 10);

            Seans updated = _seansRep.Find(seans.ID);
            updated.SeansNumber = seans.SeansNumber;
            updated.StartTime = startTime;
            updated.EndTime = endTime;
            updated.Film = film;
            updated.Saloon = saloon;


            _seansRep.Update(updated);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteSeans(int id)
        {
            _seansRep.Delete(_seansRep.Find(id));

            return RedirectToAction("Index");
        }
    }
}