using System.Collections.Generic;
using System.Linq;
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
    public class GetAll
    {
        private readonly ColorsController _controller;
        private static Mock<IColorService> _colorServiceMock;
        private readonly List<ColorDetail> _colors;

        public GetAll()
        {
            _colors = new List<ColorDetail>
            {
                new ColorDetail
                {
                    Name = "test"
                }
            };

            _colorServiceMock = new Mock<IColorService>();
            _colorServiceMock.Setup(x => x.GetAll())
                .Returns(() => _colors);

            _controller = new ColorsController(_colorServiceMock.Object);
        }

        [Fact]
        public void ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ItReturnsCollectionOfColorDetail()
        {
            // Arrange
            // Act
            var result = _controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsAssignableFrom<IEnumerable<ColorDetail>>(result.Value);
        }

        [Fact]
        public void ItCallsGetAllServiceOnce()
        {
            // Arrange
            // Act
            _controller.Get();

            // Assert
            _colorServiceMock.Verify(mock => mock.GetAll(), Times.Once());
        }

        [Fact]
        public void GivenColorServiceThenResultsReturned()
        {
            // Arrange
            // Act
            var result = _controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var colors = ((IEnumerable<ColorDetail>) result.Value).ToList();
            Assert.Equal(_colors, colors);
        }
    }
}