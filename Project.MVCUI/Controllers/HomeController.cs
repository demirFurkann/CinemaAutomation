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
        //    private List<string> GetImagePathsForFilms()
        //    {
        //        string[] imageUrls = {
        //    "1449215.jpg-c_310_420_x-f_jpg-q_x-xxyxx.jpg",
        //    "81J1DaRKzUL._AC_UF894,1000_QL80_.jpg",
        //    "indir (1).jpg",
        //    "indir (2).jpg",
        //    "indir.jpg",
        //    "megan.jpg"
        //};

        //        List<string> imagePaths = new List<string>();

        //        foreach (string imageUrl in imageUrls)
        //        {
        //            string imagePath = "Pictures/" + imageUrl;
        //            imagePaths.Add(imagePath);
        //        }

        //        return imagePaths;
        //    }


        //    public ActionResult Index()
        //    {
        //        List<FilmVM> films = GetFilms();

        //        for (int i = 0; i < films.Count; i++)
        //        {
        //            FilmVM film = films[i];

        //            List<SeansVM> seanslar = GetSeans(film.ID);
        //            film.Seanslar = seanslar;

        //            Tüm resim yollarını her bir film nesnesine sıralı bir şekilde atayın
        //            film.ImagePath = "Pictures/" + film.ID + ".jpg"; // Burada her filme özel bir afiş adı kullanılıyor
        //        }

        //        ListSeansPageVM lpvm = new ListSeansPageVM
        //        {
        //            Films = films,
        //        };
        //        return View(lpvm);
        //    }

        public ActionResult Index()
        {
            List<FilmVM> films = GetFilms();
    //        List<string> imageUrls = new List<string>
    //        {
    //"1449215.jpg-c_310_420_x-f_jpg-q_x-xxyxx.jpg",
    //"Elemental-Digital-Keyart-2400x2400-1.jpg",
    //"MV5BMDBmYTZjNjUtN2M1MS00MTQ2LTk2ODgtNzc2M2QyZGE5NTVjXkEyXkFqcGdeQXVyNzAwMjU2MTY@._V1_ (2).jpg",
    //"john-wick-bolum-4.jpg",
    //"2012949-58861826.jpg",
    //"megan.jpg"
    //        };


            for (int i = 0; i < films.Count; i++)
            {
                FilmVM film = films[i];

                List<SeansVM> seanslar = GetSeans(film.ID);
                film.Seanslar = seanslar;

                //// Tüm resim yollarını her bir film nesnesine sıralı bir şekilde atayın
                //film.ImagePath = "/Pictures/" + imageUrls[i]; // Burada URL'leri kullanıyoruz
            }

            ListSeansPageVM lpvm = new ListSeansPageVM
            {
                Films = films,
            };
            return View(lpvm);
        }



        //public ActionResult Index()
        //{
        //    List<FilmVM> films = GetFilms();

        //    foreach (FilmVM film in films)
        //    {
        //        List<SeansVM> seanslar = GetSeans(film.ID);
        //        film.Seanslar = seanslar;
        //        List<string> imagePaths = new List<string>
        //        {
        //    "Pictures/164658698-elemental.jpeg",
        //    "Pictures/81J1DaRKzUL._AC_UF894",
        //    "Pictures/1000_QL80_.jpg",
        //    "Pictures/megan.jpg",
        //    "Pictures/MV5BMDBmYTZjNjUtN2M1MS00MTQ2LTk2ODgtNzc2M2QyZGE5NTVjXkEyXkFqcGdeQXVyNzAwMjU2MTY@._V1_.jpg"
        //};
        //        film.ImagePaths = imagePaths;
        //    }

        //    ListSeansPageVM lpvm = new ListSeansPageVM
        //    {
        //        Films = films,

        //    };
        //    return View(lpvm);
        //}

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


            ListSeansPageVM seansSeatsVM = new ListSeansPageVM
            {
                Film = film,
                Seans = new List<SeansVM> { seans },
                //Seats = seats
            };

            return View(seansSeatsVM);
        }




    }
}