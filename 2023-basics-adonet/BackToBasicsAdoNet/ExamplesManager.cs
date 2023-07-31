#if DEBUG

using BackToBasicsAdoNet.Examples;

namespace BackToBasicsAdoNet;

internal static class ExamplesManager
{
	internal static void Run()
	{
        var connectionStrings = ConnectionStrings.Create();
		var enabled = true;

		if (!enabled) { QueryExample.Run(connectionStrings); }
		if (!enabled) { QueryByIdExample.Run(connectionStrings); }

		if (!enabled) { InsertExample.Run(connectionStrings); }
		if (!enabled) { BulkCopyExample.Run(connectionStrings); }

		if (!enabled) { UpdateExample.Run(connectionStrings); }
		if (!enabled) { DeleteExample.Run(connectionStrings); }

		if (!enabled) { IntegrationExample.Run(connectionStrings); }

		if (!enabled) { BulkInsertExample.Run(connectionStrings); }
		if (!enabled) { QueryMultipleExample.Run(connectionStrings); }
	}
}

#endif