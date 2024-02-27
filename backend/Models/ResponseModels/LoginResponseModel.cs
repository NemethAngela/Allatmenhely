using backend.Models;

namespace backend.Models.ResponseModels
{
    public class LoginResponseModel : BaseResponseModel
    {
        public Admin AdminUser { get; set; }
    }
}
