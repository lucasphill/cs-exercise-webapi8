using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Services.Autor;
using WebApi8.Services.Livro;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// informa que autor interface est�o implementados em autor service
builder.Services.AddScoped<IAutorInterface, AutorService>();
builder.Services.AddScoped<ILivroInterface, LivroService>();

// configura (constr�i, build) a conex�o antes de iniciar o projeto
builder.Services.AddDbContext<AppDbContext>(options => // adiciona o AppDbContext classe criada anteriormente e recebe as op��es
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // infoirma que est� utilizando sqlserver e busca a string de conex�o de dentro da configura��o (appsettings)
}); //o raciocinio �: fa�a a conex�o com o banco. ok, mas qual banco? sql server. ok, mas qual string conn? pega do appsettings.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/* 
 * dentro do controller ter� apenas uma conex�o com a interface e ser� a interface que ter� diversos m�todos a serem executados e a implementa��o dos met�dos ser� feita pelos servi�os
 * Controller -> Interface -> Servi�o -> Banco de dados (Select, insert, update e delete)
*/
