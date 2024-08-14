using CafeAPI.Data;
using CafeAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.Controllers
{
    [Route("api/FoodAPI")]  // route to our endpoints.. 'localhost:.../api/FoodAPI'
    [ApiController]
    public class FoodAPIController : ControllerBase
    {
        FoodStore foodStore = new FoodStore();

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<FoodDTO> GetFoods()
        {

            IEnumerable<FoodDTO> foods_list = foodStore.FoodList;
            return Ok(foods_list);
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
            var food = foodStore.FoodList.FirstOrDefault(u => u.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
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

            var new_food = food;
            foodStore.FoodList.Add(new_food);
            return Ok(new_food);

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
            var food_item = foodStore.FoodList.FirstOrDefault(u => u.Id == id);
            if (food_item == null)
            {
                return NotFound();
            }

            foodStore.FoodList.Remove(food_item);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateFood(int id, [FromBody]FoodDTO food)
        {
            if (food.Id != id || food == null)
            {
                return BadRequest();
            }

            return Ok(food);
        }
    }
}
