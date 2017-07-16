using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProjet2Sondage.Models.Formulaire
{
    public class FormulaireCreationSondage
    {
        //c'est le contenu de la page du formulaire de création
        public int monNumerSondage { get; set; }
        public string question { get; set; }
        public string choix1 { get; set; }
        public string choix2 { get; set; }
        public string choix3 { get; set; }
        public string choix4 { get; set; }
        public string url { get; set; }
        public List<string> choix { get; set; }
        public bool TypeChoix { get; set; }
    }
}