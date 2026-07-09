using ControleEstoque.Api.Domain.Enums;

namespace ControleEstoque.Api.Domain.Entities
{
    public class MovimentacaoEstoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
        public int Quantidade { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataMovimentacao { get; set; }
    }
}
