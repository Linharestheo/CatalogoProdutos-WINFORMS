namespace CatalogoProdutos;

public class Produto
{
    public string Codigo { get; set; } = "";
    public string Nome { get; set; } = "";
    public string Categoria { get; set; } = "";
    public decimal Preco { get; set; } = 0;
    public int Estoque { get; set; }= 0;
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public bool Ativo { get; set; } = true;
    public bool EmPromocao { get; set; }
}