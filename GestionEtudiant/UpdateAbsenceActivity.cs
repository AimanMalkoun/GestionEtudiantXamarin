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
    [Activity(Label = "Absence")]
    class UpdateAbsenceActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            List<Etudiant> etudiants = new List<Etudiant>();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_update_absence);

            ListView listViewEtudiant = FindViewById<ListView>(Resource.Id.list_view_etudiant);
            listViewEtudiant.Adapter = new CustomListAdapterUpdateStudent(EtudiantData.etudiants);



            listViewEtudiant.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var position = e.Position;
                EtudiantData.etudiants[position].Absent = !EtudiantData.etudiants[position].Absent;
            };



            Button buttonSave = FindViewById<Button>(Resource.Id.button_save);
            buttonSave.Click += delegate
            {
                SqliteDB sqliteDB = new SqliteDB();
                for(int i = 0; i < listViewEtudiant.Count; i++)
                {
                    sqliteDB.updateEtudiant(EtudiantData.etudiants[i].FullName, EtudiantData.etudiants[i].Absent, EtudiantData.etudiants[i].Cin);
                }
                Intent mainActivity = new Intent(this, typeof(MainActivity));
                StartActivity(mainActivity);
            };

            Button buttonCancel = FindViewById<Button>(Resource.Id.button_cancel);
            buttonSave.Click += delegate
            {
                Intent mainActivity = new Intent(this, typeof(MainActivity));
                StartActivity(mainActivity);
            };
        }
    }
}