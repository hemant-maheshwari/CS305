using ExpenseManager.Controller;
using ExpenseManager.Models;
using ExpenseManager.Util;
using ExpenseManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Diagnostics;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        private TransactionController transactionController;
        private FriendController friendController;
        private TransactionViewModel transactionsPageModel;
        private TotalController totalController;
        private User user;
        private Total total;
        private List<Friend> friends;

        private string imagePath;

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
            transactionsPageModel = new TransactionViewModel();
            totalController = new TotalController();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            total = await totalController.getModel(user.userId);
            transactionsPageModel.friendsList = await initializeFriendsList(user.userId);
            friendsPicker.ItemsSource = transactionsPageModel.friendsListToString();
            friends = await friendController.getAllModels(user.userId);
        }

        private async Task<List<FriendViewModel>> initializeFriendsList(int userId)
        {
            return await friendController.getAllFriendsInfo(userId);
        }

        public void verifyTransactionForm(object sender, EventArgs e)
        {
            if (entryTranscationTitle.Text == null || entryTranscationTitle.Text == "")
            {
                DisplayAlert("Invalid Title", "Please enter a title for your transaction.", "Okay");
                entryTranscationTitle.Focus();
            }
            else if (pickerTransactionType.SelectedItem == null)
            {
                DisplayAlert("Invalid Type", "Please select your transaction type.", "Okay");
                pickerTransactionType.Focus();
            }
            else if (entryTransactionAmount.Text == null || entryTransactionAmount.Text == "")
            {
                DisplayAlert("Invalid Amount", "Please select an amount for this transaction.", "Okay");
                entryTransactionAmount.Focus();
            }
            else
            {
                isAddTransactionLayoutShowing(false);
                isActivitySpinnerShowing(true);
                createTransaction();
            }
        }

        private int getFriendId()
        {
            int friendIndex = friendsPicker.SelectedIndex;
            int friendId = transactionsPageModel.friendsList[friendIndex].friendId;
            return friendId;
        }

        private void createTransaction() // creation of a new post with required fields
        {
            try
            {
                string expenseType = pickerTransactionType.SelectedItem.ToString();
                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (expenseType.Equals("Expense") && friendsPicker.SelectedItem != null) {
                    createSharedTransaction();
                }
                else if (expenseType.Equals("Expense") && friendsPicker.SelectedItem == null)
                {
                    createPersonalTransaction();
                }
                else if (expenseType.Equals("Income"))
                {
                    createIncome();
                }
                else
                {
                    DisplayAlert("Error", "Something went wrong!", "Okay");
                }

            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isAddTransactionLayoutShowing(true);
                Debug.WriteLine(ex.Message);
            }
        }

        private async void createIncome()
        {
            string imageString = imageToBase64(); //create if statement
            Transaction transaction = new Transaction(user.userId, entryTranscationTitle.Text, "Income", Double.Parse(entryTransactionAmount.Text), 0, imageString, DateTime.Now.ToString("yyyy-MM-dd"));
            bool flag = await transactionController.createModel(transaction);
            if (flag)
            {
                total.incomeAmount += getTotalTransactionAmount();
                await totalController.updateModel(total);
                await DisplayAlert("Message", "Transaction created successfully!", "Okay");
                App.Current.MainPage = new NavPage(user);
            }
            else
            {
                isActivitySpinnerShowing(false);
                isAddTransactionLayoutShowing(true);
                await DisplayAlert("Message", "Error Occured!", "Okay");
            }
        }

        private async void createPersonalTransaction()
        {
            string imageString = imageToBase64(); //create if statement
            Transaction transaction = new Transaction(user.userId, entryTranscationTitle.Text, "Expense", Double.Parse(entryTransactionAmount.Text), 0, imageString, DateTime.Now.ToString("yyyy-MM-dd"));
            bool flag = await transactionController.createModel(transaction);
            if (flag)
            {
                total.expenseAmount += getTotalTransactionAmount();
                await totalController.updateModel(total);
                await DisplayAlert("Message", "Transaction created successfully!", "Okay");
                App.Current.MainPage = new NavPage(user);
            }
            else
            {
                isActivitySpinnerShowing(false);
                isAddTransactionLayoutShowing(true);
                await DisplayAlert("Message", "Error Occured!", "Okay");
            }
        }

        private async void createSharedTransaction(){
            int friendId = getFriendId();
            string imageString = imageToBase64(); //create if statement
            Transaction transaction = new Transaction(user.userId, entryTranscationTitle.Text, "Expense", Double.Parse(entryTransactionAmount.Text), friendId, imageString, DateTime.Now.ToString("yyyy-MM-dd"));
            bool flag = await transactionController.createModel(transaction);
            if (flag)
            {
                Friend friend = getSelectedFriend(friendId);
                friend.amount += getAmount();
                flag = await friendController.updateModel(friend);
                if (flag)
                {
                    Friend friend2 = await getFriend2(friendId);
                    friend2.amount -= getAmount();
                    flag = await friendController.updateModel(friend2);
                    if (flag)
                    {
                        total.expenseAmount += getTotalTransactionAmount();
                        await totalController.updateModel(total);
                        await DisplayAlert("Message", "Transaction created successfully!", "Okay");
                        App.Current.MainPage = new NavPage(user);
                    }                   
                }
            }
            else
            {
                isActivitySpinnerShowing(false);
                isAddTransactionLayoutShowing(true);
                await DisplayAlert("Message", "Error Occured!", "Okay");
            }
        }

        private double getTotalTransactionAmount()
        {
            return Double.Parse(entryTransactionAmount.Text);
        }

        private double getAmount()
        {
            return Double.Parse(entryTransactionAmount.Text) / 2;
        }

        private Friend getSelectedFriend(int userId)
        {
            for(int i = 0; i < friends.Count; i++)
            {
                if (friends[i].userId2 == userId)
                    return friends[i];
            }
            return default(Friend);
        }

        private async Task<Friend> getFriend2(int userId) {
            List<Friend> friendsList = await friendController.getAllModels(userId);
            for (int i=0; i<friendsList.Count; i++) {
                if (friendsList[i].userId2 == user.userId) {
                    return friendsList[i];
                }
            }
            return default(Friend);
        }

        private void isAddTransactionLayoutShowing(bool status)
        {
            if(status == true)
            {
                addTransactionLayout.IsVisible = true;
                addTransactionLayout.IsEnabled = true;
            }
            else
            {
                addTransactionLayout.IsVisible = false;
                addTransactionLayout.IsEnabled = false;
            }
        }

        public void showHideFriendsPicker(object sender, EventArgs e)
        {
            if(pickerTransactionType.SelectedItem.Equals("Expense"))
            {
                friendsPicker.IsEnabled = true;
                friendsPicker.IsVisible = true;
            }
            else
            {
                friendsPicker.IsEnabled = false;
                friendsPicker.IsVisible = false;
            }
        }

        private void isActivitySpinnerShowing(bool status)
        {
            if (status == true)
            {
                activitySpinnerLayout.IsVisible = true;
                activitySpinnerLayout.IsEnabled = true;
                activitySpinner.IsEnabled = true;
                activitySpinner.IsVisible = true;
                activitySpinner.IsRunning = true;
            }
            else
            {
                activitySpinnerLayout.IsVisible = false;
                activitySpinnerLayout.IsEnabled = false;
                activitySpinner.IsEnabled = false;
                activitySpinner.IsVisible = false;
                activitySpinner.IsRunning = false;
            }
        }

        public string imageToBase64() // converting image to base64
        {
            if(imagePath != null)
            {
                using (var image = File.OpenRead(imagePath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.CopyTo(m);
                        byte[] imageBytes = m.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            else
            {
                return "";
            }
            
        }

        public interface CameraInterface //interface for selecting picture
        {
            void BringUpPhotoGallery();
        }

        public async void pickPhotoButton(object sender, EventArgs e) // selecting picture from camera roll
        {
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("no upload", "picking a photo is not supported", "ok");
                    return;
                }

                MediaFile file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null)
                    return;

                Stream photoStream = file.GetStream();

                imgReceipt.Source = ImageSource.FromStream(() => photoStream);
                imagePath = file.Path;
            };
        }
    }

}