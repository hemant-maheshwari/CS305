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
        TransactionController transactionController;
        FriendController friendController;
        TransactionViewModel transactionsPageModel;
        User user;

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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            transactionsPageModel.friendsList = await initializeFriendsList(user.userId);
            friendsPicker.ItemsSource = transactionsPageModel.friendsListToString();
        }

        private async Task<List<FriendViewModel>> initializeFriendsList(int userId)
        {
            return await friendController.getAllFriendsInfo(userId);
        }

        public void verifyTransactionForm(object sender, EventArgs e)
        {
            if(entryTranscationTitle.Text == null || entryTranscationTitle.Text == "")
            {
                DisplayAlert("Invalid Title", "Please enter a title for your transaction.", "Okay");
                entryTranscationTitle.Focus();
            }
            else if(pickerTransactionType.SelectedItem == null)
            {
                DisplayAlert("Invalid Type", "Please select your transaction type.", "Okay");
                pickerTransactionType.Focus();
            }
            else if(entryTransactionAmount.Text == null || entryTransactionAmount.Text == "")
            {
                DisplayAlert("Invalid Amount", "Please select an amount for this transaction.", "Okay");
                entryTransactionAmount.Focus();
            }
            else if(friendsPicker.SelectedItem == null)
            {
                DisplayAlert("Invalid Friend", "Please select a friend.", "Okay");
                friendsPicker.Focus();
            }
            else
            {
                isAddTransactionLayoutShowing(false);
                isActivitySpinnerShowing(true);
                createTransaction();
            }
        }

        private async void createTransaction() // creation of a new post with required fields
        {
            try
            {
                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                string imageString = imageToBase64();
                Transaction transaction = new Transaction(user.userId, entryTranscationTitle.Text, pickerTransactionType.SelectedItem.ToString(), Double.Parse(entryTransactionAmount.Text), friendsPicker.SelectedItem.ToString(), imageString, todayDate);
                bool flag = await transactionController.createModel(transaction);
                if (flag)
                {
                    await DisplayAlert("Message", "Transaction created successfully!", "Okay");
                    App.Current.MainPage = new ActivityPage();
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isAddTransactionLayoutShowing(true);
                    await DisplayAlert("Message", "Error Occured!", "Okay");
                } 
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isAddTransactionLayoutShowing(true);
                Debug.WriteLine(ex.Message);
            }
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

            using (var image = File.OpenRead(imagePath))            {                using (MemoryStream m = new MemoryStream())                {

                    image.CopyTo(m);                    byte[] imageBytes = m.ToArray();                    string base64String = Convert.ToBase64String(imageBytes);                    return base64String;                }            }        }


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