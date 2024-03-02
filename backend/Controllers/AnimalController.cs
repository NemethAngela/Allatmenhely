
using backend.Models;
using backend.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<AnimalsResponseModel>> GetAllAnimals()
        {
            try
            {
                AnimalsResponseModel response = new AnimalsResponseModel
                {
                    Animals = await _context.Animals.ToListAsync()
                };

                return response;
            }
            catch (Exception ex)
            {
                return new AnimalsResponseModel { IsError = true, ErrorMessage = $"Hiba az �llatok lek�rdez�se sor�n: {ex}" };
            }
        }

        [HttpGet]
        [Route("GetAnimalById")]
        public async Task<ActionResult<AnimalResponseModel>> GetAnimalById(int id)
        {
            try
            {
                AnimalResponseModel response = new AnimalResponseModel
                {
                    Animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == id)
                };

                if (response.Animal == null)
                {
                    return new AnimalResponseModel { IsError = true, ErrorMessage = $"Az �llat nem tal�lhat�: id: {id}" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new AnimalResponseModel { IsError = true, ErrorMessage = $"Hiba az �llatok lek�rdez�se sor�n: {ex}" };
            }
        }

        [HttpGet]
        [Route("GetAnimalsByKindId")]
        public async Task<ActionResult<AnimalsResponseModel>> GetAnimalsByKindId(int kindId)
        {
            try
            {
                AnimalsResponseModel response;

                if (kindId == -1)
                {
                    var kutyaId = _context.Kinds.FirstOrDefaultAsync(x => x.Kind1 == "Kutya")?.Id;
                    var macskaId = _context.Kinds.FirstOrDefaultAsync(x => x.Kind1 == "Macska")?.Id;

                    response = new AnimalsResponseModel
                    {
                        Animals = await _context.Animals.Where(x => x.KindId != kutyaId && x.KindId != macskaId).ToListAsync()
                    };
                }
                else
                {
                    response = new AnimalsResponseModel
                    {
                        Animals = await _context.Animals.Where(x => x.KindId == kindId).ToListAsync()
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new AnimalsResponseModel { IsError = true, ErrorMessage = $"Hiba az �llatok lek�rdez�se sor�n: {ex}" };
            }
        }

        [Authorize]
        [HttpPost]
        [Route("CreateAnimal")]
        public async Task<ActionResult<BaseResponseModel>> CreateAnimal([FromBody] Animal animal)
        {
            try
            {
                animal.IsActive = 1;
                await _context.Animals.AddAsync(animal);
                await _context.SaveChangesAsync();

                return new BaseResponseModel();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba az �llat hozz�ad�sa sor�n: {ex}" };
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateAnimal")]
        public async Task<ActionResult<BaseResponseModel>> UpdateAnimal([FromBody] Animal newAnmimal)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == newAnmimal.Id);
                if (animal == null)
                {
                    return new BaseResponseModel { IsError = true, ErrorMessage = $"Az �llat nem tal�lhat�: id: {newAnmimal.Id}" };
                }

                animal.Name = newAnmimal.Name;
                animal.KindId = newAnmimal.KindId;
                animal.Age = newAnmimal.Age;
                animal.IsMale = newAnmimal.IsMale;
                animal.IsNeutered = newAnmimal.IsNeutered;
                animal.Description = newAnmimal.Description;
                animal.Photo = newAnmimal.Photo;
                animal.IsActive = newAnmimal.IsActive;

                await _context.SaveChangesAsync();

                return new BaseResponseModel();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba az �llat m�dos�t�sa sor�n: {ex}" };
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteAnimal")]
        public async Task<ActionResult<BaseResponseModel>> DeleteAnimal(int id)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == id);
                if (animal == null)
                {
                    return new BaseResponseModel { IsError = true, ErrorMessage = $"Az �llat nem tal�lhat�: id: {id}" };
                }

                animal.IsActive = 0;
                await _context.SaveChangesAsync();

                return new BaseResponseModel();
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { IsError = true, ErrorMessage = $"Hiba a fajta t�rl�se sor�n: {ex}" };
            }
        }
    }
}