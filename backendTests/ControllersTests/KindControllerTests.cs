
using backend.Controllers;
using backend.Models;
using backend.Models.ResponseModels;
using backendTests;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace backend.Tests
{
    [TestClass()]
    public class KindControllerTests
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
        public async Task GetAllKindsTest()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);

            // Act
            var result = await controller.GetAllKinds();

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as KindsResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(response.Kinds.Count(), 4);
        }

        [TestMethod()]
        public async Task GetKindByIdTest_Successful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var expectedId = 1;

            // Act
            var result = await controller.GetKindById(expectedId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as KindResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(response.Kind.Id, expectedId);
        }

        [TestMethod()]
        public async Task GetKindByIdTest_Unsuccessful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var expectedId = 666;

            // Act
            var result = await controller.GetKindById(expectedId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as KindResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.IsNull(response.Kind);
        }

        [TestMethod()]
        public async Task CreateKindTest_Successful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var newKind = new Kind
            {
                Kind1 = "Hal"
            };

            // Act
            var result = await controller.CreateKind(newKind);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsFalse(response.IsError);
            var hal = _context.Kinds.FirstOrDefault(x => x.Kind1 == "Hal");
            Assert.IsNotNull(hal);
        }

        [TestMethod()]
        public async Task CreateKindTest_Unsuccessful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var elsoFajta = _context.Kinds.First();
            var newKind = new Kind
            {
                Kind1 = elsoFajta.Kind1
            };

            // Act
            var result = await controller.CreateKind(newKind);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.ErrorMessage, "A fajta már létezik");
        }

        [TestMethod()]
        public async Task UpdateKindTest_Successful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var elsoFajta = _context.Kinds.First();
            var regiFajta = elsoFajta.Kind1;
            elsoFajta.Kind1 = elsoFajta.Kind1 + "updated";

            // Act
            var result = await controller.UpdateKind(elsoFajta);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsFalse(response.IsError);
            var updatedKind = _context.Kinds.FirstOrDefault(x => x.Kind1 == regiFajta + "updated");
            Assert.IsNotNull(updatedKind);
        }

        [TestMethod()]
        public async Task UpdateKindTest_Unsuccessful_NotFound()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var elsoFajta = _context.Kinds.First();
            elsoFajta.Id = 666;

            // Act
            var result = await controller.UpdateKind(elsoFajta);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.ErrorMessage, $"A fajta nem található: id: {elsoFajta.Id}");
        }

        [TestMethod()]
        public async Task UpdateKindTest_Unsuccessful_AlreadyExists()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var elsoFajta = _context.Kinds.First();
            var masodikFajta = _context.Kinds.Last();
            elsoFajta.Kind1 = masodikFajta.Kind1;

            // Act
            var result = await controller.UpdateKind(elsoFajta);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.ErrorMessage, $"Ez a fajta már létezik");
        }

        [TestMethod()]
        public async Task DeleteKindTest_Successful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var horcsog = _context.Kinds.First(x => x.Kind1 == "Hörcsög");

            // Act
            var result = await controller.DeleteKind(horcsog.Id);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsFalse(response.IsError);
            var deletedKind = _context.Kinds.FirstOrDefault(x => x.Id == horcsog.Id);
            Assert.IsNull(deletedKind);
        }

        [TestMethod()]
        public async Task DeleteKindTest_Unsuccessful()
        {
            // Arrange
            TestHelper.FillTestDb(_context);
            var controller = new KindController(_context);
            var deleteId = 666;

            // Act
            var result = await controller.DeleteKind(deleteId);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as BaseResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual(response.ErrorMessage, $"A fajta nem található: id: {deleteId}");
        }
    }
}