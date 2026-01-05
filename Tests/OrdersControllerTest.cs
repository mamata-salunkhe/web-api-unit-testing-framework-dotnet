using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Controllers;
using UserApi.Services;

namespace UserApi
{
    [TestFixture]
    public class OrdersControllerTest
    {
        private Mock<IOrderService> _mockSevice;
        private OrdersController _controller;
    [SetUp]
     public void SetUp()
    {
        _mockSevice = new Mock<IOrderService>();
        _controller = new OrdersController(_mockSevice.Object);
    }
        [Test]
        public async Task GetOrders_InvlalidID_returnsBadRequest()
        {
            var result = await _controller.GetOrder(0);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
        [Test]
        public async Task GetOrders_Null_Order_NotFound()
        {
            _mockSevice
                .Setup(s => s.GetOrderById(It.IsAny<int>()))
                .ReturnsAsync((Order)null);
            var result = await _controller.GetOrder(1);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetOrders_ByID_returnsOK_andServiceCallsOnlyOnce()
        {
            var order = new Order { Id = 1, Product = "laptap" };
            _mockSevice
                .Setup(s => s.GetOrderById(It.IsAny<int>()))
                .ReturnsAsync(order);

            var result = await _controller.GetOrder(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            _mockSevice.Verify(s => s.GetOrderById(It.IsAny<int>()), Times.Once);
        }

    }
}
