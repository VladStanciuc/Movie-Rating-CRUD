using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proiect.Models;
using System.Collections;

namespace proiect.ViewModels
{
    public class FilmViewModel
    {
        public int FilmID { get; set; }
        public String Nume { get; set; }
        public int GenID { get; set; }
        public String Gen { get; set; }
        public DateTime? DataLansare { get; set; }
        public String Durata { get; set; }
        public IEnumerable<String> Genuri { get; set; }
        public IEnumerable<String> CaseProductie { get; set; }
        public IEnumerable<String> CasaProductie { get; set; }

        public FilmViewModel()
        {
            FilmID = 0;
        }

        public FilmViewModel(Film movie)
        {
            FilmID = movie.FilmID;
            Nume = movie.Nume;
            Gen = movie.Gen;
            DataLansare = movie.DataLansare;
            Durata = movie.Durata;
            CaseProductie = movie.CaseProductie;
        }
    }
}