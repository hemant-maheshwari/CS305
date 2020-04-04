using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            Spinner.IsVisible = false;
        }

        public void signUpButton(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}