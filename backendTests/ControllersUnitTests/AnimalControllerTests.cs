using backend.Controllers;
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
        public async Task GetAnimalsByKindIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task CreateAnimalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task UpdateAnimalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task DeleteAnimalTest()
        {
            Assert.Fail();
        }
    }
}