using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAppProjet2Sondage.Models.Domaine;

namespace WebAppProjet2Sondage.Models
{
    public class DAL
    {
        private int NombreDeVotants = 0;

        //CCI : static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tsdi01\Documents\PROJETS\Projet2\Projet2Sondage\WebAppProjet2Sondage\App_Data\DB_Sondage.mdf;Integrated Security=True";
        //homestatic string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\xuperseb\Documents\projet2_main\Projet2Sondage\WebAppProjet2Sondage\App_Data\DB_Sondage.mdf;Integrated Security=True";

        static string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tsdi01\Desktop\projet2_main\Projet2Sondage\WebAppProjet2Sondage\App_Data\DB_Sondage.mdf;Integrated Security = True";

        public void CreateSondage(Sondage monSondage)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();

                //initiation de la date
                DateTime date = DateTime.Today;
                string dateString = date.Year.ToString("00") + "/" + date.Month.ToString("00") + "/" + date.Day.ToString("00");

                //1 création du paramètre pour la question
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@question";  //on lui assigne un nom
                param.Value = monSondage.question; //on lui assigne une valeur
                                                   
                //1- ajout du vote dans la base
                SqlCommand cmd = new SqlCommand("INSERT INTO Sondage (DateDeCreationSondage, question, FK_idValiditeSondage, FK_idTypeDeChoixDuSondage) VALUES ('" + dateString + "', @question, 'a', '" + monSondage.TypeDeChoixDuSondage + "'); ", connection);

                cmd.Parameters.Add(param); //on rajoute le dans les paramètres à envoyer à la requête

                cmd.ExecuteNonQuery();

                //2- récupération de l'id du vote pour générer les URL
                cmd = new SqlCommand("SELECT MAX(numeroDeSondage) AS max_id FROM Sondage;", connection);
                monSondage.AffecterNumeroSondage((int)cmd.ExecuteScalar());

                //3- générer les url
                monSondage.GenererUrl("vote", date, monSondage.numeroDeSondage);
                monSondage.GenererUrl("desactivation", date, monSondage.numeroDeSondage);

                //4- update du sondage dans la base
                cmd = new SqlCommand("UPDATE Sondage SET lienVote = '" + monSondage.lienVote + "', lienDesactivation='" + monSondage.lienDesactivation + "' WHERE numeroDeSondage = " + monSondage.numeroDeSondage + ";", connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void AjoutLignesDeChoix(List<LigneDeChoix> MaListe, int numeroSondage)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int i = 1;
                foreach (var ligne in MaListe)
                {
                    //1 création du paramètre pour l'ajout de la ligne
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@ligneChoix";  //on lui assigne un nom
                    param.Value = ligne.textChoix; //on lui assigne une valeur
                    SqlCommand cmd = new SqlCommand("INSERT INTO LigneChoix (indexChoix, texteChoix, FK_numeroDeSondage) VALUES (" + i + ", @ligneChoix, " + numeroSondage + "); ", connection);
                    cmd.Parameters.Add(param); //on rajoute le dans les paramètres à envoyer à la requête
                    cmd.ExecuteNonQuery();
                    ++i;
                }
            }
        }

        public Sondage AfficherVote(string monUrl)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //initiation du reader
                SqlDataReader rdr = null;
                //on récupère les infos du sondage
                //si la requête sur le lien de vote ne donne aucun résultat, on testera si c'est un lien de désactivation
                SqlCommand cmd = new SqlCommand("SELECT numeroDeSondage, question, FK_idTypeDeChoixDuSondage, FK_idValiditeSondage, numeroDeChoix, indexChoix, texteChoix FROM [dbo].[Sondage] INNER JOIN[DBO].[LigneChoix] ON FK_numeroDeSondage = numeroDeSondage WHERE lienVote = '" + (string)monUrl + "'ORDER BY indexChoix; ", connection);
                //exécution de la requete et stockage des résultats
                rdr = cmd.ExecuteReader();

