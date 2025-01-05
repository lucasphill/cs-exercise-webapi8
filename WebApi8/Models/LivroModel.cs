namespace WebApi8.Models;

public class LivroModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public AutorModel Autor { get; set; }
}
