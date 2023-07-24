using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class ReservationVM
    {
        public int ID { get; set; }
        public string ConfirmationCode { get; set; }
        public bool IsApproved { get; set; }
        public decimal TotalPrice { get; set; }
        public string SeansNumber { get; set; }
        public string SeatNo { get; set; }
        public string MovieName { get; set; }
        public double Duration { get; set; }
        public int SaloonID { get; set; }
        public int FilmID { get; set; }
        public int SeatID { get; set; }
        public int SeansID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int AppUserID { get; set; }
        public FilmVM SelectedFilm { get; set; }

        // Seans bilgileri
        public SeansVM SelectedSeans { get; set; }
        public bool IsReserved { get; set; } //Bu özellik, seçilen film için rezervasyon yapılıp yapılmadığını temsil edecektir.
    }
}
