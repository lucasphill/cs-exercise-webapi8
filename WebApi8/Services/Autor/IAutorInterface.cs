using WebApi8.Dto.Autor;
using WebApi8.Models;

namespace WebApi8.Services.Autor;

// aqui eu informo quais metodos precisam ter nos meus serviços
public interface IAutorInterface
{
    Task<ResponseModel<List<AutorModel>>> ListarAutores(); // lista todos os autores (lista de objetos AutorModel)
    Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor); // lista apenas um autor pelo id
    Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
    Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto); //envia pela camada de transferencia apenas os dados que preciso para criar um novo objeto Autor
    Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto);
    Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor);

}
