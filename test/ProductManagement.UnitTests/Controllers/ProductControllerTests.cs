using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Interfaces;
using ProductManagement.Web.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProductManagement.Core.Entities;

namespace ProductManagement.UnitTests.Controllers {
    [TestClass]
    public class ProductControllerTests {
        private Mock<IProductRepository> _mockRepo;
        private Mock<IMapper> _mockMapper;
        private ProductController _controller;

        [TestInitialize]
        public void Setup() {
            _mockRepo = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ProductController(_mockRepo.Object, _mockMapper.Object);
        }

        [TestMethod]
        public async Task GetProducts_ReturnsOkResult_WithListOfProductDtos() {
            // Arrange
            var products = new List<ProductDto> { new ProductDto { Id = 1, Name = "Test Product" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());
            _mockMapper.Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>())).Returns(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ProductDto>));
        }


    }
}
