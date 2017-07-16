using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppProjet2Sondage.Models.Domaine;
using WebAppProjet2Sondage.Models;
using WebAppProjet2Sondage.Models.Formulaire;

namespace WebAppProjet2Sondage.Controllers
{
    public class VoteController : Controller
    {
        // GET: Vote
        public ActionResult Index(string Url)
        {
            //initiation de la DAL
            DAL dal = new DAL();

            if (dal.lireCookie(Url))
            {
                //si le cookie return true -> peut voter, on traite la page de vote
                Sondage monSondage;
                monSondage = dal.AfficherVote(Url);

                if (monSondage.numeroDeSondage != 0)
                {
                    if (monSondage.ValiditeSondage == "a")
                    {
                        if (monSondage.TypeDeChoixDuSondage == "s")
                        {
                            ViewBag.type = "radio";
                        }
                        else
                        {
                            ViewBag.type = "checkbox";
                        }
                        dal.CalculVotants(monSondage);

                        ViewBag.InformationVote = TempData["InformationVote"];
                        return View(monSondage);
                    }
                    else
                    {
                        //ViewBag.Information = "Le sondage pour lequel vous avez entrer l'url est désactivé";
                        return RedirectToAction("index", "Resultats", new { monUrl = Url });
                    }
                }
                else
                {
                    if (monSondage.ValiditeSondage == "d")
                    {
                        //ViewBag.Information = "Le sondage a bien été désactivé";
                        TempData["Information"] = "<p class='Centrage'>Le sondage a bien été désactivé</p>";
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        //ViewBag.Information = "L'url entrée est inconnue";
                        TempData["Information"] = "<p class='Centrage'>L'url entrée est inconnue</p>";
                        return RedirectToAction("index", "Home");
                    }
                }
            }
            else 
            {
                //si le cookie return false -> ne peut pas voter, afficher la page de résultats
                return RedirectToAction("index", "Resultats", new { monUrl = Url });
            }
            

        }

        [HttpPost]
        public ActionResult CreationVote(FormulaireCreationSondage formulaireCreation)
        {
            //initiation de la DAL
            DAL dal = new DAL();
            
            //Vérifier si les champs sont cochés
            if (formulaireCreation.choix1 != null)
            {
                Vote monVote = new Vote(Int32.Parse(formulaireCreation.choix1));
                dal.CreateVote(monVote);
            }
            if (formulaireCreation.choix2 != null)
            {
                Vote monVote = new Vote(Int32.Parse(formulaireCreation.choix2));
                dal.CreateVote(monVote);
            }
            if (formulaireCreation.choix3 != null)
            {
                Vote monVote = new Vote(Int32.Parse(formulaireCreation.choix3));
                dal.CreateVote(monVote);
            }
            if (formulaireCreation.choix4 != null)
            {
                Vote monVote = new Vote(Int32.Parse(formulaireCreation.choix4));
                dal.CreateVote(monVote);
            }
            
            return RedirectToAction("index", "Resultats", new { monUrl = formulaireCreation.url });

        }

        [HttpPost]
        public ActionResult  AfficheResultats(FormulaireCreationSondage formulaireCreation)
        {
            return RedirectToAction("index", "Resultats", new { monUrl = formulaireCreation.url });
        }
    }
}