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
    public class Etudiant
    {
        public string FullName { get; set; }
        public int Cin { get; set; }
        public Boolean Absent { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}