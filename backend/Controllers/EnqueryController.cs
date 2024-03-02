using backend.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    Enqueries = await _context.Enqueries.ToListAsync()
                };

                if (response.Enqueries == null || !response.Enqueries.Any())
                {
                    return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Még nincs egyetlen érdeklődés sem" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az érdeklődések lekérdezése során: {ex}" };
            }
        }

        [HttpGet]
        [Route("GetEnqueriesByAnimalId")]
        public async Task<ActionResult<EnqueriesResponseModel>> GetEnqueriesByAnimalId(int animalId)
        {
            try
            {
                EnqueriesResponseModel response = new EnqueriesResponseModel
                {
                    Enqueries = await _context.Enqueries.Where(x => x.AnimalId == animalId).ToListAsync()
                };

                if (response.Enqueries == null || !response.Enqueries.Any())
                {
                    return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Még nincs egyetlen érdeklődés sem" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az érdeklődések lekérdezése során: {ex}" };
            }
        }
    }
}