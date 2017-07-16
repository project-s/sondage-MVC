using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProjet2Sondage.Models.Domaine
{
    public class Vote
    {
        public int numeroDeVote { get; private set; }
        public DateTime dateDeVote { get; private set; }
        public int choixVote { get; private set; }  


        public Vote(int monChoixVote)
        {
            this.dateDeVote = DateTime.Today;
            this.choixVote = monChoixVote;
        }


    }
}