using Codeflix.Catalogo.Domain.Exceptions;
using Codeflix.Catalogo.Domain.SeedWork;
using Codeflix.Catalogo.Domain.Validation;

namespace Codeflix.Catalogo.Domain.Entity;
public class Categoria : AggregateRoot
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public Categoria(string nome, string descricao, bool ativo)
        :base()
    {
        Nome = nome;
        Descricao = descricao;
        Ativo = ativo;
        DataCriacao = DateTime.Now;
    }

    public void Ativar()
    {
        Ativo = true;
        ValidaCategoria();
    }

    public void Desativar()
    {
        Ativo = false;
        ValidaCategoria();
    }

    public void Update(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao ?? Descricao;
        ValidaCategoria();
    }

    public void ValidaCategoria()
    {
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.MinLenght(Nome, 3, nameof(Nome));
        DomainValidation.MaxLenght(Nome, 255, nameof(Nome));

        DomainValidation.NotNull(Descricao, nameof(Descricao));
        DomainValidation.MaxLenght(Descricao, 10_000, nameof(Descricao));
    }
}
