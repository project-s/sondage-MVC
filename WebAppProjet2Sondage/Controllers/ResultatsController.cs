using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppProjet2Sondage.Models;
using WebAppProjet2Sondage.Models.Domaine;

namespace WebAppProjet2Sondage.Controllers
{
    public class ResultatsController : Controller
    {
        // GET: Resultats
        public ActionResult Index(string monUrl)
        {

            //initiation de la DAL
            DAL dal = new DAL();

            Sondage monSondage;
            monSondage = dal.AfficherVote(monUrl);

            if (monSondage.numeroDeSondage != 0)
            {
                dal.CalculVotants(monSondage);

                if (dal.lireCookie(monUrl))
                {
                    dal.ajouterCookie(monUrl);
                }
                //tri de la liste des choix en fonction du nombre de vote décroissant
                monSondage.ligneDeChoix.Sort((col1, col2) => col1.nbVotants - col2.nbVotants);
                monSondage.ligneDeChoix.Reverse();

                return View(monSondage);
            }
            else
            {
                ViewBag.Information = "L'url entrée est inconnue";
                return RedirectToAction("index", "Home");
            }

        }
    }
}