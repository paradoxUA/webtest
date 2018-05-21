using Rentix.Controls;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Rentix.Extensions
{
	public static class DbExtentions
	{
		public static string ToStringIf(this bool condition, string value)
		{
			return condition ? value : string.Empty;
		}

		public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
		{
			var filePath = assembly.Location;
			const int c_PeHeaderOffset = 60;
			const int c_LinkerTimestampOffset = 8;

			var buffer = new byte[2048];

			using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				stream.Read(buffer, 0, 2048);

			var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
			var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

			var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

			var tz = target ?? TimeZoneInfo.Local;
			var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

			return localTime;
		}

		public static DateTime AsDayStart(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
		}

		public static DateTime AsDayEnd(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
		}

		public static string ToSqlString(this DateTime dateTime)
		{
			return String.Format("{0:s}", dateTime);
		}

		public static int ExecuteNonQueryHandled<TCommand>(this TCommand sqlCommand)
			where TCommand : DbCommand
		{
			try
			{
				return sqlCommand.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				MessageBox.Show($"Запрос \"{sqlCommand.CommandText}\" не был выполнен, c ошибкой {e.ToString()}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			
		}

		public static void Fill(this ComboBox comboBox, IEnumerable<KeyValuePair<int, string>> data, bool addEmptyItem = true)
		{
			comboBox.Items.Clear();
			if (addEmptyItem)
			{
				comboBox.Items.Add(new comboBoxItem("", -1));
			}
			foreach (var item in data)
			{
				var comboBoxItem = new comboBoxItem(item.Value, item.Key);
				comboBox.Items.Add(comboBoxItem);
			}
		}

		public static int SelectedIdx(this ComboBox comboBox)
		{
			if (comboBox.SelectedIndex < 0)
			{
				return -1;
			}
			if (comboBox.Items[comboBox.SelectedIndex] is comboBoxItem item)
			{
				return item.value;
			}
			throw new Exception();
		}
	}
}
