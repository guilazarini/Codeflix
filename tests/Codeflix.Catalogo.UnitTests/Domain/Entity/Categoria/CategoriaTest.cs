using Codeflix.Catalogo.Domain.Entity;
using Codeflix.Catalogo.Domain.Exceptions;
using Codeflix.Catalogo.UnitTests.Entity.Categoria;
using FluentAssertions;
using DomainEntity = Codeflix.Catalogo.Domain.Entity;

namespace Codeflix.Catalogo.UnitTests.Domain.Entity.Categoria;

[Collection(nameof(CategoriaTestFixture))]
public class CategoryTest
{
    private readonly CategoriaTestFixture _CategoriaTestFixture;

    public CategoryTest(CategoriaTestFixture CategoriaTestFixture)
        => _CategoriaTestFixture = CategoriaTestFixture;

    [Fact(DisplayName = nameof(Instanciar))]
    [Trait("Domain", "Categoria - Agregados")]
    public void Instanciar()
    {
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();
        var dataAntes = DateTime.Now;

        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao);
        var dataApos = DateTime.Now.AddSeconds(1);

        categoria.Should().NotBeNull();
        categoria.Nome.Should().Be(categoriaValida.Nome);
        categoria.Descricao.Should().Be(categoriaValida.Descricao);
        categoria.Id.Should().NotBeEmpty();
        categoria.DataCriacao.Should().NotBeSameDateAs(default(DateTime));
        (categoria.DataCriacao >= dataAntes).Should().BeTrue();
        (categoria.DataCriacao <= dataApos).Should().BeTrue();
        (categoria.Ativo).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstanciarComAtivo))]
    [Trait("Domain", "Categoria - Agregados")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstanciarComAtivo(bool ativo)
    {
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();
        var dataAntes = DateTime.Now;

        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao, ativo);
        var dataApos = DateTime.Now.AddSeconds(1);

        categoria.Should().NotBeNull();
        categoria.Nome.Should().Be(categoriaValida.Nome);
        categoria.Descricao.Should().Be(categoriaValida.Descricao);
        categoria.Id.Should().NotBeEmpty();
        categoria.DataCriacao.Should().NotBeSameDateAs(default(DateTime));
        (categoria.DataCriacao >= dataAntes).Should().BeTrue();
        (categoria.DataCriacao <= dataApos).Should().BeTrue();
        (categoria.Ativo).Should().Be(ativo);
    }

    [Theory(DisplayName = nameof(ErroAoInstanciarQuandoNomeEstaVazio))]
    [Trait("Domain", "Categoria - Agregados")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ErroAoInstanciarQuandoNomeEstaVazio(string? name)
    {
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();

        Action action =
            () => new DomainEntity.Categoria(name!, categoriaValida.Descricao);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Nome não pode ser nulo ou vazio.");
    }

    [Fact(DisplayName = nameof(ErroAoInstanciarQuandoDescricaoNula))]
    [Trait("Domain", "Categoria - Agregados")]
    public void ErroAoInstanciarQuandoDescricaoNula()
    {
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();

        Action action =
            () => new DomainEntity.Categoria(categoriaValida.Nome, null!);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser nulo.");
    }

    [Theory(DisplayName = nameof(ErroAoInstanciarQuandoNomeTemMenosDe3Caracteres))]
    [Trait("Domain", "Categoria - Agregados")]
    [MemberData(nameof(ObterNomesComMenosDe3Caracteres), parameters: 10)]
    public void ErroAoInstanciarQuandoNomeTemMenosDe3Caracteres(string nomeInvalido)
    {
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();

        Action action =
            () => new DomainEntity.Categoria(nomeInvalido, categoriaValida.Descricao);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    public static IEnumerable<object[]> ObterNomesComMenosDe3Caracteres(int numberOfTests = 6)
    {
        var fixture = new CategoriaTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;
            yield return new object[] {
                fixture.GetNomeCategoriaValido()[..(isOdd ? 1 : 2)]
            };
        }
    }

    [Fact(DisplayName = nameof(ErroAoInstanciarQuandoNomeTemMaisDe255Caracteres))]
    [Trait("Domain", "Categoria - Agregados")]
    public void ErroAoInstanciarQuandoNomeTemMaisDe255Caracteres()
    {
        var validCategory = _CategoriaTestFixture.GetCategoriaValido();
        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

        Action action =
            () => new DomainEntity.Categoria(invalidName, validCategory.Descricao);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Nome deve ter um tamanho menor que 255 caracteres.");
    }

    [Fact(DisplayName = nameof(ErroAoInstanciarQuandoDescricaoTemMaisDe10_000Caracteres))]
    [Trait("Domain", "Categoria - Agregados")]
    public void ErroAoInstanciarQuandoDescricaoTemMaisDe10_000Caracteres()
    {
        var descricaoInvalida = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();

        Action action =
            () => new DomainEntity.Categoria(categoriaValida.Nome, descricaoInvalida);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter um tamanho menor que 10000 caracteres.");
    }

    [Fact(DisplayName = nameof(Ativar))]
    [Trait("Domain", "Categoria - Agregados")]
    public void Ativar()
    {
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();

        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao, false);
        categoria.Ativar();

        categoria.Ativo.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Desativar))]
    [Trait("Domain", "Categoria - Agregados")]
    public void Desativar()
    {
        var categoriaValida = _CategoriaTestFixture.GetCategoriaValido();

        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao, true);
        categoria.Desativar();

        categoria.Ativo.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Atualizar))]
    [Trait("Domain", "Categoria - Agregados")]
    public void Atualizar()
    {
        var categoria = _CategoriaTestFixture.GetCategoriaValido();
        var categoriaNovosValores = _CategoriaTestFixture.GetCategoriaValido();

        categoria.Update(categoriaNovosValores.Nome, categoriaNovosValores.Descricao);

        categoria.Nome.Should().Be(categoriaNovosValores.Nome);
        categoria.Descricao.Should().Be(categoriaNovosValores.Descricao);
    }

    [Fact(DisplayName = nameof(AtualizarSomenteONome))]
    [Trait("Domain", "Categoria - Agregados")]
    public void AtualizarSomenteONome()
    {
        var categoria = _CategoriaTestFixture.GetCategoriaValido();
        var novoNome = _CategoriaTestFixture.GetNomeCategoriaValido();
        var descricaoAtual = categoria.Descricao;

        categoria.Update(novoNome);

        categoria.Nome.Should().Be(novoNome);
        categoria.Descricao.Should().Be(descricaoAtual);
    }

    [Theory(DisplayName = nameof(ErroAoAtualizarQuandoNomeEstaVazio))]
    [Trait("Domain", "Categoria - Agregados")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ErroAoAtualizarQuandoNomeEstaVazio(string? name)
    {
        var categoria = _CategoriaTestFixture.GetCategoriaValido();
        Action action =
            () => categoria.Update(name!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Nome não pode ser nulo ou vazio.");
    }

    [Theory(DisplayName = nameof(ErroAoAtualizarQuandoNomeTemMenosDe3Caracteres))]
    [Trait("Domain", "Categoria - Agregados")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    [InlineData("ca")]
    public void ErroAoAtualizarQuandoNomeTemMenosDe3Caracteres(string nomeInvalido)
    {
        var categoria = _CategoriaTestFixture.GetCategoriaValido();

        Action action =
            () => categoria.Update(nomeInvalido);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Nome deve ter um tamanho mínimo de 3 caracteres.");
    }

    [Fact(DisplayName = nameof(ErroAoAtualizarQuandoNomeTemMaisDe255Caracteres))]
    [Trait("Domain", "Categoria - Agregados")]
    public void ErroAoAtualizarQuandoNomeTemMaisDe255Caracteres()
    {
        var categoria = _CategoriaTestFixture.GetCategoriaValido();
        var nomeInvaldio = _CategoriaTestFixture.Faker.Lorem.Letter(256);

        Action action =
            () => categoria.Update(nomeInvaldio);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Nome deve ter um tamanho menor que 255 caracteres.");
    }

    [Fact(DisplayName = nameof(ErroAoAtualizarQuandoDescricaoTemMaisDe10_000Caracteres))]
    [Trait("Domain", "Categoria - Agregados")]
    public void ErroAoAtualizarQuandoDescricaoTemMaisDe10_000Caracteres()
    {
        var categoria = _CategoriaTestFixture.GetCategoriaValido();
        var descricaoInvalida =
            _CategoriaTestFixture.Faker.Commerce.ProductDescription();
        while (descricaoInvalida.Length <= 10_000)
            descricaoInvalida = $"{descricaoInvalida} {_CategoriaTestFixture.Faker.Commerce.ProductDescription()}";

        Action action =
            () => categoria.Update("Nome categoria", descricaoInvalida);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter um tamanho menor que 10000 caracteres.");
    }
}
