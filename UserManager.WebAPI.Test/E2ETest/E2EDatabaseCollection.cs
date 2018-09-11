using Xunit;

namespace UserManager.AppService.Test.E2ETest
{
    [CollectionDefinition("E2E")]
    public class E2EDatabaseCollection : ICollectionFixture<E2EControllerTestFixture>
    {
    }
}
