using ECM7.Migrator.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentix.Migrations
{
	[Migration(201805061846)]
	public class AddTerminalToUserCash : Migration
	{
		public override void Apply()
		{
			var query = $"ALTER TABLE [user_cash] ADD terminal bit";
			Database.ExecuteNonQuery(query);
		}
	}
}
