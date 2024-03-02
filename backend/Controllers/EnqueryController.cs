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
                    return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"M�g nincs egyetlen �rdekl�d�s sem" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az �rdekl�d�sek lek�rdez�se sor�n: {ex}" };
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
                    return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"M�g nincs egyetlen �rdekl�d�s sem" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az �rdekl�d�sek lek�rdez�se sor�n: {ex}" };
            }
        }
    }
}