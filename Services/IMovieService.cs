using Project_1.Models;

namespace Project_1.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll(int genreId = 0);

        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Movie Update (Movie movie);
        Movie Delete (Movie movie);

    }
}
