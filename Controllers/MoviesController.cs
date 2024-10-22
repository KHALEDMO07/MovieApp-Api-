using AutoMapper;
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
    public class MoviesController (IMovieService _movieService, IGenresService _genresService , IMapper _mapper): ControllerBase

    {
        private new List<string> _allowedExtensions = new List<string>
        {
            ".png" , ".jpg"
        }; 
        private long _maxAllowedPosterSize = 1024 * 1024;

        [HttpGet]

        public async Task<IActionResult> GetAllMovies()
        {
            var Movies = await _movieService.GetAll();
            //TODO : map into Movie Dto
            var data = _mapper.Map<IEnumerable<CreateMovieDto>>(Movies);
            
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var Movie = await _movieService.GetById(id);

            if(Movie == null)
            {
                return NotFound("There is no such movie");
            }

            var dto = new CreateMovieDto
            {
                Id = Movie.Id,
                Title = Movie.Title,
                Poster = Movie.Poster,
                Rate = Movie.Rate,
                Year = Movie.Year,
                Storeline = Movie.Storeline,
                GenreId = Movie.GenreId,
                GenreName = Movie.genre.Name
            };
          //var dto = _mapper.Map<CreateMovieDto>(Movie);
            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(int genreId)
        {
            var Movies = await _movieService.GetAll(genreId);
            //TODO : map into Movie Dto
            var data = _mapper.Map<IEnumerable<CreateMovieDto>>(Movies);

            return Ok(data);
        }
        [HttpPost]

        public async Task<IActionResult>CreateMovieAsync([FromForm] MovieDto dto)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only png or jpg are Allowed");
            }

            if(dto.Poster .Length > _maxAllowedPosterSize)
            {
                return BadRequest("The Size Of The File is too much");
            }

            var IsValidGenre = await _genresService.IsValidGenre(dto.GenreId);

            if(!IsValidGenre) {
                return BadRequest("There Is No Such Genre");
            }
            using var Datastream = new MemoryStream();

            await dto.Poster.CopyToAsync(Datastream);
            //DataStream will hold the data in bytes in the RAM
            ///MemoryStream stores Data in a stream of bytes in RAM
            ///we copy the data from Poster (as a File) to DataStream (as a stream of bytes)
            //var movie = new Movie
            //{
            //    GenreId = dto.GenreId,
            //    Title = dto.Title,
            //    Poster = Datastream.ToArray(), // => convert the stream of bytes to array of bytes
            //    Rate = dto.Rate,
            //    Storeline = dto.Storeline,
            //    Year = dto.Year, 

            //}; 
            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = Datastream.ToArray();
            await _movieService.Add(movie); 
            return Ok(movie);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateAsync(int id , [FromForm] MovieDto dto)
        {
            var movie = await _movieService.GetById(id);

            if (movie == null) { return NotFound($"There is no such movie in id {id}"); }

            var isValidGenre = await _genresService.IsValidGenre(dto.GenreId);
            if (!isValidGenre) { return BadRequest("Wrone Genre Id"); }

            if(dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest("Only jpg and png are allowed");
                }

                if(dto.Poster.Length > _maxAllowedPosterSize)
                {
                    return BadRequest("The Size of the file is too much");
                }
                var DataStream = new MemoryStream(); 
                await dto.Poster.CopyToAsync(DataStream);
                movie.Poster = DataStream.ToArray();

            }
            movie.Title = dto.Title; 
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.Storeline = dto.Storeline;    
            movie.GenreId = dto.GenreId;
            _movieService.Update(movie);
            return Ok(movie);

        }
        [HttpDelete]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _movieService.GetById(id);

            if(movie == null) { return NotFound($"There is no such movie in id {id}"); }
            _movieService.Delete(movie);
            return Ok(movie);
        }
    }
}
