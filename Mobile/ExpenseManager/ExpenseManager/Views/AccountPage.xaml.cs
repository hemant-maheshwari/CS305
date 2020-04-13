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
            this.user = CommonSettings.user;
            updateAccountLoader.IsVisible = false;
        }
        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
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
            else if (entryAccPassword.Text == null || entryAccPassword.Text == "")
            {
                DisplayAlert("Invalid Password", "Please enter a valid password.", "Okay");
                entryAccPassword.Focus();
            }
            else if (entryAccConfirmPassword.Text == null || entryAccConfirmPassword.Text == "")
            {
                DisplayAlert("Invalid Confirmation", "Please enter your password again.", "Okay");
                entryAccConfirmPassword.Focus();
                entryAccConfirmPassword.Text = "";
            }
            else if (entryAccConfirmPassword.Text != entryAccPassword.Text)
            {
                DisplayAlert("Invalid Password Confirmation", "Passwords do not match. Try again.", "Okay");
                entryAccConfirmPassword.Focus();
                entryAccConfirmPassword.Text = "";
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
            else
            {
                isActivitySpinnerShowing(true);
                updateUserAccount();

            }

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

        public async void updateUserAccount()
        {
            user.updateUser(entryAccFirstName.Text, entryAccLastName.Text, entryAccEmail.Text, entryAccPhone.Text, entryAccPassword.Text);
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

    }
}


