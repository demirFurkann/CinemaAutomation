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
    public class DefaultController : Controller
    {
        // GET: Default

        public ActionResult Index()
        {
            // Filmleri ve seansları örnek verilerden al
            List<FilmVM> films = GetSampleFilmsAndSeanslar();

            return View(films);
        }

        private List<FilmVM> GetSampleFilmsAndSeanslar()
        {
            List<FilmVM> films = new List<FilmVM>
            {
                new FilmVM
                {
                    ID = 1,
                    MovieName = "Film 1",
                    Duration = 120,
                    Type = "Tür 1",
                    Info = "Bilgi 1",
                    ImagePath = "/path/to/image1.jpg",
                    Seanslar = new List<SeansVM>
                    {
                        new SeansVM
                        {
                            ID = 101,
                            SeansNumber = "Seans 1",
                            StartTime = DateTime.Now.Date.AddHours(12),
                            EndTime = DateTime.Now.Date.AddHours(14),
                            SaloonID = 201,
                            SaloonNumber = "Salon 1",
                            MovieName = "Film 1",
                            Duration = 120,
                            FilmID = 1,
                            ReservationID = 301,
                            ConfirmationCode = "ABC123"
                        },
                        new SeansVM
                        {
                            ID = 102,
                            SeansNumber = "Seans 2",
                            StartTime = DateTime.Now.Date.AddHours(16),
                            EndTime = DateTime.Now.Date.AddHours(18),
                            SaloonID = 202,
                            SaloonNumber = "Salon 2",
                            MovieName = "Film 1",
                            Duration = 120,
                            FilmID = 1,
                            ReservationID = 302,
                            ConfirmationCode = "XYZ456"
                        }
                    }
                },
                new FilmVM
                {
                    ID = 2,
                    MovieName = "Film 2",
                    Duration = 105,
                    Type = "Tür 2",
                    Info = "Bilgi 2",
                    ImagePath = "/path/to/image2.jpg",
                    Seanslar = new List<SeansVM>
                    {
                        new SeansVM
                        {
                            ID = 103,
                            SeansNumber = "Seans 3",
                            StartTime = DateTime.Now.Date.AddHours(14),
                            EndTime = DateTime.Now.Date.AddHours(16),
                            SaloonID = 203,
                            SaloonNumber = "Salon 3",
                            MovieName = "Film 2",
                            Duration = 105,
                            FilmID = 2,
                            ReservationID = 303,
                            ConfirmationCode = "DEF789"
                        }
                    }
                }
            };

            return films;
        }
    }
}  
