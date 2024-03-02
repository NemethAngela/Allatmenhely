using backend.Helpers;
using backend.Models.RequestModels;
using backend.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(
            AllatmenhelyDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel loginRequest)
        {
            try
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email == loginRequest.Email);

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
                return new LoginResponseModel { IsError = true, ErrorMessage = $"Hiba a bejelentkezés során: {ex}" };
            }
        }
    }
}