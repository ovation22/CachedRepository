using Example.Models;
using Example.Services.Tests.Fakes;

namespace Example.Services.Tests.Factories
{
    internal static class ColorFactory
    {
        public static Color Create(FakeColorRepository fakeRepository, byte id = 1, string name = "Brown")
        {
            var color = new Color
            {
                Id = id,
                Name = name,
            };

            fakeRepository.Colors.Add(color);

            return color;
        }        
    }
}