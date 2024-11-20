using Codeflix.Catalogo.UnitTests.Common;
using DomainEntity = Codeflix.Catalogo.Domain.Entity;

namespace Codeflix.Catalogo.UnitTests.Entity.Categoria;
public class CategoriaTestFixture : BaseFixture
{
    public CategoriaTestFixture()
        : base() { }

    public string GetNomeCategoriaValido()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public string GetDescricaoCategoriaValido()
    {
        var categoryDescription =
            Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10_000)
            categoryDescription =
                categoryDescription[..10_000];
        return categoryDescription;
    }

    public DomainEntity.Categoria GetCategoriaValido()
        => new(
            GetNomeCategoriaValido(),
            GetDescricaoCategoriaValido()
        );
}

[CollectionDefinition(nameof(CategoriaTestFixture))]
public class CategoryTestFixtureCollection
    : ICollectionFixture<CategoriaTestFixture>
{ }

[CollectionDefinition(nameof(CategoriaTestFixture))]
public class CategoriaTestFixtureCollection : ICollectionFixture<CategoriaTestFixture>
{ }