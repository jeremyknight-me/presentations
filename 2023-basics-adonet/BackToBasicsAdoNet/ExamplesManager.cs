#if DEBUG

using BackToBasicsAdoNet.Examples;

namespace BackToBasicsAdoNet;

internal static class ExamplesManager
{
	internal static void Run()
	{
        var connectionStrings = ConnectionStrings.Create();
		var enabled = 1;

		if (enabled == 1) { QueryExample.Run(connectionStrings); }
		if (enabled == 2) { QueryByIdExample.Run(connectionStrings); }

		if (enabled == 3) { InsertExample.Run(connectionStrings); }
		if (enabled == 4) { BulkCopyExample.Run(connectionStrings); }

		if (enabled == 5) { UpdateExample.Run(connectionStrings); }
		if (enabled == 6) { DeleteExample.Run(connectionStrings); }

		if (enabled == 7) { IntegrationExample.Run(connectionStrings); }

		if (enabled == 8) { BulkInsertExample.Run(connectionStrings); }
		if (enabled == 9) { QueryMultipleExample.Run(connectionStrings); }
	}
}

#endif