using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpenseManager.Data;
using System.Diagnostics;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            Spinner.IsVisible = false;
           
        }

        async void createAccount(object sender, EventArgs e) {
            User user = new User(entryUsername.Text, entryPassword.Text);
            try {
                RestWebAPIService restWebAPIService = new RestWebAPIService();
                await restWebAPIService.createUserAsync(user);
                await DisplayAlert("Message","Success!","Okay");
            }
            catch (Exception ex) {
                await DisplayAlert("Message", "Filed!", "Okay");
                Debug.WriteLine(ex.Message);
            }
            
            //Account account = new Account();
        }
    }
}