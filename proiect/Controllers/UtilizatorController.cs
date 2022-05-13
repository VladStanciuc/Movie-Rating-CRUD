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
    public class UtilizatorController : Controller
    {
        private DatabaseController dbController = new DatabaseController();

        public ViewResult New()
        {
            var viewModel = new UtilizatorViewModel();

            viewModel.DataCreare = DateTime.Now;

            return View("UtilizatorForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Utilizator utilizator)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UtilizatorViewModel(utilizator);

                return View("UtilizatorForm", viewModel);
            }

            if (utilizator.UtilizatorID == 0)
                dbController.SQL(dbController.AdaugareUtilizator(utilizator), "Utilizator");
            else
            {
                dbController.SQL(dbController.ModificareUtilizator(utilizator), "Utilizator");
            }


            return RedirectToAction("Index", "Utilizator");
        }

        public ViewResult Index()
        {

            DataSet Tabela = dbController.SQL(dbController.SelectUtilizatori, "Utilizator");
            DataTable dataTable = Tabela.Tables["Utilizator"];
            ViewBag.Utilizatori = dataTable;
            

            return View();
        }

        public ActionResult Edit(int id)
        {
            DataSet Tabela = dbController.SQL(dbController.CautareUtilizatorById(id), "Utilizator");
            DataTable dataTable = Tabela.Tables["Utilizator"];

            Utilizator utilizator = new Utilizator();
            utilizator.UtilizatorID = id;

            foreach (DataRow currentRow in dataTable.Rows)
            {
           
                utilizator.Nume = currentRow["Nume"] as String;
                utilizator.Prenume = currentRow["Prenume"] as String;
                utilizator.DataCreare = currentRow["DataCreare"] as DateTime?; 
                utilizator.Parola = currentRow["Parola"] as String;
            }


            var viewModel = new UtilizatorViewModel(utilizator);

            return View("UtilizatorForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            dbController.SQL(dbController.DeleteUtilizator(id), "Utilizator");
            return RedirectToAction("Index", "Utilizator");
        }



        public ActionResult NewReview(int id)
        {
            List<String> filme = new List<String>();
            DataSet Tabela = dbController.SQL(dbController.SelectFilme, "Film");
            DataTable dataTable = Tabela.Tables["Film"];

            foreach (DataRow currentRow in dataTable.Rows)
            {
                filme.Add(currentRow["Nume"] as String);
            }

            Tabela = dbController.SQL(dbController.CautareUtilizatorById(id), "Utilizator");
            dataTable = Tabela.Tables["Utilizator"];

            Utilizator utilizator = new Utilizator();
            utilizator.UtilizatorID = id;

            var viewModel = new UtilizatorViewModel(utilizator);
            viewModel.Filme = filme;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveReview(Utilizator utilizator)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UtilizatorViewModel(utilizator);

                return View("NewReview", viewModel);
            }

            DataSet Tabela = dbController.SQL(dbController.SelectFilme, "Film");
            DataTable dataTable = Tabela.Tables["Film"];

            foreach (DataRow currentRow in dataTable.Rows)
            {
                if ((string)currentRow["Nume"] == utilizator.Film)
                    utilizator.FilmID = currentRow[0] as int?;
            }

            
            if (utilizator.DescriereFilmID == 0)
                dbController.SQL(dbController.AdaugareReview(utilizator), "Utilizator");

            return Redirect("GetReviewsForUtilizator/" + utilizator.UtilizatorID);
        }
        public ActionResult GetReviewsForUtilizator(int id)
        {

            DataSet Tabela = dbController.SQL(dbController.GetReviewsForUtilizator(id), "Utilizator");
            DataTable dataTable = Tabela.Tables["Utilizator"];
            Utilizator utilizator = new Utilizator();
            utilizator.DescriereFilm = new DescriereFilm();

            utilizator.UtilizatorID = id;

                foreach (DataRow currentRow in dataTable.Rows)
                {
                    utilizator.DescriereFilm.Film = currentRow["NumeFilm"] as string;
                    utilizator.Nume = currentRow["Nume"] as string;
                    utilizator.Prenume = currentRow["Prenume"] as string;
                    utilizator.DescriereFilm.Rating = currentRow["Rating"] as byte?;
                    utilizator.DescriereFilm.Feedback = currentRow["Feedback"] as string;
                }
                ViewBag.Reviews = dataTable;
            ViewBag.ID = id;
            

            return View("Reviews");
        }
        public ViewResult ActiviUtilizatori()
        {
            DataSet Tabela = dbController.SQL(dbController.GetCeiMaiActiviUtilizatori(), "Utilizatori");
            DataTable dataTable = Tabela.Tables["Utilizatori"];
            ViewBag.Tabela = dataTable;

            return View();
        }
    }
}