using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Project_1.Dtos;
using Project_1.Models;

namespace Project_1.Services
{
    public class MovieService(AppDbContext _context) : IMovieService
    {
        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie); 
            _context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
           _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(int genreId = 0)
        {
            return await _context.Movies.
                Where(m => m.GenreId == genreId || genreId == 0).
                OrderByDescending(m => m.Rate).Include(m => m.genre).ToListAsync();
               
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
           _context.Update(movie);
            _context.SaveChanges(); 
            return movie;   
        }
    }
}
