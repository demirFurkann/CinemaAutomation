using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class TicketSeansMap:BaseMap<TicketSeans>
    {
        public TicketSeansMap()
        {
            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.SeansID,
                x.TicketID
            });
            
        }
    }
}
