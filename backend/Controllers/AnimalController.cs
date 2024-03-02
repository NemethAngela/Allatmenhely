
using backend.Models;
using backend.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly AllatmenhelyDbContext _context;

        public AnimalController(
            AllatmenhelyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllAnimals")]
        public ActionResult<AnimalsResponseModel> GetAllAnimals()
        {
            try
            {
                AnimalsResponseModel response = new AnimalsResponseModel
                {
                    Animals = _context.Animals.ToList()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalsResponseModel { IsError = true, ErrorMessage = $"Hiba az állatok lekérdezése során: {ex}" });
            }
        }

        [HttpGet]
        [Route("GetAnimalById")]
        public ActionResult<AnimalResponseModel> GetAnimalById(int id)
        {
            try
            {
                AnimalResponseModel response = new AnimalResponseModel
                {
                    Animal = _context.Animals.FirstOrDefault(x => x.Id == id)
                };

                if (response.Animal == null)
                {
                    return NotFound(new AnimalResponseModel { IsError = true, ErrorMessage = $"Az állat nem található: id: {id}" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalResponseModel { IsError = true, ErrorMessage = $"Hiba az állatok lekérdezése során: {ex}" });
            }
        }

        [HttpGet]
        [Route("GetAnimalsByKindId")]
        public ActionResult<AnimalsResponseModel> GetAnimalsByKindId(int kindId)
        {
            try
            {
                AnimalsResponseModel response;

                if (kindId == -1)
                {
                    var kutyaId = _context.Kinds.FirstOrDefault(x => x.Kind1 == "Kutya")?.Id;
                    var macskaId = _context.Kinds.FirstOrDefault(x => x.Kind1 == "Macska")?.Id;

                    response = new AnimalsResponseModel
                    {
                        Animals = _context.Animals.Where(x => x.KindId != kutyaId && x.KindId != macskaId).ToList()
                    };
                }
                else
                {
                    response = new AnimalsResponseModel
                    {
                        Animals = _context.Animals.Where(x => x.KindId == kindId).ToList()
                    };
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new AnimalsResponseModel { IsError = true, ErrorMessage = $"Hiba az állatok lekérdezése során: {ex}" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("CreateAnimal")]
        public ActionResult<BaseResponseModel> CreateAnimal([FromBody] Animal animal)
        {
            try
            {
                animal.IsActive = 1;
                animal.TimeStamp = DateTime.Now;
                _context.Animals.Add(animal);
                _context.SaveChanges();

                return Ok(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba az állat hozzáadása során: {ex}" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateAnimal")]
        public ActionResult<BaseResponseModel> UpdateAnimal([FromBody] Animal newAnmimal)
        {
            try
            {
                var animal = _context.Animals.FirstOrDefault(x => x.Id == newAnmimal.Id);
                if (animal == null)
                {
                    return NotFound(new BaseResponseModel { IsError = true, ErrorMessage = $"Az állat nem található: id: {newAnmimal.Id}" });
                }

                animal.Name = newAnmimal.Name;
                animal.KindId = newAnmimal.KindId;
                animal.Age = newAnmimal.Age;
                animal.IsMale = newAnmimal.IsMale;
                animal.IsNeutered = newAnmimal.IsNeutered;
                animal.Description = newAnmimal.Description;
                animal.Photo = newAnmimal.Photo;
                animal.IsActive = newAnmimal.IsActive;

                _context.SaveChanges();

                return Ok(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba az állat módosítása során: {ex}" });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteAnimal")]
        public ActionResult<BaseResponseModel> DeleteAnimal(int id)
        {
            try
            {
                var animal = _context.Animals.FirstOrDefault(x => x.Id == id);
                if (animal == null)
                {
                    return NotFound(new BaseResponseModel { IsError = true, ErrorMessage = $"Az állat nem található: id: {id}" });
                }

                animal.IsActive = 0;
                _context.SaveChanges();

                return Ok(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta törlése során: {ex}" });
            }
        }
    }
}