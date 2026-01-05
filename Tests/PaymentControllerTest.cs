using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Controllers;
using UserApi.Services;

namespace UserApi
{
    public class PaymentControllerTest
    {
        [Test]
        public async Task GetPayment_InvalidID_ReturnsBadRequest()
        {
            var mockService = new Mock<IPaymentService>();
            var controller = new PaymentController(mockService.Object);
            var result = await controller.GetPayment(0);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
        [Test]
        public async Task GetPayment_PaymentIDNull_returnsNotFound()
        {
            var mockService = new Mock<IPaymentService>();
            mockService
                .Setup(s => s.GetPaymentById(It.IsAny<int>()))
                .ReturnsAsync((Payment)null);
            var controller = new PaymentController(mockService.Object);
            var result = await controller.GetPayment(1);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());

        }
        [Test]
        public async Task GetPayment_validID_ReturnsOK()
        {
            var mockService = new Mock<IPaymentService>();
            var payment = new Payment { Id = 1, Amount = 100 };
            mockService
                .Setup(s => s.GetPaymentById(It.IsAny<int>()))
                .ReturnsAsync(payment);
            var controller = new PaymentController(mockService.Object);
            var result = await controller.GetPayment(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public async Task GetPayment_validId_callServiceOnce()
        {
            var mockService = new Mock<IPaymentService>();
            mockService
                .Setup(s => s.GetPaymentById(It.IsAny<int>()))
                .ReturnsAsync(new Payment { Id = 1, Amount = 100 }); 
            var controller = new PaymentController(mockService.Object);
            var result = await controller.GetPayment(1);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            mockService.Verify(s => s.GetPaymentById(1), Times.Once);
        }
    }
}
