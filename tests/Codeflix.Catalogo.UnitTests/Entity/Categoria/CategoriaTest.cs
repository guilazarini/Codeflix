using DomainEntity = Codeflix.Catalogo.Domain.Entity;

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
       
        var categoria = new DomainEntity.Categoria(validaData.Nome, validaData.Descricao, categoria.Ativo);
        var dataDepois = DateTime.Now;

        Assert.NotNull(categoria);
        Assert.Equal(validaData.Nome, categoria.Nome);
        Assert.Equal(validaData.Descricao, categoria.Descricao);
        Assert.NotEqual(default(Guid), categoria.Id);
        Assert.NotEqual(default(DateTime), categoria.DataCriacao);
        Assert.True(categoria.DataCriacao > dataAntes);
        Assert.True(categoria.DataCriacao < dataDepois);
        Assert.True(categoria.Ativo);
    }
}
