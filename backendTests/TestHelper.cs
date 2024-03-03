
using backend;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backendTests
{
    public static class TestHelper
    {
        public static DbContextOptions<AllatmenhelyDbContext> CreateNewContextOptions()
        {
            var builder = new DbContextOptionsBuilder<AllatmenhelyDbContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            return builder.Options;
        }

        public static void FillTestDb(AllatmenhelyDbContext _context)
        {
            var admin = new Admin { Id = 1, Email = "test@test.com", PasswordSalt = "salt", PasswordHash = "vO+IQWavsCwoVSHL8U2JLw==" };
            _context.Admins.Add(admin);

            var kutya = new Kind { Id = 1, Kind1 = "Kutya" };
            var macska = new Kind { Id = 2, Kind1 = "Macska" };
            var tengerimalac = new Kind { Id = 3, Kind1 = "Tengerimalac" };
            var horcsog = new Kind { Id = 4, Kind1 = "Hörcsög" };
            _context.Kinds.AddRange(kutya, macska, tengerimalac, horcsog);

            var animals = new List<Animal>
            {
                new Animal { Id = 1, Name = "Bodri", KindId = 1, Age = 3, IsMale = 1, IsNeutered = 0, IsActive = 1, TimeStamp = DateTime.Now },
                new Animal { Id = 2, Name = "Cirmi", KindId = 2, Age = 2, IsMale = 0, IsNeutered = 1, IsActive = 1, TimeStamp = DateTime.Now },
                new Animal { Id = 3, Name = "Dundi", KindId = 3, Age = 1, IsMale = 1, IsNeutered = 0, IsActive = 1, TimeStamp = DateTime.Now },
                new Animal { Id = 4, Name = "Füles", KindId = 3, Age = 1, IsMale = 1, IsNeutered = 1, IsActive = 1, TimeStamp = DateTime.Now }
            };
            _context.Animals.AddRange(animals);

            var enqueries = new List<Enquery>
            {
                new Enquery { Id = 1, TimeStamp = DateTime.Now, Phone = "123456789", AnimalId = 1, Email = "enquery1@example.com" },
                new Enquery { Id = 2, TimeStamp = DateTime.Now, Phone = "987654321", AnimalId = 2, Email = "enquery2@example.com" },
                new Enquery { Id = 3, TimeStamp = DateTime.Now, Phone = "234567890", AnimalId = 2, Email = "enquery3@example.com" }
            };
            _context.Enqueries.AddRange(enqueries);

            _context.SaveChanges();
        }
    }
}
