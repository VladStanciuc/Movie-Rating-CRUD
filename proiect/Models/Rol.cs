using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proiect.Models
{
    public class Rol
    {
        public int RolID { get; set; }
        public string Nume { get; set; }

        public static class Roluri
        {
            public const int Admin = 1;
            public const int Utilizator = 2;
            public const int Reviewer = 3;
        }

        public static class RoleName
        {
            public const string Admin = "Admin";
            public const string Utilizator = "Utilizator";
            public const string Reviewer = "Reviewer";
        }
    }
}