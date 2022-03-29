using RestSharp;
using System.Threading.Tasks;

namespace RestAPI
{
    public static class ApiUtil
    {

        public static RestClient SetUrl(string baseUrl, string resourceUrl)
        {
            var url = $"{baseUrl}{resourceUrl}";
            var restClient = new RestClient(url);
            return restClient;
        }

        public static async Task<RestResponse> GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return await restClient.GetAsync(restRequest);
        }

        public static async Task<RestResponse> GetPostResponse(RestClient restClient, RestRequest restRequest)
        {
            return await restClient.PostAsync(restRequest);
        }

        public static RestRequest CreateGetRequest() => new RestRequest();

        public static RestRequest CreatePostRequest(string json)
        {
            RestRequest restRequest = new RestRequest();
            restRequest.AddStringBody(json, DataFormat.Json);
            return restRequest;
        }
    }
}
