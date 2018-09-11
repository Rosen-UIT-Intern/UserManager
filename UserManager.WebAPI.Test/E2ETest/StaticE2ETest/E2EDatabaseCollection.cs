using UserManager.AppService.Test.E2ETest;
using Xunit;

namespace UserManager.AppService.Test.StaticE2ETest
{
    [CollectionDefinition("StaticE2E")]
    public class E2EDatabaseCollection : ICollectionFixture<E2EControllerTestFixture>
    {
    }
}
