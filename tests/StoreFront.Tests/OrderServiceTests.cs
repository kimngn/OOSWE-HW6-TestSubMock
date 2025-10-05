using System.Collections.Generic;
using Moq;
using StoreFront.Core.Models;
using StoreFront.Core.Services;
using Xunit;

namespace StoreFront.Tests
{
    public class OrderServiceTests
    {
        //Mock example
        [Fact]
        public void CalculateOrderTotal_NoPromotion_AddsShipping()
        {
            // arrange
            var productA = new Product { ProductId = 1, Name = "A", Price = 10.00m };
            var items = new List<OrderLineItem> { new(productA, 2) }; // subtotal = 20
            var shippingPolicy = new ShippingPolicy { BaseCost = 5.00m, FreeShippingThreshold = 100.00m };
            var service = new OrderService();

            // act
            var total = service.CalculateOrderTotal(items, shippingPolicy, null);

            // assert
            Assert.Equal(25.00m, total);
        }
// mock example
        [Fact]
        public void CalculateOrderTotal_WithPromotion_GetsDiscount_And_AppliesFreeShipping()
        {
            // arrange
            var productA = new Product { ProductId = 1, Name = "A", Price = 60.00m };
            var items = new List<OrderLineItem> { new(productA, 2) }; // subtotal = 120
            var shippingPolicy = new ShippingPolicy { BaseCost = 10.00m, FreeShippingThreshold = 100.00m };
            var promotion = new Promotion { Percentage = 20m }; // 20% promo => discount = 24 (20% of 120)
            var service = new OrderService();

            // act
            var total = service.CalculateOrderTotal(items, shippingPolicy, promotion);

            // assert
            // subtotal 120 - 24 = 96 => below 100 => shipping applies => 10 => total 106
            Assert.Equal(106.00m, total);
        }

     
        [Fact]
        public void Checkout_InvokePayment_ProcessPaymentCalled()
        {
            // arrange
            var mockPayment = new Mock<IPaymentService>();
            mockPayment.Setup(p => p.ProcessPayment(It.IsAny<int>(), It.IsAny<decimal>())).Returns(true);

          
            int fakeOrderId = 42;
            decimal amount = 50.0m;

            // act
            var result = mockPayment.Object.ProcessPayment(fakeOrderId, amount);

            // assert - verify returned true and that ProcessPayment was invoked with expected values
            Assert.True(result);
            mockPayment.Verify(m => m.ProcessPayment(fakeOrderId, amount), Times.Once);
        }

        // Stub example
        [Fact]
        public void Stub_ProductInventory_UsedWhenCalculating()
        {
            // stub repository returns price for product id
            var stubRepo = new StubProductRepository();
            var product = stubRepo.GetProductById(100);
            Assert.Equal("StubbedProduct", product.Name);
            Assert.Equal(9.99m, product.Price);
        }

        // Stub 
        private class StubProductRepository
        {
            public Product GetProductById(int id)
            {
                // returns deterministic product for tests (acts as stub)
                return new Product { ProductId = id, Name = "StubbedProduct", Price = 9.99m };
            }
        }
    }
}
