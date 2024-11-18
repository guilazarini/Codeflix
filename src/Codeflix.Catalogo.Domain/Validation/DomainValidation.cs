using Codeflix.Catalogo.Domain.Exceptions;

namespace Codeflix.Catalogo.Domain.Validation;

public class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
            throw new EntityValidateException($"{fieldName} não pode ser nulo.");
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(target))
            throw new EntityValidateException($"{fieldName} não pode ser nulo ou vazio.");
    }

    public static void MinLenght(string target, int minLenght, string fieldName)
    {
        if(target.Length < minLenght)
            throw new EntityValidateException($"{fieldName} deve ter um tamanho mínimo de {minLenght} caracteres.");
    }

    public static void MaxLenght(string target, int maxLenght, string fieldName)
    {
        if (target.Length > maxLenght)
            throw new EntityValidateException($"{fieldName} deve ter um tamanho menor que {maxLenght} caracteres.");
    }
}
