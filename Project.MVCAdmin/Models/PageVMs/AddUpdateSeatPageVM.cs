using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class AddUpdateSeatPageVM
    {
        public SeatVM Seat { get; set; }
        public List<SaloonVM> Saloons { get; set; }

    }
}