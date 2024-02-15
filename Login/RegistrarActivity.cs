using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Login
{
    [Activity(Label = "RegistrarActivity")]
    public class RegistrarActivity : Activity
    {
        EditText txtNovoUsuario;
        EditText txtSenhaNovoUsuario;
        EditText txtEmail;
        Button btnCriarNovoUsuario;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registar);
            btnCriarNovoUsuario = FindViewById<Button>(Resource.Id.btnRegistrar);
            txtNovoUsuario = FindViewById<EditText>(Resource.Id.txtNovoUsuario);
            txtSenhaNovoUsuario = FindViewById<EditText>(Resource.Id.txtSenhaNovoUsuario);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            btnCriarNovoUsuario.Click += BtnCriarNovoUsuario_Click;
        }
        private void BtnCriarNovoUsuario_Click(object sender, System.EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Usuario.db3");
                var db = new SQLiteConnection(dpPath);

                db.CreateTable<Login>();

                Login tblogin = new Login();
                tblogin.usuario = txtNovoUsuario.Text;
                tblogin.senha = txtSenhaNovoUsuario.Text;
                tblogin.email = txtEmail.Text;

                db.Insert(tblogin);

                Toast.MakeText(this, "Registro incluído com sucesso...,", ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        private void CriarBancoDeDados()
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Usuario.db3");
                var db = new SQLiteConnection(dbPath);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}

