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
        TicketSeatRepository _ticketSeatRep;
        TicketRepository _ticketRep;
        public HomeController()
        {
            _filmRep = new FilmRepository();
            _seansRep = new SeansRepository();
            _seatRep = new SeatRepository();
            _ticketSeatRep = new TicketSeatRepository();
            _ticketRep = new TicketRepository();
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


        //Calısmnadı

        //private List<FilmVM> GetFilms()
        //{
        //    string serverPath = "~/Project.MVCAdmin/Pictures/"; // Resimlerin yüklendiği klasör yolu
        //    List<string> imagePaths = ImageUploader.GetImagePaths(serverPath);

        //    return _filmRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select((x, index) => new FilmVM
        //    {
        //        ID = x.ID,
        //        MovieName = x.MovieName,
        //        Duration = x.Duration,
        //        Type = x.Type,
        //        Info = x.Info,
        //        ImagePath = (index < imagePaths.Count) ? imagePaths[index] : string.Empty
        //    }).ToList();
        //}


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


            List<SeatVM> seats = GetSeats(seansId);

            ListSeansPageVM seansSeatsVM = new ListSeansPageVM
            {
                Film = film,
                Seans = new List<SeansVM> { seans },
                Seats = seats,

            };

            if (seansId != null)
            {
                TempData["seansId"] = seansId;
            }




            TempData["FilmAdi"] = film.MovieName;
            //TempData["SalonAdi"] = seans.SaloonNumber;
            //TempData["SeansTarihi"] = seans.StartTime;



            return View(seansSeatsVM);
        }


        public ActionResult AddToCart(int id, TicketBuyPageVM model)
        {


            if (id <= 0)
            {
                return Content("Geçersiz Koltuk ID'si");
            }

            Cart c = Session["add"] == null ? new Cart() : Session["add"] as Cart;

            Seat addSeat = _seatRep.Find(id);

            if (addSeat == null)
            {
                return Content("Koltuk Bulunamadı");
            }

            if (addSeat.SeatStatus == Project.ENTITIES.Enums.SeatStatus.Reserved || addSeat.SeatStatus == Project.ENTITIES.Enums.SeatStatus.Occupied)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }


            CartItem ci = new CartItem
            {
                ID = addSeat.ID,
                SeatNumber = addSeat.SeatNo,
                SeansStartTime = addSeat.Seans.StartTime,
                Price = addSeat.SeatPrice,
                SeatID = addSeat.ID,
                SeansID = addSeat.Seans.ID,
                SaloonNo = addSeat.Saloon.SaloonNumber,
                Row = addSeat.Row,

            };



            c.ReservationAdd(ci);
            Session["add"] = c;
            addSeat.SeatStatus = SeatStatus.Reserved;


            return Redirect(Request.UrlReferrer.ToString());
        }



        public ActionResult TicketBuy()
        {
            //AdminUser currentUser;
            //if (Session["BoxOfficeAttendant"] != null)
            //{
            //    currentUser = Session["BoxOfficeAttendant"] as AdminUser;
            //}
            if (Session["add"] != null)
            {
                Cart c = Session["add"] as Cart;
                CartPageVM cpvm = new CartPageVM
                {
                    Cart = c,
                };
                return View(cpvm);
            }

            return View();
        }

        [HttpPost]
        public ActionResult TicketBuy(TicketBuyPageVM model)
        {


            ViewBag.SepetBos = "Sepette Bilet Bulunmamaktadır";
            Cart sepet = Session["add"] as Cart;

            //SeansId'yi yakalamak için 
            int seansId = (int)TempData["seansId"];

            Ticket t = new Ticket();
            t.Price = sepet.TotalPrice;
            t.SeansID = seansId;
            _ticketRep.Add(t); // Ticket Id'si ni oluşturmak için

            foreach (CartItem item in sepet.Sepetim)
            {
                TicketSeat ts = new TicketSeat();
                ts.TicketID = t.ID;
                ts.SeatID = item.SeatID;

                Seat seat = _seatRep.Find(ts.SeatID);
                seat.SeatStatus = SeatStatus.Occupied;

                _ticketSeatRep.Add(ts);
            }

            Session.Remove("add");

            TempData["odeme"] = "Ödemeniz Alınmıştır... Teşekkürler";


            return RedirectToAction("Index");


        }

        [HttpPost]
        public ActionResult GetStatus(int seansId,int id)
        {
            List<Seat> seatsInSeans = _seatRep.Where(x => x.Seans.ID == seansId).ToList();

            // Belirli bir koltuğu alın
            Seat seat = _seatRep.Find(id);

            if (seatsInSeans == null || seatsInSeans.Count == 0)
            {
                return Json(new { ErrorMessage = "Koltuklar bulunamadı." });
            }

            if (seat == null)
            {
                return Json(new { ErrorMessage = "Koltuk bulunamadı." });
            }

            // Tüm seans koltuklarını dolaşarak durumları "Empty" olarak güncelleyin
            foreach (var seatInSeans in seatsInSeans)
            {
                if (seatInSeans.SeatStatus == SeatStatus.Occupied || seatInSeans.SeatStatus == SeatStatus.Reserved)
                {
                    seatInSeans.SeatStatus = SeatStatus.Empty;
                }
            }

            // Değişiklikleri veritabanına kaydedin
            _seatRep.Update(seat);

            return Json(new { SuccessMessage = "Koltuk durumları güncellendi." });
        }


    }


}