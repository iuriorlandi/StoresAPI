using Microsoft.AspNetCore.Mvc;
using Moq;
using StoresAPI.Application;
using StoresAPI.Application.DTOs;
using StoresAPI.Application.Interface;
using StoresAPI.Controllers;
using StoresAPI.Domain;
using StoresAPI.Infrastructure.Data;
using StoresAPI.Infrastructure.Repositories;

namespace StoresAPI.Tests
{
    public class StoreControllerTests
    {
        //SUT
        private StoreController _storeController;

        private Mock<IStoreApplication> _mockStoreApplication;

        public StoreControllerTests()
        {
            _mockStoreApplication = new Mock<IStoreApplication>();
            _storeController = new StoreController(_mockStoreApplication.Object);
        }

        [Fact]
        public async Task CreateStore_ValidStore_ReturnsCreated()
        {
            // Arrange
            var storeDto = new StoreDTO { Name = "Store1", Country = "Country1", Address = "Address1", CompanyId = 1 };
            var store = storeDto.ToStore();

            _mockStoreApplication.Setup(s => s.CreateStoreAsync(It.IsAny<StoreDTO>())).ReturnsAsync(store);

            // Act
            var result = await _storeController.CreateStore(storeDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task DeleteStore_ExistingStore_ReturnsNoContent()
        {
            // Arrange
            var storeId = 1;
            _mockStoreApplication.Setup(s => s.DeleteStoreAsync(storeId)).ReturnsAsync(true);

            // Act
            var result = await _storeController.DeleteStore(storeId) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task DeleteStore_NonExistingStore_ReturnsNotFound()
        {
            // Arrange
            var storeId = 999;
            _mockStoreApplication.Setup(s => s.DeleteStoreAsync(storeId)).ReturnsAsync(false);

            // Act
            var result = await _storeController.DeleteStore(storeId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetStore_ExistingStore_ReturnsOk()
        {
            // Arrange
            var storeId = 1;
            _mockStoreApplication.Setup(s => s.GetStoreAsync(storeId)).ReturnsAsync(new Store { Id = storeId });

            // Act
            var result = await _storeController.GetStore(storeId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<Store>(result.Value);
        }

        [Fact]
        public async Task GetStore_NonExistingStore_ReturnsNotFound()
        {
            // Arrange
            var storeId = 999;
            _mockStoreApplication.Setup(s => s.GetStoreAsync(storeId)).ReturnsAsync((Store)null);

            // Act
            var result = await _storeController.GetStore(storeId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateStore_ValidStore_ReturnsNoContent()
        {
            // Arrange
            var storeId = 1;
            var updatedStore = new StoreDTO { Name = "UpdatedStore", Country = "UpdatedCountry", Address = "UpdatedAddress", CompanyId = 1 };
            _mockStoreApplication.Setup(s => s.UpdateStoreAsync(storeId, updatedStore)).ReturnsAsync(new Store { Id = storeId });

            // Act
            var result = await _storeController.UpdateStore(storeId, updatedStore) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateStore_NonExistingStore_ReturnsNotFound()
        {
            // Arrange
            var storeId = 1;
            var updatedStore = new StoreDTO { Name = "UpdatedStore", Country = "UpdatedCountry", Address = "UpdatedAddress", CompanyId = 1 };
            _mockStoreApplication.Setup(s => s.UpdateStoreAsync(storeId, updatedStore)).ReturnsAsync((Store) null);

            // Act
            var result = await _storeController.UpdateStore(storeId, updatedStore) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

    }
}