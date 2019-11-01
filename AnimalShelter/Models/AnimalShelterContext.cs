using Microsoft.EntityFrameworkCore;

namespace AnimalShelter.Models
{
  public class AnimalShelterContext : DbContext
  {
    public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options)
        : base(options)
    {
    }

    public DbSet<Animal> Animals { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Animal>()
          .HasData(
              new Animal {AnimalId = 1, Type = "cat", Name = "Simba", Age = 1},
              new Animal {AnimalId = 2, Type = "cat", Name = "Toby", Age = 2},
              new Animal {AnimalId = 3, Type = "cat", Name = "Ollie", Age = 1},
              new Animal {AnimalId = 4, Type = "cat", Name = "Coco", Age = 3},
              new Animal {AnimalId = 5, Type = "dog", Name = "Buddy", Age = 2}
          );
    }

  }
}