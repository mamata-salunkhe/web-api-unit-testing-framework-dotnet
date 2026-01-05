using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UserApi.Controllers;

namespace UserApi.Tests
{
    [TestFixture]
    public class DateTimeControllerTest
    {
        private DateTimeController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new DateTimeController();
        }
        [Test]
        public void Get_ShouldReturn_DateTime_InExpectedFormat()
        {
            // Act
            var result = _controller.Get();

            // Assert - not null
            Assert.That(result, Is.Not.Null.And.Not.Empty);

            // Assert - correct format
            var isValid = DateTime.TryParseExact(result, "dd.MM.yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out _);

            Assert.That(isValid, Is.True);
        }
    }
}