                if (rdr.HasRows) //si le reader contient des données
                {
                    //alors c'est un lien de vote
                    //afficher les infos
                    //vérifier que le sondage n'est pas désactivé  <------------------------

                    //on lit le premier enregistrement du reader pour créer le sondage
                    rdr.Read();

                    bool typeDeSondage;
                    if ((string)rdr["FK_idTypeDeChoixDuSondage"] == "m")
                    {
                        typeDeSondage = true;
                    }
                    else
                    {
                        typeDeSondage = false;
                    }


                    //on remplit sondage
                    //création du sondage
                    Sondage monSondage = new Sondage((string)rdr["question"], typeDeSondage);
                    monSondage.AffecterNumeroSondage((int)rdr["numeroDeSondage"]);
                    //remplissage des lignes de choix
                    monSondage.ligneDeChoix.Add(new LigneDeChoix((int)rdr["indexChoix"], (string)rdr["texteChoix"]));
                    //permet d'ajouter le numero de choix dans la ligne de choix
                    monSondage.ligneDeChoix.Last().SetNumeroDeChoix((int)rdr["numeroDeChoix"]);
                    monSondage.SetLienDeVote(monUrl);


                    //on Vérifie si le sondage est encore actif
                    if ((string)rdr["FK_idValiditeSondage"] == "a")
                    {
                        //s'il est actif on traite l'affichage
                        while (rdr.Read())
                        {
                            monSondage.ligneDeChoix.Add(new LigneDeChoix((int)rdr["indexChoix"], (string)rdr["texteChoix"]));
                            monSondage.ligneDeChoix.Last().SetNumeroDeChoix((int)rdr["numeroDeChoix"]);
                        }
                        //doit renvoyer la vue vote/index -> actif et numéroSondage <> 0
                        return monSondage;
                    }
                    else
                    {
                        //c'est un sondage désactivé
                        while (rdr.Read())
                        {
                            monSondage.ligneDeChoix.Add(new LigneDeChoix((int)rdr["indexChoix"], (string)rdr["texteChoix"]));
                            monSondage.ligneDeChoix.Last().SetNumeroDeChoix((int)rdr["numeroDeChoix"]);
                        }
                        monSondage.DesactiverSondage(); 
                        //doit renvoyer la vue Resultats/index -> désactivé et numéroSondage <> 0
                        return monSondage;
                    }

                }
                else
                {
                    rdr.Close();
                    //sinon, on vérifier si c'est un lien de désactivation
                    cmd = new SqlCommand("SELECT numeroDeSondage, question, FK_idTypeDeChoixDuSondage, FK_idValiditeSondage, numeroDeChoix, indexChoix, texteChoix FROM [dbo].[Sondage] INNER JOIN[DBO].[LigneChoix] ON FK_numeroDeSondage = numeroDeSondage WHERE lienDesactivation = '" + monUrl + "' ORDER BY indexChoix; ", connection);
                    //exécution de la requete et stockage des résultats
                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows) //si le reader contient des données
                    {
                        rdr.Read();
                        bool typeDeSondage;
                        if ((string)rdr["FK_idTypeDeChoixDuSondage"] == "m")
                        {
                            typeDeSondage = true;
                        }
                        else
                        {
                            typeDeSondage = false;
                        }

                        Sondage monSondage = new Sondage((string)rdr["question"], typeDeSondage);
                        monSondage.DesactiverSondage(); //On Désactive le sondage
                        monSondage.AffecterNumeroSondage(0);
                        rdr.Close();
                        //requete pour mettre à jour la base de donnée => FK_idValiditeSondage = 'd'
                        cmd = new SqlCommand("UPDATE Sondage SET FK_idValiditeSondage = 'd' WHERE lienDesactivation = '" + monUrl + "';", connection);
                        cmd.ExecuteNonQuery();


                        monSondage.SetLienDeVote(monUrl);
                        //doit renvoyer la vue home/index -> désactivé et numéroSondage = 0
                        return monSondage;
                    }
                    else
                    {
                        //sinon c'est un lien inconnu et on affiche la page d'accueil.
                        //+ message d'erreur
                        Sondage monSondage = new Sondage("question", true);
                        monSondage.SetLienDeVote(monUrl);
                        //doit renvoyer la vue home/index -> actif et numéroSondage <= 0
                        return monSondage;
                    }
                }

            } //fin du using

        }//fin AfficherVote()

        public void CreateVote(Vote monVote)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                          
                //initiation de la date
                DateTime date = DateTime.Today;
                string dateString = date.Year.ToString("00") + "/" + date.Month.ToString("00") + "/" + date.Day.ToString("00");

                //ajout du vote dans la base
                SqlCommand cmd = new SqlCommand("INSERT INTO Vote (dateDeVote, FK_numeroDeChoix) VALUES ('" + dateString + "', " + monVote.choixVote + " ); ", connection);
                cmd.ExecuteNonQuery();
                
            }
        }

        public void CalculVotants(Sondage monSondage)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                NombreDeVotants = 0;
                foreach (var ligne in monSondage.ligneDeChoix)
                {
                    //pour chaque ligne de choix du sondage
                    //récupérer le numero de choix et faire une requète dans vote
                    //pour compter pour chaque ligne de choix le nombre de votre
                    //mettre cette donnée dans la ligne de choix avec une méthode de la classe
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Vote WHERE FK_numeroDeChoix = " + ligne.numeroDeChoix + ";", connection);
                    int nbVote = (int)command.ExecuteScalar();
                    ligne.SetNbVotants(nbVote);
                    NombreDeVotants = NombreDeVotants + nbVote;
                }
                monSondage.SetNbTotalVotants(NombreDeVotants);
            }
        }

        public void ajouterCookie(string url)
        {
            //Create a Cookie with a suitable Key.
            HttpCookie voteCookie = new HttpCookie(url);

            //Set the Cookie value.
            voteCookie.Values[url] = "avote";

            //set the expire time
            voteCookie.Expires = DateTime.Now.AddYears(1);

            //Add the Cookie to Browser.
            HttpContext.Current.Response.Cookies.Add(voteCookie);
        }

        public Boolean lireCookie(string url)
        {
            HttpCookie myCookie = new HttpCookie(url);
            myCookie = HttpContext.Current.Request.Cookies[url];

            // Read the cookie information and display it.
            if (myCookie==null)
            {
                //le cookie n'existe pas -> peut voter
                return true;
            }
            else
            {
                //le cookie existe -> a voté ou a vu les résultats
                return false;
            }
        }



    }
}