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