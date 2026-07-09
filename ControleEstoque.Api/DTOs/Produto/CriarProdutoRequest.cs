namespace ControleEstoque.Api.DTOs.Produto
{
    public class CriarProdutoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int CategoriaId { get; set; }
    }
}
