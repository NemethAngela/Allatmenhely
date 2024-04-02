using backend.Models;
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
                    Enqueries = await _context.Enqueries.Include(x => x.Animal).ToListAsync()
                };

                return response;
            }
            catch (Exception ex)
            {
                return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az érdeklõdések lekérdezése során: {ex}" };
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
                    Enqueries = await _context.Enqueries.Include(x => x.Animal).Where(x => x.Animal.Id == animalId).ToListAsync()
                };

                return response;
            }
            catch (Exception ex)
            {
                return new EnqueriesResponseModel { IsError = true, ErrorMessage = $"Hiba az érdeklõdések lekérdezése során: {ex}" };
            }
        }

        [HttpPost]
        [Route("CreateEnquery")]
        public async Task<ActionResult<BaseResponseModel>> CreateEnquery([FromBody] Enquery newEnquery)
        {
            try
            {
                await _context.Enqueries.AddAsync(newEnquery);
                await _context.SaveChangesAsync();

                return new BaseResponseModel();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba az érdeklõdés felvétele során: {ex}" };
            }
        }
    }
}