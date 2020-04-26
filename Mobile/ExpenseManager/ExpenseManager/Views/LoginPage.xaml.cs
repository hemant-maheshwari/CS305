using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ExpenseManager.Controller;
using ExpenseManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private UserController userController;
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
            userController = new UserController();
        }

        public void goToSignUpPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new SignUpPage();
        }

        public void verifyLoginForm(object sender, EventArgs e)
        {
            if (entryUsername.Text == " " || entryUsername.Text == null)
            {
                DisplayAlert("Invalid username", "Please enter a username", "Okay");
                entryUsername.Focus();
            }
            else if (entryPassword.Text == " " || entryPassword.Text == null)
            {
                DisplayAlert("Invalid password", "Please enter a password.", "Okay");
                entryPassword.Focus();
            }
            else
            {
                isActivitySpinnerShowing(true);
                signIn();
            }
        }

        private async void signIn()
        {
            User user = new User(entryUsername.Text, entryPassword.Text);
            try { 
                user = await checkUserExistence(user);
                if (user != null)
                {
                    //DisplayAlert("Login", "Login Success", "Okay");
                    App.Current.MainPage = new NavPage(user);          //PASS USER AS PARAMETER!!!!!!!!!!!!!!!!!
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    await DisplayAlert("Login Failed", "Incorrect Username or Password", "Try Again");
                }
            }catch(Exception e)
            {
                isActivitySpinnerShowing(false);
                entryUsername.Text = "";
                entryPassword.Text = "";
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(e.Message);
            }
        }

        private async Task<User> checkUserExistence(User user)
        {
            return await userController.checkUser(user);
        }

        private void isActivitySpinnerShowing(bool status)
        {
            if (status.Equals(true))
            {
                signInLayout.IsVisible = false;
                signInLayout.IsEnabled = false;
                activitySpinnerLayout.IsVisible = true;
                loginPageSpinner.IsVisible = true;
                loginPageSpinner.IsRunning = true;
                loginPageSpinner.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerLayout.IsVisible = false;
                signInLayout.IsVisible = true;
                signInLayout.IsEnabled = true;
                loginPageSpinner.IsVisible = false;
                loginPageSpinner.IsRunning = false;
                loginPageSpinner.IsEnabled = false;

            }
        }

        public void goToForgotPasswordPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new ForgotPasswordPage();
        }
    }
}



