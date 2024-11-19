namespace Codeflix.Catalogo.Application.UseCase.Categoria.AddCategoria;
public class CreateCategoriaInput
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }

    public CreateCategoriaInput(string nome, string? descricao = null, bool ativo = true)
    {
        Nome = nome;
        Descricao = descricao ?? "";
        Ativo = ativo;
    }
}
