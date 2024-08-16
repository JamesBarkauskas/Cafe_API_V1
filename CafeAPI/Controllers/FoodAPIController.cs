using CafeAPI.Data;
using CafeAPI.Models;
using CafeAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CafeAPI.Repository.IRepository;

namespace CafeAPI.Controllers
{
    [Route("api/FoodAPI")]  // route to our endpoints.. 'localhost:.../api/FoodAPI'
    [ApiController]
    public class FoodAPIController : ControllerBase
    {
        //private readonly AppDbContext _db;
        private readonly IFoodRepository _dbFood;
        private readonly IMapper _mapper;
        public FoodAPIController(IFoodRepository dbFood, IMapper mapper)
        {
            //_db = db;
            _dbFood = dbFood;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Food>> GetFoods()
        {
            IEnumerable<Food> foods_list = await _dbFood.GetAllAsync();
            // IEnumerable<Food> foods_list = _db.Foods.ToList<Food>();
            //IEnumerable<FoodDTO> foods_list_dto = _mapper.Map<FoodDTO>(foods_list);
            //List<FoodDTO> foods_list_dto = new();
            //foreach(Food food in foods_list)
            //{
            //    FoodDTO food_dto = new()
            //    {
            //        Id = food.Id,
            //        Name = food.Name,
            //        Price = food.Price,
            //        ImageUrl = food.ImageUrl,
            //        Details = food.Details
            //    };
            //    foods_list_dto.Add(food_dto);
            //}

            //var foods_dto_list = _mapper.Map<List<FoodDTO>>(foods_list);
            return Ok(_mapper.Map<List<FoodDTO>>(foods_list));
            
        }

        [HttpGet("{id:int}", Name ="GetFood")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FoodDTO>> GetFood(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("ErrorMessages", "Id of " +id+ " does not exist.");
                return BadRequest(ModelState);
            }
            var food = await _dbFood.GetAsync(u => u.Id == id);
            if (food == null)
            {
                ModelState.AddModelError("ErrorMessages", "Id of " + id + " does not exist.");
                return NotFound(ModelState);
            }

            return Ok(_mapper.Map<FoodDTO>(food));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<FoodDTO>> CreateFood([FromBody]FoodCreateDTO food)
        {
            if (food == null)
            {
                ModelState.AddModelError("ErrorMessages", "Food object cannot be null");
                return BadRequest(ModelState);
            }

            //Food model = new()
            //{
            //    Id = food.Id,
            //    Name = food.Name,
            //    Price = food.Price,
            //    ImageUrl = food.ImageUrl,
            //    Details = food.Details,
            //    CreatedDate = DateTime.Now
            //};



            //await _db.Foods.AddAsync(_mapper.Map<Food>(food));
            //await _db.SaveChangesAsync();
            await _dbFood.CreateAsync(_mapper.Map<Food>(food));
            return Ok(food);

        }

        [HttpDelete]
        //[ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Food>> DeleteFood(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("ErrorMessages", "Id of 0 does not exist.");
                return BadRequest(ModelState);
            }
            var food_item = _dbFood.GetAsync(u => u.Id == id);
            if (food_item == null)
            {
                ModelState.AddModelError("ErrorMessages", "Id of " + id + " does not exist.");
                return NotFound(ModelState);
            }

            await _dbFood.RemoveAsync(await food_item);     // unsure why I need to include 'await' before 'food_item' too...
            //await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateFood")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateFood(int id, [FromBody]FoodUpdateDTO food)
        {
            if (food.Id != id || food == null)
            {
                ModelState.AddModelError("ErrorMessages", "Id of object must match id of parameter.");
                return BadRequest(ModelState);
            }

            Food model = _mapper.Map<Food>(food);
            await _dbFood.UpdateAsync(model);

            return Ok(food);
        }
    }
}
