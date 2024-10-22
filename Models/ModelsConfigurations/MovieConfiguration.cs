using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project_1.Models.ModelsConfigurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x=> x.Id);

            builder.Property(x => x.Title).HasMaxLength(250);

            builder.Property(x => x.Storeline).HasMaxLength(2500);
        }
    }
}
