using Xunit;
using InventoryLibrary;

namespace InventoryManagement.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestUserCreation()
        {
            var user = new User("Test");
            Assert.Equal("Test", user.name);
        }

        [Fact]
        public void TestItemCreation()
        {
            var item = new Item("Item1");
            Assert.Equal("Item1", item.name);
        }
    }
}
