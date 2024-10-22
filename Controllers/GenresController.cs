using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_1.Dtos;
using Project_1.Models;
using Project_1.Services;

namespace Project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var records = await _genresService.GetAll();

            return Ok(records);
        }

        [HttpPost]

        public async Task<IActionResult> CreateGenreAsync(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };

            await _genresService.Add(genre);
            return Ok(genre);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreateGenreDto dto)
        {
            var genre = await  _genresService.GetById(id);

            if (genre == null)
            {
                return NotFound($"No Genre Was Found With Id {id}");
            }

            _genresService.Update(genre);
            return Ok(genre);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync(int id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
            {
                return NotFound($"No Genre Was Found With Id {id}");
            }

            _genresService.Delete(genre);
            return Ok("Delete is Done!");
        }

    }
}
