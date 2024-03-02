using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using backend.Controllers;
using backend.Models;
using backend.Models.RequestModels;
using backend.Models.ResponseModels;
using Microsoft.Extensions.Configuration;
using backendTests;

namespace backend.Tests
{
    [TestClass()]
    public class AdminControllerTests
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
        public async Task LoginTest_Successful()
        {
            // Arrange
            var admin = new Admin { Id = 1, Email = "test@test.com", PasswordSalt = "salt", PasswordHash = "vO+IQWavsCwoVSHL8U2JLw==" };
            _context.Admins.Add(admin);
            _context.SaveChanges();

            var controller = new AdminController(_context, _configMock.Object);

            // Act
            var loginRequest = new LoginRequestModel { Email = "test@test.com", Password = "almafa" };
            var result = await controller.Login(loginRequest);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as LoginResponseModel;
            Assert.IsFalse(response.IsError);
            Assert.AreEqual(admin.Id, response.Id);
            Assert.AreEqual(admin.Email, response.Email);
            Assert.IsNotNull(response.Token);
        }

        [TestMethod()]
        public async Task LoginTest_Unsuccessful_WrongEmail()
        {
            // Arrange
            var admin = new Admin { Id = 1, Email = "test@test.com", PasswordSalt = "salt", PasswordHash = "vO+IQWavsCwoVSHL8U2JLw==" };
            _context.Admins.Add(admin);
            _context.SaveChanges();

            var controller = new AdminController(_context, _configMock.Object);

            // Act
            var loginRequest = new LoginRequestModel { Email = "blabla@test.com", Password = "almafa" };
            var result = await controller.Login(loginRequest);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as LoginResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Admin nem található", response.ErrorMessage);
        }

        [TestMethod()]
        public async Task LoginTest_Unsuccessful_WrongPassword()
        {
            // Arrange
            var admin = new Admin { Id = 1, Email = "test@test.com", PasswordSalt = "salt", PasswordHash = "vO+IQWavsCwoVSHL8U2JLw==" };
            _context.Admins.Add(admin);
            _context.SaveChanges();

            var controller = new AdminController(_context, _configMock.Object);

            // Act
            var loginRequest = new LoginRequestModel { Email = "test@test.com", Password = "wrong_password" };
            var result = await controller.Login(loginRequest);

            // Assert
            Assert.IsNotNull(result);
            var response = result.Value as LoginResponseModel;
            Assert.IsTrue(response.IsError);
            Assert.AreEqual("Admin email vagy jelszó nem megfelelő", response.ErrorMessage);
        }
    }
}
