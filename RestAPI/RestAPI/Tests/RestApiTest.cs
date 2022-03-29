using NUnit.Framework;
using System.Collections.Generic;

namespace RestAPI
{
    public class Tests
    {
        private static Dictionary<string, string> testData = DeserializeUtil.GetTestData();
        private string baseUrl = testData["baseUrl"];
        private ApplicationApi applicationApi = new ApplicationApi();
        [Test]
        public void CheckGetPostsTest()
        {
            string resourceUrl = testData["posts"];
            var client = ApiUtil.SetUrl(baseUrl, resourceUrl);
            var request = ApiUtil.CreateGetRequest();
            var response = ApiUtil.GetResponse(client, request).Result;
            var actualStatusCode = (int)response.StatusCode;
            int expectedStatusCode = int.Parse(testData["StatusCodeOk"]); 
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "The status code is incorrect");

            var expectedType = testData["Type"];
            Assert.AreEqual(expectedType, response.ContentType, "The list in response body is not json");

            var listPosts = DeserializeUtil.GetData<List<Post>>(response.Content);
            Assert.IsTrue(applicationApi.IsSortedAscending(listPosts), "Posts are not sorted ascending (by id)");
        }

        [Test]
        public void CheckGetPostTest()
        {
            string resourceUrl = testData["post99"];
            var client = ApiUtil.SetUrl(baseUrl, resourceUrl);
            var request = ApiUtil.CreateGetRequest();
            var response = ApiUtil.GetResponse(client, request).Result;
            var actualStatusCode = (int)response.StatusCode;
            int expectedStatusCode = int.Parse(testData["StatusCodeOk"]);
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "The status code is incorrect");

