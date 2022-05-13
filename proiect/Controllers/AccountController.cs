using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using proiect.Models;
using proiect.ViewModels;
using System.Web.Security;
using System.Data;

namespace proiect.Controllers
{
    public class AccountController : Controller
    {
        private DatabaseController dbController = new DatabaseController();

        public ActionResult Login()
        {
            return View("Login", new Utilizator());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Utilizator model)
        {

            string mail = null;
            string parola = null;
            DateTime? datacreare = null;
            string nume = null;
            string prenume = null;
            int id = 0;
            DataSet Tabela = dbController.SQL(dbController.LoginUtilizator(model), "Utilizator");
            DataTable dataTable = Tabela.Tables["Utilizator"];
            foreach(DataRow currentRow in dataTable.Rows)
            {
                mail = currentRow["mail"] as string;
                parola = currentRow["Parola"] as string;
                datacreare = currentRow["DataCreare"] as DateTime?;
                nume = currentRow["Nume"] as string;
                prenume = currentRow["Prenume"] as string;
                id = Int32.Parse(currentRow[0].ToString());
            }
            Utilizator user = new Utilizator
            {
                Mail = mail,
                Parola = parola,
                DataCreare = datacreare,
                Nume = nume,
                Prenume = prenume,
                UtilizatorID = id
            };
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Error");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Mail, false);
                return RedirectToAction("Index", "Home");
            }


        }
        public ActionResult LoginAdmin()
        {
            return View("Login", new Utilizator());
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginAdmin(Utilizator model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            string mail = null;
            string parola = null;
            DateTime datacreare = DateTime.Now;
            string nume = null;
            string prenume = null;
            int id = 0;
            DataSet Tabela = dbController.SQL(dbController.LoginUtilizator(model), "Utilizator");
            DataTable dataTable = Tabela.Tables["Utilizator"];
            foreach (DataRow currentRow in dataTable.Rows)
            {
                mail = currentRow["mail"] as string;
                parola = currentRow["Parola"] as string;
                datacreare = DateTime.Parse(currentRow["DataCreare"] as string);
                nume = currentRow["Nume"] as string;
                prenume = currentRow["Prenume"] as string;
                id = Int32.Parse(currentRow["UtilizatorID"] as string);
            }
            Utilizator user = new Utilizator
            {
                Mail = mail,
                Parola = parola,
                DataCreare = datacreare,
                Nume = nume,
                Prenume = prenume,
                UtilizatorID = id
            };

            if (user == null)
            {

                ModelState.AddModelError("", "Incorrect email or password.");
                return View("Login");
            }
            else
            {   
                int userRole = 0;
                Tabela = dbController.SQL(dbController.GetAdminRole(model), "UtilizatorRol");
                dataTable = Tabela.Tables["UtilizatorRol"];
                foreach(DataRow currentRow in dataTable.Rows)
                {
                    userRole = Int32.Parse(currentRow["UtilizatorID"] as string);
                }

                if (userRole != 0)
                {
                    FormsAuthentication.SetAuthCookie(model.Mail, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "You must login as an administrator.");
                }

            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Roles.DeleteCookie();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            var viewModel = new Utilizator();
            viewModel.DataCreare = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Register(Utilizator ut)
        {
            string message = "";
            if (!ModelState.IsValid)
            {
                var viewModel = new UtilizatorViewModel(ut);

                return View("Register", viewModel);
            }
           if (ut.UtilizatorID == 0)
                {
                dbController.SQL(dbController.InregistrareUtilizator(ut), "Utilizator");
                }
            
            return View("Login");
        }
    }
}