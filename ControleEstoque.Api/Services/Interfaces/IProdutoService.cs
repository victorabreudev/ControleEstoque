using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.DTOs.Produto;

namespace ControleEstoque.Api.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> ObterTodosAsync();
    Task<Produto?> ObterPorIdAsync(int id);
    Task<int> CriarAsync(CriarProdutoRequest request);
    Task<bool> AtualizarAsync(int id, AtualizarProdutoRequest request);
    Task<bool> DeletarAsync(int id);
}