using backend.Controllers.ResponseModels;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KindController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly ILogger<KindController> _logger;

        public KindController(
            AllatmenhelyDbContext context,
            ILogger<KindController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllKinds")]
        public async Task<ActionResult<KindsResponseModel>> GetAllKinds()
        {
            try
            {
                KindsResponseModel response = new KindsResponseModel
                {
                    Kinds = _context.Kinds.ToList()
                };

                if (response.Kinds == null || !response.Kinds.Any())
                {
                    return NotFound(new KindsResponseModel { IsError = true, ErrorMessage = $"Még nincs egyetlen fajta sem" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new KindsResponseModel { IsError = true, ErrorMessage = $"Hiba a fajták lekérdezése során: {ex}" });
            }
        }

        [HttpGet]
        [Route("GetKindById")]
        public async Task<ActionResult<KindResponseModel>> GetKindById(int id)
        {
            try
            {
                KindResponseModel response = new KindResponseModel
                {
                    Kind = _context.Kinds.FirstOrDefault(x => x.Id == id)
                };

                if (response.Kind == null)
                {
                    return NotFound(new KindResponseModel { IsError = true, ErrorMessage = $"A fajta nem található: id: {id}" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new KindResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta lekérdezése során: {ex}" });
            }
        }

        [HttpPost]
        [Route("CreateKind")]
        public async Task<ActionResult<BaseResponseModel>> CreateKind([FromBody] Kind newKind)
        {
            try
            {
                var exists = _context.Kinds.Any(x => x.Kind1 == newKind.Kind1);
                if (exists)
                {
                    return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"A fajta már létezik" });
                }

                _context.Kinds.Add(newKind);
                _context.SaveChanges();

                return Ok(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta felvétele során: {ex}" });
            }
        }

        [HttpPut]
        [Route("UpdateKind")]
        public async Task<ActionResult<BaseResponseModel>> UpdateKind([FromBody] Kind newKind)
        {
            try
            {
                var kind = _context.Kinds.FirstOrDefault(x => x.Id == newKind.Id);
                if (kind == null)
                {
                    return NotFound(new BaseResponseModel { IsError = true, ErrorMessage = $"A fajta nem található: id: {newKind.Id}" });
                }

                var exists = _context.Kinds.Any(x => x.Kind1 == newKind.Kind1 && x.Id != newKind.Id);
                if (exists)
                {
                    return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Ez a fajta már létezik" });
                }

                kind.Kind1 = newKind.Kind1;
                _context.SaveChanges();

                return Ok(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta módosítása során: {ex}" });
            }
        }

        [HttpDelete]
        [Route("DeleteKind")]
        public async Task<ActionResult<BaseResponseModel>> DeleteKind(int id)
        {
            try
            {
                var kind = _context.Kinds.FirstOrDefault(x => x.Id == id);
                if (kind == null)
                {
                    return NotFound(new BaseResponseModel { IsError = true, ErrorMessage = $"A fajta nem található: id: {id}" });
                }

                _context.Kinds.Remove(kind);
                _context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta törlése során: {ex}" });
            }
        }
    }
}