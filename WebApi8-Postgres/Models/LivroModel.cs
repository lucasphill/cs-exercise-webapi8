namespace WebApi8_Postgres.Models
{
    public class LivroModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public AutorModel Autor { get; set; }
    }
}
