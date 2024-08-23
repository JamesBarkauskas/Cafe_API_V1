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

        public async Task<IActionResult> UpdateFood(int foodId) // the param 'foodId' here must match the asp-route name given in the indexFood page...
        {
            var response = await _foodService.GetAsync<APIResponse>(foodId);    // make api call to grab the food item with the id being passed in
            if (response != null && response.IsSuccess)
            {
                FoodDTO model = JsonConvert.DeserializeObject<FoodDTO>(Convert.ToString(response.Result));  // first convert to string, then deserialize that into a FoodDTO
                return View(_mapper.Map<FoodUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFood(FoodUpdateDTO dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _foodService.UpdateAsync<APIResponse>(dto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("IndexFood");
                }
            }
            return View(dto);
        }

        public async Task<IActionResult> DeleteFood(int foodId)
        {
            var response = await _foodService.GetAsync<APIResponse>(foodId);
            if (response != null && response.IsSuccess)
            {
                FoodDTO model = JsonConvert.DeserializeObject<FoodDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFood(FoodDTO dto)
        {
            var response = await _foodService.DeleteAsync<APIResponse>(dto.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction("IndexFood");
            }
            return View(dto);
        }
    }
}
