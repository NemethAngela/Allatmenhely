using backend.Controllers;
using backend.Models;
using backend.Models.ResponseModels;
using backendTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace backend.Tests
{
    [TestClass()]
    public class EnqueryControllerTests : TestBase
    {
        [TestMethod()]
        public async Task GetAllEnqueriesTest()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new EnqueryController(_context);

            // Act
            var result = await controller.GetAllEnqueries();

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as EnqueriesResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(response.Enqueries.Count(), 3);
        }

        [TestMethod()]
        public async Task GetEnqueriesByAnimalIdTest()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new EnqueryController(_context);
            var animalId = 2;

            // Act
            var result = await controller.GetEnqueriesByAnimalId(animalId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as EnqueriesResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(response.Enqueries.Count(), 2);
        }

        [TestMethod()]
        public async Task CreateEnqueryTest()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new EnqueryController(_context);
            var newEnquery = new Enquery
            {
                AnimalId = 1,
                Email = "erdeklodes@gmail.com",
                Phone = "+3620582635"
            };

            // Act
            var result = await controller.CreateEnquery(newEnquery);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsFalse(response.IsError);
            var erdeklodes = _context.Enqueries.FirstOrDefault(x => x.Email == "erdeklodes@gmail.com");
            Assert.IsNotNull(erdeklodes);
            Assert.AreEqual(erdeklodes.Phone, "+3620582635");
            Assert.AreEqual(erdeklodes.AnimalId, 1);
        }
    }
}