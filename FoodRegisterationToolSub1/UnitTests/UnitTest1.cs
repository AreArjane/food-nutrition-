using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ItemController"/> class.
    /// </summary>
    // Explicitly define your namespace for the "Item" class to avoid conflicts.
    using ItemNamespace = UnitTests.Item;
    /// <summary>
        /// Initializes a new instance of the <see cref="UnitTest1"/> class.
        /// Sets up a mock database and controller instance for testing.
        /// </summary>
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
         /// <summary>
        /// Tests that a valid item can be created and is added to the database.
        /// </summary>
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
       /// <summary>
        /// Tests that attempting to create an item with invalid data returns null.
        /// </summary>
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
        /// <summary>
        /// Tests that a valid item ID returns the corresponding item.
        /// </summary>
        [Fact]
        public void GetItem_ValidId_ReturnsItem()
        {
            // Act
            var result = _controller.GetItem(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Item1", result.Name);
        }
     /// <summary>
        /// Tests that an invalid item ID returns null.
        /// </summary>
        [Fact]
        public void GetItem_InvalidId_ReturnsNull()
        {
            // Act
            var result = _controller.GetItem(999);

            // Assert
            Assert.Null(result);
        }
     /// <summary>
        /// Tests that an existing item can be updated and the changes are persisted.
        /// </summary>
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
        /// <summary>
        /// Tests that attempting to update a non-existent item returns null.
        /// </summary>
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
        /// <summary>
        /// Tests that an item with a valid ID can be deleted from the database.
        /// </summary>
        [Fact]
        public void DeleteItem_ValidId_ReturnsTrue()
        {
            // Act
            var result = _controller.DeleteItem(1);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(_mockDb, item => item.Id == 1);
        }
         /// <summary>
        /// Tests that attempting to delete a non-existent item returns false.
        /// </summary>
        [Fact]
        public void DeleteItem_InvalidId_ReturnsFalse()
        {
            // Act
            var result = _controller.DeleteItem(999);

            // Assert
            Assert.False(result);
        }
    }
   /// <summary>
    /// Manages CRUD operations for <see cref="Item"/> objects in a simulated database.
    /// </summary>
    public class ItemController
    {
        private readonly List<ItemNamespace> _db;
       /// <summary>
        /// Initializes a new instance of the <see cref="ItemController"/> class with a reference to the database.
        /// </summary>
        /// <param name="db">The database to manage.</param>
        public ItemController(List<ItemNamespace> db)
        {
            _db = db;
        }
       /// <summary>
        /// Adds a new item to the database if it passes validation.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>The created item, or null if validation fails.</returns>
        public ItemNamespace CreateItem(ItemNamespace item)
        {
            if (item.Id <= 0 || string.IsNullOrEmpty(item.Name)) return null;
            _db.Add(item);
            return item;
        }
   /// <summary>
        /// Retrieves an item by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>The matching item, or null if no match is found.</returns>
        public ItemNamespace GetItem(int id) => _db.FirstOrDefault(i => i.Id == id);
       /// <summary>
        /// Updates an existing item with new data.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="updatedItem">The updated item data.</param>
        /// <returns>The updated item, or null if the item does not exist.</returns>
        public ItemNamespace UpdateItem(int id, ItemNamespace updatedItem)
        {
            var item = _db.FirstOrDefault(i => i.Id == id);
            if (item == null) return null;

            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            return item;
        }
        /// <summary>
        /// Updates an existing item with new data.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="updatedItem">The updated item data.</param>
        /// <returns>The updated item, or null if the item does not exist.</returns>
        public bool DeleteItem(int id)
        {
            var item = _db.FirstOrDefault(i => i.Id == id);
            if (item == null) return false;

            _db.Remove(item);
            return true;
        }
    }
  /// <summary>
    /// Represents an item entity with an ID, name, and description.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; set; }
    }
}
