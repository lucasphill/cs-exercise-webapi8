using Microsoft.EntityFrameworkCore;
using WebApi8_Postgres.Data;
using WebApi8_Postgres.Dto.Autor;
using WebApi8_Postgres.Dto.Livro;
using WebApi8_Postgres.Models;

namespace WebApi8_Postgres.Services.Livro;

public class LivroService : ILivroInterface
{
    private readonly AppDbContext _context; //propriedade do contexto / conn com o banco
    
    public LivroService(AppDbContext appDbContex)
    {
        _context = appDbContex; //atribui o contexto informado no builder para dentro da nossa váriavel pois vamos usála aqui
    }

    public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(Guid idLivro)
    {
        var resposta = new ResponseModel<LivroModel>();
        try
        {
            var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
            if (livro == null) 
            {
                resposta.Message = "Livro não encontrado";
                return resposta;
            }
            resposta.Dados = livro;
            resposta.Message = "Livro encontrado";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(Guid idAutor)
    {
        var resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros.Include(a => a.Autor).Where(a => a.Autor.Id == idAutor).ToListAsync();

            #region verifica se o autor existe
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);
            if (autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }
            #endregion

            if (livro.Count == 0)
            {
                resposta.Message = "Este autor não possui livros";
                return resposta;
            }

            resposta.Dados = livro;
            resposta.Message = "Todos os livros foram listados";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
    {
        var resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == livroCriacaoDto.Autor.Id);
            if (autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }

            var livro = new LivroModel()
            {
                Titulo = livroCriacaoDto.Titulo,
                Autor = autor
            };

            _context.Add(livro);
            await _context.SaveChangesAsync();

            resposta.Message = "Autor adicionado com sucesso";
            resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
    {
        var resposta = new ResponseModel<LivroModel>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == livroEdicaoDto.Autor.Id);
            if (autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }

            var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);
            if (livro == null)
            {
                resposta.Message = "Livro não encontrado";
                return resposta;
            }
            livro.Titulo = livroEdicaoDto.Titulo;
            livro.Autor = autor;

            await _context.SaveChangesAsync();

            resposta.Message = "Autor editado com sucesso";
            resposta.Dados = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
    {
        var resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();
            resposta.Dados = livros;
            resposta.Message = "Listando todos os livros";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> RemoverLivro(Guid idlivro)
    {
        var resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == idlivro);
            if (livro == null)
            {
                resposta.Message = "Livro não encontrado";
                return resposta;
            }
            _context.Remove(livro);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
            resposta.Message = "Livro removido com sucesso";

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
