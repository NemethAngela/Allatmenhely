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
        public ActionResult<IEnumerable<Kind>> GetAllKinds()
        {
            var kinds = _context.Kinds.ToList();
            return Ok(kinds);
        }

        [HttpGet]
        [Route("GetKindById")]
        public ActionResult<Kind> GetKindById(int id)
        {
            var kind = _context.Kinds.FirstOrDefault(x => x.Id == id);
            if (kind == null)
            {
                return NotFound("A fajta nem található");
            }

            return Ok(kind);
        }

        [HttpPost]
        [Route("CreateKind")]
        public ActionResult<bool> CreateKind([FromBody] Kind newKind)
        {
            try
            {
                var exists = _context.Kinds.Any(x => x.Kind1 == newKind.Kind1);
                if (exists)
                {

                }

                _context.Kinds.Add(newKind);
                _context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateKind")]
        public ActionResult<bool> UpdateKind([FromBody] Kind newKind, int id)
        {
            try
            {
                var kind = _context.Kinds.FirstOrDefault(x => x.Id == id);
                if (kind == null)
                {
                    return NotFound("A fajta nem található");
                }

                kind.Kind1 = newKind.Kind1;
                _context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    return NotFound("A fajta nem található");
                }

                _context.Kinds.Remove(kind);
                _context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}