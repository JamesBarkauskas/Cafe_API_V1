using AutoMapper;
using Cafe_Web.Models;
using Cafe_Web.Models.Dto;
using Cafe_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cafe_Web.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IMapper _mapper;

        public FoodController(IFoodService foodService, IMapper mapper)
        {
            _foodService = foodService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexFood()
        {
            List<FoodDTO> foods = new();
            var response = await _foodService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                foods = JsonConvert.DeserializeObject<List<FoodDTO>>(Convert.ToString(response.Result));
            }
            return View(foods);
        }
    }
}
