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

// informa que autor interface estão implementados em autor service
builder.Services.AddScoped<IAutorInterface, AutorService>();
builder.Services.AddScoped<ILivroInterface, LivroService>();

// configura (constrói, build) a conexão antes de iniciar o projeto
builder.Services.AddDbContext<AppDbContext>(options => // adiciona o AppDbContext classe criada anteriormente e recebe as opções
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // infoirma que está utilizando sqlserver e busca a string de conexão de dentro da configuração (appsettings)
}); //o raciocinio é: faça a conexão com o banco. ok, mas qual banco? sql server. ok, mas qual string conn? pega do appsettings.

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
 * dentro do controller terá apenas uma conexão com a interface e será a interface que terá diversos métodos a serem executados e a implementação dos metódos será feita pelos serviços
 * Controller -> Interface -> Serviço -> Banco de dados (Select, insert, update e delete)
*/
