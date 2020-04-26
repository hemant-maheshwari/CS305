using ExpenseManager.Controller;
using ExpenseManager.Models;
using ExpenseManager.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFriendPage : ContentPage
    {
        private UserController userController;
        private FriendController friendController;
        private User user;
        
        
        public AddFriendPage()
        {
            InitializeComponent();
            Init();
        }
        public void Init() //initialize screen components
        {
            BackgroundColor = Constants.backgroundColor;
            userController = new UserController();
            friendController = new FriendController();
        }
        protected override void OnAppearing() //data when screen is shown
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
        }
        public void goToHomePage(object sender, EventArgs e) //navigate to user home page
        {
            Application.Current.MainPage = new NavPage();
        }
        public void searchButtonPressed(object sender, EventArgs e) //function when search button is pressed
        {
            var keyword = addFriendPageBar.Text;

            if (keyword.Length < 3 || keyword.Length > 20)
            {
                isActivitySpinnerShowing(false);
                DisplayAlert("Invalid Search", "Enter a valid UserName", "Okay");
                addFriendPageBar.Focus();
            }
            else
            {
                isActivitySpinnerShowing(true);
                searchFriend();
            }
        }

        public async void addFriend(object sender, EventArgs e)  //add friend info sent to controller
        {
            string frienduserName = foundFriend.Text;
            User friendUser = await userController.getUserFromUsername(frienduserName);
            Friend friend1 = new Friend(user.userId, friendUser.userId, 0);
            Friend friend2 = new Friend(friendUser.userId, user.userId, 0);
            bool flag = await friendController.createModel(friend1);
            if (flag) {
                flag = await friendController.createModel(friend2);
                if (flag)
                {
                    isActivitySpinnerShowing(true);
                    await DisplayAlert("Message", "Friend Added Succesfully", "Okay");
                    App.Current.MainPage = new NavPage();
                }
                else {
                    await DisplayAlert("Message", "Error Occured!", "Okay");
                }
                
            }else
            {
                await DisplayAlert("Message", "Error Occured", "Okay");
                App.Current.MainPage = new AddFriendPage();
            }

        }

        private async void searchFriend() // searches database for matching user
        {
            isActivitySpinnerShowing(true);
            try
            {
                User foundUser = await getUserFromUsername(addFriendPageBar.Text);
                if (foundUser != null)
                {
                    isActivitySpinnerShowing(false);
                    //user = foundUser;
                    foundFriend.Text = foundUser.username;
                    foundFriend.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Message", "User not Found", "Okay");
                    isActivitySpinnerShowing(false);
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                await DisplayAlert("Error", "Error occurred.", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<User> getUserFromUsername(string username) // calls for user from rest api
        {
            return await userController.getUserFromUsername(username);
        }


        public void isActivitySpinnerShowing(bool status) //determines the visibility activity spinner
        {
            if (status.Equals(true))
            {
                addFriendLayout.IsVisible = false;
                addFriendLayout.IsEnabled = false;
                activitySpinnerAddFriendLayout.IsVisible = true;
                addFriendLoader.IsVisible = true;
                addFriendLoader.IsRunning = true;
                addFriendLoader.IsEnabled = true;


            }
            if (status.Equals(false))
            {
                activitySpinnerAddFriendLayout.IsVisible = false;
                addFriendLayout.IsVisible = true;
                addFriendLayout.IsEnabled = true;
                addFriendLoader.IsVisible = false;
                addFriendLoader.IsRunning = false;
                addFriendLoader.IsEnabled = false;

            }
        }
    }
}

