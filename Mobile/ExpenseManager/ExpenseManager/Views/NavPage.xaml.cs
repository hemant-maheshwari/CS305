﻿using ExpenseManager.Models;
using ExpenseManager.Util;
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
    public partial class NavPage : TabbedPage
    {
        User user;
        public NavPage(User user)
        {
            InitializeComponent();
            Init();
            this.user = user;
        }

        public NavPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            CommonSettings.user = user;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }



}
       