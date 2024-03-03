using backend.Controllers;
using backend.Models.ResponseModels;
using backendTests;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace backend.Tests
{
    [TestClass()]
    public class EnqueryControllerTests
    {
        private AllatmenhelyDbContext _context;
        private Mock<IConfiguration> _configMock;

        [TestInitialize]
        public void Initialize()
        {
            var options = TestHelper.CreateNewContextOptions();
            _context = new AllatmenhelyDbContext(options);
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(x => x["Jwt:Secret"]).Returns("ThisIsAVerySecretKeyOfAllatmenhelyWebsite");
            _configMock.Setup(x => x["Jwt:ExpirationHours"]).Returns("24");
        }

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
            Assert.AreEqual(response.Enqueries.Count(), 2);
        }


        [TestMethod()]
        public void GetEnqueriesByAnimalIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateEnqueryTest()
        {
            Assert.Fail();
        }
    }
}