using System;

namespace AssetRegister.Helpers
{
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
