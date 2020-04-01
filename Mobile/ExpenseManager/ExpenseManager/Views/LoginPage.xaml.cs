﻿using System;
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
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

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
            if (user.checkInformation())
            {
                //DisplayAlert("Login", "Login Success", "Okay");
                App.Current.MainPage = new ExpensesPage();
            }
            else
            {
                DisplayAlert("Login Failed", "Incorrect Username or Password", "Try Again");
            }
        }

        public void forgotPasswordButton(object sender, EventArgs e)
        {
            App.Current.MainPage = new ForgotPasswordPage();
        }
    }
}
 

