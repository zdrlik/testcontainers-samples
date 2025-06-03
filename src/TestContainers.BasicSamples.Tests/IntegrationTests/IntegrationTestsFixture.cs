using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace TestContainers.BasicSamples.Tests.IntegrationTests;
public class IntegrationTestsFixture : IAsyncLifetime
{
    private readonly MsSqlContainer? sqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU13-ubuntu-22.04")
        .WithPortBinding(1433, true)
        .Build();
    private const string DatabaseInitScript = @"IntegrationTests/init_testdb.sql";

    public string ConnectionString { get; private set; } = "";

    public async Task InitializeAsync()
    {
        await sqlContainer!.StartAsync();
        var scriptResult = await sqlContainer.ExecScriptAsync(File.ReadAllText(DatabaseInitScript));
        if (IsErrorResult(scriptResult))
        {
            throw new InvalidOperationException($"Failed to execute initialization script: {scriptResult.Stderr}");
        }
        var virtualCardDbConnectionStringBuilder = new SqlConnectionStringBuilder(sqlContainer.GetConnectionString())
        {
            InitialCatalog = "TestDb",
            UserID = "test_dbuser",
            Password = "SqlonLinux?!"
        };
        ConnectionString = virtualCardDbConnectionStringBuilder.ConnectionString;
    }

    public async Task DisposeAsync()
    {
        await sqlContainer!.StopAsync();
    }

    private static bool IsErrorResult(DotNet.Testcontainers.Containers.ExecResult scriptResult)
    {
        return scriptResult.ExitCode != 0 || scriptResult.Stderr.Contains("ERROR", StringComparison.Ordinal);
    }
}
