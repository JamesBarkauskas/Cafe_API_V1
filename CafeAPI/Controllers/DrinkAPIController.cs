using AutoMapper;
using CafeAPI.Models;
using CafeAPI.Models.Dto;
using CafeAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetDrinks()
        {
            IEnumerable<Drink> drinks = await _dbDrink.GetAllAsync();
            //if (drinks == null)
            //{
            //    ModelState.AddModelError("ErrorMessages", "Nothing exists..");
            //    return BadRequest(ModelState);
            //}
            _response.Result = _mapper.Map<List<DrinkDTO>>(drinks);
            _response.StatusCode = HttpStatusCode.OK;
            return _response;
        }

        [HttpGet("{id:int}", Name ="GetDrink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetDrink(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("ErrorMessages", "Id of 0 does not exist.");
                return BadRequest(ModelState);
            }
            var drink = await _dbDrink.GetAsync(u => u.Id == id);
            if (drink == null)
            {
                ModelState.AddModelError("ErrorMessages", "Id of " + id + " doesn't exist.");
                return NotFound(ModelState);
            }
            _response.Result = drink;
            _response.StatusCode = HttpStatusCode.OK;
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> CreateDrink([FromBody]DrinkCreateDTO drink)
        {
            if (drink == null)
            {
                ModelState.AddModelError("ErrorMessages", "Drink obj cannot be null.");
                return BadRequest(ModelState);
            }

            await _dbDrink.CreateAsync(_mapper.Map<Drink>(drink));
            _response.Result = drink;
            _response.StatusCode=HttpStatusCode.OK;
            return _response;
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> RemoveDrink(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("ErrorMessages", "Id cannot be 0");
                return BadRequest(ModelState);
            }
            var drink = await _dbDrink.GetAsync(u => u.Id == id);
            if (drink == null)
            {
                ModelState.AddModelError("ErrorMessages", "Id of " + id + " is invalid.");
                return NotFound(ModelState);
            }

            await _dbDrink.RemoveAsync(drink);
            _response.StatusCode = HttpStatusCode.OK;
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateDrink")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> UpdateDrink(int id, [FromBody]DrinkDTO drink)
        {
            if (id != drink.Id)
            {
                ModelState.AddModelError("ErrorMessages", "Id must match the id entered.");
                return BadRequest(ModelState);
            }

            if (drink == null)
            {
                ModelState.AddModelError("ErrorMessages", "Drink obj cannot be null");
                return BadRequest(ModelState);
            }

            await _dbDrink.UpdateAsync(_mapper.Map<Drink>(drink));
            _response.StatusCode = HttpStatusCode.OK;
            return _response;
        }
    }
}
