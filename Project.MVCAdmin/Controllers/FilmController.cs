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
        public FilmController()
        {
            _filmRep = new FilmRepository();
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

        public ActionResult ListFilms()
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
        public ActionResult AddFilm(FilmVM film, HttpPostedFileBase image, string fileName)
        {
            Film f = new Film
            {
                MovieName = film.MovieName,
                ID = film.ID,
                Duration = film.Duration,
                ImagePath = film.ImagePath = ImageUploader.UploadImage("/Pictures/", image, fileName),
                Info = film.Info,
                Type = film.Type,

            };
            _filmRep.Add(f);
            return RedirectToAction("ListFilms");
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
            updated.ImagePath = film.ImagePath = ImageUploader.UploadImage("/Pictures/", image, fileName);
            updated.Type = film.Type;
            updated.Info = film.Info;

            _filmRep.Update(updated);
            return RedirectToAction("ListFilms");

        }

        public ActionResult DeleteFilm(int id)
        {
            _filmRep.Delete(_filmRep.Find(id));

            return RedirectToAction("ListFilms");

        }
    }
}