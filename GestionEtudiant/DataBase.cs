using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GestionEtudiant
{
    class DataBase
    {
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                                                  "ProjectDataBase.db3");

        public DataBase()
        {
            //Creating database, if it doesn't already exist 
            if (!File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Etudiant>();
                db.CreateTable<Filiere>();
                db.CreateTable<Matiere>();
                db.CreateTable<Professeur>();
                db.CreateTable<Absence>();
                AddFiliere();
                AddMatiere();
            }
            
        }

       
        public string AddProfesseur(string login,string password)
        {
            var data = new SQLiteConnection(dbPath);
            Professeur p1 = new Professeur();
            p1.login = login;
            p1.password = password;
            var table = data.Table<Professeur>();
            var d1 = table.Where(x => x.login == p1.login ).FirstOrDefault();
            if (d1 == null)
            {
                data.Insert(p1);
                return "Sucessfully Added";
            }
            else
                return "login already Exist";

        }
        public bool LoginValidate(string login, string password)
        {
            var data = new SQLiteConnection(dbPath);
            var table = data.Table<Professeur>();
            
            var d1 =table.Where(x => x.login == login && x.password == password).FirstOrDefault();

            if (d1 != null)
            {
                return true;
            }
            else
                return false;
        }
        public string SelectAllFiliere()
        {
            string data = "";
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Filiere>();
            foreach (var s in table)
            {
                data += s.IdFiliere + " " + s.NomFiliere + "\n";
            }
            return data;
        }

        public void insertAbsence(bool absent,string date,int idfiliere,int idmatiere,string cinetudiant)
        {
            var db = new SQLiteConnection(dbPath);
            var absence = new Absence();
            absence.Absent = absent;
            absence.IdFiliere = idfiliere;
            absence.IdMatiere = idmatiere;
            absence.cinE = cinetudiant;
            absence.date = date;
            db.Insert(absence);
        }
        public string SelectAllEtudiant()
        {
            string data = "";
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<Etudiant>();
            foreach (var s in table)
            {
                data += s.Cin + " " + s.Nom + " " + s.Prenom + "\n";
            }
            return data;
        }
        public void AddFiliere()
        {
            var listfiliere = new List<string>();
            listfiliere.Add("Informatique");
            listfiliere.Add("Industriel");
            listfiliere.Add("GTR");
            listfiliere.Add("GPMC");
            var db = new SQLiteConnection(dbPath);
            for (int i = 0; i < 4; i++)
            {
                Filiere newFiliere = new Filiere();
                newFiliere.NomFiliere = listfiliere[i];
                db.Insert(newFiliere);
            }
        }
        public string AddStudent(string Cin, string Nom, string Prenom, string Email, string Tel, string NomFiliere)
        {
            var db = new SQLiteConnection(dbPath);
            var newEtudiant = new Etudiant();
            newEtudiant.Cin = Cin;


            newEtudiant.Nom = Nom;
            newEtudiant.Prenom = Prenom;
            newEtudiant.Email = Email;
            newEtudiant.Tel = Tel;
          
            //adding filiere
            var f = db.Query<Filiere>("SELECT * FROM Filiere WHERE NomFiliere = ?", NomFiliere);
            if (f.Count() == 0)
            {
                return "Cette filière n'existe pas";
            }
            else
            {
                newEtudiant.IdFiliere = f[0].IdFiliere;
            }
            //adding all infos in db
            var tables = db.Table<Etudiant>().FirstOrDefault(x => x.Cin == Cin);
            if (tables == null)
            {
                db.Insert(newEtudiant);
                return "Ajouté avec succès";
            }
            else
                return "Déjà ajouté";
        }

        public Etudiant Search(string name, string filiere)
        {
            var db = new SQLiteConnection(dbPath);
            var EtudiantSearched = db.Query<Etudiant>("SELECT * from Etudiant, Filiere WHERE Etudiant.IdFiliere = Filiere.IdFiliere and Etudiant.Nom = ? and Filiere.NomFiliere = ? "
                , name, filiere);
            return EtudiantSearched.FirstOrDefault();
        }
        //pour les spinners
        public List<String> selectfilieres()
        {
            var db = new SQLiteConnection(dbPath);
            List<String> list = new List<string>();
            var tab_fil = db.Table<Filiere>();
            foreach (var f in tab_fil)
            {
                list.Add(f.NomFiliere);
            }
            return list;
        }

        public int getFiliereId(string filiere)
        {
            var db = new SQLiteConnection(dbPath);
            var tab_fil = db.Table<Filiere>();
            foreach (var f in tab_fil)
            {
                if (f.NomFiliere.Equals(filiere))
                {
                    return f.IdFiliere;
                }
            }
            return -1;
        }

        public int getMatiereId(string matiere)
        {
            var db = new SQLiteConnection(dbPath);
            var tab_fil = db.Table<Matiere>();
            foreach (var f in tab_fil)
            {
                if (f.NomMatiere.Equals(matiere))
                {
                    return f.Id;
                }
            }
            return -1;
        }

        public List<Etudiants> selectEtudiantFiliere(string filiere)
        {
            var db = new SQLiteConnection(dbPath);
            List<Etudiants> list = new List<Etudiants>();
            var tab_etu = db.Table<Etudiant>();
            foreach (var f in tab_etu)
            {
                if(f.IdFiliere == getFiliereId(filiere))
                {
                    list.Add(new Etudiants()
                    {
                        FullName = f.Nom + " " + f.Prenom,
                        Cin = f.Cin,
                        Absent = f.absent
                    });
                }
            }
            return list;
        }

        public List<Etudiants> selectAllEtudiants()
        {
            var db = new SQLiteConnection(dbPath);
            List<Etudiants> list = new List<Etudiants>();
            var tab_etu = db.Table<Etudiant>();
            foreach (var f in tab_etu)
            {
                list.Add(new Etudiants()
                {
                    FullName = f.Nom +" " + f.Prenom,
                    Cin = f.Cin,
                });
            }
            return list;
        }
        public List<String> selectmatieres(/*Filiere filiere*/)
        {
            var db = new SQLiteConnection(dbPath);
            List<String> list = new List<string>();
            var tab_mat = db.Table<Matiere>()/*.Where(x => x.IdFiliere == filiere.Id)*/;
            foreach (var f in tab_mat)
            {
                list.Add(f.NomMatiere);
            }
            return list;
        }
        public void AddMatiere()
        {
            var listmatiere = new List<string>();
            listmatiere.Add("c#");
            listmatiere.Add("php");
            listmatiere.Add("developpement mobile");
            listmatiere.Add("JAVA");
            var db = new SQLiteConnection(dbPath);
            for (int i = 0; i < 4; i++)
            {
                Matiere newmatiere = new Matiere();
                newmatiere.NomMatiere = listmatiere[i];
                db.Insert(newmatiere);
            }
        }

        public List<Absence> selectAbsence(string cin, string matiere, string filiere)
        {
            var db = new SQLiteConnection(dbPath);
            var fil = getFiliereId(filiere);
            var mat = getMatiereId(matiere);
            var absences = db.Query<Absence>("SELECT * FROM Absence WHERE cinE = ? and IdFiliere = ? and IdMatiere = ?", cin, fil, mat);
            return absences;

        }
        public int[] selectnombres(List<Absence> absences)
        {
            int nb_abs = 0;
            int nb_pre = 0;
            foreach (var one in absences)
            {
                if (one.Absent == true)
                {
                    nb_abs++;
                }
                else if (one.Absent == false)
                {
                    nb_pre++;
                }
            }
            int[] r = { nb_abs, nb_pre };
            return r;
        }
        public Absence selectdateabsence(List<Absence> absences, string date)
        {
            var one = absences.Where(x => x.date == date).FirstOrDefault();
            //Console.WriteLine("fatixa" + one.Absent + one.date);
            return one;
        }
        public void updateAbsence(int id, bool absent)
        {
            var db = new SQLiteConnection(dbPath);
            var absence = new Absence();
            absence.IdAbsence = id;
            absence.Absent = absent;
            db.Update(absence);

        }


        [Table("Professeur")]
            public class Professeur
        {
            [PrimaryKey, AutoIncrement, Column("id_Professeur")]
            public int Id { get; set; }
            [MaxLength(20)]
            public string login { get; set; }

            [MaxLength(20)]
            public string password { get; set; }

        }

        [Table("Etudiant")]
        public class Etudiant
        {
            [PrimaryKey, MaxLength(10)]
            public string Cin { get; set; }
            [MaxLength(10)]
            public string Nom { get; set; }
            [MaxLength(10)]
            public string Prenom { get; set; }

            [MaxLength(28)]
            public string Email { get; set; }
            [MaxLength(28)]
            public string Tel { get; set; }
            public int IdFiliere { get; set; }
            public bool absent { get; set; }


        }

        [Table("Absence")]
        public class Absence
        {
            [PrimaryKey, MaxLength(10),AutoIncrement]
            public int IdAbsence { get; set; }
            
            public int IdFiliere { get; set; }
            public int IdMatiere { get; set; }
            public string cinE { get; set; }
            [MaxLength(28)]
            public bool Absent { get; set; }
            public string date { get; set; }
        }
        [Table("Filiere")]
        public class Filiere
        {
            [PrimaryKey, AutoIncrement]
            public int IdFiliere { get; set; }
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


    }
}
