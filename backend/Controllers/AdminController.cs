using backend.Helpers;
using backend.Models.RequestModels;
using backend.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;

        public AdminController(
            AllatmenhelyDbContext context,
            ILogger<AdminController> logger,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel loginRequest)
        {
            try
            {
                var admin = _context.Admins.FirstOrDefault(x => x.Email == loginRequest.Email);

                if (admin == null)
                {
                    return new LoginResponseModel { IsError = true, ErrorMessage = "Admin nem tal�lhat�" };
                }

                var valid = HashHelper.VerifyMD5Hash(loginRequest.Password, admin.PasswordSalt, admin.PasswordHash);

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

                var token = JWTHelper.GenerateToken(admin, _configuration);
                //Response.Cookies.Append("jwtToken", token, cookieOptions);

                LoginResponseModel response = new LoginResponseModel
                {
                    Id = admin.Id,
                    Email = admin.Email,
                    Token = token
                };

                return response;
            }
            catch (Exception ex)
            {
                return new LoginResponseModel { IsError = true, ErrorMessage = $"Hiba a bejelentkez�s sor�n: {ex}" };
            }
        }
    }
}