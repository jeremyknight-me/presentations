using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BackToBasicsAdoNet.EntityFramework.Chinook;
using BenchmarkDotNet.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackToBasicsAdoNet.Benchmarks;

public class GetListBenchmarks
{
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
	public IReadOnlyList<Artist> Ado()
	{
		using var command = this.sqlConnection.CreateCommand();
		command.CommandType = CommandType.Text;
		command.CommandText = "SELECT TOP 20 ArtistId, Name FROM dbo.Artist;";

		if (this.sqlConnection.State != ConnectionState.Open)
		{
			this.sqlConnection.Open();
		}

		using var reader = command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
		if (!reader.HasRows)
		{
			return new List<Artist>();
		}

		var list = new List<Artist>();
		while (reader.Read())
		{
			var artist = new Artist
			{
				ArtistId = reader.GetInt32("ArtistId"),
				Name = reader.GetString("Name")
			};
			list.Add(artist);
		}

		return list;
	}

	[Benchmark]
	public IReadOnlyList<Artist> EF_ToList()
	{
		var artist = this.chinookContext.Artists.Take(20).ToList();
		return artist;
	}

	[Benchmark]
	public IReadOnlyList<Artist> EF_ToList_NoTrack()
	{
		var artist = this.chinookContext.Artists.AsNoTracking().Take(20).ToList();
		return artist;
	}
}
