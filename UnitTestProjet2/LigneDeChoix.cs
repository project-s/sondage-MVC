using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppProjet2Sondage.Models.Domaine;
using WebAppProjet2Sondage.Models;

namespace UnitTestProjet2
{
    [TestClass]
    public class ligneDeChoix
    {
        [TestMethod]
        public void CreationLigneDeChoix()
        {
            LigneDeChoix maLigne = new LigneDeChoix(1, "Mon premier choix");

            Assert.AreEqual(1, maLigne.indexChoix);
            Assert.AreEqual("Mon premier choix", maLigne.textChoix);
        }



    }
}
