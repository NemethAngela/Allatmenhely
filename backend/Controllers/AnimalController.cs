using backend.Controllers.ResponseModels;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly ILogger<AnimalController> _logger;

        public AnimalController(
            AllatmenhelyDbContext context,
            ILogger<AnimalController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllAnimals")]
        public async Task<ActionResult<AnimalsResponseModel>> GetAllAnimals()
        {
            try
            {
                AnimalsResponseModel response = new AnimalsResponseModel
                {
                    Animals = _context.Animals.ToList()
                };

                if (response.Animals == null || !response.Animals.Any())
                {
                    return NotFound(new AnimalsResponseModel { IsError = true, ErrorMessage = $"M�g nincs egyetlen �llat sem" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalsResponseModel { IsError = true, ErrorMessage = $"Hiba az �llatok lek�rdez�se sor�n: {ex}" });
            }
        }

        [HttpGet]
        [Route("GetAnimalById")]
        public async Task<ActionResult<AnimalResponseModel>> GetAnimalById(int id)
        {
            try
            {
                AnimalResponseModel response = new AnimalResponseModel
                {
                    Animal = _context.Animals.FirstOrDefault(x => x.Id == id)
                };

                if (response.Animal == null)
                {
                    return NotFound(new AnimalResponseModel { IsError = true, ErrorMessage = $"Az �llat nem tal�lhat�: id: {id}" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalResponseModel { IsError = true, ErrorMessage = $"Hiba az �llatok lek�rdez�se sor�n: {ex}" });
            }
        }
    }
}