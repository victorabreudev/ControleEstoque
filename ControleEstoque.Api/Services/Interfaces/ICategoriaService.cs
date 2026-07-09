using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.DTOs.Categoria;

namespace ControleEstoque.Api.Services.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> ObterTodosAsync();
    Task<Categoria?> ObterPorIdAsync(int id);
    Task<int> CriarAsync(CriarCategoriaRequest request);
    Task<bool> AtualizarAsync(int id, AtualizarCategoriaRequest request);
    Task<bool> DeletarAsync(int id);
}