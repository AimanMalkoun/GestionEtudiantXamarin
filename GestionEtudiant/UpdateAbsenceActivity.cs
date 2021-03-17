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
    [Activity(Label = "Absence", MainLauncher = false)]
    class UpdateAbsenceActivity : Activity,Interface1
    {
        
        List<String> listeEtud = new List<string>();
        static string date;
        public string fil;
        public string mat;
        private void spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            fil = (string)spinner.GetItemAtPosition(e.Position);
        }
        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mat = (string)spinner.GetItemAtPosition(e.Position);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_update_absence);
            DataBase dataBase = new DataBase();
            var Spinnerfil = FindViewById<Spinner>(Resource.Id.spinnerfiliere);
            var Spinnermat = FindViewById<Spinner>(Resource.Id.spinnermatiere);
            List<string> filieres = dataBase.selectfilieres();//data from db
            var adapterfil = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, filieres);//create adapter
            adapterfil.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Spinnerfil.Adapter = adapterfil;//set adapter

            List<string> matieres = dataBase.selectmatieres();//data from db
            var adaptermat = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, matieres);//create adapter
            adaptermat.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Spinnermat.Adapter = adaptermat;//set adapter
            Spinnerfil.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner1_ItemSelected);
            Spinnermat.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);

            List<Etudiants> etudiants;


            ListView listViewEtudiant;
            Button buttonSearch = FindViewById<Button>(Resource.Id.button_search);
            buttonSearch.Click += delegate
            {
                etudiants = new List<Etudiants>();
                listViewEtudiant = FindViewById<ListView>(Resource.Id.list_view_students);
                EtudiantData et = new EtudiantData(fil);
                et.filiere = fil;
                listViewEtudiant.Adapter = new CustomListAdapterUpdateStudent(et.etudiants);


                /* listViewEtudiant.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                 {
                     var position = e.Position;

                     et.etudiants[position].Absent = !et.etudiants[position].Absent;
                     Toast.MakeText(this, et.etudiants[position].FullName + " was updated successfully to " + et.etudiants[position].Absent, ToastLength.Long).Show();



                 };*/

               
            Button buttonSave = FindViewById<Button>(Resource.Id.butSave);
                buttonSave.Click += delegate
                {
                    date = DateTime.Now.ToString("d/M/yyyy");
                    for (int i = 0; i < listViewEtudiant.Count; i++)
                    {
                        foreach (string cne in listeEtud)
                        {
                            if (et.etudiants[i].Cin == cne)
                            {
                                et.etudiants[i].Absent = true;
                            }
                        }
                        if (et.etudiants[i].Absent)
                        {
                            dataBase.insertAbsence(
                                true,
                                date,
                                dataBase.getFiliereId(fil),
                                dataBase.getMatiereId(mat),
                                et.etudiants[i].Cin
                                );
                        }
                        else
                        {
                            dataBase.insertAbsence(
                                false,
                                date,
                                dataBase.getFiliereId(fil),
                                dataBase.getMatiereId(mat),
                                et.etudiants[i].Cin
                                );
                        }
                        
                    }
                    Toast.MakeText(this,"Mise-à-jour avec succès!", ToastLength.Long).Show();
                    Intent mainActivity = new Intent(this, typeof(HomeActivity));
                    StartActivity(mainActivity);
                };
            };
            
                Button buttonCancel = FindViewById<Button>(Resource.Id.button6);
                buttonCancel.Click += delegate
                {
                    Intent mainActivity = new Intent(this, typeof(HomeActivity));
                    StartActivity(mainActivity);
                };
            }


        public void retourner(string IDE)
        {
            listeEtud.Add(IDE);
        }
    }
}
