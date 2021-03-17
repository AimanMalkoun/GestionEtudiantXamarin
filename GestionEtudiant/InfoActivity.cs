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
            //ces donnees sont issues de la page de recherche  
            string cin = Intent.GetStringExtra("cin");
            string filiere = Intent.GetStringExtra("filiere");
            string matiere = Intent.GetStringExtra("matiere");
            string fullname = Intent.GetStringExtra("fullname");

            TextView nameStudent = FindViewById<TextView>(Resource.Id.nameStudent);
            Button buttonAbs = FindViewById<Button>(Resource.Id.butAbs);
            Button buttonPre = FindViewById<Button>(Resource.Id.butPre);
            Button update = FindViewById<Button>(Resource.Id.update);
            EditText date = FindViewById<EditText>(Resource.Id.date);

            DataBase sq = new DataBase();
            var absences = sq.selectAbsence(cin, matiere, filiere);
            var result = sq.selectnombres(absences);
            nameStudent.Text = fullname;
            buttonAbs.Text = result[0].ToString();
            buttonPre.Text = result[1].ToString();
            update.Click += delegate
            {

                var etudiant = sq.selectdateabsence(absences, date.Text);
                if (etudiant == null)
                {
                    Toast.MakeText(this, "La date recherchée n'est pas valide pour cet étudiant", ToastLength.Long).Show();
                }
                else
                {
                    Intent intent = new Intent(this, typeof(UpdateActivity));
                    intent.PutExtra("fullname", fullname);
                    intent.PutExtra("date", date.Text);
                    intent.PutExtra("absent", etudiant.Absent);
                    intent.PutExtra("id", etudiant.IdAbsence);
                    StartActivity(intent);

                }
            };
        }
    }
}