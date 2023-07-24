using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class SeansVM
    {
        public int ID { get; set; }
        public string SeansNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SaloonID { get; set; }
        public string SaloonNumber { get; set; }
        public string MovieName { get; set; }
        public double Duration { get; set; }
        public int FilmID { get; set; }
        public int ReservationID { get; set; }
        public string ConfirmationCode { get; set; }


    }
}
