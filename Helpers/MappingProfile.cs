using AutoMapper;
using Project_1.Dtos;
using Project_1.Models;

namespace Project_1.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, CreateMovieDto>();
            CreateMap<MovieDto, Movie>().ForMember(src => src.Poster, options => options.Ignore());
        }
    }
}
