using WebApi8_Postgres.Dto.Vinculo;
using WebApi8_Postgres.Models;

namespace WebApi8_Postgres.Dto.Livro;

public class LivroEdicaoDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public LivroAutorVinculo Autor { get; set; }
}
