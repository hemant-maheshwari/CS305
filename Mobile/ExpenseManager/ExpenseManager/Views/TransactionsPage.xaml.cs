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
    public partial class TransactionsPage : ContentPage
    {
        TransactionController transactionController;
        FriendController friendController;
        TransactionPageModel transactionsPageModel;
        User user;

        public TransactionsPage()
        {
            
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            transactionController = new TransactionController();
            friendController = new FriendController();
            transactionsPageModel = new TransactionPageModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.USER_SETTING] as User;
            transactionsPageModel.friendsList = await initializeFriendsList(user.userId);
            friendsPicker.ItemsSource = transactionsPageModel.friendsListToString();
        }

        private void showFriendsList(object sender, EventArgs e)
        {
            isAddedFriendsLayoutShowing(false);
            isFriendsPickerLayoutShowing(true);
            

        }

        //DisplayAlert("test","test","ok");
        //lblFriendsList.Text = lblFriendsList.Text + ", " + friendsPicker.SelectedItem;

        private async Task<List<FriendInfo>> initializeFriendsList(int userId)
        {
            return await friendController.getAllFriendsInfo(userId);
        }

        private void isAddedFriendsLayoutShowing(bool status)
        {
            if (status.Equals(true)){
                addedFriendsLayout.IsVisible = true;
                addedFriendsLayout.IsEnabled = true;
            }
            else
            {
                addedFriendsLayout.IsVisible = false;
                addedFriendsLayout.IsEnabled = false;
            }
        }

        private void isFriendsPickerLayoutShowing(bool status)
        {
            if (status.Equals(true))
            {
                friendsPickerLayout.IsVisible = true;
                friendsPickerLayout.IsEnabled = true;
                friendsPicker.IsVisible = true;
                friendsPicker.IsEnabled = true;
            }
            else
            {
                friendsPickerLayout.IsVisible = false;
                friendsPickerLayout.IsEnabled = false;
                friendsPicker.IsVisible = false;
                friendsPicker.IsEnabled = false;
            }

        }

    }



}