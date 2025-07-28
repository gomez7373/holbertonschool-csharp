using System;
using Xunit;
using InventoryLibrary;

namespace InventoryManagement.Tests
{
    /// <summary>Unit tests for CRUD functionality.</summary>
    public class UnitTest1
    {
        [Fact]
        public void BaseClass_InitializesCorrectly()
        {
            var obj = new BaseClass();
            Assert.False(string.IsNullOrWhiteSpace(obj.id));
            Assert.True(obj.date_created <= DateTime.Now);
            Assert.True(obj.date_updated <= DateTime.Now);
        }

        [Fact]
        public void CanCreateAndRetrieveItem()
        {
            var storage = new JSONStorage();
            var item = new Item("Test") { description = "desc", price = 10.5f };
            storage.New(item);
            var key = $"Item.{item.id}";
            Assert.True(storage.All().ContainsKey(key));
        }

        [Fact]
        public void CanDeleteObject()
        {
            var storage = new JSONStorage();
            var user = new User("User");
            storage.New(user);
            var key = $"User.{user.id}";
            Assert.True(storage.Delete(key));
        }
    }
}
