using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpenseManager.Controller;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private UserController userController;
        private TotalController totalController;

        public SignUpPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            userController = new UserController();
            totalController = new TotalController();
            BackgroundColor = Constants.backgroundColor;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            signUpLoader.IsVisible = false;
        }

        public void verifyUserForm(object sender, EventArgs e)
        {
            if (entryFirstName.Text == " " || entryFirstName.Text == null)
            {
                DisplayAlert("Invalid First Name", "Please enter your first name.", "Okay");
                entryFirstName.Focus();
            }
            else if (entryLastName.Text == " " || entryLastName.Text == null)
            {
                DisplayAlert("Invalid Last Name", "Please enter your last name.", "Okay");
                entryLastName.Focus();
            }
            else if (entryEmail.Text == null || !Regex.IsMatch(entryEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                DisplayAlert("Invalid Email", "Please enter a valid email.", "Okay");
                entryEmail.Focus();
                entryEmail.Text = "";
            }
            else if (entryUsername.Text == null || entryUsername.Text == "")
            {
                DisplayAlert("Invalid Username", "Please enter a valid username", "Okay");
                entryUsername.Focus();
            }
            else if (entryPassword.Text == null || entryPassword.Text == "")
            {
                DisplayAlert("Invalid Password", "Please enter a valid password.", "Okay");
                entryPassword.Focus();
            }
            else if (entryConfirmPassword.Text == null || entryConfirmPassword.Text == "")
            {
                DisplayAlert("Invalid Confirmation", "Please enter your password again.", "Okay");
                entryConfirmPassword.Focus();
                entryConfirmPassword.Text = "";
            }
            else if (entryConfirmPassword.Text != entryPassword.Text)
            {
                DisplayAlert("Invalid Password Confirmation", "Passwords do not match. Try again.", "Okay");
                entryConfirmPassword.Focus();
                entryConfirmPassword.Text = "";
            }
            else if (entryPhoneNumber.Text == null || entryPhoneNumber.Text == "")
            {
                DisplayAlert("Invalid Phone Number", "Please enter a phone number.", "Okay");
                entryPhoneNumber.Focus();
            }
            else if (entryPhoneNumber.Text.Length < 10 || entryPhoneNumber.Text.Length > 11)
            {
                DisplayAlert("Invalid Phone Number", "Invalid phone number. Try again", "Okay");
                entryPhoneNumber.Focus();
                entryPhoneNumber.Text = "";
            }
            else
            {
                isActivitySpinnerShowing(true);
                createUserAccount();
            }
        }

        private void isActivitySpinnerShowing(bool status)
        {
            if (status.Equals(true))
            {
                signUpLayout.IsVisible = false;
                signUpLayout.IsEnabled = false;
                activitySpinnerLayout.IsVisible = true;
                signUpLoader.IsVisible = true;
                signUpLoader.IsRunning = true;
                signUpLoader.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerLayout.IsVisible = false;
                signUpLayout.IsVisible = true;
                signUpLayout.IsEnabled = true;
                signUpLoader.IsVisible = false;
                signUpLoader.IsRunning = false;
                signUpLoader.IsEnabled = false;
            }
        }

        private async Task<bool> checkUsernameExistence(string username)
        {
            return await userController.checkUsername(username);
        }

        private async void createUserAccount()
        {
            User user = new User(entryUsername.Text, entryPassword.Text, entryFirstName.Text, entryLastName.Text, entryEmail.Text, entryPhoneNumber.Text);
            try
            {
                if (!await checkUsernameExistence(user.username))
                {
                    bool flag = await userController.createModel(user);
                    if (flag)
                    {
                        user = await userController.getUserFromUsername(user.username);
                        Total total = new Total(user.userId, 0, 0);
                        flag = await totalController.createModel(total);
                        if (flag)
                        {
                            isActivitySpinnerShowing(false);
                            await DisplayAlert("Message", "User account created successfully!", "Okay");
                            App.Current.MainPage = new LoginPage();
                        }
                        else
                        {
                            await userController.deleteModel(user.userId);
                            isActivitySpinnerShowing(false);
                            await DisplayAlert("Message", "Error Occured!", "Okay");
                        }
                        
                    }
                    else
                    {
                        isActivitySpinnerShowing(false);
                        await DisplayAlert("Message", "Error Occured!", "Okay");
                    }
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    await DisplayAlert("Message", "Username already exists!", "Okay");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        public void goToLoginPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}