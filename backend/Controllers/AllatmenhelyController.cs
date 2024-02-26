using backend.Controllers.RequestModels;
using backend.Controllers.ResponseModels;
using backend.Helpers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using backend.Helpers;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllatmenhelyController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly ILogger<AllatmenhelyController> _logger;

        public AllatmenhelyController(
            AllatmenhelyDbContext context,
            ILogger<AllatmenhelyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Admin

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel loginRequest)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.Email == loginRequest.Email);

            if (admin == null)
            {
                return new LoginResponseModel { IsError = true, ErrorMessage = "Admin nem található" };
            }

            var valid = HashHelper.VerifyMD5Hash(loginRequest.Password, admin.PasswordSalt, admin.PasswordHash);

            if (!valid)
            {
                return new LoginResponseModel { IsError = true, ErrorMessage = "Admin email vagy jelszó nem megfelelõ" };
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                MaxAge = TimeSpan.FromDays(1)
            };

            var token = JWTHelper.GenerateToken(admin);
            Response.Cookies.Append("jwtToken", token, cookieOptions);

            return Ok(new LoginResponseModel { adminUser = admin });
        }

        #endregion

        #region Animal

        [HttpGet]
        [Route("Animals/GetAllAnimals")]
        public ActionResult<IEnumerable<Animal>> GetAllAnimals()
        {
            var animals = _context.Animals.ToList();
            return Ok(animals);
        }

        [HttpGet]
        [Route("Animals/GetAnimalById")]
        public ActionResult<Animal> GetAnimalById(int id)
        {
            var animal = _context.Animals.FirstOrDefault(x => x.Id == id);
            if (animal == null)
            {
                return NotFound("Az állat nem található");
            }

            return Ok(animal);
        }

        #endregion

        #region Enquery

        #endregion

        #region Kind

        [HttpGet]
        [Route("Kinds/GetAllKinds")]
        public ActionResult<IEnumerable<Kind>> GetAllKinds()
        {
            var kinds = _context.Kinds.ToList();
            return Ok(kinds);
        }

        [HttpGet]
        [Route("Kinds/GetKindById")]
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
        [Route("Kinds/CreateKind")]
        public ActionResult<bool> CreateKind([FromBody] Kind newKind)
        {
            try
            {
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
        [Route("Kinds/UpdateKind")]
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

        #endregion
    }
}