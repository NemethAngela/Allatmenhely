using backend.Models;

namespace backend.Controllers.ResponseModels
{
    public class EnqueryResponseModel : BaseResponseModel
    {
        public Enquery Enquery { get; set; }
    }

    public class EnqueriesResponseModel : BaseResponseModel
    {
        public IEnumerable<Enquery> Enqueries { get; set; }
    }
}
