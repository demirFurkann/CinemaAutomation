using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ReservationTools
{
    public class Cart
    {

        Dictionary<int, CartItem> _sepetim;
        public Cart()
        {
            _sepetim = new Dictionary<int, CartItem>();
        }
        public List<CartItem> Sepetim
        {
            get
            {
                return _sepetim.Values.ToList();
            }
        }

        public void ReservationAdd(CartItem item)
        {
            // Koltuk eklendiyse bir daha aynı koltuğu eklememek için

            if (!_sepetim.ContainsKey(item.ID)) _sepetim.Add(item.ID, item);

        }

        public void ReservationDel(int id)
        {
            if (_sepetim[id].Amount > 0)
            {
                _sepetim[id].Amount--;
                return;
            }

            _sepetim.Remove(id);

        }

        public decimal TotalPrice
        {
            get
            {
                return _sepetim.Sum(x => x.Value.SubTotal);
            }
            set
            {
                
            }
        }
    }
}