using CafeAPI.Data;
using CafeAPI.Models;
using CafeAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
        public ActionResult<Food> GetFoods()
        {

            IEnumerable<Food> foods_list = _db.Foods.ToList<Food>();
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


            return Ok(_mapper.Map<List<FoodDTO>>(foods_list));
        }

        [HttpGet("{id:int}", Name ="GetFood")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<FoodDTO> GetFood(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var food = _db.Foods.FirstOrDefault(u => u.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FoodDTO>(food));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<FoodDTO> CreateFood([FromBody]FoodDTO food)
        {
            if (food == null || food.Id != 0)
            {
                return BadRequest();
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


            
            _db.Foods.Add(_mapper.Map<Food>(food));
            _db.SaveChanges();
            return Ok(food);

        }

        [HttpDelete]
        //[ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFood(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var food_item = _db.Foods.FirstOrDefault(u => u.Id == id);
            if (food_item == null)
            {
                return NotFound();
            }

            _db.Foods.Remove(food_item);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateFood(int id, [FromBody]FoodDTO food)
        {
            if (food.Id != id || food == null)
            {
                return BadRequest();
            }

            Food model = _mapper.Map<Food>(food);
            _db.Update(model);
            _db.SaveChanges();

            return Ok(food);
        }
    }
}
