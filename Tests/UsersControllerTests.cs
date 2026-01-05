using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using UserApi.Controllers;
using UserApi.Services;

namespace WebServiceUnitTestingDemo.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UsersController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UsersController(_userServiceMock.Object);
        }

        [Test]
        public async Task CheckUser_EmptyEmail_ReturnsBadRequest()
        {
            var result = await _controller.CheckUser("");

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
        [Test]
        public async Task CheckUser_UserExists_ReturnsOk()
        {
            _userServiceMock
                .Setup(s => s.UserExists("test@mail.com"))
                .ReturnsAsync(true);

            var result = await _controller.CheckUser("test@mail.com");
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public async Task GetProduct_InvalidId_ReturnsBadRequest()
        {
            var result = await _controller.GetProduct(0);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
            
        }
        [Test]
        public void GetOrder_ServiceThrowsException_Throws()
        {
            var mockService = new Mock<IOrderService>();
            mockService.Setup(s => s.GetOrderById(It.IsAny<int>()))
                       .ThrowsAsync(new Exception("DB error"));

            var controller = new OrdersController(mockService.Object);

            Assert.ThrowsAsync<Exception>(() => controller.GetOrder(1));
        }

        [Test]
        public async Task CheckUser_ValidEmail_CallsServiceOnce()
        {
            _userServiceMock
                .Setup(s => s.UserExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            await _controller.CheckUser("test@mail.com");

            _userServiceMock.Verify(s => s.UserExists("test@mail.com"), Times.Once);
        }

        [Test]
        public void CreateUser_ShouldReturnCreatedResult_AndCallServiceOnce()
        {
            var user = new User{ Id = 1, Name = "Mamata" };
            _userServiceMock
                .Setup(s => s.CreateUser(It.IsAny<User>()))
                .Returns(user);

            var result = _controller.Create(user);
            //assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());

            _userServiceMock.Verify(s => s.CreateUser(It.IsAny<User>()), Times.Once);

        }
   


    }
}
