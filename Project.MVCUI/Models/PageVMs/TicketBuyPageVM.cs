using Project.ENTITIES.Models;
using Project.MVCUI.OutherRequestModel;
using Project.VM.AdminPureVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class TicketBuyPageVM
    {
        public TicketVM Ticket { get; set; }
        public BoxOfficeVM BoxOffice { get; set; }
        public SeansVM Seans { get; set; }
        public AdminUserVM AdminUser { get; set; }
        public ReservationVM Reservation { get; set; }

        public List<SeatVM>Seats { get; set; }
        public SeatVM Seat { get; set; }
        public PaymentRequestModel PaymentRM{ get; set; }


       
    }
}