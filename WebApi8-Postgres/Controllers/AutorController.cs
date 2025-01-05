using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi8_Postgres.Dto.Autor;
using WebApi8_Postgres.Models;
using WebApi8_Postgres.Services.Autor;

namespace WebApi8_Postgres.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorInterface _autorInterface;
    public AutorController(IAutorInterface autorInterface)
    {
        _autorInterface = autorInterface;
    }

    [HttpGet("ListarAutores")]
    public async Task<ActionResult<List<ResponseModel<LivroModel>>>> ListarAutores()
    {
        var autores = await _autorInterface.ListarAutores();
        return Ok(autores);
    }

    [HttpGet("BuscarAutorPorId/{idAutor}")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarAutorPorId(Guid idAutor)
    {
        var autor = await _autorInterface.BuscarAutorPorId(idAutor);
        return Ok(autor);
    }

    [HttpGet("BuscarAutorPorIdLivro/{idLivro}")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarAutorPorIdLivro(Guid idLivro)
    {
        var autor = await _autorInterface.BuscarAutorPorIdLivro(idLivro);
        return Ok(autor);
    }

    [HttpPost("CriarAutor")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
    {
        var autor = await _autorInterface.CriarAutor(autorCriacaoDto);
        return Ok(autor);
    }

    [HttpPut("EditarAutor")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
    {
        var autor = await _autorInterface.EditarAutor(autorEdicaoDto);
        return Ok(autor);
    }

    [HttpDelete("RemoverAutor")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> RemoverAutor(Guid idAutor)
    {
        var autor = await _autorInterface.RemoverAutor(idAutor);
        return Ok(autor);
    }
}
