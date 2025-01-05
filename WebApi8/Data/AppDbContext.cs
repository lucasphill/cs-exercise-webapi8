using Microsoft.EntityFrameworkCore;
using WebApi8.Models;

namespace WebApi8.Data;

public class AppDbContext : DbContext
{
    //intermedia a aplicação com a conexão do banco de dados.
    //pra isso recebe qual banco, forma de conexão em DbContextOptions
    //herda a base em DbContext
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
                   
    }

    //quais tabelas quero criar:
    public DbSet<AutorModel> Autores { get; set; }
    public DbSet<LivroModel> Livros { get; set; }

}