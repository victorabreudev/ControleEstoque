using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.DTOs.Movimentacao;

namespace ControleEstoque.Api.Services.Interfaces;

public interface IMovimentacaoService
{
    Task<int> RegistrarEntradaAsync(EntradaEstoqueRequest request);
    Task<int> RegistrarSaidaAsync(SaidaEstoqueRequest request);
    Task<IEnumerable<MovimentacaoEstoque>> ObterTodasAsync();
    Task<IEnumerable<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId);
}