using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BackToBasicsAdoNet.EntityFramework.Chinook;
using BenchmarkDotNet.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackToBasicsAdoNet.Benchmarks;

public class GetMultipleBenchmarks
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
	public (IReadOnlyList<Genre>, IReadOnlyList<MediaType>) Ado()
	{
		using var command = this.sqlConnection.CreateCommand();
		command.CommandType = CommandType.Text;
		command.CommandText =
			"""
			SELECT GenreId, Name FROM dbo.Genre;
			SELECT MediaTypeId, Name FROM dbo.MediaType;
			""";

		if (this.sqlConnection.State != ConnectionState.Open)
		{
			this.sqlConnection.Open();
		}

		using var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

		var genres = new List<Genre>();
		var mediaTypes = new List<MediaType>();

		if (reader.HasRows)
		{
			while (reader.Read())
			{
				var genre = new Genre
				{
					GenreId = reader.GetInt32("GenreId"),
					Name = reader.GetString("Name")
				};
				genres.Add(genre);
			}
		}

		reader.NextResult();

		if (reader.HasRows)
		{
			while (reader.Read())
			{
				var artist = new MediaType
				{
					MediaTypeId = reader.GetInt32("MediaTypeId"),
					Name = reader.GetString("Name")
				};
				mediaTypes.Add(artist);
			}
		}

		return (genres, mediaTypes);
	}

	[Benchmark]
	public (IReadOnlyList<Genre>, IReadOnlyList<MediaType>) EF_ToList()
	{
		var genres = this.chinookContext.Genres.ToList();
		var mediaTypes = this.chinookContext.MediaTypes.ToList();
		return (genres, mediaTypes);
	}

	[Benchmark]
	public (IReadOnlyList<Genre>, IReadOnlyList<MediaType>) EF_ToList_NoTrack()
	{
		var genres = this.chinookContext.Genres.AsNoTracking().ToList();
		var mediaTypes = this.chinookContext.MediaTypes.AsNoTracking().ToList();
		return (genres, mediaTypes);
	}
}
