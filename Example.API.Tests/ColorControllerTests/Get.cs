using Example.API.Controllers;
using Example.DTO.Color;
using Example.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Example.API.Tests.ColorControllerTests
{
    [Collection("ColorController")]
    [Trait("Category", "ColorController")]
    public class Get
    {
        private readonly ColorsController _controller;
        private static Mock<IColorService> _colorServiceMock;
        private readonly ColorDetail _color;

        public Get()
        {
            _color = new ColorDetail
            {
                Name = "Color"
            };

            _colorServiceMock = new Mock<IColorService>();
            _colorServiceMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(() => _color);
            _colorServiceMock.Setup(x => x.Get(-1))
                .Returns(() => null);

            _controller = new ColorsController(_colorServiceMock.Object);
        }

        [Fact]
        public void ItAcceptsInteger()
        {
            // Arrange
            // Act
            _controller.Get(1);
        }

        [Fact]
        public void ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ItReturnsColorDetail()
        {
            // Arrange
            // Act
            var result = _controller.Get(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsType<ColorDetail>(result.Value);
        }

        [Fact]
        public void ItCallsGetServiceOnce()
        {
            // Arrange
            // Act
            _controller.Get(1);

            // Assert
            _colorServiceMock.Verify(mock => mock.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void ItCallsGetServiceWithProvidedId()
        {
            // Arrange
            const int id = 1;

            // Act
            _controller.Get(id);

            // Assert
            _colorServiceMock.Verify(mock => mock.Get(id), Times.Once());
        }

        [Fact]
        public void GivenColorServiceThenResultsReturned()
        {
            // Arrange
            // Act
            var result = _controller.Get(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var color = ((ColorDetail) result.Value);
            Assert.Equal(_color, color);
        }

        [Fact]
        public void GivenColorNotFoundExceptionThenNotFoundObjectResult()
        {
            // Arrange
            // Act
            var result = _controller.Get(-1);

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GivenColorNotFoundExceptionThenMessageReturned()
        {
            // Arrange
            // Act
            var result = _controller.Get(-1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Color Not Found", result.Value);
        }
    }
}