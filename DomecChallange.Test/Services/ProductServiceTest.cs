using DomecChallange.Data.Context;
using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Dtos.Enums;
using DomecChallange.Dtos.ProdcutDtos;
using DomecChallange.Service.Interfaces;
using DomecChallange.Service.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DomecChallange.Test.Services
{
    [TestClass]
    public class ProductServiceTest
    {
        private DomecChallangeDbContext mockContext;
        private ProductService mockService;
        private Product item;
        [TestInitialize]
        public void Setup()
        {
            DbContextOptions<DomecChallangeDbContext> options;
            var builder = new DbContextOptionsBuilder<DomecChallangeDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            mockContext = new DomecChallangeDbContext(options);
            mockService = new ProductService(mockContext);
            item = new Product { Code = 1, Name = "orange", Quantity = 1 };
        }

        [TestCleanup]
        public void Teardown()
        {
            mockService = null;
            mockContext = null;
        }

        [TestMethod]
        public async Task CreateAsync_WhenCall_ShouldHaveAddedItem()
        {
            var result = await mockService.CreateAsync(item);

            Assert.IsTrue(mockContext.Products.Any(a => a.Code == item.Code));
            Assert.AreEqual(mockContext.Products.FirstOrDefault(a => a.Code == item.Code).Code,result.ReturnModel.Code);
            Assert.IsInstanceOfType(result,typeof(StatusDto<Product>));
            Assert.AreEqual(result.Status, StatusEnum.Success);
        }
        [TestMethod]
        public async Task CreateAsync_WhenCallWithDuplicateProductName_ShouldReturnFalse()
        {
            var initial = await mockService.CreateAsync(item);

            var result = await mockService.CreateAsync(item);

            Assert.IsInstanceOfType(result, typeof(StatusDto<Product>));
            Assert.AreNotEqual(result.Status, StatusEnum.Success);

        }
        [TestMethod]
        public async Task UpdateAsync_WhenCall_ShouldUpdateAddedItem()
        {
            var add = await mockService.CreateAsync(item);
            var addedItem = mockContext.Products.FirstOrDefault(a => a.Code == item.Code);

            addedItem.Quantity = 2;
            var result = await mockService.UpdateAsync(addedItem);

            Assert.IsTrue(mockContext.Products.Any(a => a.Quantity == 2));
            Assert.AreEqual(mockContext.Products.FirstOrDefault(a => a.Code == item.Code).Quantity, result.ReturnModel.Quantity);
            Assert.IsInstanceOfType(result, typeof(StatusDto<Product>));
            Assert.AreEqual(result.Status , StatusEnum.Success);

        }
        [TestMethod]
        public async Task DeleteAsync_WhenCall_ShouldRemoveAddedItem()
        {
            var add = await mockService.CreateAsync(item);
            var addedItem = mockContext.Products.FirstOrDefault(a => a.Code == item.Code);

            var result = await mockService.DeleteAsync(addedItem.UniqueId);

            Assert.IsTrue(!mockContext.Products.Any(a => a.Code == addedItem.Code));
            Assert.IsInstanceOfType(result, typeof(StatusDto<Product>));
            Assert.AreEqual(result.Status, StatusEnum.Success);

        }
        [TestMethod]
        public async Task GetAsync_WhenCallByName_ShouldReturnAddedItem()
        {
            var add = await mockService.CreateAsync(item);

            var result = await mockService.GetAsync(item.Name);

            Assert.AreEqual(result.Name, item.Name);
            Assert.IsInstanceOfType(result, typeof(Product));

        }
        [TestMethod]
        public void CheckingEntryData_WhenCallWithInvalidProductName_ShouldReturnInvalidStatusMessage()
        {
            var model = new EditProductDto { Name = String.Empty , Quantity =2 };


            var result = mockService.CheckingEntryData(model);


            Assert.IsInstanceOfType(result, typeof(ValidationMessageDto));
            Assert.IsFalse(result.IsValid);

        }
        [TestMethod]
        public void CheckingEntryData_WhenCallWithInvalidQuantity_ShouldReturnInvalidStatusMessage()
        {
            var model = new EditProductDto { Quantity = 0 , Name="apple" };


            var result = mockService.CheckingEntryData(model);


            Assert.IsInstanceOfType(result, typeof(ValidationMessageDto));
            Assert.IsFalse(result.IsValid);

        }

       

    }

}