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
    public class BookingControllerTest
    {
        private Mock<IBookingService> _mockService;
        private BookingController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IBookingService>();
            _controller = new BookingController(_mockService.Object);
        }
        [Test]
        public void CreateBooking_NullBooking_ReturnsBadRequest()
        {
            var result = _controller.CreateBooking(null);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void CreateBooking_validBooking_returnsOK()
        {
            var booking = new Booking { Id = 1, Customer = "Mamata Salunkhe" };
            _mockService
                .Setup(s => s.Create(It.IsAny<Booking>()))
                .Returns(booking);
            var result = _controller.CreateBooking(booking);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void CreateBooking_valid_callsServiceOnce()
        {
            var booking = new Booking { Id = 1, Customer = "Mamata Salunkhe" };
            _mockService
                .Setup(s => s.Create(It.IsAny<Booking>()))
                .Returns(booking);
            _controller.CreateBooking(booking);
            _mockService.Verify(s => s.Create(It.IsAny<Booking>()), Times.Once);
        }
        [Test]
        public void UpdateBooking_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.UpdateBooking(0, new Booking());
            Assert.That(result, Is.InstanceOf<BadRequestResult>());

        }
        [Test]
        public void UpdateBooking_NotFound_ReturnsNotFound()
        {
            _mockService
                .Setup(s => s.Update(1, It.IsAny<Booking>()))
                .Returns((Booking)null);
            var result = _controller.UpdateBooking(1, new Booking());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
        [Test]
        public void UpdateBooking_Valid_returnsOK_CallServiceOnce()
        {
            var booking = new Booking { Id = 1, Customer = "Mamata Salunkhe" };
            _mockService
                .Setup(s => s.Update(1, It.IsAny<Booking>()))
                .Returns(booking);
            var result = _controller.UpdateBooking(1, booking);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            _mockService.Verify(s => s.Update(1, It.IsAny<Booking>()), Times.Once);
        }
        [Test]
        public void PatchBooking_InvalidInputs_ReturnsBadRequest()
        {
            var result = _controller.UpdateCustomer(0, "");
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
        [Test]
        public void PatchBooking_NotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.UpdateCustomer(1, "Mamata"))
                .Returns(false);
            var result = _controller.UpdateCustomer(1, "Mamata");
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
        [Test]
        public void PatchBooking_Valid_ReturnsNoContent()
        {
            _mockService.Setup(s => s.UpdateCustomer(1, "Mamata"))
                .Returns(true);
            var result = _controller.UpdateCustomer(1, "Mamata");
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockService.Verify(s => s.UpdateCustomer(1, "Mamata"), Times.Once);
        }
        [Test]
        public void DeleteBooking_InvalidID_returnsBadRequest()
        {
            var result = _controller.DeleteBooking(0);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
        [Test]
        public void DeleteBooking_NotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.Delete(1))
                .Returns(false);
            var result = _controller.DeleteBooking(1);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());

        }
        [Test]
        public void DeleteBooking_Valid_ReturnsNoContent_AndCallsServiceOnce()
        {
            _mockService.Setup(s => s.Delete(1))
                .Returns(true);
            var result = _controller.DeleteBooking(1);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockService.Verify(s => s.Delete(1), Times.Once);
        }
    }
}
