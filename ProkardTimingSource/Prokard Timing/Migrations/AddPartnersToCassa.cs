using ECM7.Migrator.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentix.Migrations
{
	[Migration(201805061553)]
	public class AddPartnersToCassa : Migration
	{
		public override void Apply()
		{
			var query = $"ALTER TABLE [cassa] ADD partner_id INT, ref_code NVARCHAR(14)";
			Database.ExecuteNonQuery(query);
		}
	}
}
