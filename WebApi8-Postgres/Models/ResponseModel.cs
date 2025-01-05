namespace WebApi8_Postgres.Models;

public class ResponseModel<T>
{
    public T? Dados { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public DateTime Timestamp { get; } = DateTime.Now;
}
