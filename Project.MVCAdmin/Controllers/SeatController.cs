using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCAdmin.Controllers
{
    public class SeatController : Controller
    {
        SaloonRepository _saloonRep;
        SeatRepository _seatRep;
        public SeatController()
        {
            _saloonRep = new SaloonRepository();
            _seatRep = new SeatRepository();
        }


        private List<SaloonVM> GetSaloons()
        {
            return _saloonRep.Select(x => new SaloonVM
            {
                ID = x.ID,
                SaloonNumber = x.SaloonNumber,
            }).ToList();
        }

        private List<SeatVM> GetSeats(int SaloonID)
        {
            List<Seat> seat = _seatRep.Where(x => x.SaloonID == SaloonID).ToList();

            List<SeatVM> seatVM = seat.Select(x => new SeatVM
            {
                ID = x.ID,
                SaloonID = x.SaloonID.Value,
                Row = x.Row,
                SaloonNumber = x.Saloon.SaloonNumber,
                SeatNo = x.SeatNo,
                SeatStatus = x.SeatStatus,
                SeatPrice=x.SeatPrice,
            }).ToList();
            return seatVM;


        }

        public ActionResult Index()
        {
            List<SaloonVM> saloons = GetSaloons();

            ListSeatPageVM spvm = new ListSeatPageVM
            {
                Saloons = saloons,
                Seats = null
            };


            return View(spvm);
        }

        [HttpPost]
        public ActionResult Index(ListSeatPageVM model)
        {
            if (model.Saloon != null && model.Saloon.ID != 0)
            {
                List<SeatVM> seatList = GetSeats(model.Saloon.ID);
                model.Seats = seatList;
            }
            else
            {
                model.Seats = null;
            }

            model.Saloons = GetSaloons();

            return View(model);
        }
        public ActionResult AddSeat()
        {
            List<SaloonVM> saloons = GetSaloons();

            AddUpdateSeatPageVM spvm = new AddUpdateSeatPageVM
            {
                Saloons = saloons,
                Seat = new SeatVM()
            };
            return View(spvm);

        }
        [HttpPost]
        public ActionResult AddSeat(SeatVM seat)
        {
            Saloon saloon = _saloonRep.Find(seat.SaloonID);

            string seatRow = "abcdefgh";

            int totalSeatCount = saloon.Capacity;
            int seatsPerRow = totalSeatCount / seatRow.Length;
            int remainingSeats = totalSeatCount % seatRow.Length;


            for (int i = 0; i < seatRow.Length; i++)
            {
                int seatsInCurrentRow = seatsPerRow + (i < remainingSeats ? 1 : 0);
                for (int j = 1; j <= seatsInCurrentRow; j++)
                {
                    Seat s = new Seat
                    {
                        SeatNo = $"{j}",
                        Row = $"{seatRow[i]}",
                        Saloon = saloon,
                        SeatStatus= seat.SeatStatus,
                        SeatPrice= seat.SeatPrice,
                    };
                    _seatRep.Add(s);
                }

            }
            return RedirectToAction("Index");

        }
        public ActionResult UpdateSeat(int id)
        {
            List<SaloonVM> saloons = GetSaloons();

            SeatVM seat = _seatRep.Where(x => x.ID == id).Select(x => new SeatVM
            {
                ID = x.ID,
                Row = x.Row,
                SeatNo = x.SeatNo,
                SaloonNumber = x.Saloon.SaloonNumber,
                SaloonID=x.Saloon.ID
            }).FirstOrDefault();
            AddUpdateSeatPageVM spvm = new AddUpdateSeatPageVM
            {
                Saloons = saloons,
                Seat = seat,
            };
            return View(spvm);
        }
        [HttpPost]
        public ActionResult UpdateSeat(SeatVM seat)
        {
            Saloon saloon = _saloonRep.Find(seat.SaloonID);

            Seat updated = _seatRep.Find(seat.ID);
            updated.SeatNo=seat.SeatNo;
            updated.Row = seat.Row;
            updated.Saloon=saloon;

            _seatRep.Update(updated);

            return RedirectToAction("Index");

        }

        public ActionResult DeleteSeat(int id)
        {
            _seatRep.Delete(_seatRep.Find(id));

            return RedirectToAction("Index");
        }


    }
}