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

namespace Project.MVCAdmin.Controllers
{
    public class FilmController : Controller
    {
        FilmRepository _filmRep;
        SeansRepository _seansRep;
        public FilmController()
        {
            _filmRep = new FilmRepository();
            _seansRep = new SeansRepository();
        }

        private List<FilmVM> GetFilms()
        {
            return _filmRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new FilmVM
            {
                ID = x.ID,
                MovieName = x.MovieName,
                Duration = x.Duration,
                ImagePath = x.ImagePath,
                Info = x.Info,
                Type = x.Type,

            }).ToList();
        }

        public ActionResult Index()
        {
            List<FilmVM> films = GetFilms();
            ListFilmPageVM lpvm = new ListFilmPageVM
            {
                Films = films,
            };
            _filmRep.GetAll();
            return View(lpvm);

        }

        public ActionResult AddFilm()
        {
            return View();
        }

        [HttpPost]

        // film bitince başka seansla cakısmasın 

        public ActionResult AddFilm(FilmVM film, HttpPostedFileBase image, string fileName)
        {
            Film f = new Film
            {
                MovieName = film.MovieName,
                ID = film.ID,
                Duration = film.Duration,
                ImagePath = film.ImagePath = ImageUploader.UploadImage(image, fileName),
                Info = film.Info,
                Type = film.Type,

            };
            _filmRep.Add(f);
            return RedirectToAction("Index");
        }


        public ActionResult UpdateFilm(int id)
        {
            AddUpdateFilmPageVM flpvm = new AddUpdateFilmPageVM
            {
                Film = _filmRep.Where(x => x.ID == id).Select(x => new FilmVM
                {
                    ID = x.ID,
                    MovieName = x.MovieName,
                    Duration = x.Duration,
                    ImagePath = x.ImagePath,
                    Type = x.Type,
                    Info = x.Info,

                }).FirstOrDefault()
            };
            return View(flpvm);
        }

        [HttpPost]
        public ActionResult UpdateFilm(FilmVM film, HttpPostedFileBase image, string fileName)
        {
            Film updated = _filmRep.Find(film.ID);
            updated.MovieName = film.MovieName;
            updated.Duration = film.Duration;
            updated.ImagePath = film.ImagePath = ImageUploader.UploadImage( image, fileName);
            updated.Type = film.Type;
            updated.Info = film.Info;

            List<Seans> updateSeans = _seansRep.Where(x => x.FilmId == updated.ID).ToList();
            foreach (Seans seans in updateSeans)
            {
                seans.Film.Duration = updated.Duration;
                DateTime endTime = seans.StartTime.AddMinutes(updated.Duration + 10);
                seans.EndTime = endTime;
                _seansRep.Update(seans);

            }
            _filmRep.Update(updated);

            return RedirectToAction("Index");

        }

        public ActionResult DeleteFilm(int id)
        {
            Film film = _filmRep.Find(id);

            List<Seans> seanslar = _seansRep.Where(x => x.FilmId == film.ID);

            _seansRep.DeleteRange(seanslar);

            _filmRep.Delete(film);

            return RedirectToAction("Index");

        }
    }
}