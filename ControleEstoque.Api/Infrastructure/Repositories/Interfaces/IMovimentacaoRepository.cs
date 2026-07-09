using ControleEstoque.Api.Domain.Entities;

namespace ControleEstoque.Api.Infrastructure.Repositories.Interfaces
{
    public interface IMovimentacaoRepository
    {
        Task<int> CriarAsync(MovimentacaoEstoque movimentacao);
        Task<IEnumerable<MovimentacaoEstoque>> ObterTodasAsync();
        Task<IEnumerable<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId);
    }
}
