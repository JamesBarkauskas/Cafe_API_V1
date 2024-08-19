using AutoMapper;
using CafeAPI.Models;
using CafeAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Controllers
{
    [Route("api/DrinkAPI")]
    [ApiController]
    public class DrinkAPIController : ControllerBase
    {
        private readonly IDrinkRepository _dbDrink;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public DrinkAPIController(IDrinkRepository dbDrink, IMapper mapper)
        {
            _dbDrink = dbDrink;
            _mapper = mapper;
            _response = new();
        }


    }
}
