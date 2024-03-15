using backend.Controllers;
using backend.Models;
using backend.Models.ResponseModels;
using backendTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace backend.Tests
{
    [TestClass()]
    public class AnimalControllerTests : TestBase
    {
        [TestMethod()]
        public async Task GetAllAnimalsTest()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);

            // Act
            var result = await controller.GetAllAnimals();

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as AnimalsResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(response.Animals.Count(), 4);
        }

        [TestMethod()]
        public async Task GetAnimalByIdTest_Successful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var expectedId = 1;

            // Act
            var result = await controller.GetAnimalById(expectedId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as AnimalResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(response.Animal.Id, expectedId);
        }

        [TestMethod()]
        public async Task GetAnimalByIdTest_Unsuccessful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var expectedId = 666;

            // Act
            var result = await controller.GetAnimalById(expectedId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as AnimalResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.IsNull(response.Animal);
        }

        [TestMethod()]
        public async Task GetAnimalsByKindIdTest_NoCatsOrDogs()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var kindId = -1;

            // Act
            var result = await controller.GetAnimalsByKindId(kindId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as AnimalsResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.IsNotNull(response.Animals);
            Assert.AreEqual(response.Animals.Count(), 2);
        }

        [TestMethod()]
        public async Task GetAnimalsByKindIdTest_CatsOnly()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var kindId = 2;

            // Act
            var result = await controller.GetAnimalsByKindId(kindId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as AnimalsResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.IsNotNull(response.Animals);
            Assert.AreEqual(response.Animals.Count(), 1);
        }

        [TestMethod()]
        public async Task CreateAnimalTest()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var expectedAnimalName = "TestAnimal";
            var newAnimal = new Animal
            {
                Name = expectedAnimalName,
                Description = "Nagyon kedves cica",
                KindId = 2,
                Age = 1,
                IsMale = 1,
                IsNeutered = 1,
                IsActive = 1
            };

            // Act
            var result = await controller.CreateAnimal(newAnimal);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsFalse(response.IsError);
            var animal = _context.Animals.FirstOrDefault(x => x.Name == expectedAnimalName);
            Assert.IsNotNull(animal);
        }

        [TestMethod()]
        public async Task UpdateAnimalTest_NotFound()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var elsoAllat = _context.Animals.First();
            elsoAllat.Id = 666;

            // Act
            var result = await controller.UpdateAnimal(elsoAllat);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.ErrorMessage, $"Az állat nem található: id: {elsoAllat.Id}");
        }

        [TestMethod()]
        public async Task UpdateAnimalTest_Successful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var elsoAllat = _context.Animals.First();
            var regiAllatName = elsoAllat.Name;
            elsoAllat.Name = regiAllatName + "updated";

            // Act
            var result = await controller.UpdateAnimal(elsoAllat);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsFalse(response.IsError);
            var updatedAnimal = _context.Animals.FirstOrDefault(x => x.Name == regiAllatName + "updated");
            Assert.IsNotNull(updatedAnimal);
        }

        [TestMethod()]
        public async Task DeleteAnimalTest_Successful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var allat = _context.Animals.First();

            // Act
            var result = await controller.DeleteAnimal(allat.Id);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsFalse(response.IsError);
            var deletedAnimal = _context.Animals.FirstOrDefault(x => x.Id == allat.Id);
            Assert.IsNotNull(deletedAnimal);
            Assert.AreEqual(0, (int)deletedAnimal.IsActive);
        }

        [TestMethod()]
        public async Task DeleteAnimalTest_NotFound()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new AnimalController(_context);
            var deleteId = 666;

            // Act
            var result = await controller.DeleteAnimal(deleteId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.ErrorMessage, $"Az állat nem található: id: {deleteId}");
        }
    }
}