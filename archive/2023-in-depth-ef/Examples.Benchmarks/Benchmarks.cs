using BenchmarkDotNet.Attributes;
using Dapper;
using Examples.Data;
using Examples.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Examples.Benchmarks;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    private ChinookContext context = default!;
    private SqlConnection connection = default!;

    [GlobalSetup]
    public async Task Setup()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddUserSecrets(typeof(Benchmarks).Assembly, true);
        var config = configBuilder.Build();
        var connectionString = config.GetConnectionString("Chinook");

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ChinookContext>();
        dbContextOptionsBuilder.UseSqlServer(connectionString);
        this.context = new ChinookContext(dbContextOptionsBuilder.Options);

        this.connection = new SqlConnection(connectionString);
    }

    [GlobalCleanup]
    public async Task Cleanup()
    {
        this.context.Dispose();
        this.connection.Dispose();
    }

    private int GetRandomId() => Random.Shared.Next(1, 225);

    #region GetById

    [Benchmark]
    public async Task<Artist?> EF_GetById_Find()
        => await this.context.Artists.FindAsync(this.GetRandomId());

    [Benchmark]
    public async Task<Artist?> EF_GetById_FirstOrDefault()
        => await this.context.Artists.FirstOrDefaultAsync(x => x.ArtistId == this.GetRandomId());

    [Benchmark]
    public async Task<Artist?> EF_GetById_FirstOrDefault_Compiled()
        => await firstMovieAsync(this.context, this.GetRandomId());

    private static readonly Func<ChinookContext, int, Task<Artist?>> firstMovieAsync
        = EF.CompileAsyncQuery((ChinookContext context, int id)
            => context.Artists.FirstOrDefault(x => x.ArtistId == id));

    [Benchmark]
    public async Task<Artist?> EF_GetById_FirstOrDefault_NoTracking()
        => await this.context.Artists.AsNoTracking().FirstOrDefaultAsync(x => x.ArtistId == this.GetRandomId());

    [Benchmark]
    public async Task<Artist?> EF_GetById_FirstOrDefault_NoTracking_Compiled()
        => await firstMovieNoTrackingAsync(this.context, this.GetRandomId());

    private static readonly Func<ChinookContext, int, Task<Artist?>> firstMovieNoTrackingAsync
        = EF.CompileAsyncQuery((ChinookContext context, int id)
            => context.Artists.AsNoTracking().FirstOrDefault(x => x.ArtistId == id));

    [Benchmark]
    public async Task<Artist?> Dapper_GetById()
        => await this.connection.QuerySingleOrDefaultAsync<Artist>(
            "select top 1 * from dbo.Artist where ArtistId = @Id",
            new { Id = this.GetRandomId() }
        );

    #endregion
}
