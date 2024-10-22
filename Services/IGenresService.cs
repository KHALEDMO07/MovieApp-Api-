using Project_1.Models;

namespace Project_1.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(int id);
        Task<Genre> Add(Genre genre); 
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<bool> IsValidGenre(int id);
    }
}
