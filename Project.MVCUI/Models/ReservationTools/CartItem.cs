using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ReservationTools
{
    public class CartItem
    {
        public int ID { get; set; }
        public string SeatNumber { get; set; }
        public short Amount { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal
        {
            get
            {
                return Amount * Price;
            }
        }

        public CartItem()
        {
            Amount++;
        }

    }
}