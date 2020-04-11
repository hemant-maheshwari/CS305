using System;
using System.Collections.Generic;
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
            

            entryUsername.Completed += (sender, e) => entryPassword.Focus();
            entryPassword.Completed += (sender, e) => signIn(sender, e);
        }

        public void goToSignUpPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new SignUpPage();
        }

        public async void signIn(object sender, EventArgs e)
        {
            isActivitySpinnerShowing(true);
            User user = new User(entryUsername.Text, entryPassword.Text);
            user = await checkUserExistence(user);
            if (user.userId != 0)
            {
                //DisplayAlert("Login", "Login Success", "Okay");
                App.Current.MainPage = new ExpensesPage();              //PASS USER AS PARAMETER!!!!!!!!!!!!!!!!!
            }
            else
            {
                isActivitySpinnerShowing(false);
                await DisplayAlert("Login Failed", "Incorrect Username or Password", "Try Again");
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
 


