using ControleEstoque.Api.Domain.Entities;
using static ControleEstoque.Api.DTOs.Movimentacao.HistoricoMovimentacaoRequest;

namespace ControleEstoque.Api.Infrastructure.Repositories.Interfaces
{
    public interface IMovimentacaoRepository
    {
        Task<int> CriarAsync(MovimentacaoEstoque movimentacao);
        Task<IEnumerable<MovimentacaoEstoque>> ObterTodasAsync();
        Task<IEnumerable<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId);
        Task<IEnumerable<HistoricoMovimentacaoResponse>> ObterHistoricoAsync();
    }
}
