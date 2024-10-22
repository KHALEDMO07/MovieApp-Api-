using System.ComponentModel.DataAnnotations;

namespace Project_1.Dtos
{
    public class CreateGenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
