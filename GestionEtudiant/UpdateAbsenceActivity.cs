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
        static string date;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var prefs = Application.Context.GetSharedPreferences("CallMeSomethingCool", FileCreationMode.Private);
            if (!prefs.Contains("FirstExecution"))
            {
                date = DateTime.Now.ToString();
                var editor = prefs.Edit();
                editor.PutBoolean("FirstExecution", false);
                editor.Commit();
            }

            DateTime dateTime = Convert.ToDateTime(date);
            if (dateTime == Convert.ToDateTime(date).AddDays(1))
            {
                EtudiantData.clean();
                date = Convert.ToString(dateTime);
            }

            List<Etudiant> etudiants = new List<Etudiant>();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_update_absence);

            ListView listViewEtudiant = FindViewById<ListView>(Resource.Id.list_view_students);
            listViewEtudiant.Adapter = new CustomListAdapterUpdateStudent(EtudiantData.etudiants);



            listViewEtudiant.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var position = e.Position;
                EtudiantData.etudiants[position].Absent = !EtudiantData.etudiants[position].Absent;
                if (EtudiantData.etudiants[position].Absent)
                {
                    int temp = Convert.ToInt32(EtudiantData.etudiants[position].nbAbssence) + 1;
                    EtudiantData.etudiants[position].nbAbssence = Convert.ToInt32(temp);
                }
                else
                {
                    EtudiantData.etudiants[position].nbPresence = (Convert.ToInt32(EtudiantData.etudiants[position].nbPresence) + 1);
                }
            };



            Button buttonSave = FindViewById<Button>(Resource.Id.button8);
            buttonSave.Click += delegate
            {
                SqliteDB sqliteDB = new SqliteDB();
                for(int i = 0; i < listViewEtudiant.Count; i++)
                {
                    sqliteDB.updateEtudiant(EtudiantData.etudiants[i].FullName, EtudiantData.etudiants[i].Absent, EtudiantData.etudiants[i].Cin, EtudiantData.etudiants[i].nbPresence, EtudiantData.etudiants[i].nbAbssence);
                }
                Intent mainActivity = new Intent(this, typeof(HomeActivity));
                StartActivity(mainActivity);
            };

            Button buttonCancel = FindViewById<Button>(Resource.Id.button6);
            buttonSave.Click += delegate
            {
                Intent mainActivity = new Intent(this, typeof(HomeActivity));
                StartActivity(mainActivity);
            };
        }
    }
}