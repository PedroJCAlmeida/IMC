using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using SQLite;
using System;
using System.IO;

namespace Login
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtUsuario;
        EditText txtSenha;
        Button btnCriar;
        Button btnLogin;
        Button btnSobre;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnCriar = FindViewById<Button>(Resource.Id.btnRegistrar);
            btnSobre = FindViewById<Button>(Resource.Id.btnSobre);
            txtUsuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            txtSenha = FindViewById<EditText>(Resource.Id.txtSenha);
            btnLogin.Click += BtnLogin_Click;
            btnCriar.Click += BtnCriar_Click;
            btnSobre.Click += BtnSobre_Click;
            

            CriarBancoDeDados();
        }
        private void BtnCriar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegistrarActivity));
        }
        private void BtnSobre_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SobreActivity));
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Usuario.db3");
                var db = new SQLiteConnection(dbPath);
                var dados = db.Table<Login>();

                var login = dados.Where(x => x.usuario == txtUsuario.Text && x.senha == txtSenha.Text).FirstOrDefault();

                if (login != null)
                {
                    Toast.MakeText(this, "Login realizado com sucesso", ToastLength.Short).Show();
                    var atividade2 = new Intent(this, typeof(LoginActivity));
                    //pega os dados digitados em txtUsuario
                    atividade2.PutExtra("nome", FindViewById<EditText>(Resource.Id.txtUsuario).Text);
                    StartActivity(atividade2);
                }
                else
                {
                    Toast.MakeText(this, "Nome do usuário e/ou Senha inválida(os)", ToastLength.Short).Show();
                }
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
  

