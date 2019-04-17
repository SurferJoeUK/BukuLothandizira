using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetRegister.Objects
{
	public class NotDateTime
	{
		private string[] MonthStrings =
		{
			"",
			"January",
			"February",
			"March",
			"April",
			"May",
			"June",
			"July",
			"August",
			"September",
			"October",
			"November",
			"December"
		};

		public string[] MonthStringsShort =
		{
			"",
			"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
		};
		public List<NotDateTimeParts> Dates { get; set; }
	}

	public class NotDateTimeParts
	{
		public NotDateTimeParts(string input)
		{

		}
		public NotDateTimeParts(int? day, int? month, int? year)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
		}

		public int? Year { get; set; }
		public int? Month { get; set; }
		public int? Day { get; set; }
	}

	public enum Months
	{
		January=1,
		February,
		March,
		April,
		May,
		June,
		July, 
		August,
		September,
		October,
		November,
		December,
	}
	
}
