namespace Codeflix.Catalogo.Application.UseCase.Categoria.AddCategoria;

public interface ICreateCategoria
{
    public Task<CreateCategoriaOutput> Handle(CreateCategoriaInput input, CancellationToken cancellationToken);
}
