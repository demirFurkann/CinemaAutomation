using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class TicketListPageVM
    {
        public List<TicketVM> Tickets{ get; set; }
    }
}