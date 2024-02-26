using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllatmenhelyController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly ILogger<AllatmenhelyController> _logger;

        public AllatmenhelyController(
            AllatmenhelyDbContext context,
            ILogger<AllatmenhelyController> logger)
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
        [Route("GetAnimalsById")]
        public ActionResult<Animal> GetAnimalsById(int id)
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