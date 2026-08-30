namespace ControleEstoque.Api.DTOs.Produto
{
    public class ProdutoEstoqueBaixoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int QuantidadeEstoque { get; set; }
    }
}
