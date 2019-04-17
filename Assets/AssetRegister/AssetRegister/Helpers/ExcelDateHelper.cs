using System;

namespace AssetRegister.Helpers
{
	public static class DateHelper
	{
		// Changes the date so that it is 1 milisecond before the next day
		public static DateTime UntilEndOfDay(this DateTime untilDate)
		{
			return untilDate.AddDays(1).AddMilliseconds(-1);
		}
	}
	public static class ExcelDateHelper
	{
		public static DateTime GetDate(string value)
		{
			var iDate = Int32.Parse(value);

			if (iDate > 59)
			{
				iDate -= 1; //Excel/Lotus 2/29/1900 bug   
			}

			var dtDate = new DateTime(1899, 12, 31).AddDays(iDate);

			return dtDate;
		}
	}
}
