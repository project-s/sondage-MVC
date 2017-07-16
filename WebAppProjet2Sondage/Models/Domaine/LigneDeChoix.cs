using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProjet2Sondage.Models.Domaine
{
    public class LigneDeChoix
    {
        public int nbVotants { get; private set; }
        public int numeroDeChoix { get; private set; }
        public int indexChoix { get; private set; }
        public string textChoix { get; private set; }
        


        public LigneDeChoix(int monIndexChoix, string monTexteChoix)
        {
            this.indexChoix = monIndexChoix;
            this.textChoix = monTexteChoix;
        }

        public void SetNumeroDeChoix(int monNumeroDeChoix)
        {
            this.numeroDeChoix = monNumeroDeChoix;
        }

        public void SetNbVotants(int monNbVotants)
        {
            this.nbVotants = monNbVotants;
        }
    }
}