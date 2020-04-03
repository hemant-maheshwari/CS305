using System;
using System.Collections.Generic;
using ExpenseManager.Models;
using Xamarin.Forms;

namespace ExpenseManager.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
        }

        public void sendResetLink(System.Object sender, System.EventArgs e)
        {
        }
    }
}
