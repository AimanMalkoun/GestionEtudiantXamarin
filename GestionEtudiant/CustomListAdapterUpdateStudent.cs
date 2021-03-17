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
    class CustomListAdapterUpdateStudent : BaseAdapter<Etudiants>
    {
        List<Etudiants> etudiants;
       
       
        Interface1 listener;


        public CustomListAdapterUpdateStudent(List<Etudiants> etudiants)
        {
            this.etudiants = etudiants;

        }

        public override Etudiants this[int position]
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
            listener = parent.Context as Interface1;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_absent_etudiant, parent, false);

                 var checkBoxEtudiant = view.FindViewById<CheckBox>(Resource.Id.checkboxetudant);
                var nameEtudiant = view.FindViewById<TextView>(Resource.Id.nameStudent);

                view.Tag = new ViewHolder()
                {
                    CheckBoxEtudiant = checkBoxEtudiant,
                    nameEtudiant = nameEtudiant
                };
            }

            var holder = (ViewHolder)view.Tag;
            

            holder.CheckBoxEtudiant.Text = etudiants[position].FullName;
            //holder.CheckBoxEtudiant.Checked = etudiants[position].Absent;
           holder. CheckBoxEtudiant.Click += (o, e) => {
                if (holder.CheckBoxEtudiant.Checked)
               {
                   listener.retourner(etudiants[position].Cin);
               }
                    
               
            };




            return view;

        }
    }
    }