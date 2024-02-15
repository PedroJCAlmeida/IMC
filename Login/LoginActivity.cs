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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;


namespace Login
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        TextView txtTextoLogin;
        //TextView txtUsuario;
        EditText txtAltura;
        EditText txtPeso;
        TextView txtIMC;
        TextView txtClassificacao;
        Button btnRegistrarIMC;
        Button btnHistorico;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);

            txtTextoLogin = FindViewById<TextView>(Resource.Id.txtTextoLogin);
            //pega os dados obtidos na primeira atividade e exibe no TextField
            FindViewById<TextView>(Resource.Id.txtTextoLogin).Text = txtTextoLogin.Text + " : " + Intent.GetStringExtra("nome") ?? "Erro ao obter os dados";
            Typeface typeface = Typeface.CreateFromAsset(Assets, "AntipastoPro-Light_trial.ttf");
            txtTextoLogin.SetTypeface(typeface, TypefaceStyle.Normal);

            btnRegistrarIMC = FindViewById<Button>(Resource.Id.btnRegistrarIMC);
            btnHistorico = FindViewById<Button>(Resource.Id.btnHistorico);
            txtAltura = FindViewById<EditText>(Resource.Id.txtAltura);
            txtPeso = FindViewById<EditText>(Resource.Id.txtPeso);
            //txtUsuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            
            txtIMC = FindViewById<TextView>(Resource.Id.txtIMC);
            

            txtClassificacao = FindViewById<TextView>(Resource.Id.txtClassificacao);
            Typeface typeface2 = Typeface.CreateFromAsset(Assets, "AntipastoPro-Light_trial.ttf");
            txtClassificacao.SetTypeface(typeface2, TypefaceStyle.Normal);

            btnRegistrarIMC.Click += BtnRegistrarIMC_Click;
            btnHistorico.Click += BtnHistorico_Click;
        }

        private void BtnHistorico_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HistoricoActivity));
        }

        private void BtnRegistrarIMC_Click(object sender, EventArgs e)
        {
            if(txtAltura.Text == "")
            {
                Toast.MakeText(this, "Falta inserir a Altura", ToastLength.Short).Show();
                
            }
            else if (txtPeso.Text == "")
            {
                Toast.MakeText(this, "Falta inserir o Peso", ToastLength.Short).Show();

            }
            else if(int.Parse(txtAltura.Text) <= 40 || int.Parse(txtAltura.Text) >= 220)
            {
                Toast.MakeText(this, "Altura inválida", ToastLength.Short).Show();
                txtAltura.Text = "";
            }
            else if(int.Parse(txtPeso.Text) <= 40 || int.Parse(txtPeso.Text) >= 220)
            {
                Toast.MakeText(this, "Peso inválido", ToastLength.Short).Show();
                txtPeso.Text = "";
            }
            else
            {

            
            try
            {
                string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Usuario.db3");
                var db = new SQLiteConnection(dpPath);

                db.CreateTable<Historico>();

                Historico tbhistorico = new Historico();
                tbhistorico.usuario = txtTextoLogin.Text;
                tbhistorico.altura = txtAltura.Text;
                tbhistorico.peso = txtPeso.Text;
                tbhistorico.imc = (double.Parse(txtPeso.Text) / (double.Parse(txtAltura.Text)* double.Parse(txtAltura.Text)/10000)).ToString("F");
               

                db.Insert(tbhistorico);

                Toast.MakeText(this, "Registro incluído com sucesso...,", ToastLength.Short).Show();

                 txtIMC.Text=tbhistorico.imc;
                 
                 
                 double valor_imc = double.Parse(txtIMC.Text);
                if(valor_imc < 18.5)
                {
                    txtClassificacao.Text = "Atenção !!! Abaixo do Peso";
                }
                if (valor_imc >= 18.5 && valor_imc <= 24.9)
                {
                    txtClassificacao.Text = "Parabéns !!! Peso Saudável";
                }
                if (valor_imc >= 25 && valor_imc <= 29.9)
                {
                    txtClassificacao.Text = "Atençaõ !!! Sobrepeso";
                }
                if (valor_imc >= 30 && valor_imc <= 39.9)
                {
                    txtClassificacao.Text = "Muita Atenção !!! Obesidade";
                }
                if (valor_imc >= 40)
                {
                    txtClassificacao.Text = "Buque ajuda médica !!! Obesidade Mórbida";
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        }
    }
}