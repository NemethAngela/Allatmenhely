using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Configuration;
using backendTests;

namespace backend.Tests
{
    public class TestBase
    {
        public AllatmenhelyDbContext _context;
        public Mock<IConfiguration> _configMock;

        [TestInitialize]
        public void Initialize()
        {
            var options = TestHelper.CreateNewContextOptions();
            _context = new AllatmenhelyDbContext(options);
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(x => x["Jwt:Secret"]).Returns("ThisIsAVerySecretKeyOfAllatmenhelyWebsite");
            _configMock.Setup(x => x["Jwt:ExpirationHours"]).Returns("24");
        }
    }
}
