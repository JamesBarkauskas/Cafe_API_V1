using Cafe_Web.Models;

namespace Cafe_Web.Services.IServices
{
    public interface IBaseService   // need a service that will make an API request and fetch the API Response..
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);    // using generic type T allows us to not have to declare the type beforehand.. can be generic for now until called
    }
}
