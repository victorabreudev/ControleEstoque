namespace ControleEstoque.Api.DTOs.Movimentacao
{
    public class HistoricoMovimentacaoRequest
    {
        public class HistoricoMovimentacaoResponse
        {
            public int Id { get; set; }

            public string Produto { get; set; } = string.Empty;

            public string Categoria { get; set; } = string.Empty;

            public int Quantidade { get; set; }

            public string TipoMovimentacao { get; set; } = string.Empty;

            public DateTime DataMovimentacao { get; set; }
        }
    }
}
