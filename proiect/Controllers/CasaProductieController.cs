using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class CasaProductieController : Controller
    {
        private DatabaseController dbController = new DatabaseController();
        // GET: CasaProductie
        public ActionResult Index()
        {
            DataSet Tabela = dbController.SQL(dbController.SelectCaseProductie, "CaseProductie");
            DataTable dataTable = Tabela.Tables["CaseProductie"];
            ViewBag.CaseProductie = dataTable;

            return View();
        }

        public ActionResult ViewFilme(int id)
        {
            DataSet Tabela = dbController.SQL(dbController.GetFilmeCasaProductie(id), "CaseProductie");
            DataTable dataTable = Tabela.Tables["CaseProductie"];
            ViewBag.CaseProductie = dataTable;

            return View();
        }
    }
}