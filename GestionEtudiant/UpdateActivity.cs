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
    [Activity(Label = "UpdateActivity")]
    public class UpdateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.update);
            DataBase sq = new DataBase();
            var nom =Intent.GetStringExtra("fullname");
            var absent =Intent.GetBooleanExtra("absent",false);
            var id = Intent.GetIntExtra("id",0);
            Console.WriteLine("***" + absent);
            var box = FindViewById<CheckBox>(Resource.Id.item);
            var appliquer = FindViewById<Button>(Resource.Id.apply);
            box.Checked = absent;
            box.Text = nom;
            appliquer.Click += delegate
            {
                sq.updateAbsence(id, box.Checked);
                Intent intent = new Intent(this, typeof(HomeActivity));
                StartActivity(intent);

            };
        }
    }
}