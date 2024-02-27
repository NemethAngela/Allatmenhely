using backend.Models.ResponseModels;
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
        public async Task<ActionResult<EnqueriesResponseModel>> GetAllEnqueries()
        {
            try
            {
                EnqueriesResponseModel response = new EnqueriesResponseModel
                {
                    Enqueries = _context.Enqueries.ToList()
                };

                if (response.Enqueries == null || !response.Enqueries.Any())
                {
                    return NotFound(new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Még nincs egyetlen érdeklõdés sem" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az érdeklõdések lekérdezése során: {ex}" });
            }
        }
    }
}