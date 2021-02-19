using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

using System;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;

namespace GestionEtudiant
{
    [Activity(Label = "@string/app_name")]
    public class PageRegistration : AppCompatActivity
    {
        ImageView img;
        Button inscription;
        string login;
        string pass1;
        string pass2;
        DataBase db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.RegistrationPage);

            img = FindViewById<ImageView>(Resource.Id.left);
            db = new DataBase();
            inscription = FindViewById<Button>(Resource.Id.inscription);
            

            inscription.Click += delegate
            {
                login = FindViewById<TextInputEditText>(Resource.Id.nom).Text;
                pass1 = FindViewById<TextInputEditText>(Resource.Id.mdp1).Text;
                pass2 = FindViewById<TextInputEditText>(Resource.Id.mdp2).Text;
                validation();
            };
            img.Click += Droit;


        }

      

        private void Droit(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PageLogin));
            StartActivity(intent);
        }



        private async void validation()
        {
            if (login=="" || pass1=="" || pass2=="")
            {
                
                Toast.MakeText(this, "Veuillez remplir les champs vides !", ToastLength.Long).Show();
                
            }
            else if (pass1!=pass2)
            {
                
               Toast.MakeText(this, "Les mots de passe ne sont pas identiques ", ToastLength.Long).Show();
                

            }
            else if(pass1.Length<4)
            {
                Toast.MakeText(this, "Le mot de passe  doit au moins contenir 4 charactères", ToastLength.Long).Show();
               
            }
            else if(pass1.Length >= 15)
            {
                Toast.MakeText(this, "Le mot de passe ne doit pas dépasser 15 charactères", ToastLength.Long).Show();
                
            }
           

            else if (db.AddProfesseur(login, pass1)=="Sucessfully Added")
            {
                Toast.MakeText(this, "Données ajoutées avec succès, Veuillez vous connecter", ToastLength.Long).Show();
                ViderChamps();
                Intent intent = new Intent(this, typeof(PageLogin));
                StartActivity(intent);
                
            }
            else if(db.AddProfesseur(login, pass1) == "login already Exist")
            {
             
                Toast.MakeText(this, "Ce nom d'utilisateur est déjà utilisé , Veuillez rechoisir votre login ", ToastLength.Long).Show();
                

            }

        }

       
        public void ViderChamps()
        {
            login = "";
            pass1 = "";
            pass2 = "";
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}