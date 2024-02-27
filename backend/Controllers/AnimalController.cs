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
        public ActionResult<IEnumerable<Animal>> GetAllAnimals()
        {
            var animals = _context.Animals.ToList();
            return Ok(animals);
        }

        [HttpGet]
        [Route("GetAnimalById")]
        public ActionResult<Animal> GetAnimalById(int id)
        {
            var animal = _context.Animals.FirstOrDefault(x => x.Id == id);
            if (animal == null)
            {
                return NotFound("Az állat nem található");
            }

            return Ok(animal);
        }
    }
}