            var post = DeserializeUtil.GetData<Post>(response.Content);
            int expectedUserId = int.Parse(testData["idUser99"]);
            int expectedId = int.Parse(testData["id99"]);
            Assert.AreEqual(expectedUserId, post.UserId, "The user id didn't match");
            Assert.AreEqual(expectedId, post.Id, "The user id didn't match");
            Assert.IsTrue(post.Title != string.Empty, "The title is empty");
            Assert.IsTrue(post.Body != string.Empty, "The body is empty");
        }

        [Test]
        public void NotFoundPostTest()
        {
            string resourceUrl = testData["post150"];
            var client = ApiUtil.SetUrl(baseUrl, resourceUrl); 
             var request = ApiUtil.CreateGetRequest();
            var response = ApiUtil.GetResponse(client, request).Result;
            var actualStatusCode = (int)response.StatusCode; 
            int expectedStatusCode = int.Parse(testData["StatusCodeNotFound"]);
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "The status code is incorrect");

            string expectedBody = testData["emptyBody"]; 
            Assert.AreEqual(expectedBody, response.Content, "The response body is not empty");
        }

        [Test]
        public void CheckPutPostTest()
        {
            string resourceUrl = testData["posts"];
            string expectedTitle = StringUtil.GetRandomString();
            string expectedBody = StringUtil.GetRandomString();
            var userJson = applicationApi.GetRandomUserJson(expectedTitle, expectedBody);
            var client = ApiUtil.SetUrl(baseUrl, resourceUrl);
            var request = ApiUtil.CreatePostRequest(userJson);
            var response = ApiUtil.GetPostResponse(client, request).Result;
            var actualStatusCode = (int)response.StatusCode;
            int expectedStatusCode = int.Parse(testData["StatusCodeCreated"]); 
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "The status code is incorrect");

            var post = DeserializeUtil.GetData<Post>(response.Content);
            int expectedUserId = int.Parse(testData["createdIdUser"]); 
            int expectedId = int.Parse(testData["createdId"]);
            Assert.AreEqual(expectedUserId, post.UserId, "The user id didn't match");
            Assert.AreEqual(expectedId, post.Id, "The user id didn't match");
            Assert.AreEqual(expectedTitle, post.Title, "The title is empty");
            Assert.AreEqual(expectedBody, post.Body, "The body is empty");
        }

        [Test]
        public void CheckGetUsersTest()
        {
            string resourceUrl = testData["users"];
            var client = ApiUtil.SetUrl(baseUrl, resourceUrl);
            var request = ApiUtil.CreateGetRequest();
            var response = ApiUtil.GetResponse(client, request).Result;
            var actualStatusCode = (int)response.StatusCode;
            int expectedStatusCode = int.Parse(testData["StatusCodeOk"]);
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "The status code is incorrect");

            var expectedType = testData["Type"];
            Assert.AreEqual(expectedType, response.ContentType, "The list in response body is not json");

            int idUser = int.Parse(testData["idUser"]);
            var usersList = DeserializeUtil.GetData<List<User>>(response.Content);
            var user = applicationApi.GetUserById(idUser, usersList);
            string expectedName = testData["name"];
            string expectedUserName = testData["username"];
            string expectedEmail = testData["email"];
            string expectedStreet = testData["street"];
            string expectedSuite = testData["suite"];
            string expectedCity = testData["city"];
            string expectedZipcode = testData["zipcode"];
            string expectedLat = testData["lat"];
            string expectedLng = testData["lng"];
            string expectedPhone = testData["phone"];
            string expectedWebsite = testData["website"];
            string expectedNameCompany = testData["nameCompany"];
            string expectedCatchPhrase = testData["catchPhrase"];
            string expectedBs = testData["bs"];
            Assert.AreEqual(expectedName, user.Name, "The user name is incorrect");
            Assert.AreEqual(expectedUserName, user.Username, "The username is incorrect");
            Assert.AreEqual(expectedEmail, user.Email, "The user email is incorrect");
            Assert.AreEqual(expectedStreet, user.Address.Street, "The user street is incorrect");
            Assert.AreEqual(expectedSuite, user.Address.Suite, "The user suite is incorrect");
            Assert.AreEqual(expectedCity, user.Address.City, "The user city is incorrect");
            Assert.AreEqual(expectedZipcode, user.Address.Zipcode, "The user zipcode is incorrect");
            Assert.AreEqual(expectedLat, user.Address.Geo.Lat, "The user lat is incorrect");
            Assert.AreEqual(expectedLng, user.Address.Geo.Lng, "The user lng is incorrect");
            Assert.AreEqual(expectedPhone, user.Phone, "The user phone is incorrect");
            Assert.AreEqual(expectedWebsite, user.Website, "The user Website is incorrect");
            Assert.AreEqual(expectedNameCompany, user.Company.Name, "The company name is incorrect");
            Assert.AreEqual(expectedCatchPhrase, user.Company.CatchPhrase, "The company catchPhrase is incorrect");
            Assert.AreEqual(expectedBs, user.Company.Bs, "The company bs is incorrect");

            applicationApi.WriteUsersFile(user);
        }

        [Test]
        public void CheckGetUserTest()
        {
            string resourceUrl = testData["users5"];
            var client = ApiUtil.SetUrl(baseUrl, resourceUrl);
            var request = ApiUtil.CreateGetRequest();
            var response = ApiUtil.GetResponse(client, request).Result;
            var actualStatusCode = (int)response.StatusCode;
            int expectedStatusCode = int.Parse(testData["StatusCodeOk"]);
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "The status code is incorrect");

            var actualUser = DeserializeUtil.GetData<User>(response.Content);
            var expectedUser = applicationApi.GetPreviousUser();
            Assert.AreEqual(expectedUser.Name, actualUser.Name, "The user name is incorrect");
            Assert.AreEqual(expectedUser.Username, actualUser.Username, "The username is incorrect");
            Assert.AreEqual(expectedUser.Email, actualUser.Email, "The user email is incorrect");
            Assert.AreEqual(expectedUser.Address.Street, actualUser.Address.Street, "The user street is incorrect");
            Assert.AreEqual(expectedUser.Address.Suite, actualUser.Address.Suite, "The user suite is incorrect");
            Assert.AreEqual(expectedUser.Address.City, actualUser.Address.City, "The user city is incorrect");
            Assert.AreEqual(expectedUser.Address.Zipcode, actualUser.Address.Zipcode, "The user zipcode is incorrect");
            Assert.AreEqual(expectedUser.Address.Geo.Lat, actualUser.Address.Geo.Lat, "The user lat is incorrect");
            Assert.AreEqual(expectedUser.Address.Geo.Lng, actualUser.Address.Geo.Lng, "The user lng is incorrect");
            Assert.AreEqual(expectedUser.Phone, actualUser.Phone, "The user phone is incorrect");
            Assert.AreEqual(expectedUser.Website, actualUser.Website, "The user Website is incorrect");
            Assert.AreEqual(expectedUser.Company.Name, actualUser.Company.Name, "The company name is incorrect");
            Assert.AreEqual(expectedUser.Company.CatchPhrase, actualUser.Company.CatchPhrase, "The company catchPhrase is incorrect");
            Assert.AreEqual(expectedUser.Company.Bs, actualUser.Company.Bs, "The company bs is incorrect");

            applicationApi.DeleteUserFile();
        }
    }
}