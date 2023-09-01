using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCAdmin.Controllers
{
    public class TicketController : Controller
    {
        TicketRepository _ticketRep;
        public TicketController()
        {
            _ticketRep = new TicketRepository();
        }


        private List<TicketVM> GetTickets()
        {
            return _ticketRep.Select(x => new TicketVM
            {
                ID = x.ID,
                TotalPrice = x.Price,
                Type = x.Type

            }).ToList();


        }
        private decimal CalculateTicketPrice(TicketType type, decimal basePrice)
        {
            switch (type)
            {
                case TicketType.Standart:
                    return basePrice;
                case TicketType.VIP:
                    return basePrice * 0.5m;
                case TicketType.Student:
                    return basePrice * 0.6m;
                default:
                    return basePrice;
            }
        }

        

        public ActionResult Index()
        {

            List<TicketVM> tickets = GetTickets();
            TicketListPageVM ticketList = new TicketListPageVM
            {
                Tickets = tickets,
            };

            return View(ticketList);
        }

        [HttpGet]
        public ActionResult AddTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTicket(TicketVM ticket)
        {
            decimal basePrice = 1000; // Varsayılan standart fiyat
            decimal calculatedPrice = CalculateTicketPrice(ticket.Type, basePrice);

            Ticket newTicket = new Ticket
            {
                ID = ticket.ID,
                Type = ticket.Type,
                Price = calculatedPrice
            };
            _ticketRep.Add(newTicket);
          
            // Yeni bilet nesnesini kullanarak işlemleri yapabilirsiniz (örneğin, kaydetmek)

            return RedirectToAction("Index");
        }
    }
}