using backend.Controllers.RequestModels;
using backend.Controllers.ResponseModels;
using backend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            AllatmenhelyDbContext context,
            ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel loginRequest)
        {
            try
            {
                LoginResponseModel response = new LoginResponseModel
                {
                    AdminUser = _context.Admins.FirstOrDefault(x => x.Email == loginRequest.Email)
                };

                if (response.AdminUser == null)
                {
                    return new LoginResponseModel { IsError = true, ErrorMessage = "Admin nem tal�lhat�" };
                }

                var valid = HashHelper.VerifyMD5Hash(loginRequest.Password, response.AdminUser.PasswordSalt, response.AdminUser.PasswordHash);

                if (!valid)
                {
                    return new LoginResponseModel { IsError = true, ErrorMessage = "Admin email vagy jelsz� nem megfelel�" };
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.None,
                    MaxAge = TimeSpan.FromDays(1)
                };

                var token = JWTHelper.GenerateToken(response.AdminUser);
                Response.Cookies.Append("jwtToken", token, cookieOptions);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalsResponseModel { IsError = true, ErrorMessage = $"Hiba a bejelentkez�s sor�n: {ex}" });
            }
        }
    }
}