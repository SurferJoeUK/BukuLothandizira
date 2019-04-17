using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AssetRegister.Attributes.Event;
using AssetRegister.Objects;

namespace AssetRegister.Helpers
{
	public static class DateTimeHelper
	{
		public static DateTime? AsDateTime(this NotDateTime input)
		{
			

			List<DateTime> dt = new List<DateTime>();
			foreach (var ndt in input.Dates)
			{
				int day = 1;
				int month = 1;
				int year = DateTime.Now.Year;

				if (ndt.Day.HasValue && ndt.Day>0 && ndt.Day<32)
				{
					day = ndt.Day.Value;
				}

				if (ndt.Month.HasValue && ndt.Month > 0 && ndt.Month<=12)
				{
					month = ndt.Month.Value;
				}

				if (ndt.Year.HasValue && ndt.Year > 2000 && ndt.Year < 2100)
				{
					year = ndt.Year.Value;
				}

				try
				{
					dt.Add(new DateTime(year, month, day));
				}
				catch
				{
					dt.Add(DateTime.Now);
				}
				
			}

			try
			{
				var dtOrdered = dt.Where(dtx => dtx > DateTime.Now.AddDays(-1)).OrderBy(dtx => dtx).Take(1).First();
				return dtOrdered;
			}
			catch (Exception e)
			{
				return null;
			}

		}
	}
	public static class DataHelper
	{
		/// <summary>
		/// Returns true if the row is null or if there are only 0 or 1 columns with values
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public static bool IsEmpty(DataRow row)
		{
			var ii = row.ItemArray.Count(i => IsNullEquivalent(i));

			var iii = row.ItemArray.Count(i => i.HasValue());


			return row == null || row.ItemArray.All(i => i.IsNullEquivalent()) ||
			       row.ItemArray.Count(i => i.HasValue()) < 2;
			//
		}

		public static bool IsNullEquivalent(this object value)
		{
			return value == null
			       || value is DBNull
			       || string.IsNullOrWhiteSpace(value.ToString());
		}

		public static bool HasValue(this object value)
		{
			return !string.IsNullOrWhiteSpace(value?.ToString());
		}
	}
}