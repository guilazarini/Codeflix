namespace Codeflix.Catalogo.Application.UseCase.Categoria.AddCategoria;
public class CreateCategoriaOutput
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }

    public CreateCategoriaOutput(Guid id, string nome, string? descricao, bool ativo, DateTime dataCriacao)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Ativo = ativo;
        DataCriacao = dataCriacao;
    }
}
