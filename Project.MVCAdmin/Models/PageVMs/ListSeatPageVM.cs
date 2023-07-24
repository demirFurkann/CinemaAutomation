using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class ListSeatPageVM
    {
        public List<SeatVM> Seats { get; set; }
        public List<SaloonVM> Saloons { get; set; }
        public SaloonVM Saloon { get; set; }
    }
}