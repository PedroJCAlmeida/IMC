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

namespace Login
{
    public class Login
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        [MaxLength(50)]
        public string usuario { get; set; }
        
        [MaxLength(15)]
        public string senha { get; set; }
        
        [MaxLength(50)]
        public string email { get; set; }
    }
}