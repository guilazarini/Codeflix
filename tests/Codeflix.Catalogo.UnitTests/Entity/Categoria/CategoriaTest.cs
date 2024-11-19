using DomainEntity = Codeflix.Catalogo.Domain.Entity;
using FluentAssertions;
using Codeflix.Catalogo.Domain.Entity;
namespace Codeflix.Catalogo.UnitTests.Entity.Categoria;

public class CategoriaTest
{
    [Fact(DisplayName = nameof(Instanciar))]
    [Trait("Domain", "Categoria - Agregação")]
    public void Instanciar()
    {
        var validaData = new
        {
            Nome = "nome categoria",
            Descricao = "Descrição categoria",
            Ativo = true
        };

        var dataAntes = DateTime.Now;
       
        var categoria = new DomainEntity.Categoria(validaData.Nome, validaData.Descricao, validaData.Ativo);
        var dataDepois = DateTime.Now;

        categoria.Should().NotBeNull();
        categoria.Nome.Should().Be(validaData.Nome);
        categoria.Id.Should().NotBeEmpty();
        categoria.DataCriacao.Should().NotBeSameDateAs(default(DateTime));
        (categoria.DataCriacao >= dataAntes).Should().BeTrue();
        (categoria.DataCriacao <= dataDepois).Should().BeTrue();
        (categoria.Ativo).Should().BeTrue();
    }
}
