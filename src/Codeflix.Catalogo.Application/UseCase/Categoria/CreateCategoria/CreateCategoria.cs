using Codeflix.Catalogo.Application.Interfaces;
using Codeflix.Catalogo.Domain.Repository;
using DomainEntity = Codeflix.Catalogo.Domain.Entity;

namespace Codeflix.Catalogo.Application.UseCase.Categoria.AddCategoria;

public class CreateCategoria : ICreateCategoria
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoriaRepository _categoriaRepository;

    public CreateCategoria(IUnitOfWork unitOfWork, ICategoriaRepository categoriaRepository)
    {
        _unitOfWork = unitOfWork;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<CreateCategoriaOutput> Handle(CreateCategoriaInput input, CancellationToken cancellationToken)
    {
        var categoria = new DomainEntity.Categoria(input.Nome, input.Descricao);

        await _categoriaRepository.Insert(categoria, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return new CreateCategoriaOutput(
            categoria.Id,
            categoria.Nome,
            categoria.Descricao,
            categoria.Ativo,
            categoria.DataCriacao
        );
    }
}
