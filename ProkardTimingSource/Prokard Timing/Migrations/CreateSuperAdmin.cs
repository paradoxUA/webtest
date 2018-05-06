using ECM7.Migrator.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentix.Migrations
{
	[Migration(201805012158)]
	public class CreateSuperAdmin : Migration
	{
		public CreateSuperAdmin()
		{

		}
		public override void Apply()
		{
			var zeroDateTime = datetimeConverter.toDateTimeString(DateTime.Now);
			var query =
				"IF NOT EXISTS (SELECT * FROM program_users WHERE login = 'admin' and password = 'KVTKjlqWX7rz0xqMNSk/DQ==') " +
				"BEGIN INSERT INTO program_users " +
				"(login, password, created, stat, name, surname ,barcode, modified, deleted) " +
				$"VALUES ('admin', 'KVTKjlqWX7rz0xqMNSk/DQ==', '{zeroDateTime}', 2, 'Super', 'Admin', 0, '{zeroDateTime}', 0) " +
				$"END";
			Database.ExecuteNonQuery(query);
		}

		public override void Revert()
		{
			base.Revert();
		}
	}
}
