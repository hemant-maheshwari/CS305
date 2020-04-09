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

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private UserController userController;
        public SignUpPage()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            userController = new UserController();
            BackgroundColor = Constants.backgroundColor;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            Spinner.IsVisible = false;
        }

        private async Task<bool> checkUsernameExistence(string username) {
            return await userController.checkUsername(username);
        }

        public async void createUserAccount(object sender, EventArgs e)
        {
            User user = new User(entryUsername.Text, entryPassword.Text, entryFirstName.Text, entryLastName.Text, entryEmail.Text, entryPhoneNumber.Text);
            try
            {
                if (!await checkUsernameExistence(user.username)) {
                    bool flag = await userController.createUser(user);
                    if (flag){
                        await DisplayAlert("Message", "User account created successfully!", "Okay");
                        App.Current.MainPage = new LoginPage();
                    }
                    else{
                        await DisplayAlert("Message", "Error Occured!", "Okay");
                    }
                }
                else{
                    await DisplayAlert("Message", "Username already exists!", "Okay");
                }
            }
            catch (Exception ex){
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