using ControleEstoque.Api.Domain.Entities;

namespace ControleEstoque.Api.Infrastructure.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto?> ObterPorIdAsync(int id);
        Task<int> CriarAsync(Produto produto);
        Task<bool> AtualizarAsync(Produto produto);
        Task<bool> DeletarAsync(int id);
        Task<bool> AtualizarEstoqueAsync(int produtoId, int novaQuantidade);
    }
}
