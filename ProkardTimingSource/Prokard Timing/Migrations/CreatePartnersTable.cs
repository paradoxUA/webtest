using ECM7.Migrator.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentix.Migrations
{
	[Migration(201805020942)]
	public class CreatePartnersTable : Migration
	{
		public override void Apply()
		{
			var query = 
				"CREATE TABLE partners (" +
					"id int IDENTITY(1,1)," +
					"name nvarchar(max)," +
					"commission float," +
					"deleted bit NOT NULL DEFAULT 0," +
					"PRIMARY KEY(id))";
			Database.ExecuteNonQuery(query);
		}
	}
}
