namespace ControleEstoque.Api.DTOs.Produto
{
    public class AtualizarProdutoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int CategoriaId { get; set; }
        public bool Ativo { get; set; }
    }
}
