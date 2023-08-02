using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ReservationTools
{
    public class CartItem
    {
        public int ID { get; set; }
        public int SeatID { get; set; }
        public int? SeansID { get; set; }
        public DateTime SeansStartTime { get; set; }
        public string SeatNumber { get; set; }
        public string Row { get; set; }
        public string SaloonNo { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public SeatStatus SeatStatus { get; set; }
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