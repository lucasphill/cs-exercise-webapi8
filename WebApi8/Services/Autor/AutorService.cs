using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Dto.Autor;
using WebApi8.Models;

namespace WebApi8.Services.Autor;

public class AutorService : IAutorInterface // respeita as regras de autor interface, busca os metodos da interface
{
    private readonly AppDbContext _context;
    public AutorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

            if (autor == null) 
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }

            resposta.Dados = autor;
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

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
    {
        ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
        try
        {
            var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

            if (livro == null)
            {
                resposta.Message = "Autor não encontrado";
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
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = new AutorModel()
            {
                Nome = autorCriacaoDto.Nome,
                Sobrenome = autorCriacaoDto.Sobrenome
            };

            _context.Add(autor);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Autores.ToListAsync();
            resposta.Message = "Autor adicionado com sucesso";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status=false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);
            if (autor == null)
            {
                resposta.Message = "Autor não encontrado";
                return resposta;
            }
            autor.Nome = autorEdicaoDto.Nome;
            autor.Sobrenome = autorEdicaoDto.Sobrenome;

            _context.Update(autor);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Autores.ToListAsync();
            resposta.Message = "Autor editado com sucesso";

            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
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

            resposta.Dados = await _context.Autores.ToListAsync();
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

    public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
    {
        ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
        try
        {
            var autores = await _context.Autores.ToListAsync();
            resposta.Dados = autores;
            resposta.Message = "Todos autores foram coletados";

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
