using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using TP1.BO;

namespace TP1
{
    class Program
    {

        static void Main(string[] args)
        {
            //Initialisation des données
            InitialiserDatas();

            //Afficher la liste des prénoms des auteurs dont le nom commence par "G"
            var prenomsStartByG = ListeAuteurs
                .Where(aut => aut.Nom.ToUpper().StartsWith("G")
                )
                .Select(aut => aut.Prenom);

            Console.WriteLine("Q1 - Afficher la liste des prenoms des auteurs dont le nom commence par \"G\"");

            foreach (var prenom in prenomsStartByG)
            {
                Console.WriteLine(" " + prenom);
            }
            Console.WriteLine();

            //Afficher l’auteur ayant écrit le plus de livres
            var auteur = ListeLivres
                //Grouper les livres par auteur
                .GroupBy(livre => livre.Auteur)
                //Compter les nombre de livre et trier par ordre décroissant
                .OrderByDescending(livre => livre.Count())
                //Prendre le premier de la liste (ne fonctionne pas en cas d'égalité)
                .FirstOrDefault().Key;
            Console.WriteLine("Q2 - Auteur ayant écrit le plus de livres");

            Console.WriteLine(" " + $"{auteur.Prenom} {auteur.Nom}");
            Console.WriteLine();

            // Afficher le nombre moyen de page par livre  par auteur
            var listeLivresParAuteur = ListeLivres.GroupBy(livre => livre.Auteur);

            Console.WriteLine("Q3 - Nombre moyen de page par livre par auteur:");
            foreach (var livre in listeLivresParAuteur)
            {
                Console.WriteLine(" " + $"Auteur: {livre.Key.Prenom} {livre.Key.Nom} Nombre de page moyen: {livre.Average(li => li.NbPages)}");
            }
            Console.WriteLine();

            // Afficher le titre du livre avec le plus de pages
            var livrePlusPage = ListeLivres
                .OrderByDescending(livre => livre.NbPages)
                .FirstOrDefault();
            Console.WriteLine($"Q4 - Livre avec le plus de page {livrePlusPage.Titre} avec {livrePlusPage.NbPages} pages");
            Console.WriteLine();

            // Afficher combien ont gagné les auteurs en moyenne
            var gainMoyen = ListeAuteurs.Average(aut => aut.Factures.Sum(facture => facture.Montant));
            Console.WriteLine($"Q5 - Gain moyen : {gainMoyen}");
            Console.WriteLine();

            // Afficher les auteurs et la liste de leurs livres
            Console.WriteLine("Q6 - Liste des livres par auteurs:");
            foreach (var livre in listeLivresParAuteur)
            {
                Console.WriteLine();
                Console.WriteLine($"Auteur : {livre.Key.Prenom} {livre.Key.Nom}");
                foreach (var l in livre)
                {
                    Console.WriteLine(" " + l.Titre);
                }
            }
            Console.WriteLine();

            // Afficher les titres de tous les livres triés par ordre alphabétique
            var ListeLivresAscTitre = ListeLivres.OrderBy(l => l.Titre);
            Console.WriteLine("Q7 - Liste des livres par odre alphabétique");
            foreach (var livre in ListeLivresAscTitre)
            {
                Console.WriteLine(" " + livre.Titre);
            }
            Console.WriteLine();


            // Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne
            var moyennePage = ListeLivres.Average(l => l.NbPages);
            var livresSupMoyenne = ListeLivres.Where(l => l.NbPages > moyennePage);
            Console.WriteLine("Q8 - Affiche les livres dont le nombre de pages est supérieur à la moyenne.");
            foreach (var livre in livresSupMoyenne)
            {
                Console.WriteLine(" " + $"{livre.Titre} {livre.NbPages}");
            }

            Console.WriteLine();

            // Afficher l'auteur ayant écrit le moins de livres
            var auteurMoinsDeLivres = ListeLivres
                .GroupBy(l => l.Auteur)
                .OrderBy(n => n.Count())
                .FirstOrDefault().Key;
            Console.WriteLine($"Q9 - Auteur ayant écrit le moins de livre: {auteurMoinsDeLivres.Prenom} {auteurMoinsDeLivres.Nom}");
            Console.WriteLine();

            Console.ReadKey();
        }

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }
    }
}
