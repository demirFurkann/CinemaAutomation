using Microsoft.Ajax.Utilities;
using Project.BLL.Repositories.ConcRep;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Project.ENTITIES.Models;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Project.VM.PureVMs;
using Project.MVCAdmin.Models.PageVMs;

namespace Project.MVCAdmin.Controllers
{
    public class ReservationController : Controller
    {
      ReservationRepository _reservationRep;
        AppUserRepository _appuserRep;
        SeatRepository _seatRep;
        FilmRepository _filmRep;
        SaloonRepository _saloonRep;
        SeansRepository _seansRep;

        public ReservationController()
        {

            _reservationRep = new ReservationRepository();
            _appuserRep = new AppUserRepository();
            _seatRep = new SeatRepository();
            _filmRep = new FilmRepository();
            _saloonRep = new SaloonRepository();
            _seansRep = new SeansRepository();
        }

        private List<SaloonVM> GetSaloons()
        {
            return _saloonRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new SaloonVM
            {
                ID = x.ID,
                Capacity = x.Capacity,
                SaloonNumber = x.SaloonNumber,
            }).ToList();
        }

        private List<FilmVM> GetFilms()
        {
            return _filmRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new FilmVM
            {
                ID = x.ID,
                MovieName = x.MovieName,
                Duration = x.Duration,

            }).ToList();
        }

        private List<ReservationVM> GetReservations()
        {
            return _reservationRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new ReservationVM
            {
                ID = x.ID,
                ConfirmationCode = x.ConfirmationCode,
                IsApproved = x.IsApproved,

            }).ToList();
        }


        private List<SeansVM> GetSeans(int seansID)
        {
            List<Reservation> reservation = _reservationRep.Where(x => x.SeansID == seansID).ToList();

            List<SeansVM> reservationVMs = reservation.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new SeansVM
            {
                ID = x.ID,
                SeansNumber = x.Seans.SeansNumber,
                ConfirmationCode = x.ConfirmationCode,

            }).ToList();

            return reservationVMs;

        }

        public ActionResult Index()
        {
            // Veritabanından rezervasyonları al ve modeli oluştur
            List<ReservationVM> reservations = GetReservations(); // Veritabanından rezervasyonları alacak bir metot kullanılmalıdır.

            return View(reservations);
        }

        private List<SeansVM> GetAllSeans()
        {
            return _seansRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new SeansVM
            {
                ID = x.ID,
                SeansNumber = x.SeansNumber,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                SaloonID = x.Saloon.ID, // Doğru veri ismi: SalonID
                SaloonNumber = x.Saloon.SaloonNumber, // Doğru veri ismi: SalonNumber
                MovieName = x.Film.MovieName, // Doğru veri ismi: Film.MovieName
                Duration = x.Film.Duration, // Doğru veri ismi: Film.Duration
                FilmID = x.Film.ID, // Doğru veri ismi: Film.ID
                ReservationID =x.ID, // Veri ismi: ReservationID
                //ConfirmationCode = x.
            }).ToList();
        }


        private string GenerateConfirmationCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string confirmationCode = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return confirmationCode;
        }


        [HttpGet]
        public ActionResult AddReservation()
        {
            // Gerekli verileri ViewModel aracılığıyla sayfaya gönderiyoruz
            AddUpdateReservationPageVM model = new AddUpdateReservationPageVM
            {
                Reservation = new ReservationVM(),
                Reservations = GetReservations(),
                Films = GetFilms(),
                Seans = GetAllSeans()
            };

            // ConfirmationCode oluşturarak, sayfadaki ilgili alanı dolduruyoruz
            model.Reservation.ConfirmationCode = GenerateConfirmationCode();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddReservation(ReservationVM reservation, AppUserVM appUser)
        {
            // Kullanıcıyı temsil eden AppUser nesnesini oluşturuyoruz ve veritabanına kaydediyoruz
            AppUser ap = new AppUser
            {
                UserName = appUser.UserName,
            };
            _appuserRep.Add(ap);

            // Rezervasyon nesnesini oluşturuyoruz ve veritabanına kaydediyoruz
            Reservation r = new Reservation
            {
                ID = ap.ID,
                ConfirmationCode = GenerateConfirmationCode(), // Rastgele doğrulama kodu oluşturuluyor
                TotalPrice = reservation.TotalPrice,
            };
            _reservationRep.Add(r);

            // "Index" sayfasına yönlendirme yaparak, rezervasyon işlemi tamamlandıktan sonra bu sayfaya gideceğini belirtiyoruz
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateReservation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Veritabanından ilgili rezervasyonu çekiyoruz
            Reservation reservation = _reservationRep.Find(id.Value);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            // ViewModel aracılığıyla rezervasyon bilgilerini sayfaya gönderiyoruz
            AddUpdateReservationPageVM updatereservationpagevm = new AddUpdateReservationPageVM
            {
                Reservation = new ReservationVM
                {
                    ID = reservation.ID,
                    ConfirmationCode = reservation.ConfirmationCode,
                    IsApproved = reservation.IsApproved,
                    TotalPrice = reservation.TotalPrice
                }
            };

            return View(updatereservationpagevm);
        }
        [HttpPost]
        public ActionResult UpdateReservation(ReservationVM reservation)
        {
            // Güncellenecek rezervasyonu veritabanından çekiyoruz
            Reservation updated = _reservationRep.Find(reservation.ID);
            if (updated == null)
            {
                return HttpNotFound();
            }

            // Güncelleme işlemini gerçekleştiriyoruz
            updated.ConfirmationCode = reservation.ConfirmationCode;
            updated.IsApproved = reservation.IsApproved;
            updated.TotalPrice = reservation.TotalPrice;

            // Güncelleme işlemini veritabanına kaydediyoruz
            _reservationRep.Update(updated);

            // "Index" sayfasına yönlendirme yapıyoruz, güncelleme işlemi tamamlandıktan sonra bu sayfaya gideceğini belirtiyoruz
            return RedirectToAction("Index");
        }
        
        public ActionResult DeleteReservation(int id)
        {
            _reservationRep.Delete(_reservationRep.Find(id));

            return RedirectToAction("Index");

        }
    }
}