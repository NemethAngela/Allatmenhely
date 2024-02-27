using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnqueryController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly ILogger<EnqueryController> _logger;

        public EnqueryController(
            AllatmenhelyDbContext context,
            ILogger<EnqueryController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("GetAllEnqueries")]
        public ActionResult<IEnumerable<Enquery>> GetAllEnqueries()
        {
            var enqueries = _context.Enqueries.ToList();
            return Ok(enqueries);
        }
    }
}