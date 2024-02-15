using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Login.Resources.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using static Android.Content.ClipData;

namespace Login
{
    [Activity(Label = "HistoricoActivity")]
    public class HistoricoActivity : Activity
    {
        Spinner spinner;
        TextView txtId;
        TextView txtNid;
        //TextView txtUsuario;
        EditText txtAltura;
        EditText txtPeso;
        TextView txtIMC;
        Button btnEliminar;
        Button btnAtualizar;
        JavaList<string> Lista = new JavaList<string>();

        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Usuario.db3");

        //falta conseguir realizar o select a base de dados. 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.historico);

            //txtUsuario = FindViewById<TextView>(Resource.Id.txtUsuario);
            txtId = FindViewById<TextView>(Resource.Id.txtId);
            txtNid = FindViewById<TextView>(Resource.Id.txtNid);
            txtAltura = FindViewById<EditText>(Resource.Id.txtAltura);
            txtPeso = FindViewById<EditText>(Resource.Id.txtPeso);
            txtIMC = FindViewById<TextView>(Resource.Id.txtIMC);
            
            
            todoHistorico();

            btnEliminar = FindViewById<Button>(Resource.Id.btnEliminar);
            btnAtualizar = FindViewById<Button>(Resource.Id.btnAtualizar);
            btnEliminar.Click += BtnEliminar_Click;
            btnAtualizar.Click += BtnAtualizar_Click;
            //try
            //{
            //    dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Usuario.db3");
            //    var db = new SQLiteConnection(dpPath);
            //    var dados = db.Table<Historico>();

            //    var login = dados.Where(x => x.usuario == txtUsuario.Text).FirstOrDefault();
            //        bool semhistorico = true;


            //        foreach (var item in dados)
            //        {

            // txtUsuario.Text = item.usuario.ToString();
            //txtAltura.Text = "Altura: "+item.altura.ToString();
            //txtPeso.Text = "Peso: " + item.peso.ToString();
            //txtIMC.Text = "IMC: " + item.imc.ToString();

            //            semhistorico = false;                    

            //        }

            //        Toast.MakeText(this, "Histórico...,", ToastLength.Short).Show();


            //        if (semhistorico == false)
            //        {
            //            Toast.MakeText(this, "Sem histórico !!!", ToastLength.Short).Show();
            //        }

            //}                            
            //catch (Exception ex)
            //{
            //    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            //}
        }

       

        private void todoHistorico()
        {

            var db = new SQLite.SQLiteConnection(dbPath);
            try
            {
                var dados = db.Table<Historico>();


                foreach (var item in dados)
                {

                    Lista.Add(item.id.ToString());
                }
                Toast.MakeText(this, "Itens Adicionados ao filro ", ToastLength.Short).Show();

                spinner = FindViewById<Spinner>(Resource.Id.spinner);

                spinner.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, Lista);

                spinner.ItemSelected += Spinner_ItemSelected;

            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            bool encontrar = false;

            var db = new SQLite.SQLiteConnection(dbPath);

            try
            {
                var dados = db.Table<Historico>();


                long id = long.Parse(Lista[e.Position]);
                foreach (var item in dados)
                {
                    
                    if (id == item.id)
                    {
                        txtId.Text = item.id.ToString();
                        txtAltura.Text = item.altura;
                        txtPeso.Text = item.peso;
                        txtIMC.Text = "IMC: " + item.imc;


                        Toast.MakeText(this, "Registo IMC " + item.id.ToString() + " encontrado com sucesso!", ToastLength.Short).Show();
                        encontrar = true;
                    }
                }

                if (!encontrar)
                {
                    Toast.MakeText(this, "Registo IMC não encontrado!", ToastLength.Long).Show();
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        private void BtnAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Usuario.db3");
                var db = new SQLiteConnection(dbPath);
                int id_registo = Convert.ToInt32(txtId.Text); 

                db.Table<Historico>().Delete(x =>x.id == id_registo);

                Toast.MakeText(this, "Registro excluído com sucesso...,", ToastLength.Short).Show();
                StartActivity(typeof(HistoricoActivity));

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
           
        }
    }

    }

    
