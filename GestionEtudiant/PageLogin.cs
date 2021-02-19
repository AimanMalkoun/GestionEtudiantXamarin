using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Text;
using Android.Text.Style;
using System;
using Android.Content;
using Android.Views;
using Android.Support.Design.Widget;

namespace GestionEtudiant
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class PageLogin : AppCompatActivity
    {
        DataBase db;
        ImageView img;
        string login;
        string password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginPage);
            
            img = FindViewById<ImageView>(Resource.Id.right);
            Button cnx = FindViewById<Button>(Resource.Id.connexion);
            cnx.Click += connect;
            img.Click += Droit;
            db = new DataBase();
            

        }

        private void connect(object sender, EventArgs e)
        {
            login = FindViewById<TextInputEditText>(Resource.Id.nomUtilisateur).Text;
            password = FindViewById<TextInputEditText>(Resource.Id.mdpLogin).Text;
            vérification();
        }

        private void vérification()
        {
            if (login == "" || password == "" )
            {

                Toast.MakeText(this, "Veuillez remplir les champs vides !", ToastLength.Long).Show();

            }
            else if (db.LoginValidate(login, password))
            {
                Toast.MakeText(this, "Connexion Etablie", ToastLength.Long).Show();
                login = "";
                password = "";
                Intent intent = new Intent(this, typeof(Pagehome));
                StartActivity(intent);
            }
            else if(!(db.LoginValidate(login, password)))
            {
                Toast.MakeText(this, "Login ou mot de passe Incorrect, Veuillez resaisir vos informations", ToastLength.Long).Show();
                login = "";
                password = "";
            }
        }

        private void Droit(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PageRegistration));
            StartActivity(intent);
        }

       

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}