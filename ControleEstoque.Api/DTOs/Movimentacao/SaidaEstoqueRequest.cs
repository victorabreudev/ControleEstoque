namespace ControleEstoque.Api.DTOs.Movimentacao
{
    public class SaidaEstoqueRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public string? Observacao { get; set; }
    }
}
