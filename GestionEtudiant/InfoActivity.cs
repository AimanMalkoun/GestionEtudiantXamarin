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
    [Activity(Label = "Information", Theme = "@style/AppTheme", MainLauncher = false)]
    public class InfoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.info);
          /* DataBase sq = new DataBase();
            EditText nameStudent = FindViewById<EditText>(Resource.Id.nameStudent);
            Button buttonAbs = FindViewById<Button>(Resource.Id.butAbs);
            Button buttonPre = FindViewById<Button>(Resource.Id.butPre);
            nameStudent.Text = ;
            buttonAbs.Text = ;
            buttonPre.Text = ;*/

        }
    }
}