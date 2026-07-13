using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.Domain.Enums;
using ControleEstoque.Api.DTOs.Movimentacao;
using ControleEstoque.Api.Infrastructure.Repositories.Interfaces;
using ControleEstoque.Api.Services.Interfaces;
using static ControleEstoque.Api.DTOs.Movimentacao.HistoricoMovimentacaoRequest;

namespace ControleEstoque.Api.Services;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public MovimentacaoService(
        IMovimentacaoRepository movimentacaoRepository,
        IProdutoRepository produtoRepository)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<int> RegistrarEntradaAsync(EntradaEstoqueRequest request)
    {
        if (request.Quantidade <= 0)
            throw new ArgumentException("A quantidade de entrada deve ser maior que zero.");

        var produto = await _produtoRepository.ObterPorIdAsync(request.ProdutoId);
        if (produto is null)
            throw new KeyNotFoundException("Produto não encontrado.");

        produto.QuantidadeEstoque += request.Quantidade;

        await _produtoRepository.AtualizarEstoqueAsync(produto.Id, produto.QuantidadeEstoque);

        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = request.ProdutoId,
            TipoMovimentacao = TipoMovimentacao.Entrada,
            Quantidade = request.Quantidade,
            Observacao = request.Observacao
        };

        return await _movimentacaoRepository.CriarAsync(movimentacao);
    }

    public async Task<int> RegistrarSaidaAsync(SaidaEstoqueRequest request)
    {
        if (request.Quantidade <= 0)
            throw new ArgumentException("A quantidade de saída deve ser maior que zero.");

        var produto = await _produtoRepository.ObterPorIdAsync(request.ProdutoId);
        if (produto is null)
            throw new KeyNotFoundException("Produto não encontrado.");

        if (produto.QuantidadeEstoque < request.Quantidade)
            throw new InvalidOperationException("Estoque insuficiente para realizar a saída.");

        produto.QuantidadeEstoque -= request.Quantidade;

        await _produtoRepository.AtualizarEstoqueAsync(produto.Id, produto.QuantidadeEstoque);

        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = request.ProdutoId,
            TipoMovimentacao = TipoMovimentacao.Saida,
            Quantidade = request.Quantidade,
            Observacao = request.Observacao
        };

        return await _movimentacaoRepository.CriarAsync(movimentacao);
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ObterTodasAsync()
        => await _movimentacaoRepository.ObterTodasAsync();

    public async Task<IEnumerable<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId)
        => await _movimentacaoRepository.ObterPorProdutoIdAsync(produtoId);

    public async Task<IEnumerable<HistoricoMovimentacaoResponse>> ObterHistoricoAsync()
    {
        var historico = await _movimentacaoRepository.ObterHistoricoAsync();

        return historico.ToList();
    }
}