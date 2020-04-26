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
    public partial class FriendsPage : ContentPage
    {
        private FriendController friendController;
        private User user;
        public FriendsPage()
        {
            InitializeComponent();
            Init();

        }

        public void Init()  // initialize screen components
        {
            BackgroundColor = Constants.backgroundColor;
            boxViewFriends.Color = Constants.logoColorYellow;
            friendController = new FriendController();
        }




        protected async override void OnAppearing() //data appearing when screen shows
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            List<FriendViewModel> friends = await friendController.getAllFriendsInfo(user.userId);
            friendsListView.ItemsSource = friends;
        }

        public void addFriendButton(object sender, EventArgs e) //add friend button
        {
            App.Current.MainPage = new AddFriendPage();
        }


        public void isActivitySpinnerShowing(bool status) //determines the visibility activity spinner
        {
            if (status.Equals(true))
            {
                followingLayout.IsVisible = false;
                followingLayout.IsEnabled = false;
                activitySpinnerFriendsLayout.IsVisible = true;
                friendsLoader.IsVisible = true;
                friendsLoader.IsRunning = true;
                friendsLoader.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerFriendsLayout.IsVisible = false;
                followingLayout.IsVisible = true;
                followingLayout.IsEnabled = true;
                friendsLoader.IsVisible = false;
                friendsLoader.IsRunning = false;
                friendsLoader.IsEnabled = false;

            }
           

        }



    }
}