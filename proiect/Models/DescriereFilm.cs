using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models
{
    public class DescriereFilm
    {
        public int DescriereFilmID { get; set; }
        public string Film { get; set; }
        public int? FilmID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public byte? Rating { get; set; }
        public string Feedback { get; set; }
    }
}