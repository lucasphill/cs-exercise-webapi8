using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Dto.Autor;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livro;

public class LivroService : ILivroInterface
{
    private readonly AppDbContext _context;
    public LivroService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
    {
        ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
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

    public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros.Include(a => a.Autor).Where(livroBanco => livroBanco.Autor.Id == idAutor).ToListAsync();
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);


            if (autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }
            if (livro.Count == 0) {
                resposta.Message = $"Nenhum livro cadastrado para {autor.Nome}";
                return resposta;
            }


            resposta.Dados = livro;
            resposta.Message = $"Livros localizados";
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
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);
            if (autor == null) 
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }

            var livro = new LivroModel()
            {
                Name = livroCriacaoDto.Name,
                Autor = autor
            };
            _context.Add(livro);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
            resposta.Message = "Livro adicionado com sucesso";

            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);
            if (livro == null)
            {
                resposta.Message = "Livro não encontrado";
                return resposta;
            }

            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == livroEdicaoDto.Autor.Id);
            if (autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }

            livro.Name = livroEdicaoDto.Name;
            livro.Autor = autor;

            _context.Update(livro);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
            resposta.Message = "Livro editado com sucesso";

            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
            if (livro == null)
            {
                resposta.Message = "Livro não encontrado";
                return resposta;
            }
            _context.Remove(livro);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Livros.ToListAsync();
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

    public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {
            var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();

            resposta.Dados = livros;
            resposta.Message = "Todos livros foram coletados";

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
