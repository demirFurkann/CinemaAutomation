using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class SeatVM
    {
        public int ID { get; set; }
        public string SeatNo { get; set; }
        public string Row { get; set; }

        public int SaloonID { get; set; }
        public string SaloonNumber { get; set; }

        public SeatStatus SeatStatus { get; set; }

        public SeatVM()
        {
            SeatStatus = SeatStatus.Empty;
        }
    }
}
