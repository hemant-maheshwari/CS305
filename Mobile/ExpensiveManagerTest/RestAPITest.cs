using System;
using ExpenseManager.Views;
using ExpenseManager.Controller;
using ExpenseManager.Service;
using ExpenseManager.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ExpensiveManagerTest
{
    [TestFixture()]
    public class RestAPITest
    {
        RestAPIService restAPIService;
        RestAPICRUDService<User> restAPICRUDService;

        public RestAPITest()
        {
            restAPIService = new RestAPIService();
            restAPICRUDService = new RestAPICRUDService<User>();
        }
        [Test()]
        public void testCheckUserASync()  
        {
            User testUser = new User("kyle1776", "123");
            User expectedUser = new User("kyle1776", "123", "Kyle", "Riccardi", "kyle@gmail.com", "5555555555");
            expectedUser.userId = 42;
            Assert.AreEqual(expectedUser.userId, restAPIService.checkUserAsync(testUser).Result.userId);
        }
        [Test()]
        public void testCheckUsernameAsync()
        {
            string testUsername = "kyle1776";
            bool expectedBool = true;
            Assert.AreEqual(expectedBool, restAPIService.checkUsernameAsync(testUsername).Result);
        }
        [Test()]
        public void testGetUserFromUsernameAsync()
        {

            string testUsername = "kyle1776";
            User expectedUser = new User("kyle1776", "123", "Kyle", "Riccardi", "kyle@gmail.com", "5555555555");
            expectedUser.userId = 42;
            Assert.AreEqual(expectedUser.userId, restAPIService.getUserFromUsernameAsync(testUsername).Result.userId);
        }
        [Test()]
        public void testGetAllFriendsInfoAsync()
        {
            int testUserId = 39;
            int expectedListLength = 2;

            Assert.AreEqual(expectedListLength, restAPIService.getAllFriendsInfoAsync(testUserId).Result.Count);

        }
        [Test()]
        public void testGetAllActivityAsync()
        {
            int testUserId = 39;
            int expectedListLength = 3;

            Assert.AreEqual(expectedListLength, restAPIService.getAllActivityAsnyc(testUserId).Result.Count);
        }
        [Test()]
        public void testGetModelAsync()
        {
            int testUserId = 42;
            String expectedUsername = "kyle1776";
            Assert.AreEqual(expectedUsername, restAPICRUDService.getModelAsync(testUserId).Result.username);
        }
        [Test()]
        public void testUpdateModelAsync()
        {
            User expectedUser = new User("kyle1776", "123", "Kyle", "Riccardi", "kyle@gmail.com", "5555555555");
            expectedUser.userId = 42;

            bool expectedResult = true;
            Assert.AreEqual(expectedResult, restAPICRUDService.updateModelAsync(expectedUser).Result);
        }
    }
}
