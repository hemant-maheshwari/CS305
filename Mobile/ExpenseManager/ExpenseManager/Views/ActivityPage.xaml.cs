using ExpenseManager.Controller;
using ExpenseManager.Models;
using ExpenseManager.Util;
using ExpenseManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPage : ContentPage
    {
        private TransactionController transactionController;
        //private UserController userController;
        private User user;
        

        public ActivityPage()
        {
            InitializeComponent();
          
            Init();

        }

        public void Init() // initializing of screen components
        {
            BackgroundColor = Constants.backgroundColor;
            transactionController = new TransactionController();
        }
        protected async override void OnAppearing()  //data shown when screen shows
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            List<ActivityViewModel> activity = await transactionController.getAllActivity(user.userId);
            activityListView.ItemsSource = activity;
        }

    }



}