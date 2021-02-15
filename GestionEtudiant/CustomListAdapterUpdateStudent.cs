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
    class CustomListAdapterUpdateStudent : BaseAdapter<Etudiant>
    {
        List<Etudiant> etudiants;

        public CustomListAdapterUpdateStudent(List<Etudiant> etudiants)
        {
            this.etudiants = etudiants;
        }

        public override Etudiant this[int position]
        {
            get
            {
                return etudiants[position];
            }
        }

        public override int Count
        {
            get
            {
                return etudiants.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_absent_etudiant, parent, false);

                var checkBoxEtudiant = view.FindViewById<CheckBox>(Resource.Id.checkboxetudant);

                view.Tag = new ViewHolder() { CheckBoxEtudiant = checkBoxEtudiant };
            }

            var holder = (ViewHolder)view.Tag;

            holder.CheckBoxEtudiant.Text = etudiants[position].FullName;
            holder.CheckBoxEtudiant.Enabled = etudiants[position].Absent;


            return view;

        }
    }
}