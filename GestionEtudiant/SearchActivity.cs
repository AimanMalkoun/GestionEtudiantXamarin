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
using SQLite;
using Android.Support.Design.Widget;

namespace GestionEtudiant
{
    [Activity(Label = "SearchActivity", Theme = "@style/AppTheme")]
    public class SearchActivity : Activity
    {
        public string fil;
        public string mat;
        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            fil =(string) spinner.GetItemAtPosition(e.Position);
        }
        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mat = (string)spinner.GetItemAtPosition(e.Position);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.search_page);
            // Create your application here
            var Searchname = FindViewById<TextView>(Resource.Id.EditText1);

            DataBase dbcnx = new DataBase();
            //inserer des elements juste pour essayer ; à le supprimer apres et remplacer avec la base de donnes original
           // dbcnx.updatetry();
            var Searchbutton = FindViewById<Button>(Resource.Id.search_button);
            var Nomtxt = FindViewById<TextInputEditText>(Resource.Id.EditText1);
            var Cancelbutton = FindViewById<Button>(Resource.Id.cancel_button);
            var Spinnerfil = FindViewById<Spinner>(Resource.Id.spinnerfil) ;
            var Spinnermat = FindViewById<Spinner>(Resource.Id.spinnermat) ;
            //remplir les spinner avec les elements de a base de donnes

            List<string> filieres = dbcnx.selectfilieres();//data from db
            var adapterfil = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem,filieres);//create adapter
            adapterfil.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Spinnerfil.Adapter = adapterfil;//set adapter

            List<string> matieres = dbcnx.selectmatieres();//data from db
            var adaptermat = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, matieres);//create adapter
            adaptermat.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Spinnermat.Adapter = adaptermat;//set adapter
            Spinnerfil.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner1_ItemSelected);
            Spinnermat.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);

            Searchbutton.Click += delegate
            {
                var nom = Nomtxt.Text;
                var element = dbcnx.Search(nom, fil);

                if (element == null)
                {
                    Toast.MakeText(this, "Cet etudiant n'existe pas dans la base de données", ToastLength.Long).Show();
                }
                else
                {
                    //essayer ; à le supprimmer après
                    //Toast.MakeText(this, element.Cin + " " + element.Nom, ToastLength.Long).Show();
                    
                     Intent intent = new Intent(this, typeof(InfoActivity));
                     
                     intent.PutExtra("fullname", element.Prenom + " " + element.Nom);
                     intent.PutExtra("cin", element.Cin);
                     intent.PutExtra("filiere", fil);
                     intent.PutExtra("matiere", mat);
                     StartActivity(intent);
                    }
            };
            Cancelbutton.Click += delegate
             {
                 //mainacivity doit etre remplacer par la page de menu
                 Intent intent = new Intent(this, typeof(HomeActivity));
                 StartActivity(intent);
             };
        }
    }
}