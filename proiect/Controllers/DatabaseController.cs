using System;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using proiect.Models;

namespace proiect.Controllers
{
    public class DatabaseController : Controller
    {

        public String connectionString = "Server=localhost;Database=ProiectBD;User ID=sa;pwd=1234%asd";
        public String SelectUtilizatori = "Select * from utilizator";
        public String SelectFilme = "Select * from film";
        public String SelectGenuri = "Select * from gen";
        public String SelectCaseProductie = "Select * from CasaProductie";


        DataSet ds = new DataSet();


        public DataSet SQL(String query, String Tabela)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter sqlData = new SqlDataAdapter(query, conn);

                conn.Open();
                sqlData.Fill(ds, Tabela);
                conn.Close();
            }
            return (ds);
        }

        public String CautareUtilizatorById(int id)
        {
            String cautareUtilizatorById = "select Nume, Prenume, Parola, DataCreare from Utilizator where UtilizatorID = " + id;
            return cautareUtilizatorById;
        }

        public String CautareUtilizator(Utilizator utilizator)
        {
            String cautareUtilizator = "select Nume, Prenume, Parola, DataCreare from Utilizator where UtilizatorID = " + utilizator.UtilizatorID;
            return cautareUtilizator;
        }
        public String AdaugareUtilizator(Utilizator utilizator)
        {
            String adaugareUtilizator = "insert into utilizator" +
                              "(Nume, Prenume, Parola, DataCreare) VALUES" +
                               "('"+ utilizator.Nume+ "','" + utilizator.Prenume +"', '"+ utilizator.Parola +"', '"+ DateTime.Now +"')";

            return adaugareUtilizator;
        }

        public String ModificareUtilizator(Utilizator utilizator)
        {
            String modificareUtilizator = "update utilizator set " +
                              "Nume = " + "'" + utilizator.Nume + "'," +
                              "Prenume = " + "'" + utilizator.Prenume + "'," +
                              "DataCreare = " + "'" + utilizator.DataCreare + "'," +
                              "Parola = " + "'" + utilizator.Parola + "' " +
                              "where UtilizatorID = " + utilizator.UtilizatorID + ";" ;
                              
                               

            return modificareUtilizator;
        }

        public String AdaugareFilm(Film film)
        {
            String adaugareFilm = "insert into film" +
                              "(Nume, GenID, DataLansare, Durata) VALUES" +
                               "('" + film.Nume + "','" + film.GenID + "', '" + film.DataLansare + "', '" + film.Durata + "')";

            return adaugareFilm;
        }

        public String CautareFilmById(int id)
        {
            String cautareFilmById = "select Nume, GenID, DataLansare, Durata from Film where FilmID = " + id;
            return cautareFilmById;
        }

        public String ModificareFilm(Film film)
        {
            String modificareFilm = "update film set " +
                              "Nume = " + "'" + film.Nume + "'," +
                              "GenID = " + "'" + film.GenID + "'," +
                              "DataLansare = " + "'" + film.DataLansare + "'," +
                              "Durata = " + "'" + film.Durata + "' " +
                              "where FilmID = " + film.FilmID + ";";

            return modificareFilm;
        }

        public String PopulareGenuri()
        {
            String populareGenuri = "INSERT INTO Gen(Nume) VALUES('Actiune') " +
                                    "INSERT INTO Gen(Nume) VALUES('Thriller') " +
                                    "INSERT INTO Gen(Nume) VALUES('Familie') " +
                                    "INSERT INTO Gen(Nume) VALUES('Drama') " +
                                    "INSERT INTO Gen(Nume) VALUES('Comedie') ";
            return populareGenuri;
        }
        public String CautareGenById(int? id)
        {
            String cautareGenById = "select Nume from Gen where GenID = " + id;
            return cautareGenById;
        }
        public String CautareGenByNume(String nume)
        {
            String cautareGenById = "select GenID from Gen where nume like ' " + nume + "'";
            return cautareGenById;
        }

        public String CautareUserByMail(string mail)
        {
            String CautareUserByMail = "select UtilizatorID from Utilizator where mail like '"+mail+"'";
            return CautareUserByMail;
        }

        public String SelectUtilizatorRolByID(int id)
        {
            String SelectUtilizatorRolByID = "Select RolID from UtilizatorRol where UtilizatorID = " + id;
            return SelectUtilizatorRolByID;
        }
        public String CautareRolByID(int id)
        {
            String SelectUtilizatorRolByID = "Select Nume from Rol where RolID = " + id;
            return SelectUtilizatorRolByID;
        }

        public String InregistrareUtilizator(Utilizator ut)
        {
            String adaugareUtilizator = "insert into utilizator" +
                              "(mail, Parola, nume, prenume, datacreare) VALUES" +
                               "('" + ut.Mail + "','" + ut.Parola + "','" + ut.Nume + "','" + ut.Prenume + "','"+ut.DataCreare+"')";

            return adaugareUtilizator;
        }

        public String LoginUtilizator(Utilizator ut)
        {
            String FetchUtilizator = "select * from utilizator where mail like '"+ut.Mail+"'" + "and parola like '"+ut.Parola+"'";
            return FetchUtilizator;
        }

        public String GetAdminRole(Utilizator ut)
        {
            string GetAdmin = "select UtilizatorID from UtilizatorRol where RolID = " + 1 + "and UtilizatorID = "+ut.UtilizatorID;
            return GetAdmin;
        }

        public String DeleteFilm(int id)
        {
            string DeleteFilm = "delete from film where FilmId = " + id;
            return DeleteFilm;
        }

        public String DeleteUtilizator(int id)
        {
            string DeleteUtilizator =
                "delete from UtilizatorRol where UtilizatorID = " + id + " " + 
                "delete from Utilizator where UtilizatorID = " + id;
            return DeleteUtilizator;
        }

        public String GetReviewsForUtilizator(int id)
        {
            string GetReviews =
                "select Utilizator.UtilizatorID, Film.Nume as NumeFilm, Utilizator.Nume, Utilizator.Prenume, DescriereFilm.Rating, DescriereFilm.Feedback from " +
                "Film inner join DescriereFilm on DescriereFilm.FilmID = Film.FilmID " +
                "join Utilizator on DescriereFilm.UtilizatorID = Utilizator.UtilizatorID " +
                "where Utilizator.UtilizatorID = " + id;
            return GetReviews;
        }

        public String AdaugareReview(Utilizator ut)
        {
            string AdaugareReview = "insert into DescriereFilm (FilmID, UtilizatorID, Rating, Feedback) VALUES " +
                                    "('" + ut.FilmID + "', '" +
                                       ut.UtilizatorID + "', '" +
                                       ut.Rating + "', '" +
                                       ut.Feedback + "')";
            return AdaugareReview;
        }

        public String AdaugareCasaProductieFilm(int FilmID, int? CasaProductieID)
        {
            string AdaugareCasaProductieFilm = "insert into CasaProductieFilm (FilmID, CasaProductieID) select " + FilmID + "," + CasaProductieID +
                                        " where not exists(select 1 from CasaProductieFilm where FilmID = " + FilmID + " and CasaProductieID = " +
                                        CasaProductieID +")";
            return AdaugareCasaProductieFilm;
        }

        public String GetFilmeCasaProductie(int id)
        {
            string Get = "select Film.Nume from Film inner join CasaProductieFilm on Film.FilmID = CasaProductieFilm.FilmID " +
                        "inner join CasaProductie on CasaProductie.CasaProductieID = CasaProductieFilm.CasaProductieID " +
                        "where CasaProductie.CasaProductieID =" + id;
            return Get;
        }

        public String GetFilmeCuCelPutin2CaseProductie()
        {
            string query = "select Film.Nume from Film inner join CasaProductieFilm on Film.FilmID = CasaProductieFilm.FilmID " +
                         "inner join CasaProductie on CasaProductie.CasaProductieID = CasaProductieFilm.CasaProductieID " +
                         " group by Film.Nume " +
                         " having count(Film.Nume) > 2";

           return query;
        }

        public String GetCeiMaiActiviUtilizatori()
        {
            string query = "select top 10 Utilizator.Nume " +
                           "from Utilizator inner join DescriereFilm on Utilizator.UtilizatorID = DescriereFilm.UtilizatorID " +
                           "group by Utilizator.Nume " +
                           "order by count(*) desc ";
            return query;
        }

        public String GetCeleMaiVotateFilme()
        {
            string query = "select top 10 Film.Nume " +
                           "from Film inner join DescriereFilm on Film.FilmID = DescriereFilm.FilmID " +
                           "group by Film.Nume " +
                           "order by count(*) desc ";
            return query;
        }

        public String GetFilmeCuRatingMaxim()
        {
            string query = "select Film.Nume " +
                           "from Film inner join DescriereFilm on Film.FilmID = DescriereFilm.FilmID " +
                           "group by Film.Nume " +
                           "having sum(DescriereFilm.Rating)/ Count(*) = 5";
            return query;
        }

        public String GetFilmePesteMedie()
        {
            string query = "select Film.Nume from Film inner join DescriereFilm on Film.FilmID = DescriereFilm.FilmID " +
                           "group by Film.Nume " +
                           "having avg(1.0 * Rating) > (select avg(1.0 * Rating) from DescriereFilm)";
            return query;
        }

    }
}