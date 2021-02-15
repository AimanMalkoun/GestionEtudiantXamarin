using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;

namespace GestionEtudiant
{
    class SqliteDB
    {
        string dbPath = Path.Combine( System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Gestion.db3");

        public SqliteDB()
        {
            //Creating database, if it doesn't already exist 
            if (!File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Etudiant>();
                db.CreateTable<Filiere>();
                db.CreateTable<Matiere>();
                db.CreateTable<Professeur>();
            }
        }

        public string SelectAllFiliere()
        {
            string data = "";
            var db = new SQLiteConnection(dbPath);
            Console.WriteLine("Reading data From Table");
            var table = db.Table<Filiere>();
            foreach (var s in table)
            {
                data += s.Id + " " + s.NomFiliere + "\n";
            }
            return data;
        }

        public void updateEtudiant(string FullName, bool absent, int cin, int nbAbs, int nbPres)
        {
            var db = new SQLiteConnection(dbPath);
            var newEtudiant = new Etudiant();
            newEtudiant.Absent =  Convert.ToString(absent);
            newEtudiant.NbAbsence = nbAbs;
            newEtudiant.NbPresence = nbPres;
            string[] name = FullName.Split(" ");
            newEtudiant.Nom = name[0];
            newEtudiant.Prenom = name[1];
            newEtudiant.Cin = cin;
            db.Update(newEtudiant);
        }
        public string SelectAllEtudiant()
        {
            string data = "";
            var db = new SQLiteConnection(dbPath);
            Console.WriteLine("Reading data From Table");
            var table = db.Table<Etudiant>();
            foreach (var s in table)
            {
                data += s.Cin + " " + s.Nom + " " + s.Prenom + " " + s.Absent + " " + s.NbPresence + " " + s.NbAbsence + "\n";
            }
            return data;
        }

        [Table("Etudiant")]
        public class Etudiant
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Cin { get; set; }
            [MaxLength(10)]
            public string Nom { get; set; }
            [MaxLength(10)]
            public string Prenom { get; set; }

            [MaxLength(28)]
            public string Email { get; set; }
            [MaxLength(28)]
            public string Tel { get; set; }
            public int IdFiliere { get; set; }
            public int IdMatiere { get; set; }

            public int NbAbsence { get; set; }
            public int NbPresence { get; set; }
            [MaxLength(28)]
            public string Absent { get; set; }

        }


        [Table("Filiere")]
        public class Filiere
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [MaxLength(8)]
            public string NomFiliere { get; set; }
        }

        [Table("Matiere")]
        public class Matiere
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [MaxLength(8)]
            public string NomMatiere { get; set; }
        }

        [Table("Professeur")]
        public class Professeur
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [MaxLength(8)]
            public string Login { get; set; }
            [MaxLength(8)]
            public string Password { get; set; }
        }
    }
}