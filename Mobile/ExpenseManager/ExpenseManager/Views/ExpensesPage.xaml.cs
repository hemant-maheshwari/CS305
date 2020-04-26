using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseManager.Models;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using ExpenseManager.Controller;
using System.Diagnostics;
using ExpenseManager.Util;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesPage : ContentPage
    {
        private TotalController totalController;
        private User user;
        private Total total;
        public ExpensesPage()
        {
            InitializeComponent();
            Init();
        }  

        public void Init() //initilazes screen components
        {
            BackgroundColor = Constants.backgroundColor;
            totalController = new TotalController();

        }

        protected async override void OnAppearing() //data when screen is shown
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            total = await totalController.getModel(user.userId);
            lblExpenseAmount.Text = total.expenseAmount.ToString();
            lblIncomeAmount.Text = total.incomeAmount.ToString();
            lblWelcome.Text = "Welcome "+user.firstName+"!";
        }
    }
}

    
        
    
        
    