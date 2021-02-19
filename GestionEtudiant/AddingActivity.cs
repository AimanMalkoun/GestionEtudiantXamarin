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
    [Activity(Label = "@string/addStu", Theme = "@style/AppTheme", MainLauncher = true)]
    class AddingActivity: Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.adding_student);

            DataBase sq = new DataBase();            

            EditText cin = FindViewById<EditText>(Resource.Id.cin);
            EditText nom = FindViewById<EditText>(Resource.Id.nom);
            EditText prenom = FindViewById<EditText>(Resource.Id.prenon);
            EditText email = FindViewById<EditText>(Resource.Id.email);
            EditText tele = FindViewById<EditText>(Resource.Id.tele);
            EditText nom_filiere = FindViewById<EditText>(Resource.Id.filiere);
            
            Button buttonAdd = FindViewById<Button>(Resource.Id.add);
            buttonAdd.Click += delegate
            {
                var result = sq.AddStudent(cin.Text, nom.Text, prenom.Text, email.Text, tele.Text, nom_filiere.Text);
                if (cin.Text == "" || nom.Text == "" || prenom.Text == "" || email.Text == "" || tele.Text == "" || nom_filiere.Text == "")
                {
                    Toast.MakeText(this, "Remplir les champs vides !", ToastLength.Long).Show();
                }
                else if (result == "Cette filière n'existe pas")
                {
                    Toast.MakeText(this, "Cette filière n'existe pas", ToastLength.Long).Show();
                }

                else if (result == "Ajouté avec succès")
                {
                    Toast.MakeText(this, "Ajouté avec succès", ToastLength.Long).Show();
                    Intent mainActivity = new Intent(this, typeof(HomeActivity));
                    StartActivity(mainActivity);
                }
                else if (result == "Déjà ajouté")
                {
                    Toast.MakeText(this, "Déjà ajouté", ToastLength.Long).Show();
                }
            };
            Button buttonCancel = FindViewById<Button>(Resource.Id.cancel);
            buttonCancel.Click += delegate
            {
                Intent mainActivity = new Intent(this, typeof(HomeActivity));
                StartActivity(mainActivity);
            };
        }


    }
}