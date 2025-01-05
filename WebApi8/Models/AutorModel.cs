using System.Text.Json.Serialization;

namespace WebApi8.Models;

public class AutorModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    
    [JsonIgnore] //ignora as propriedades abaixo no momento de criação do autor
    public ICollection<LivroModel> Livros { get; set; } //ICollection pois posso ter uma coleção de livros independente de tamanho da coleção (posso ter vários objetos LivroModel dentro do meu objeto autor)

}
