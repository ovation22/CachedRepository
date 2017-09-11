using System.Collections.Generic;
using System.Linq;
using Example.DTO.Color;
using Example.Services.Tests.Factories;
using Example.Services.Tests.Fakes;
using Xunit;

namespace Example.Services.Tests.ColorServiceTests
{
    [Collection("ColorService")]
    [Trait("Category", "ColorService")]
    public class GetAll
    {
        private readonly ColorService _colorService;
        private readonly FakeColorRepository _fakeRepository;

        public GetAll()
        {
            _fakeRepository = new FakeColorRepository();
            ColorFactory.Create(_fakeRepository);

            _colorService = new ColorService(_fakeRepository);
        }

        [Fact]
        public void ItReturnsCollectionOfColorDetail()
        {
            // Arrange
            // Act
            var colors = _colorService.GetAll();

            // Assert
            Assert.NotNull(colors);
            Assert.IsAssignableFrom<IEnumerable<ColorDetail>>(colors);
        }

        [Fact]
        public void ItReturnsAllColors()
        {
            // Arrange
            // Act
            var colors = _colorService.GetAll();

            // Assert
            Assert.NotNull(colors);
            Assert.IsAssignableFrom<IEnumerable<ColorDetail>>(colors);
            Assert.Equal(_fakeRepository.Colors.Count, colors.Count());
        }

        [Fact]
        public void ItReturnsAllColorsWithProperties()
        {
            // Arrange
            // Act
            var colors = _colorService.GetAll().ToList();

            // Assert
            Assert.NotNull(colors);
            Assert.IsAssignableFrom<IEnumerable<ColorDetail>>(colors);

            for (var i = 0; i < colors.Count; i++)
            {
                Assert.NotNull(_fakeRepository.Colors[i].Name);
                Assert.Equal(_fakeRepository.Colors[i].Name, colors[i].Name);
                Assert.NotNull(_fakeRepository.Colors[i].Id);
                Assert.Equal(_fakeRepository.Colors[i].Id, colors[i].Id);
            }
        }
    }
}