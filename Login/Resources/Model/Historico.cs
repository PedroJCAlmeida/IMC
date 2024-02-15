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

namespace Login.Resources.Model
{
    public class Historico
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(50)]
        public string usuario { get; set; }

        [MaxLength(6)]
        public string peso { get; set; }

        [MaxLength(6)]
        public string altura { get; set; }
        [MaxLength(4)]
        public string imc { get; set; }
    }
}