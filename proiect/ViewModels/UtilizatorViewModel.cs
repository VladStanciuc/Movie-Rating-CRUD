using proiect.Models;
using System;
using System.Collections.Generic;

namespace proiect.ViewModels
{
    public class UtilizatorViewModel
    {
        public int UtilizatorID { get; set; }
        public String Nume { get; set; }
        public String Prenume { get; set; }
        public string Parola { get; set; }
        public DateTime? DataCreare { get; set; }
        public String Mail { get; set; }
        public DescriereFilm DescriereFilm { get; set; }
        public String Film { get; set; }
        public String Rating { get; set; }
        public String Feedback { get; set; }
        public int DescriereFilmID { get; set; }
        public IEnumerable<String> Filme { get; set; }
        public String Rol { get; set; }


        public UtilizatorViewModel()
        {
            UtilizatorID = 0;
        }
        public UtilizatorViewModel(Utilizator ut)
        {
            UtilizatorID = ut.UtilizatorID;
            Nume = ut.Nume;
            Prenume = ut.Prenume;
            Parola = ut.Parola;
            DataCreare = ut.DataCreare;
            Mail = ut.Mail;
            DescriereFilm = ut.DescriereFilm;
            Film = ut.Film;
        }
    }
}