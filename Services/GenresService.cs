using Microsoft.EntityFrameworkCore;
using Project_1.Models;

namespace Project_1.Services
{
    public class GenresService(AppDbContext _context): IGenresService
    {
        
        public async Task<Genre> Add(Genre genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();
            return genre;

        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g=>g.Name).ToListAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> IsValidGenre(int id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id);
        }

        public Genre Update(Genre genre)
        {
           _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
