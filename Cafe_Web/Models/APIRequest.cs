using Microsoft.AspNetCore.Mvc;
using static Cafe_Utility.SD;

namespace Cafe_Web.Models
{
    public class APIRequest 
    {
        public ApiType ApiType { get; set; }    // Request type.. GET,POST,etc..
        public string Url { get; set; }         // url to endpoint..
        public object Data { get; set; }        // data being passed in the case of create/update..
    }
}
