using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppProjet2Sondage.Models.Domaine;
using WebAppProjet2Sondage.Models;

namespace UnitTestProjet2
{
    [TestClass]
    public class vote
    {
        [TestMethod]
        public void CreationVote()
        {
            Vote monVote = new Vote(2);

            Assert.AreEqual(2, monVote.choixVote);
            Assert.AreEqual(DateTime.Today, monVote.dateDeVote);
        }



    }
}
