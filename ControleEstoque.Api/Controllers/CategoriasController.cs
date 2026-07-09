using Microsoft.AspNetCore.Mvc;
using ControleEstoque.Api.DTOs.Categoria;
using ControleEstoque.Api.Services.Interfaces;

namespace ControleEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var categorias = await _categoriaService.ObterTodosAsync();
        return Ok(categorias);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var categoria = await _categoriaService.ObterPorIdAsync(id);
        if (categoria is null)
            return NotFound(new { mensagem = "Categoria não encontrada." });

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarCategoriaRequest request)
    {
        var id = await _categoriaService.CriarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarCategoriaRequest request)
    {
        var atualizado = await _categoriaService.AtualizarAsync(id, request);
        return Ok(new { sucesso = atualizado });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var deletado = await _categoriaService.DeletarAsync(id);
        return Ok(new { sucesso = deletado });
    }
}