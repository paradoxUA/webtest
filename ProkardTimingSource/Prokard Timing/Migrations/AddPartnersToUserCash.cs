using ECM7.Migrator.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentix.Migrations
{
	[Migration(201805061558)]
	public class AddPartnersToUserCash : Migration
	{
		public override void Apply()
		{
			var query = $"ALTER TABLE [user_cash] ADD partner_id INT, ref_code NVARCHAR(14)";
			Database.ExecuteNonQuery(query);
		}
	}
}
