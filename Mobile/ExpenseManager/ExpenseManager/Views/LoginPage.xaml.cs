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

        private static string BLANK = "";

        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init() //initializes components on startup
        {
            BackgroundColor = Constants.backgroundColor;
            lblUsername.TextColor = Constants.initialScreensTextColor;
            lblPassword.TextColor = Constants.initialScreensTextColor;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            userController = new UserController();
        }

        public void goToSignUpPage(object sender, EventArgs e) //takes user to the sign up page
        {
            App.Current.MainPage = new SignUpPage();
        }

        public void verifyLoginForm(object sender, EventArgs e) //verifies if form was input correctly
        {
            if (entryUsername.Text == BLANK || entryUsername.Text == null)
            {
                DisplayAlert("Invalid username", "Please enter a username", "Okay");
                entryUsername.Focus();
            }
            else if (entryPassword.Text == BLANK || entryPassword.Text == null)
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

        private async void signIn() //creates a user object with username and password checks for existence in database
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
                entryUsername.Text = BLANK;
                entryPassword.Text = BLANK;
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(e.Message);
            }
        }

        private async Task<User> checkUserExistence(User user) //sends user to controller to see if it exists in database
        {
            return await userController.checkUser(user);
        }

        private void isActivitySpinnerShowing(bool status) //displays/hides activity spinner
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
 
        public void goToForgotPasswordPage(object sender, EventArgs e) //takes user to forgot password page
        {
            App.Current.MainPage = new ForgotPasswordPage();
        }
    }
}



