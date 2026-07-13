using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.DTOs.Movimentacao;
using static ControleEstoque.Api.DTOs.Movimentacao.HistoricoMovimentacaoRequest;

namespace ControleEstoque.Api.Services.Interfaces;

public interface IMovimentacaoService
{
    Task<int> RegistrarEntradaAsync(EntradaEstoqueRequest request);
    Task<int> RegistrarSaidaAsync(SaidaEstoqueRequest request);
    Task<IEnumerable<MovimentacaoEstoque>> ObterTodasAsync();
    Task<IEnumerable<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId);
    Task<IEnumerable<HistoricoMovimentacaoResponse>> ObterHistoricoAsync();

}