using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models
{
    public class Utilizator
    {
        public int UtilizatorID { get; set; }
        public String Nume { get; set; }
        public String Prenume { get; set; }
        public string Rol { get; set; }

        public string Parola { get; set; }

        public DateTime? DataCreare { get; set; }
        public String Mail { get; set; }
        public DescriereFilm DescriereFilm { get; set; }
        public String Film { get; set; }
        public int DescriereFilmID { get; set; }
        public int? FilmID { get; set; }
        public byte? Rating { get; set; }
        public string Feedback { get; set; }
    }
}