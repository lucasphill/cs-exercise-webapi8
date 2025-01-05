using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi8_Postgres.Dto.Livro;
using WebApi8_Postgres.Models;
using WebApi8_Postgres.Services.Livro;

namespace WebApi8_Postgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> BuscarLivroPorId(Guid idLivro)
        {
            var livros = await _livroInterface.BuscarLivroPorId(idLivro);
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> BuscarLivroPorIdAutor(Guid idAutor)
        {
            var livro = await _livroInterface.BuscarLivroPorIdAutor(idAutor);
            return Ok(livro);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livro = await _livroInterface.CriarLivro(livroCriacaoDto);
            return Ok(livro);
        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livro = await _livroInterface.EditarLivro(livroEdicaoDto);
            return Ok(livro);
        }

        [HttpDelete("RemoverLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> RemoverLivro(Guid idLivro)
        {
            var livros = await _livroInterface.RemoverLivro(idLivro);
            return Ok(livros);
        }
    }
}
