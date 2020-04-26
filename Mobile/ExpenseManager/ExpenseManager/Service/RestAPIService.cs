using ExpenseManager.Models;
using ExpenseManager.Util;
using ExpenseManager.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Service
{
    public class RestAPIService: WebAPIConfiguration
    {
        
        private async Task<Response> getHTTPResponse(HttpResponseMessage response) {
            string result = await response.Content.ReadAsStringAsync();
            Response responseObject = JsonConvert.DeserializeObject<Response>(result);
            return responseObject;
        }

        private User getUserFromResponse(Response response) {
            string userString = response.data;
            User user = JsonConvert.DeserializeObject<User>(userString);
            return user;
        }

        private List<FriendViewModel> getFriendViewModelFromResponse(Response response) // getting a list of friend view model from web api back to controller
        {
            string stringFriendList = response.data;
            FriendViewModel[] friendViewModelArray = JsonConvert.DeserializeObject<FriendViewModel[]>(stringFriendList);
            return friendViewModelArray.ToList<FriendViewModel>();

        }

        private List<ActivityViewModel> getActivityViewModelFromResponse(Response response)  // getting a list of activity view model from web api back to controller
        {
            string stringActivityList = response.data;
            ActivityViewModel[] activityViewModelArray = JsonConvert.DeserializeObject<ActivityViewModel[]>(stringActivityList);
            return activityViewModelArray.ToList<ActivityViewModel>();
        }

        public async Task<bool> checkUsernameAsync(string username) {
            string url = WEB_API_BASE_URL + "user/check/"+username;
            //var json = JsonConvert.SerializeObject(user);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return responseObject.status;
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return false;
            }
        }

        public async Task<User> checkUserAsync(User user)
        {
            string url = WEB_API_BASE_URL + "user/login/";
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url,content);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getUserFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(User);
            }
        }

        public async Task<User> getUserFromUsernameAsync(string username)
        {
            string url = WEB_API_BASE_URL + "user/validateUsername/" + username;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getUserFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(User);
            }
        }

        public async Task<List<FriendViewModel>> getAllFriendsInfoAsync(int userId)   // requesting a list of friend view model from web api by passing userID
        {
            string url = WEB_API_BASE_URL + "friend/getAllFriendInfo/" + userId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getFriendViewModelFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<FriendViewModel>);
            }
        }

        public async Task<List<ActivityViewModel>> getAllActivityAsnyc(int userId) // requesting a list of activity view model from web api by passing userID
        {
            string url = WEB_API_BASE_URL + "transaction/getAllActivity/" + userId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getActivityViewModelFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<ActivityViewModel>);
            }
        }
       

    }
}
