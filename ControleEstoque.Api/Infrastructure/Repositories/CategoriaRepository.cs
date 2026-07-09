using Dapper;
using ControleEstoque.Api.Domain.Entities;
using ControleEstoque.Api.Infrastructure.Data;
using ControleEstoque.Api.Infrastructure.Repositories.Interfaces;

namespace ControleEstoque.Api.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly DapperContext _context;

    public CategoriaRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> ObterTodosAsync()
    {
        const string sql = "SELECT Id, Nome FROM Categoria ORDER BY Nome";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Categoria>(sql);
    }

    public async Task<Categoria?> ObterPorIdAsync(int id)
    {
        const string sql = "SELECT Id, Nome FROM Categoria WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Categoria>(sql, new { Id = id });
    }

    public async Task<int> CriarAsync(Categoria categoria)
    {
        const string sql = @"
            INSERT INTO Categoria (Nome)
            VALUES (@Nome);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, categoria);
    }

    public async Task<bool> AtualizarAsync(Categoria categoria)
    {
        const string sql = @"
            UPDATE Categoria
            SET Nome = @Nome
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(sql, categoria);
        return linhasAfetadas > 0;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        const string sql = "DELETE FROM Categoria WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(sql, new { Id = id });
        return linhasAfetadas > 0;
    }
}