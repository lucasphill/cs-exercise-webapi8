using Microsoft.EntityFrameworkCore;
using WebApi8_Postgres.Data;
using WebApi8_Postgres.Dto.Autor;
using WebApi8_Postgres.Models;

namespace WebApi8_Postgres.Services.Autor;

public class AutorService : IAutorInterface
{
    private readonly AppDbContext _context;
    public AutorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(Guid idAutor)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);
            if (autor == null) 
            {
                resposta.Message = "Nenhum autor encontrado";
                return resposta;
            }
            resposta.Message = "Autor encontrado";
            resposta.Dados = autor;
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(Guid idLivro)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
        try
        {
            var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

            if (livro == null)
            {
                resposta.Message = "Nenhum livro encontrado";
                return resposta;
            }

            resposta.Dados = livro.Autor;
            resposta.Message = "Autor encontrado";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
    {
        var resposta = new ResponseModel<List<AutorModel>>();
        try
        {   
            var autor = new AutorModel()
            {
                Nome = autorCriacaoDto.Nome,
                Sobrenome = autorCriacaoDto.Sobrenome
            };

            _context.Add(autor);
            await _context.SaveChangesAsync();

            resposta.Dados = _context.Autores.ToList();
            resposta.Message = "Autor criado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<AutorModel>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
    {
        var resposta = new ResponseModel<AutorModel>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);
            if(autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }
            autor.Nome = autorEdicaoDto.Nome;
            autor.Sobrenome = autorEdicaoDto.Sobrenome;

            await _context.SaveChangesAsync();

            resposta.Message = "Autor editado com sucesso";
            resposta.Dados = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
                    
    }

    public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autores = await _context.Autores.ToListAsync();
            resposta.Dados = autores;
            resposta.Message = "Todos autores foram listados";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> RemoverAutor(Guid idAutor)
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);
            if (autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }
            _context.Remove(autor);
            await _context.SaveChangesAsync();

            resposta.Dados = _context.Autores.ToList();
            resposta.Message = "Autor removido com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}
