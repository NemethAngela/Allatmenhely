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
        [Route("Kinds/GetAllKinds")]
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
                    return NotFound(new AnimalsResponseModel { IsError = true, ErrorMessage = $"M�g nincs egyetlen fajta sem" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalsResponseModel { IsError = true, ErrorMessage = $"Hiba a fajt�k lek�rdez�se sor�n: {ex}" });
            }
        }

        [HttpGet]
        [Route("Kinds/GetKindById")]
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
                    return NotFound(new AnimalResponseModel { IsError = true, ErrorMessage = $"A fajta nem tal�lhat�: id: {id}" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta lek�rdez�se sor�n: {ex}" });
            }
        }

        [HttpPost]
        [Route("Kinds/CreateKind")]
        public async Task<ActionResult<BaseResponseModel>> CreateKind([FromBody] Kind newKind)
        {
            try
            {
                var exists = _context.Kinds.Any(x => x.Kind1 == newKind.Kind1);
                if (exists)
                {
                    return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"A fajta m�r l�tezik" });
                }

                _context.Kinds.Add(newKind);
                _context.SaveChanges();

                return Ok(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta felv�tele sor�n: {ex}" });
            }
        }

        [HttpPut]
        [Route("Kinds/UpdateKind")]
        public async Task<ActionResult<BaseResponseModel>> UpdateKind([FromBody] Kind newKind)
        {
            try
            {
                var kind = _context.Kinds.FirstOrDefault(x => x.Id == newKind.Id);
                if (kind == null)
                {
                    return NotFound(new AnimalResponseModel { IsError = true, ErrorMessage = $"A fajta nem tal�lhat�: id: {newKind.Id}" });
                }

                var exists = _context.Kinds.Any(x => x.Kind1 == newKind.Kind1 && x.Id != newKind.Id);
                if (exists)
                {
                    return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Ez a fajta m�r l�tezik" });
                }

                kind.Kind1 = newKind.Kind1;
                _context.SaveChanges();

                return Ok(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta m�dos�t�sa sor�n: {ex}" });
            }
        }

        [HttpDelete]
        [Route("DeleteKind")]
        public ActionResult<bool> DeleteKind(int id)
        {
            try
            {
                var kind = _context.Kinds.FirstOrDefault(x => x.Id == id);
                if (kind == null)
                {
                    return NotFound(new AnimalResponseModel { IsError = true, ErrorMessage = $"A fajta nem tal�lhat�: id: {id}" });
                }

                _context.Kinds.Remove(kind);
                _context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta t�rl�se sor�n: {ex}" });
            }
        }
    }
}