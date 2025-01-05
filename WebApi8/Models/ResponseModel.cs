namespace WebApi8.Models;

// Será utilizado para as respostas de qualquer dados solicitados, por isso será do tipo genérico
public class ResponseModel<T>
{
    public T? Dados { get; set; } // caso a pesquisa no banco seja vazia Dados será null
    public string Message { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
}
