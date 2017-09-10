using System.Collections.Generic;
using Example.DTO.Color;
using Microsoft.AspNetCore.Mvc;
using Example.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Example.API.Controllers
{
    [Route("api/[controller]")]
    public class ColorsController : Controller
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        /// <summary>
        /// Gets the full list of colors
        /// </summary>
        /// <response code="200">Colors found</response>
        /// <response code="500">Oops! Something went horribly wrong</response>
        /// <returns>IEnumerable&lt;Models.ColorSummary&gt;</returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<ColorDetail>))]
        [ProducesResponseType(typeof(IEnumerable<ColorDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var colors = _colorService.GetAll();

            return Ok(colors);
        }

        /// <summary>
        /// Gets an individual color's information
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Color found</response>
        /// <response code="404">Color not found</response>
        /// <response code="500">Oops! Something went horribly wrong</response>
        /// <returns>string</returns>
        [HttpGet("{id}")]
        [Produces("application/json", Type = typeof(ColorDetail))]
        [ProducesResponseType(typeof(ColorDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            var color = _colorService.Get(id);

            if (color == null)
            {
                return NotFound("Color Not Found");
            }

            return Ok(color);
        }
    }
}