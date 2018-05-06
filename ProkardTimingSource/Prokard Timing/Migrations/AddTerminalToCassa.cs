using ECM7.Migrator.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentix.Migrations
{
	[Migration(201805061844)]
	public class AddTerminalToCassa : Migration
	{
		public override void Apply()
		{
			var query = $"ALTER TABLE [cassa] ADD terminal bit";
			Database.ExecuteNonQuery(query);
		}
	}
}
