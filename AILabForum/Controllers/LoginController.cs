using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AILabForum.Models;

namespace AILabForum.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autoryzacja(Models.User userModel)
        {
            using (AILabForumEntities db = new AILabForumEntities())
            {
                var userDetails = db.Users.Where(x => x.login == userModel.login && x.password == userModel.password).FirstOrDefault();
                //sprawdzenie podanych danych przy logowaniu, jeżeli klienta nie ma w bazie wyświetla komunikat o błedzie
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wprowadzono nieprawidłowe dane !!!";
                    return View("Index", userModel);
                }
                else
                {
                    //poprawne zalogowanie użytkownika, utworzenie plików sesji, przekierowanie do strony głównej aplikacji
                    Session["id"] = userDetails.id;
                    Session["login"] = userDetails.login;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            int userId = (int)Session["id"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
            //wylogowanie użytkownika, usunięcie sesji, powrót do strony logowania
        }
    }
}