using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManager.Models;
using Xamarin.Forms;
using ExpenseManager.Controller;
using System.Diagnostics;

namespace ExpenseManager.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        UserController userController;
        User user;

        private static string BLANK = "";

        public ForgotPasswordPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init() //initializes components on app startup
        {
            BackgroundColor = Constants.backgroundColor;
            userController = new UserController();
            user = new User();
        }

        public void verifyUsernameForm(object sender, EventArgs e)
        {
            if(entryUsername.Text == null || entryUsername.Text.Equals(BLANK))
            {
                DisplayAlert("Invalid Username", "Please enter your username.", "Okay");
                entryUsername.Focus();
            }
            else
            {
                isForgotPasswordLayoutShowing(false);
                isActivitySpinnerShowing(true);
                checkExistingUsername();
            }
        }

        private async void checkExistingUsername() //checks to see if username exists
        {  
            try
            {
                User completeUser = await getUserFromUsername(entryUsername.Text);
                if (completeUser.userId != 0)
                {
                    isActivitySpinnerShowing(false);
                    isUpdatePasswordLayoutShowing(true);
                    user = completeUser;
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isForgotPasswordLayoutShowing(true);
                    await DisplayAlert("Invalid Username", "There is no account associated with this username.", "Okay");
                }
            }catch(Exception ex)
            {
                isActivitySpinnerShowing(false);
                isForgotPasswordLayoutShowing(true);
                await DisplayAlert("Error", "Error occurred.", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<User> getUserFromUsername(string username) //gets user from username
        {
            return await userController.getUserFromUsername(username);
        }


        public void verifyUpdatedPassword(object sender, EventArgs e) //verifies in passwords were input correctly
        {
            if(entryNewPassword.Text == null || entryNewPassword.Text.Equals(BLANK))
            {
                DisplayAlert("Invalid Password", "Please enter your new password.", "Okay");
                entryNewPassword.Focus();
            }
            else if(entryConfirmNewPassword.Text == null || entryConfirmNewPassword.Text.Equals(BLANK))
            {
                DisplayAlert("Invalid Confirmation", "Please confirm your new password.", "Okay");
                entryConfirmNewPassword.Focus();
            }
            else if (passwordsMatch(entryNewPassword.Text, entryConfirmNewPassword.Text).Equals(false))
            {
                entryConfirmNewPassword.Text = BLANK;
                entryNewPassword.Text = BLANK;
                DisplayAlert("Invalid Password", "Passwords do not match.", "Okay");
            }
            else
            {
                isUpdatePasswordLayoutShowing(false);
                isActivitySpinnerShowing(true);
                updatePassword(entryNewPassword.Text);

            }
        }

        private async void updatePassword(string password) //updates user password and sends it to controller 
        {
            user.password = password;
            try
            {
                if (await userController.updateModel(user))
                {
                    await DisplayAlert("Update Success", "Password updated successfully", "Okay");
                    App.Current.MainPage = new LoginPage();

                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isForgotPasswordLayoutShowing(true);
                    await DisplayAlert("Update Unsuccessful", "Something went wrong.", "Okay");
                }
            }catch(Exception ex)
            {
                isActivitySpinnerShowing(false);
                isForgotPasswordLayoutShowing(true);
                await DisplayAlert("Error", "Error occurred", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private bool passwordsMatch(string password, string confirmPassword) //checks to see if new password fields are equal to each other
        {
            return password.Equals(confirmPassword);
        }


        private void isActivitySpinnerShowing(bool status) //diplays/hides activity spinner
        {
            if (status.Equals(true))
            {
                activitySpinnerLayout.IsVisible = true;
                forgotPasswordLoader.IsVisible = true;
                forgotPasswordLoader.IsRunning = true;
                forgotPasswordLoader.IsEnabled = true;

            }
            else
            {
                activitySpinnerLayout.IsVisible = false;
                forgotPasswordLoader.IsVisible = false;
                forgotPasswordLoader.IsRunning = false;
                forgotPasswordLoader.IsEnabled = false;
            }
        }

        private void isForgotPasswordLayoutShowing(bool status) //displays/hides forgot password page
        {
            if (status.Equals(true))
            {
                forgotPasswordLayout.IsEnabled = true;
                forgotPasswordLayout.IsVisible = true;

            }
            else
            {
                forgotPasswordLayout.IsVisible = false;
                forgotPasswordLayout.IsEnabled = false;
            }
        }

        private void isUpdatePasswordLayoutShowing(bool status) //displays/hides update password page
        {
            if (status.Equals(true))
            {
                updatePasswordLayout.IsEnabled = true;
                updatePasswordLayout.IsVisible = true;

            }
            else
            {
                updatePasswordLayout.IsVisible = false;
                updatePasswordLayout.IsEnabled = false;
            }
        }

        private void goToLoginPage(object sender, EventArgs e) //returns user to login page
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}
