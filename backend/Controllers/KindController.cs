using backend.Models;
using backend.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KindController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;

        public KindController(AllatmenhelyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllKinds")]
        public async Task<ActionResult<KindsResponseModel>> GetAllKinds()
        {
            try
            {
                KindsResponseModel response = new KindsResponseModel
                {
                    Kinds = await _context.Kinds.ToListAsync()
                };

                if (response.Kinds == null || !response.Kinds.Any())
                {
                    return new KindsResponseModel { IsError = true, ErrorMessage = $"M�g nincs egyetlen fajta sem" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new KindsResponseModel { IsError = true, ErrorMessage = $"Hiba a fajt�k lek�rdez�se sor�n: {ex}" };
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
                    Kind = await _context.Kinds.FirstOrDefaultAsync(x => x.Id == id)
                };

                if (response.Kind == null)
                {
                    return new KindResponseModel { IsError = true, ErrorMessage = $"A fajta nem tal�lhat�: id: {id}" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new KindResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta lek�rdez�se sor�n: {ex}" };
            }
        }

        [HttpPost]
        [Route("CreateKind")]
        public async Task<ActionResult<BaseResponseModel>> CreateKind([FromBody] Kind newKind)
        {
            try
            {
                var exists = await _context.Kinds.AnyAsync(x => x.Kind1 == newKind.Kind1);
                if (exists)
                {
                    return new BaseResponseModel { IsError = true, ErrorMessage = $"A fajta m�r l�tezik" };
                }

                await _context.Kinds.AddAsync(newKind);
                await _context.SaveChangesAsync();

                return new BaseResponseModel();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta felv�tele sor�n: {ex}" };
            }
        }

        [HttpPut]
        [Route("UpdateKind")]
        public async Task<ActionResult<BaseResponseModel>> UpdateKind([FromBody] Kind newKind)
        {
            try
            {
                var kind = await _context.Kinds.FirstOrDefaultAsync(x => x.Id == newKind.Id);
                if (kind == null)
                {
                    return new BaseResponseModel { IsError = true, ErrorMessage = $"A fajta nem tal�lhat�: id: {newKind.Id}" };
                }

                var exists = await _context.Kinds.AnyAsync(x => x.Kind1 == newKind.Kind1 && x.Id != newKind.Id);
                if (exists)
                {
                    return new BaseResponseModel { IsError = true, ErrorMessage = $"Ez a fajta m�r l�tezik" };
                }

                kind.Kind1 = newKind.Kind1;
                await _context.SaveChangesAsync();

                return new BaseResponseModel();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta m�dos�t�sa sor�n: {ex}" };
            }
        }

        [HttpDelete]
        [Route("DeleteKind")]
        public async Task<ActionResult<BaseResponseModel>> DeleteKind(int id)
        {
            try
            {
                var kind = await _context.Kinds.FirstOrDefaultAsync(x => x.Id == id);
                if (kind == null)
                {
                    return new BaseResponseModel { IsError = true, ErrorMessage = $"A fajta nem tal�lhat�: id: {id}" };
                }

                _context.Kinds.RemoveRange(kind);
                await _context.SaveChangesAsync();

                return new BaseResponseModel();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta t�rl�se sor�n: {ex}" };
            }
        }
    }
}