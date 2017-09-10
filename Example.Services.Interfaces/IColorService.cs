using System.Collections.Generic;
using Example.DTO.Color;

namespace Example.Services.Interfaces
{
    public interface IColorService
    {
        IEnumerable<ColorDetail> GetAll();
        ColorDetail Get(int id);
    }
}