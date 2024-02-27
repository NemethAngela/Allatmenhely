using backend.Models;

namespace backend.Models.ResponseModels
{
    public class KindResponseModel : BaseResponseModel
    {
        public Kind Kind { get; set; }
    }

    public class KindsResponseModel : BaseResponseModel
    {
        public IEnumerable<Kind> Kinds { get; set; }
    }
}
