using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.DTOs.Movimentacao;
using ControleEstoque.Api.Infrastructure.Data;
using ControleEstoque.Api.Infrastructure.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static ControleEstoque.Api.DTOs.Movimentacao.HistoricoMovimentacaoRequest;

namespace ControleEstoque.Api.Infrastructure.Repositories;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly DapperContext _context;

    public MovimentacaoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> CriarAsync(MovimentacaoEstoque movimentacao)
    {
        const string sql = @"
            INSERT INTO MovimentacaoEstoque (ProdutoId, TipoMovimentacao, Quantidade, Observacao)
            VALUES (@ProdutoId, @TipoMovimentacao, @Quantidade, @Observacao);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, movimentacao);
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ObterTodasAsync()
    {
        const string sql = @"
            SELECT Id, ProdutoId, TipoMovimentacao, Quantidade, Observacao, DataMovimentacao
            FROM MovimentacaoEstoque
            ORDER BY DataMovimentacao DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<MovimentacaoEstoque>(sql);
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId)
    {
        const string sql = @"
            SELECT Id, ProdutoId, TipoMovimentacao, Quantidade, Observacao, DataMovimentacao
            FROM MovimentacaoEstoque
            WHERE ProdutoId = @ProdutoId
            ORDER BY DataMovimentacao DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<MovimentacaoEstoque>(sql, new { ProdutoId = produtoId });
    }

    public async Task<IEnumerable<HistoricoMovimentacaoResponse>> ObterHistoricoAsync()
    {
        const string sql = @"
            SELECT
                m.Id,
                p.Nome AS Produto,
                c.Nome AS Categoria,
                m.Quantidade,
                m.TipoMovimentacao,
                m.DataMovimentacao
            FROM MovimentacaoEstoque m
            INNER JOIN Produto p
                ON p.Id = m.ProdutoId
            INNER JOIN Categoria c
                ON c.Id = p.CategoriaId
            ORDER BY m.DataMovimentacao DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<HistoricoMovimentacaoResponse>(sql);
    }
}