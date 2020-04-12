using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavPage : TabbedPage
    {
        public NavPage()
        {
            InitializeComponent();
            Init();

        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
        

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }

    }



}
       