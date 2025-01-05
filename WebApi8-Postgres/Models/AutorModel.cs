using System.Text.Json.Serialization;

namespace WebApi8_Postgres.Models;

public class AutorModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    
    [JsonIgnore]
    public ICollection<LivroModel> Livro { get; set; }
}
