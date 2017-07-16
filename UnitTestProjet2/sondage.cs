using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppProjet2Sondage.Models.Domaine;
using WebAppProjet2Sondage.Models;

namespace UnitTestProjet2
{
    [TestClass]
    public class sondage
    {
        [TestMethod]
        public void CreationSondage()
        {
            Sondage monSondage = new Sondage("Qui?", true);


            Assert.AreEqual("Qui?", monSondage.question);
            Assert.AreEqual("s", monSondage.TypeDeChoixDuSondage);
            Assert.AreEqual("a", monSondage.ValiditeSondage);
            Assert.AreEqual(0, monSondage.numeroDeSondage);

        }

        [TestMethod]
        public void DesactiverSondage()
        {
            Sondage monSondage = new Sondage("Qui?", true);
            monSondage.DesactiverSondage();
            Assert.AreEqual("d", monSondage.ValiditeSondage);
        }


    }
}
