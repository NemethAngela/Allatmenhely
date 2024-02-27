using backend.Models;

namespace backend.Controllers.ResponseModels
{
    public class AnimalResponseModel : BaseResponseModel
    {
        public Animal Animal { get; set; }
    }

    public class AnimalsResponseModel : BaseResponseModel
    {
        public IEnumerable<Animal> Animals { get; set; }
    }
}
