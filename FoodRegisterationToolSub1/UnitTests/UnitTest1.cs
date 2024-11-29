using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    // Explicitly define your namespace for the "Item" class to avoid conflicts.
    using ItemNamespace = UnitTests.Item;

    public class UnitTest1
    {
        private readonly List<ItemNamespace> _mockDb;
        private readonly ItemController _controller;

        public UnitTest1()
        {
            _mockDb = new List<ItemNamespace>
            {
                new ItemNamespace { Id = 1, Name = "Item1", Description = "Description1" },
                new ItemNamespace { Id = 2, Name = "Item2", Description = "Description2" }
            };
            _controller = new ItemController(_mockDb);
        }

        [Fact]
        public void CreateItem_ValidData_ReturnsCreatedItem()
        {
            // Arrange
            var newItem = new ItemNamespace { Id = 3, Name = "Item3", Description = "Description3" };

            // Act
            var result = _controller.CreateItem(newItem);

            // Assert
            Assert.Equal(newItem, result);
            Assert.Contains(newItem, _mockDb);
        }

        [Fact]
        public void CreateItem_InvalidData_ReturnsNull()
        {
            // Arrange
            var newItem = new ItemNamespace { Id = 0, Name = "", Description = "" };

            // Act
            var result = _controller.CreateItem(newItem);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetItem_ValidId_ReturnsItem()
        {
            // Act
            var result = _controller.GetItem(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Item1", result.Name);
        }

        [Fact]
        public void GetItem_InvalidId_ReturnsNull()
        {
            // Act
            var result = _controller.GetItem(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateItem_ValidId_ReturnsUpdatedItem()
        {
            // Arrange
            var updatedItem = new ItemNamespace { Name = "UpdatedName", Description = "UpdatedDescription" };

            // Act
            var result = _controller.UpdateItem(1, updatedItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedName", result.Name);
            Assert.Equal("UpdatedDescription", result.Description);
        }

        [Fact]
        public void UpdateItem_InvalidId_ReturnsNull()
        {
            // Arrange
            var updatedItem = new ItemNamespace { Name = "UpdatedName", Description = "UpdatedDescription" };

            // Act
            var result = _controller.UpdateItem(999, updatedItem);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteItem_ValidId_ReturnsTrue()
        {
            // Act
            var result = _controller.DeleteItem(1);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(_mockDb, item => item.Id == 1);
        }

        [Fact]
        public void DeleteItem_InvalidId_ReturnsFalse()
        {
            // Act
            var result = _controller.DeleteItem(999);

            // Assert
            Assert.False(result);
        }
    }

    public class ItemController
    {
        private readonly List<ItemNamespace> _db;

        public ItemController(List<ItemNamespace> db)
        {
            _db = db;
        }

        public ItemNamespace CreateItem(ItemNamespace item)
        {
            if (item.Id <= 0 || string.IsNullOrEmpty(item.Name)) return null;
            _db.Add(item);
            return item;
        }

        public ItemNamespace GetItem(int id) => _db.FirstOrDefault(i => i.Id == id);

        public ItemNamespace UpdateItem(int id, ItemNamespace updatedItem)
        {
            var item = _db.FirstOrDefault(i => i.Id == id);
            if (item == null) return null;

            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            return item;
        }

        public bool DeleteItem(int id)
        {
            var item = _db.FirstOrDefault(i => i.Id == id);
            if (item == null) return false;

            _db.Remove(item);
            return true;
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
