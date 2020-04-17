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

        private User user;
        public ExpensesPage()
        {
            InitializeComponent();
            Init();
        }  

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}

    
        
    
        
    