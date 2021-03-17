using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GestionEtudiant
{
    public class Etudiants 
    {
        public string FullName;
        public string Cin { get; set; }
        public bool Absent { get; set; }
       

        public override string ToString()
        {
            return FullName;
        }
    }
}