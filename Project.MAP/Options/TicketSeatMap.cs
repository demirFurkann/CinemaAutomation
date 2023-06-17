using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class TicketSeatMap:BaseMap<TicketSeat>
    {
        public TicketSeatMap()
        {
            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.SeatID,
                x.TicketID
            });
        }
    }
}
