﻿using System;
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

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesPage : ContentPage
    {
       

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

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }
  
        }
   

     
    }

    
        
    
        
    