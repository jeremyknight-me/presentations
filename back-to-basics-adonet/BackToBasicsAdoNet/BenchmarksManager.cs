#if RELEASE

using BackToBasicsAdoNet.Benchmarks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

namespace BackToBasicsAdoNet;

internal static class BenchmarksManager
{
	internal static void Run()
	{
		var config = DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default);
		var enabled = true;
		if (!enabled) { BenchmarkRunner.Run<GetByIdBenchmarks>(config); }
		if (!enabled) { BenchmarkRunner.Run<GetListBenchmarks>(config); }
		if (!enabled) { BenchmarkRunner.Run<GetMultipleBenchmarks>(config); }
	}
}

#endif