using System.Net;

namespace CafeAPI.Models
{
    public class APIResponse    // instead of our api methods returning different types, we'll have one standard return type...
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
