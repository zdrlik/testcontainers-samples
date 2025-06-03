using Dapper;
using Microsoft.Data.SqlClient;

namespace TestContainers.BasicSamples;

public class SqlUserRepository : IUserRepository
{
    private readonly string _connectionString;

    public SqlUserRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task<UserEntity?> GetUserByIdAsync(int id)
    {
        const string sql = "SELECT Id, Name, Email FROM Users WHERE Id = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QuerySingleOrDefaultAsync<UserEntity?>(sql, new { Id = id });
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        const string sql = "SELECT Id, Name, Email FROM Users";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<UserEntity>(sql);
    }
}
