using System;
using System.Collections.Generic;
using ExpenseManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            lblUsername.TextColor = Constants.mainTextColor;
            lblPassword.TextColor = Constants.mainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

            entryUsername.Completed += (s,e) => entryPassword.Focus();
            entryPassword.Completed += (s, e) => SignInProcedure(s, e); 
        }

        void SignInProcedure(object sender, EventArgs e){
            User user = new User(entryUsername.Text, entryPassword.Text);
            if (user.checkInformation()){
                DisplayAlert("Login","Login Success","Okay");
            }else {
                DisplayAlert("Login","Incorrect Login,empty username or password.", "Okay");
            }
        }
    }
}
