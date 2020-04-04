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

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            lblUsername.TextColor = Constants.initialScreensTextColor;
            lblPassword.TextColor = Constants.initialScreensTextColor;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            ActivitySpinner.IsVisible = false;
            

            entryUsername.Completed += (s, e) => entryPassword.Focus();
            entryPassword.Completed += (s, e) => signInButton(s, e);
        }

        public void signUpButton(object sender, EventArgs e)
        {
            App.Current.MainPage = new SignUpPage();
        }

        public void signInButton(object sender, EventArgs e)
        {
            User user = new User(entryUsername.Text, entryPassword.Text);
            if (user != null)
            {
                //DisplayAlert("Login", "Login Success", "Okay");
                App.Current.MainPage = new ExpensesPage();
            }
            else
            {
                DisplayAlert("Login Failed", "Incorrect Username or Password", "Try Again");
            }
        }

        //public bool checkInformation()  //verifies login information
        //{
        //    if (username == null || password == null)
        //    {
        //        return false;
        //    }
        //    if (!this.username.Equals("") && !this.password.Equals(""))
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //} 

        public void forgotPasswordButton(object sender, EventArgs e)
        {
            App.Current.MainPage = new ForgotPasswordPage();
        }
    }
}
 


