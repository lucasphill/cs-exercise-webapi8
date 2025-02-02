﻿using WebApi8.Dto.Autor;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livro;

public interface ILivroInterface
{
    Task<ResponseModel<List<LivroModel>>> ListarLivros();
    Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
    Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor);
    Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto); //envia pela camada de transferencia apenas os dados que preciso para criar um novo objeto Autor
    Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
    Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
}
