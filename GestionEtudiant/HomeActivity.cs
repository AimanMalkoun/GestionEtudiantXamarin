using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace GestionEtudiant
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class HomeActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_home);


            Button btnAddStudent = FindViewById<Button>(Resource.Id.addStudent);
            btnAddStudent.Click += delegate
            {
                Intent add = new Intent(this, typeof(AddingActivity));
                StartActivity(add);
            };

            Button btnSearch = FindViewById<Button>(Resource.Id.search);
             btnSearch.Click += delegate
             {
                 Intent search = new Intent(this, typeof(SearchActivity));
                 StartActivity(search);
             };


             Button btnAbssence = FindViewById<Button>(Resource.Id.absence);
             btnAbssence.Click += delegate
             {
                 Intent add = new Intent(this, typeof(UpdateAbsenceActivity));
                 StartActivity(add);
             };

             Button btnLogout = FindViewById<Button>(Resource.Id.logout);
             btnLogout.Click += delegate
             {
                 Intent add = new Intent(this, typeof(PageLogin));
                 StartActivity(add);
             };


        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}