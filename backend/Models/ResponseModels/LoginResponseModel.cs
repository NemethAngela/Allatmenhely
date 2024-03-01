
namespace backend.Models.ResponseModels
{
    public class LoginResponseModel : BaseResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
