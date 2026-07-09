using Dapper;
using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.Infrastructure.Data;
using ControleEstoque.Api.Infrastructure.Repositories.Interfaces;

namespace ControleEstoque.Api.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly DapperContext _context;

    public ProdutoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        const string sql = @"
            SELECT Id, Nome, Descricao, Preco, QuantidadeEstoque, CategoriaId, DataCadastro, Ativo
            FROM Produto
            ORDER BY Nome";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Produto>(sql);
    }

    public async Task<Produto?> ObterPorIdAsync(int id)
    {
        const string sql = @"
            SELECT Id, Nome, Descricao, Preco, QuantidadeEstoque, CategoriaId, DataCadastro, Ativo
            FROM Produto
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
    }

    public async Task<int> CriarAsync(Produto produto)
    {
        const string sql = @"
            INSERT INTO Produto (Nome, Descricao, Preco, QuantidadeEstoque, CategoriaId, Ativo)
            VALUES (@Nome, @Descricao, @Preco, @QuantidadeEstoque, @CategoriaId, @Ativo);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, produto);
    }

    public async Task<bool> AtualizarAsync(Produto produto)
    {
        const string sql = @"
            UPDATE Produto
            SET Nome = @Nome,
                Descricao = @Descricao,
                Preco = @Preco,
                CategoriaId = @CategoriaId,
                Ativo = @Ativo
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(sql, produto);
        return linhasAfetadas > 0;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        const string sql = "DELETE FROM Produto WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(sql, new { Id = id });
        return linhasAfetadas > 0;
    }

    public async Task<bool> AtualizarEstoqueAsync(int produtoId, int novaQuantidade)
    {
        const string sql = @"
            UPDATE Produto
            SET QuantidadeEstoque = @NovaQuantidade
            WHERE Id = @ProdutoId";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(sql, new
        {
            ProdutoId = produtoId,
            NovaQuantidade = novaQuantidade
        });

        return linhasAfetadas > 0;
    }
}