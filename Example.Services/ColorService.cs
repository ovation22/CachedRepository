using System.Collections.Generic;
using System.Linq;
using Example.DTO.Color;
using Example.Models;
using Example.Repositories.Interfaces;
using Example.Services.Interfaces;

namespace Example.Services
{
    public class ColorService : IColorService
    {
        private readonly ICachedRepository<Color> _repository;

        public ColorService(ICachedRepository<Color> repository)
        {
            _repository = repository;
        }

        public IEnumerable<ColorDetail> GetAll()
        {
            return _repository.GetAll().Select(Map);
        }

        public ColorDetail Get(int id)
        {
            var color = _repository.Get(x => x.Id == id);
            
            return color == null ? null : Map(color);
        }
        
        private static ColorDetail Map(Color color)
        {
            return new ColorDetail
            {
                Id = color.Id,
                Name = color.Name
            };
        }
    }
}