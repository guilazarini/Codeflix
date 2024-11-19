using DomainEntity = Codeflix.Catalogo.Domain.Entity;

namespace Codeflix.Catalogo.UnitTests.Entity.Categoria;
public class CategoriaTestFixture
{
    public DomainEntity.Categoria GetValorCategoria()
        => new ("Nome categoria", "Descrição categoria");
}

[CollectionDefinition(nameof(CategoriaTestFixture))]
public class CategoriaTestFixtureCollection : ICollectionFixture<CategoriaTestFixture>
{ }