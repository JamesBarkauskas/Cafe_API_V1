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
            var response = await _foodService.GetAllAsync<APIResponse>();   // call the api..

            if (response != null && response.IsSuccess)
            {
                foods = JsonConvert.DeserializeObject<List<FoodDTO>>(Convert.ToString(response.Result));
            }
            return View(foods);
        }

        public async Task<IActionResult> CreateFood()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // should include when using POST method..?
        public async Task<IActionResult> CreateFood(FoodCreateDTO dto)
        {
            if (ModelState.IsValid) // refers to the validations of the FoodCreateDTO model...
            {
                var response = await _foodService.CreateAsync<APIResponse>(dto);    // if modelstate is valid, call the api..
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("IndexFood");
                }
            }
            return View(dto);
        }
    }
}
