using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.DTOs.Produto;
using ControleEstoque.Api.Infrastructure.Repositories.Interfaces;
using ControleEstoque.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ControleEstoque.Api.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public ProdutoService(
        IProdutoRepository produtoRepository,
        ICategoriaRepository categoriaRepository)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
        => await _produtoRepository.ObterTodosAsync();

    public async Task<Produto?> ObterPorIdAsync(int id)
        => await _produtoRepository.ObterPorIdAsync(id);

    public async Task<int> CriarAsync(CriarProdutoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (request.Preco < 0)
            throw new ArgumentException("O preço não pode ser negativo.");

        if (request.QuantidadeEstoque < 0)
            throw new ArgumentException("A quantidade em estoque não pode ser negativa.");

        var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
        if (categoria is null)
            throw new KeyNotFoundException("Categoria não encontrada.");

        var produto = new Produto
        {
            Nome = request.Nome.Trim(),
            Descricao = request.Descricao,
            Preco = request.Preco,
            QuantidadeEstoque = request.QuantidadeEstoque,
            CategoriaId = request.CategoriaId,
            Ativo = true
        };

        return await _produtoRepository.CriarAsync(produto);
    }

    public async Task<bool> AtualizarAsync(int id, AtualizarProdutoRequest request)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);
        if (produto is null)
            throw new KeyNotFoundException("Produto não encontrado.");

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (request.Preco < 0)
            throw new ArgumentException("O preço não pode ser negativo.");

        var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
        if (categoria is null)
            throw new KeyNotFoundException("Categoria não encontrada.");

        produto.Nome = request.Nome.Trim();
        produto.Descricao = request.Descricao;
        produto.Preco = request.Preco;
        produto.CategoriaId = request.CategoriaId;
        produto.Ativo = request.Ativo;

        return await _produtoRepository.AtualizarAsync(produto);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);
        if (produto is null)
            throw new KeyNotFoundException("Produto não encontrado.");

        return await _produtoRepository.DeletarAsync(id);
    }

    public async Task<List<ProdutoEstoqueBaixoResponse>> ObterEstoqueBaixoAsync(int quantidadeMinima)
    {
        var produtos = await _produtoRepository.ObterEstoqueBaixoAsync(quantidadeMinima);

        return produtos.ToList();
    }
}