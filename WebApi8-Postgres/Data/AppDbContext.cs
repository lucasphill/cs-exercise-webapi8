using Microsoft.EntityFrameworkCore;
using WebApi8_Postgres.Models;

namespace WebApi8_Postgres.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AutorModel> Autores { get; set; }
    public DbSet<LivroModel> Livros { get; set; }
}
