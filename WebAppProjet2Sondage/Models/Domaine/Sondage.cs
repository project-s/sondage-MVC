using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProjet2Sondage.Models.Domaine
{
    public class Sondage
    {
        public int numeroDeSondage { get; private set; }
        public DateTime dateDeCreation { get; private set; }
        public string question { get; private set; }
        public List<LigneDeChoix> ligneDeChoix { get; private set; }
        public string TypeDeChoixDuSondage { get; private set; }
        public string ValiditeSondage { get; private set; }
        public string lienVote { get; private set; }
        public string lienDesactivation { get; private set; }
        public int nbTotalVotants { get; private set; }

        public Sondage(string maQuestion, bool monTypeDeChoixDuSondage)
        {
            //constructeur du sondage
            //La question et le type de choix du sondage sont nécessaire à la création
            this.question = maQuestion;
            this.numeroDeSondage = 0;
            this.ligneDeChoix = new List<LigneDeChoix>();
            if (!monTypeDeChoixDuSondage)
            {
                this.TypeDeChoixDuSondage="s";
            }
            else
            {
                this.TypeDeChoixDuSondage="m";
            }

            this.ValiditeSondage = "a"; //activé
        }

        public void AffecterNumeroSondage(int numeroDeSondage)
        {
            this.numeroDeSondage = numeroDeSondage;
        }
        
        public void AjouterLigneChoix(LigneDeChoix maLigneDeChoix)
        {
            //Ajoute une ligne de choix au sondage
            this.ligneDeChoix.Add(maLigneDeChoix);
        }

        public void DesactiverSondage()
        {
            this.ValiditeSondage = "d"; //désactivé
        }

        public void GenererUrl(string TypeDUrl, DateTime madateDeCreation, int monNumeroDeSondage)
        {
            string dico = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

            string Url="";

            if (TypeDUrl=="vote")
            {
                Random rand = new Random(DateTime.Now.Second);
                Url = Url + dico[rand.Next(0, dico.Length)] + dico[rand.Next(0, dico.Length)];
                Url = Url + monNumeroDeSondage;
                Url = Url + dico[rand.Next(0, dico.Length)];
                this.lienVote = Url;

            }
            else if (TypeDUrl == "desactivation")
            {
                Random rand2 = new Random(DateTime.Now.Millisecond);
                Url = Url + dico[rand2.Next(0, dico.Length)] + dico[rand2.Next(0, dico.Length)];
                Url = Url + monNumeroDeSondage;
                Url = Url + madateDeCreation.Second.ToString("00");
                Url = Url + dico[rand2.Next(0, dico.Length)];
                this.lienDesactivation = Url;
            }
        }

        public void SetNbTotalVotants(int monNbTotalVotants)
        {
            this.nbTotalVotants = monNbTotalVotants;
        }

        public void SetLienDeVote(string url)
        {
            this.lienVote = url;
        }

    }
}