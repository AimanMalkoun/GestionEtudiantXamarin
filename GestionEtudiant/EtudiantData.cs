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
    public class EtudiantData
    {
        public string filiere { get; set; }
        public List<Etudiants> etudiants { get; private set; }

        public EtudiantData(string filiere)
        {
            this.filiere = filiere;
            var temp = new List<Etudiants>();
            etudiants = new List<Etudiants>();
            AddUser(etudiants, filiere);
            AddUser(temp, filiere);
        }

        public void clean()
        {
            for(int i = 0; i < etudiants.Count; i++)
            {
                etudiants[i].Absent = false;
            }
        }
        
        public void AddUser(List<Etudiants> etudiants, string filiere)
        {
            
            DataBase dataBase = new DataBase();
            List < Etudiants > temp = dataBase.selectEtudiantFiliere(filiere);
            foreach(var e in temp)
            {
                etudiants.Add(new Etudiants()
                {
                    FullName = e.FullName,
                    Absent = e.Absent,
                    Cin = e.Cin
                });

            }
            
           



        }
    }
}