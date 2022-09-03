using Newtonsoft.Json;
using System.Text;
using UserAPI.Models;

namespace UserAPI.IntegrationTests
{
    [TestCaseOrderer("UserAPI.IntegrationTests.TestPriorityOrderer", "UserAPI.IntegrationTests")]
    public class UserControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private static bool isCreateUserDone;
        private static bool isGetAllUsersDone;
        private static bool isGetUserByIdDone;
        private static bool isUpdateUserDone;
        private static bool isGetUserByIdAfterUpdateDone;
        private static bool isDeleteUserDone;

        public UserControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact, TestPriority(1)]
        public async Task Post_CreateUser_ReturnsObjectWithUser()
        {
            User user = new()
            {
                Id = 1,
                Name = "Misha",
                Age = 26
            };

            string json = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/User", httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Misha", responseString);
            Assert.Contains("26", responseString);
            isCreateUserDone = true;
        }

        [Fact, TestPriority(2)]
        public async Task Get_GetAllUsers_ReturnsObjectWithUsersList()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User?useCache=false");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Misha", responseString);
            Assert.Contains("26", responseString);
            Assert.True(isCreateUserDone);
            isGetAllUsersDone = true;
        }

        [Fact, TestPriority(3)]
        public async Task Get_GetUserById_ReturnsObjectWithUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/id?id=1&useCache=false");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Misha", responseString);
            Assert.Contains("26", responseString);
            Assert.True(isCreateUserDone);
            Assert.True(isGetAllUsersDone);
            isGetUserByIdDone = true;
        }

        [Fact, TestPriority(4)]
        public async Task Put_UpdateUser_ReturnsNoContent()
        {
            User user = new()
            {
                Id = 1,
                Name = "Misha",
                Age = 27
            };

            string json = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/User/1", httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.DoesNotContain("Bad Request", responseString);
            Assert.True(isCreateUserDone);
            Assert.True(isGetAllUsersDone);
            Assert.True(isGetUserByIdDone);
            isUpdateUserDone = true;
        }

        [Fact, TestPriority(5)]
        public async Task Get_GetUserByIdAfterUpdate_ReturnsObjectWithUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/id?id=1&useCache=false");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Misha", responseString);
            Assert.Contains("27", responseString);
            Assert.True(isCreateUserDone);
            Assert.True(isGetAllUsersDone);
            Assert.True(isGetUserByIdDone);
            Assert.True(isUpdateUserDone);
            isGetUserByIdAfterUpdateDone = true;
        }
        
        [Fact, TestPriority(6)]
        public async Task Delete_DeleteUser_ReturnsNoContent()
        {
            var response = await _client.DeleteAsync("/api/User/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.DoesNotContain("Not Found", responseString);
            Assert.True(isCreateUserDone);
            Assert.True(isGetAllUsersDone);
            Assert.True(isGetUserByIdDone);
            Assert.True(isUpdateUserDone);
            Assert.True(isGetUserByIdAfterUpdateDone);
            isDeleteUserDone = true;
        }

        [Fact, TestPriority(7)]
        public async Task Get_GetUserByIdAfterDelete_ReturnsNotFound()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/id?id=1&useCache=false");
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Not Found", responseString);
            Assert.True(isCreateUserDone);
            Assert.True(isGetAllUsersDone);
            Assert.True(isGetUserByIdDone);
            Assert.True(isUpdateUserDone);
            Assert.True(isGetUserByIdAfterUpdateDone);
            Assert.True(isDeleteUserDone);
        }
    }
}