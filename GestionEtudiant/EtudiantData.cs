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

namespace GestionEtudiant
{
    public static class EtudiantData
    {
        public static List<Etudiant> etudiants { get; private set; }

        static EtudiantData()
        {
            var temp = new List<Etudiant>();

            AddUser(temp);
            AddUser(temp);
            AddUser(temp);

            etudiants = temp.OrderBy(i => i.FullName).ToList();
        }

        static void AddUser(List<Etudiant> users)
        {
            SqliteDB sqliteDb = new SqliteDB();
            string data = sqliteDb.SelectAllEtudiant();
            String[] lineEtudiants = data.Split("\n");
            String[] etudiants = new string[lineEtudiants.Length];
            for(int i = 0; i < lineEtudiants.Length; i++)
            {
                String[] etudiantsSplited = lineEtudiants[i].Split(" ");
                users.Add(new Etudiant()
                {
                    FullName = etudiantsSplited[1] + " " + etudiantsSplited[2],
                    Absent = Convert.ToBoolean(etudiantsSplited[3]),
                    Cin = Convert.ToInt32(etudiantsSplited[0])
                }) ;

            }
            

           
        }
    }
}