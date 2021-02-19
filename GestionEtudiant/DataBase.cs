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
                db.CreateTable<Professeur>();
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

    }
}
