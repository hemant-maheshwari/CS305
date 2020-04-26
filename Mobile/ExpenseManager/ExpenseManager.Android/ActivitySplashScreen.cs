
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ExpenseManager.Droid
{
    [Activity(MainLauncher = true, NoHistory =true, Label ="ExpenseManager")]
    public class ActivitySplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.layoutSplashDesign);
            Handler h = new Handler();
            h.PostDelayed(new System.Action(() =>
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            }), 2000);
        }
    }
}
