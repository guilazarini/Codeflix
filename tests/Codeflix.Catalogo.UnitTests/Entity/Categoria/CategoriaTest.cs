using DomainEntity = Codeflix.Catalogo.Domain.Entity;
using FluentAssertions;
using Codeflix.Catalogo.Domain.Entity;
using Codeflix.Catalogo.Domain.Exceptions;
namespace Codeflix.Catalogo.UnitTests.Entity.Categoria;

[Collection(nameof(CategoriaTestFixture))]
public class CategoriaTest
{
    private readonly CategoriaTestFixture _categoriaTestFixture;

    public CategoriaTest(CategoriaTestFixture categoriaTestFixture)
    {
        _categoriaTestFixture = categoriaTestFixture;
    }

    [Fact(DisplayName = nameof(Instanciar))]
    [Trait("Domain", "Categoria - Agregação")]
    public void Instanciar()
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        var dataAntes = DateTime.Now;
       
        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao);
        var dataDepois = DateTime.Now;

        categoria.Should().NotBeNull();
        categoria.Nome.Should().Be(categoriaValida.Nome);
        categoria.Descricao.Should().Be(categoriaValida.Descricao);
        categoria.Id.Should().NotBeEmpty();
        categoria.DataCriacao.Should().NotBeSameDateAs(default(DateTime));
        (categoria.DataCriacao >= dataAntes).Should().BeTrue();
        (categoria.DataCriacao <= dataDepois).Should().BeTrue();
        (categoria.Ativo).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstanciarAtivo))]
    [Trait("Domain", "Categoria - Agregação")]
    [InlineData(false)]
    [InlineData(true)]
    public void InstanciarAtivo(bool ativo)
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        var dataAntes = DateTime.Now;

        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao, ativo);
        var dataDepois = DateTime.Now;

        categoria.Should().NotBeNull();
        categoria.Nome.Should().Be(categoriaValida.Nome);
        categoria.Descricao.Should().Be(categoriaValida.Descricao);
        categoria.Id.Should().NotBeEmpty();
        categoria.DataCriacao.Should().NotBeSameDateAs(default(DateTime));
        (categoria.DataCriacao >= dataAntes).Should().BeTrue();
        (categoria.DataCriacao <= dataDepois).Should().BeTrue();
        (categoria.Ativo).Should().Be(ativo);
    }

    [Theory(DisplayName = nameof(InstanciarErroQuandoNomeEstiverVazio))]
    [Trait("Domain", "Categoria - Agregação")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstanciarErroQuandoNomeEstiverVazio(string? nome)
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        Action action =
            () => new DomainEntity.Categoria(nome!, categoriaValida.Descricao);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Nome não pode ser nulo ou vazio.");
    }

    [Fact(DisplayName = nameof(InstanciarErroQuandoNomeEstiverVazio))]
    [Trait("Domain", "Categoria - Agregação")]
    public void InstanciarErroQuandoDescricaoEstiverNulo()
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        Action action =
            () => new DomainEntity.Categoria(categoriaValida.Nome, null!);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser nulo.");
    }

    [Theory(DisplayName = nameof(InstanciarErroQuandoNomeForMenorQue3Caracteres))]
    [Trait("Domain", "Categoria - Agregação")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    [InlineData("ca")]
    public void InstanciarErroQuandoNomeForMenorQue3Caracteres(string nomeInvalido)
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        Action action =
           () => new DomainEntity.Categoria(nomeInvalido, categoriaValida.Descricao);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Nome deve ter um tamanho mínimo de 3 caracteres.");
    }

    [Fact(DisplayName = nameof(InstanciarErroQuandoNomeForMenorQue3Caracteres))]
    [Trait("Domain", "Categoria - Agregação")]
    public void InstanciarErroQuandoNomeFormaiorQue255Caracteres()
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        var nomeInvalido = string.Join(null, Enumerable.Range(0, 255).Select(_ => "a").ToArray());
        Action action =
           () => new DomainEntity.Categoria(nomeInvalido, categoriaValida.Descricao);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Nome deve ter um tamanho menor que 255 caracteres.");
    }

    [Fact(DisplayName = nameof(InstanciarErroQuandoDescricaoFormaiorQue10kCaracteres))]
    [Trait("Domain", "Categoria - Agregação")]
    public void InstanciarErroQuandoDescricaoFormaiorQue10kCaracteres()
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        var descricaoInvalido = string.Join(null, Enumerable.Range(0, 10_000).Select(_ => "a").ToArray());
        Action action =
           () => new DomainEntity.Categoria(categoriaValida.Nome, descricaoInvalido);
        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter um tamanho menor que 10000 caracteres.");
    }

    [Fact(DisplayName = nameof(Ativo))]
    [Trait("Domain", "Categoria - Agregação")]
    public void Ativo()
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao, false);
        categoria.Ativar();

        categoria.Ativo.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Desativado))]
    [Trait("Domain", "Categoria - Agregação")]
    public void Desativado()
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();

        var categoria = new DomainEntity.Categoria(categoriaValida.Nome, categoriaValida.Descricao, true);
        categoria.Desativar();

        categoria.Ativo.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Categoria - Agregação")]
    public void Update()
    {
        var categoriaValida = _categoriaTestFixture.GetValorCategoria();
        var novosValores = new { Nome = "Novo nome", Descricao = "Nova descrição" };

        categoriaValida.Update(novosValores.Nome, novosValores.Descricao);

        categoriaValida.Nome.Should().Be(novosValores.Nome);
        categoriaValida.Descricao.Should().Be(novosValores.Descricao);
    }
}
