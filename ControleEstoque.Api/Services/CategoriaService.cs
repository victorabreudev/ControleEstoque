using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.DTOs.Categoria;
using ControleEstoque.Api.Infrastructure.Repositories.Interfaces;
using ControleEstoque.Api.Services.Interfaces;

namespace ControleEstoque.Api.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Categoria>> ObterTodosAsync()
        => await _categoriaRepository.ObterTodosAsync();

    public async Task<Categoria?> ObterPorIdAsync(int id)
        => await _categoriaRepository.ObterPorIdAsync(id);

    public async Task<int> CriarAsync(CriarCategoriaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome da categoria é obrigatório.");

        var categoria = new Categoria
        {
            Nome = request.Nome.Trim()
        };

        return await _categoriaRepository.CriarAsync(categoria);
    }

    public async Task<bool> AtualizarAsync(int id, AtualizarCategoriaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome da categoria é obrigatório.");

        var categoriaExistente = await _categoriaRepository.ObterPorIdAsync(id);
        if (categoriaExistente is null)
            throw new KeyNotFoundException("Categoria não encontrada.");

        categoriaExistente.Nome = request.Nome.Trim();

        return await _categoriaRepository.AtualizarAsync(categoriaExistente);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var categoriaExistente = await _categoriaRepository.ObterPorIdAsync(id);
        if (categoriaExistente is null)
            throw new KeyNotFoundException("Categoria não encontrada.");

        return await _categoriaRepository.DeletarAsync(id);
    }
}