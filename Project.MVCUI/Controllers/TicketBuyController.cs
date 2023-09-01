using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.PageVMs;
using Project.MVCUI.Models.ReservationTools;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class TicketBuyController : Controller
    {
        SeatRepository _seatRep;
        SeansRepository _seansRep;
        TicketRepository _ticketRep;
        TicketSeatRepository _ticketSeatRep;
        ReservationRepository _resRep;

        ReservationSeatRepository _resSeatRep;
        public TicketBuyController()
        {
            _ticketSeatRep = new TicketSeatRepository();
            _seansRep = new SeansRepository();
            _seatRep = new SeatRepository();
            _ticketRep = new TicketRepository();
            _resRep = new ReservationRepository();
            _resSeatRep = new ReservationSeatRepository();
        }







        public ActionResult AddToCart(int id)
        {

            if (id <= 0)
            {
                return Content("Geçersiz Koltuk ID'si");
            }

            Cart c = Session["add"] == null ? new Cart() : Session["add"] as Cart;

            Seat addSeat = _seatRep.Find(id);




            if (addSeat == null)
            {
                return Content("Koltuk bilgisi Bulunamadı");
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
                TicketType=addSeat.TicketType 
  
            };




            c.ReservationAdd(ci);
            Session["add"] = c;
            addSeat.SeatStatus = SeatStatus.Reserved;
            ApplyDiscounts(c, id);


            return Redirect(Request.UrlReferrer.ToString());
        }

        public void ApplyDiscounts(Cart cart, int id)
        {
            bool vipUser = Session["VipUser"] != null;
            bool student = Session["Student"] != null;

            decimal vipDiscount = 0.5m;    // VipUser indirimi oranı (%50)
            decimal studentDiscount = 0.6m; // Student indirimi oranı (%40)

            Seat addSeat = _seatRep.Find(id);
            bool vipTicketApplied = false; // İndirimli bilet kontrolü
            bool studentTicketApplied = false;  

            foreach (var item in cart.Sepetim)
            {
                if (!vipTicketApplied && vipUser)
                {
                    item.Price = addSeat.SeatPrice * vipDiscount; // Sadece bir adet bilet indirimli olacak
                    vipTicketApplied = true; // İndirimin sadece bir kez uygulandığından emin olmak için
                }
                else if (!studentTicketApplied && student)
                {
                    item.Price = addSeat.SeatPrice * studentDiscount;
                    studentTicketApplied = true;
                }
            }
       

        }









        public ActionResult TicketReservation()
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
        public ActionResult TicketReservation(TicketBuyPageVM model)
        {


            ViewBag.SepetBos = "Sepette Bilet Bulunmamaktadır";
            Cart sepet = Session["add"] as Cart;

            //SeansId'yi yakalamak için 
            int seansId = (int)TempData["seansId"];

            //Ticket t = new Ticket();
            //t.Price = sepet.TotalPrice;
            //t.SeansID = seansId;
            //_ticketRep.Add(t); // Ticket Id'si ni oluşturmak için
            Reservation r = new Reservation();


            r.TotalPrice = sepet.TotalPrice;
            r.SeansID = seansId;

            _resRep.Add(r);


            foreach (CartItem item in sepet.Sepetim)
            {
                //TicketSeat ts = new TicketSeat();
                //ts.TicketID = r.ID;
                //ts.SeatID = item.SeatID;

                ReservationSeat rs = new ReservationSeat();
                rs.ReservationID = r.ID;
                rs.SeatID = item.SeatID;




                Seat seat = _seatRep.Find(rs.SeatID);
                seat.SeatStatus = SeatStatus.Occupied;

                _resSeatRep.Add(rs);
            }

            Session.Remove("add");
            Session.Remove("VipUser");
            Session.Remove("Student");

            TempData["odeme"] = "Ödemeniz Alınmıştır... Teşekkürler";


            return RedirectToAction("Index","Home");


        }

        [HttpPost]
        public ActionResult GetStatus(int seansId, int id)
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