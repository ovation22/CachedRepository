using Example.Services.Tests.Factories;
using Example.Services.Tests.Fakes;
using Xunit;

namespace Example.Services.Tests.ColorServiceTests
{
    [Trait("Category", "ColorService")]
    public class Get
    {
        private readonly FakeColorRepository _fakeRepository;

        public Get()
        {
            _fakeRepository = new FakeColorRepository();
        }

        [Theory]
        [InlineData(1, "Brown")]
        [InlineData(2, "Black")]
        [InlineData(3, "White")]
        public void ItReturnsColorFromRepository(byte id, string name)
        {
            // Arrange
            var expectedColor = ColorFactory.Create(_fakeRepository, id, name);
            var service = new ColorService(_fakeRepository);

            // Act
            var actualColor = service.Get(expectedColor.Id);

            // Assert
            Assert.True(_fakeRepository.GetCalled);
            Assert.Equal(expectedColor.Id, actualColor.Id);
            Assert.Equal(expectedColor.Name, actualColor.Name);
        }

        [Fact]
        public void GivenColorNotFoundThenNullColor()
        {
            // Arrange
            var service = new ColorService(_fakeRepository);

            // Act
            var actualColor = service.Get(-1);

            // Assert
            Assert.Null(actualColor);
        }
    }
}