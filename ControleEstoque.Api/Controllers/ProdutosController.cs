using Microsoft.AspNetCore.Mvc;
using ControleEstoque.Api.DTOs.Produto;
using ControleEstoque.Api.Services.Interfaces;
using ControleEstoque.Api.Infrastructure.Repositories;

namespace ControleEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var produto = await _produtoService.ObterPorIdAsync(id);
        if (produto is null)
            return NotFound(new { mensagem = "Produto não encontrado." });

        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProdutoRequest request)
    {
        var id = await _produtoService.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarProdutoRequest request)
    {
        var atualizado = await _produtoService.AtualizarAsync(id, request);
        return Ok(new { sucesso = atualizado });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var deletado = await _produtoService.DeletarAsync(id);
        return Ok(new { sucesso = deletado });
    }
    [HttpGet("estoque-baixo")]
    public async Task<IActionResult> ObterEstoqueBaixo(
    [FromQuery] int quantidadeMinima = 10)
    {
        var produtos = await _produtoService.ObterEstoqueBaixoAsync(quantidadeMinima);

        return Ok(produtos);
    }
}