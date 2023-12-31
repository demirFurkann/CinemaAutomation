﻿using Project.MVCUI.Models.ReservationTools;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class CartPageVM
    {
        public Cart Cart { get; set; }
        public SeansVM Seans { get; set; }
        public ReservationVM Reservation { get; set; }
        public SeatVM Seat { get; set; }

    }
}