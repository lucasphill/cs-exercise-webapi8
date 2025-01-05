using WebApi8_Postgres.Dto.Livro;
using WebApi8_Postgres.Models;

namespace WebApi8_Postgres.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(Guid idLivro);
        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(Guid idAutor);
        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);
        Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
        Task<ResponseModel<List<LivroModel>>> RemoverLivro(Guid idlivro);
    }
}
