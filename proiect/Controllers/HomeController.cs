using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using proiect.Models;

namespace proiect.Controllers
{
    public class HomeController : Controller
    {
        DatabaseController dbController = new DatabaseController();
        public ActionResult Index()
        {
            DataSet Tabela = dbController.SQL(dbController.SelectUtilizatori, "Utilizator");
            DataTable dataTable = Tabela.Tables["Utilizator"];
            ViewBag.Utilizatori = dataTable;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult New(Utilizator utilizator)
        {
            dbController.SQL(dbController.AdaugareUtilizator(utilizator), "Utilizator");
            return RedirectToAction("Index");
        }
    }
}