using Microsoft.AspNetCore.Mvc;
using ControleEstoque.Api.DTOs.Movimentacao;
using ControleEstoque.Api.Services.Interfaces;

namespace ControleEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacoesController : ControllerBase
{
    private readonly IMovimentacaoService _movimentacaoService;

    public MovimentacoesController(IMovimentacaoService movimentacaoService)
    {
        _movimentacaoService = movimentacaoService;
    }

    [HttpPost("entrada")]
    public async Task<IActionResult> RegistrarEntrada([FromBody] EntradaEstoqueRequest request)
    {
        var id = await _movimentacaoService.RegistrarEntradaAsync(request);
        return Ok(new { mensagem = "Entrada registrada com sucesso.", id });
    }

    [HttpPost("saida")]
    public async Task<IActionResult> RegistrarSaida([FromBody] SaidaEstoqueRequest request)
    {
        var id = await _movimentacaoService.RegistrarSaidaAsync(request);
        return Ok(new { mensagem = "Saída registrada com sucesso.", id });
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var movimentacoes = await _movimentacaoService.ObterTodasAsync();
        return Ok(movimentacoes);
    }

    [HttpGet("produto/{produtoId:int}")]
    public async Task<IActionResult> ObterPorProdutoId(int produtoId)
    {
        var movimentacoes = await _movimentacaoService.ObterPorProdutoIdAsync(produtoId);
        return Ok(movimentacoes);
    }

    [HttpGet("historico")]
    public async Task<IActionResult> Historico()
    {
        var historico = await _movimentacaoService.ObterHistoricoAsync();

        return Ok(historico);
    }
}