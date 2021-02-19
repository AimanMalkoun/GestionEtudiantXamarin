using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GestionEtudiant
{
    [Activity(Label = "Gestion Etudiant",
        MainLauncher = true,
        Theme = "@style/Theme.Splash",
        NoHistory = true,
        Icon = "@drawable/logo_small")]
    public class SplachScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            StartActivity(typeof(PageLogin));
        }
    }
}