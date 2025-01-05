using WebApi8.Dto.Vinculo;
using WebApi8.Models;

namespace WebApi8.Dto.Livro
{
    public class LivroCriacaoDto
    {
        public string Name { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
