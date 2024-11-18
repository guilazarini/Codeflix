namespace Codeflix.Catalogo.Domain.Exceptions;

public class EntityValidateException : Exception
{
    public EntityValidateException(string? message) : base(message)
    {
    }
}
