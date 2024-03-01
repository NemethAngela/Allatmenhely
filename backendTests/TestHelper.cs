
using backend;
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
    }
}
