using ExpenseManager.Controller;
using ExpenseManager.Models;
using ExpenseManager.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private UserController userController;
        private User user;

        public AccountPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            userController = new UserController();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            entryAccFirstName.Text = user.firstName;
            entryAccLastName.Text = user.lastName;
            entryAccEmail.Text = user.email;
            entryAccPhone.Text = user.phone;
        }

        public void updateUserForm(object sender, EventArgs e)
        {
            if (entryAccFirstName.Text == " " || entryAccFirstName.Text == null)
            {
                DisplayAlert("Invalid First Name", "Please enter your first name.", "Okay");
                entryAccFirstName.Focus();
            }
            else if (entryAccLastName.Text == " " || entryAccLastName.Text == null)
            {
                DisplayAlert("Invalid Last Name", "Please enter your last name.", "Okay");
                entryAccLastName.Focus();
            }
            else if (entryAccEmail.Text == null || !Regex.IsMatch(entryAccEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                DisplayAlert("Invalid Email", "Please enter a valid email.", "Okay");
                entryAccEmail.Focus();
                entryAccEmail.Text = "";
            }
            else if (entryAccPhone.Text == null || entryAccPhone.Text == "")
            {
                DisplayAlert("Invalid Phone Number", "Please enter a phone number.", "Okay");
                entryAccPhone.Focus();
            }
            else if (entryAccPhone.Text.Length < 10 || entryAccPhone.Text.Length > 11)
            {
                DisplayAlert("Invalid Phone Number", "Invalid phone number. Try again", "Okay");
                entryAccPhone.Focus();
                entryAccPhone.Text = "";
            }
            else if (entryAccPassword.Text != null && entryAccPassword.Text != "")
            {
                if (entryAccConfirmPassword.Text == null || entryAccConfirmPassword.Text == "")
                {
                    DisplayAlert("Invalid Confirmation", "Please enter your password again.", "Okay");
                    entryAccConfirmPassword.Focus();
                    entryAccConfirmPassword.Text = "";
                }
                else if (!passwordsMatch(entryAccPassword.Text, entryAccConfirmPassword.Text))
                {
                    DisplayAlert("Invalid Password Confirmation", "Passwords do not match. Try again.", "Okay");
                    entryAccConfirmPassword.Focus();
                    entryAccConfirmPassword.Text = "";
                }
                else
                {
                    user.firstName = entryAccFirstName.Text;
                    user.lastName = entryAccLastName.Text;
                    user.email = entryAccEmail.Text;
                    user.phone = entryAccPhone.Text;
                    user.password = entryAccPassword.Text;
                    isActivitySpinnerShowing(true);
                    updateUserAccount();
                }
            }
            else
            {
                user.firstName = entryAccFirstName.Text;
                user.lastName = entryAccLastName.Text;
                user.email = entryAccEmail.Text;
                user.phone = entryAccPhone.Text;
                isActivitySpinnerShowing(true);
                updateUserAccount();

            }

        }

        public async void updateUserAccount()
        {
            user.updateUser(user.firstName, user.lastName, user.email, user.phone, user.password);
            try
            {
                bool flag = await userController.updateModel(user);
                if (flag)
                {
                    isActivitySpinnerShowing(false);
                    await DisplayAlert("Message", "User account updated successfully!", "Okay");
                    App.Current.MainPage = new LoginPage();
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    await DisplayAlert("Message", "Error Occured!", "Okay");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(ex.Message);
            }

        }

        private bool passwordsMatch(string password, string confirmPassword)    //checks to see if new password and confirm password match
        {
            return password.Equals(confirmPassword);
        }

        public void logOut(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

        public void isActivitySpinnerShowing(bool status)
        {
            if (status.Equals(true))
            {
                updateLayout.IsVisible = false;
                updateLayout.IsEnabled = false;
                activitySpinnerAccountLayout.IsVisible = true;
                updateAccountLoader.IsVisible = true;
                updateAccountLoader.IsRunning = true;
                updateAccountLoader.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerAccountLayout.IsVisible = false;
                updateLayout.IsVisible = true;
                updateLayout.IsEnabled = true;
                updateAccountLoader.IsVisible = false;
                updateAccountLoader.IsRunning = false;
                updateAccountLoader.IsEnabled = false;

            }
        }

    }
}


