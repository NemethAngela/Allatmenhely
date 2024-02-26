using backend.Models;

namespace backend.Controllers.ResponseModels
{
    public class LoginResponseModel : BaseResponseModel
    {
        public Admin adminUser { get; set; }
    }
}
