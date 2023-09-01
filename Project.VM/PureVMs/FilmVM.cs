using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class FilmVM
    {
        public int ID { get; set; }
        public string MovieName { get; set; }
        public double Duration { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
        public string ImagePath { get; set; } // Sadece bir tane resim yolu tutulacak
        public List<SeansVM> Seanslar { get; set; }
        public List<string> ImagePaths { get; set; }
        public FilmVM()
        {
            Seanslar = new List<SeansVM>();
            ImagePaths = new List<string>();
        }
    }




}


