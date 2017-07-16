using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppProjet2Sondage.Models;
using WebAppProjet2Sondage.Models.Domaine;
using WebAppProjet2Sondage.Models.Formulaire;

namespace WebAppProjet2Sondage.Controllers
{
    public class CreationController : Controller
    {
        // GET: Creation
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreationSondage(FormulaireCreationSondage formulaireCreation)
        {
            
            //initiation de la DAL
            DAL dal = new DAL();
            //initialtion du sondage
            Sondage monSondage = new Sondage(formulaireCreation.question, formulaireCreation.TypeChoix);
            
            //vérification des choix : 
            //on n'ajoute la ligne de choix que si la ligne a été renseignée
            if (!String.IsNullOrEmpty(formulaireCreation.choix1))
            {
                monSondage.ligneDeChoix.Add(new LigneDeChoix(1, formulaireCreation.choix1));
            }

            if (!String.IsNullOrEmpty(formulaireCreation.choix2))
            {
                monSondage.ligneDeChoix.Add(new LigneDeChoix(2, formulaireCreation.choix2));
            }

            if (!String.IsNullOrEmpty(formulaireCreation.choix3))
            {
                monSondage.ligneDeChoix.Add(new LigneDeChoix(3, formulaireCreation.choix3));
            }

            if (!String.IsNullOrEmpty(formulaireCreation.choix4))
            { 
                monSondage.ligneDeChoix.Add(new LigneDeChoix(4, formulaireCreation.choix4));
            }

            //création du sondage dans la base
            dal.CreateSondage(monSondage);


            int numero = monSondage.numeroDeSondage;
            //création des lignes de choix dans la base
            dal.AjoutLignesDeChoix(monSondage.ligneDeChoix, monSondage.numeroDeSondage);

            
            TempData["InformationVote"] = "<p>Lien de Vote a partager : /Vote/Index?Url=" + monSondage.lienVote + "<br /></p>";
            TempData["InformationVote"] = TempData["InformationVote"] + "<p>Lien de Résultatsr : /Resultats/Index?monUrl=" + monSondage.lienVote + "<br /></p>";
            TempData["InformationVote"] = TempData["InformationVote"] + "<p>lien de Désactivation à garder précautionneusement : /Vote/Index?Url=" + monSondage.lienDesactivation + "<p>";

            return RedirectToAction("index","Vote", new { Url = monSondage.lienVote });
        }


    }

}