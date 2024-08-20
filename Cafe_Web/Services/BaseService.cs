using Cafe_Utility;
using Cafe_Web.Models;
using Cafe_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Cafe_Web.Services
{
    // our generic class to make an api request and fetch the response... calls an endpoint, gets response, returns response back..
    public class BaseService : IBaseService
    {
        public APIResponse responseModel {  get; set; }
        public IHttpClientFactory httpClient { get; set; }  // this allows us to actually call the api

        public BaseService(IHttpClientFactory httpClient)
        {
            responseModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("CafeAPI");
                HttpRequestMessage requestMessage = new HttpRequestMessage();  // message is what gets exchanged bt client/server? contains header, http verb, data..
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.RequestUri = new Uri(apiRequest.Url);       // grabs the url from our request obj..
                if (apiRequest.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");   // install Newtonsoft..
                }

                // determine which Http method...
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        requestMessage.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        requestMessage.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        requestMessage.Method = HttpMethod.Delete;
                        break;
                    default:
                        requestMessage.Method = HttpMethod.Get;
                        break;
                }

                // our request message is configured.. has Headers, Uri, Data, and Method...
                // when we send the above request, we'll get a response back.. (below)

                HttpResponseMessage responseMessage = null; // response message containing status code and data.. of the request..? null by deffault
                responseMessage = await client.SendAsync(requestMessage);  // set our response equal to whatever is returned when we send our request message.. this calls the api endpoint by sending our configured request...

                var apiContent = await responseMessage.Content.ReadAsStringAsync(); // extract the content from the response message
                var apiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponse;


                //try
                //{
                //    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                //    if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                //    {
                //        apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                //        apiResponse.IsSuccess = false;
                //        var res = JsonConvert.SerializeObject(apiResponse);     // converts obj to a json string
                //        var returnObj = JsonConvert.DeserializeObject<T>(res);  // converts to obj of type T
                //        return returnObj;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                //    return exceptionResponse;
                //}
                //var apiRes = JsonConvert.DeserializeObject<T>(apiContent);
                //return apiRes;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse   // in case of error, we create a APIResponse that has errors and IsFalse...
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto); // have to serialize and deserialize the obj.. cannot return a dto obj, must be of type T..?
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
