using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proiect.Models;
using proiect.ViewModels;
using System.Data;

namespace proiect.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        private DatabaseController dbController = new DatabaseController();

        public ViewResult Index()
        {

            DataSet Tabela = dbController.SQL(dbController.SelectFilme, "Film");
            DataTable dataTable = Tabela.Tables["Film"];
            ViewBag.Filme = dataTable;

            Tabela = dbController.SQL(dbController.SelectGenuri, "Gen");
            dataTable = Tabela.Tables["Gen"];

            ViewBag.Genuri = dataTable;


            return View();
        }
        public ViewResult New()
        {

            List<String> genuri = new List<String>();
            DataSet Tabela = dbController.SQL(dbController.SelectGenuri, "Gen");
            DataTable dataTable = Tabela.Tables["Gen"];

            foreach (DataRow currentRow in dataTable.Rows)
            {
                genuri.Add(currentRow["Nume"] as String);
            }

            List<String> caseProductie = new List<String>();
            Tabela = dbController.SQL(dbController.SelectCaseProductie, "CasaProductie");
            dataTable = Tabela.Tables["CasaProductie"];

            foreach(DataRow currentRow in dataTable.Rows)
            {
                caseProductie.Add(currentRow["Nume"] as String);
            }

            var viewModel = new FilmViewModel
            {
                Genuri = genuri,
                CaseProductie = caseProductie
            };

            return View("FilmForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Film movie)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new FilmViewModel(movie);

                return View("FilmForm", viewModel);
            }

            List<String> genuri = new List<String>();
            DataSet Tabela = dbController.SQL(dbController.SelectGenuri, "Gen");
            DataTable dataTable = Tabela.Tables["Gen"];

            foreach (DataRow currentRow in dataTable.Rows)
            {
                if ((string)currentRow["Nume"] == movie.Gen)
                    movie.GenID = currentRow[0] as int?;
            }

            Tabela = dbController.SQL(dbController.SelectCaseProductie, "CasaProductie");
            dataTable = Tabela.Tables["CasaProductie"];

          
            if (movie.FilmID == 0)
                dbController.SQL(dbController.AdaugareFilm(movie), "Film");
            else
            {
                dbController.SQL(dbController.ModificareFilm(movie), "Film");
            }

            if (movie.CasaProductie != null)
            {
                foreach (string casa in movie.CasaProductie)
                {
                    foreach (DataRow currentRow in dataTable.Rows)
                    {
                        if ((string)currentRow["Nume"] == casa)
                            Tabela = dbController.SQL(dbController.AdaugareCasaProductieFilm(movie.FilmID, currentRow[0] as int?), "CasaProductieFilm");
                    }

                }
            }

            return RedirectToAction("Index", "Film");
        }


        public ActionResult Edit(int id)
        {
            DataSet Tabela = dbController.SQL(dbController.CautareFilmById(id), "Film");
            DataTable dataTable = Tabela.Tables["Film"];

            Film film = new Film();
            film.FilmID = id;

            foreach (DataRow currentRow in dataTable.Rows)
            {
                film.Nume = currentRow["Nume"] as String;
                film.Durata = (currentRow["Durata"] as String);
                film.DataLansare = currentRow["DataLansare"] as DateTime?;
            }

            if (film == null)
                return HttpNotFound();

            List<String> genuri = new List<String>();
            Tabela = dbController.SQL(dbController.SelectGenuri, "Gen");
            dataTable = Tabela.Tables["Gen"];

            foreach (DataRow currentRow in dataTable.Rows)
            {
                genuri.Add(currentRow["Nume"] as String);
            }


            List<String> caseProductie = new List<String>();
            Tabela = dbController.SQL(dbController.SelectCaseProductie, "CasaProductie");
            dataTable = Tabela.Tables["CasaProductie"];

            foreach (DataRow currentRow in dataTable.Rows)
            {
                caseProductie.Add(currentRow["Nume"] as String);
            }

            var viewModel = new FilmViewModel(film);
            viewModel.Genuri = genuri;
            viewModel.CaseProductie = caseProductie;

            return View("FilmForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            dbController.SQL(dbController.DeleteFilm(id), "Film");
            return RedirectToAction("Index", "Film");
        }

        public ActionResult DouaCaseProductie()
        {
            DataSet Tabela = dbController.SQL(dbController.GetFilmeCuCelPutin2CaseProductie(), "Film");
            DataTable dataTable = Tabela.Tables["Film"];
            ViewBag.CaseProductie = dataTable;

            return View();
        }

        public ViewResult VotateFilme()
        {
            DataSet Tabela = dbController.SQL(dbController.GetCeleMaiVotateFilme(), "Film");
            DataTable dataTable = Tabela.Tables["Film"];
            ViewBag.CaseProductie = dataTable;

            return View();
        }

        public ViewResult BestMovies()
        {
            DataSet Tabela = dbController.SQL(dbController.GetFilmeCuRatingMaxim(), "Film");
            DataTable dataTable = Tabela.Tables["Film"];
            ViewBag.CaseProductie = dataTable;

            return View();
        }

        public ViewResult OverMedieMovies()
        {
            DataSet Tabela = dbController.SQL(dbController.GetFilmePesteMedie(), "Film");
            DataTable dataTable = Tabela.Tables["Film"];
            ViewBag.CaseProductie = dataTable;

            return View();
        }
        
    }
}