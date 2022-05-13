using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models
{
    public class Film
    {
        public int FilmID { get; set; }
        public String Nume { get; set; }
        public int? GenID { get; set; }
        public String Gen { get; set; }
        public DateTime? DataLansare { get; set; }
        public String Durata { get; set; }
        public IEnumerable<String> CasaProductie { get; set; }
        public IEnumerable<String> CaseProductie { get; set; }
    }
}