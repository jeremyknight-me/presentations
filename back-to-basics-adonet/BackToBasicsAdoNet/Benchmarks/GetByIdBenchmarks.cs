using System.Data;
using System.Linq;
using BackToBasicsAdoNet.EntityFramework.Chinook;
using BenchmarkDotNet.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackToBasicsAdoNet.Benchmarks;

public class GetByIdBenchmarks
{
	private const int artistId = 137;
	private ChinookContext chinookContext;
	private SqlConnection sqlConnection;

	[GlobalSetup]
	public void Setup()
	{
		var connectionStrings = ConnectionStrings.Create();

		var optionsBuilder = new DbContextOptionsBuilder<ChinookContext>();
		optionsBuilder.UseSqlServer(connectionStrings.Chinook);
		this.chinookContext = new ChinookContext(optionsBuilder.Options);
		this.sqlConnection = new SqlConnection(connectionStrings.Chinook);
	}

	[GlobalCleanup] 
	public void Cleanup() 
	{ 
		this.chinookContext?.Dispose();
		this.sqlConnection?.Dispose();
	}

	[Benchmark]
	public Artist Ado()
	{
		using var command = this.sqlConnection.CreateCommand();
		command.CommandType = CommandType.Text;
		command.CommandText = "SELECT ArtistId, Name FROM dbo.Artist WHERE ArtistId = @id";

		command.Parameters.Add("@id", SqlDbType.Int);
		command.Parameters["@id"].Value = artistId;

		if (this.sqlConnection.State != ConnectionState.Open)
		{
			this.sqlConnection.Open();
		}

		using var reader = command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.CloseConnection);
		if (!reader.HasRows)
		{
			return null;
		}

		reader.Read();
		var artist = new Artist
		{
			ArtistId = reader.GetInt32("ArtistId"),
			Name = reader.GetString("Name")
		};

		return artist;
	}

	[Benchmark]
	public Artist EF_FirstOrDefault()
	{
		var artist = this.chinookContext.Artists.FirstOrDefault(x => x.ArtistId == artistId);
		return artist;
	}

	[Benchmark]
	public Artist EF_FirstOrDefault_NoTrack()
	{
		var artist = this.chinookContext.Artists.AsNoTracking().FirstOrDefault(x => x.ArtistId == artistId);
		return artist;
	}
}
