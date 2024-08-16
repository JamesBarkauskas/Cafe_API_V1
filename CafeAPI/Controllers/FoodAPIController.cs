using CafeAPI.Data;
using CafeAPI.Models;
using CafeAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Controllers
{
    [Route("api/FoodAPI")]  // route to our endpoints.. 'localhost:.../api/FoodAPI'
    [ApiController]
    public class FoodAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public FoodAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Food>> GetFoods()
        {
            IEnumerable<Food> foods_list = await _db.Foods.ToListAsync();
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
            var food = await _db.Foods.FirstOrDefaultAsync(u => u.Id == id);
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


            
            await _db.Foods.AddAsync(_mapper.Map<Food>(food));
            await _db.SaveChangesAsync();
            return Ok(food);

        }

        [HttpDelete]
        //[ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteFood(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("ErrorMessages", "Id of 0 does not exist.");
                return BadRequest(ModelState);
            }
            var food_item = _db.Foods.FirstOrDefault(u => u.Id == id);
            if (food_item == null)
            {
                ModelState.AddModelError("ErrorMessages", "Id of " + id + " does not exist.");
                return NotFound(ModelState);
            }

            _db.Foods.Remove(food_item);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFood(int id, [FromBody]FoodUpdateDTO food)
        {
            if (food.Id != id || food == null)
            {
                ModelState.AddModelError("ErrorMessages", "Id of object must match id of parameter.");
                return BadRequest(ModelState);
            }

            Food model = _mapper.Map<Food>(food);
            _db.Update(model);
            await _db.SaveChangesAsync();

            return Ok(food);
        }
    }
}
