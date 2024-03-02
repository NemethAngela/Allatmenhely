using backend.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnqueryController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;

        public EnqueryController(
            AllatmenhelyDbContext context)
        {
            _context = context;
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
                    return NotFound(new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Még nincs egyetlen érdeklődés sem" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az érdeklődések lekérdezése során: {ex}" });
            }
        }
    }
}