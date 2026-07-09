using ControleEstoque.Api.Domain.Entities;

namespace ControleEstoque.Api.Infrastructure.Repositories.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> ObterTodosAsync();
    Task<Categoria?> ObterPorIdAsync(int id);
    Task<int> CriarAsync(Categoria categoria);
    Task<bool> AtualizarAsync(Categoria categoria);
    Task<bool> DeletarAsync(int id);
}