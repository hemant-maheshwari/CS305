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

        public async void verifyExistingUsername(object sender, EventArgs e) //checks to see if username exists
        {
            isForgotPasswordLayoutShowing(false);
            isActivitySpinnerShowing(true);
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
                await DisplayAlert("Error", "Error occurred.", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<User> getUserFromUsername(string username) //gets user from username
        {
            return await userController.getUserFromUsername(username);
        }


        public async void verifyUpdatedPassword(object sender, EventArgs e) //verifies in passwords were input correctly
        {
            if (passwordsMatch(entryNewPassword.Text, entryConfirmNewPassword.Text).Equals(false))
            {
                await DisplayAlert("Invalid Password", "Passwords do not match.", "Okay");
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
            if (status.Equals(false))
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
            if (status.Equals(false))
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
            if (status.Equals(false))
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
