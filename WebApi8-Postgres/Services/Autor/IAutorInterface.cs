using WebApi8_Postgres.Dto.Autor;
using WebApi8_Postgres.Models;

namespace WebApi8_Postgres.Services.Autor;

public interface IAutorInterface
{
    Task<ResponseModel<List<AutorModel>>> ListarAutores();
    Task<ResponseModel<AutorModel>> BuscarAutorPorId(Guid idAutor);
    Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(Guid idLivro);
    Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto);
    Task<ResponseModel<AutorModel>> EditarAutor(AutorEdicaoDto autorEdicaoDto);
    Task<ResponseModel<List<AutorModel>>> RemoverAutor(Guid idAutor);
}
