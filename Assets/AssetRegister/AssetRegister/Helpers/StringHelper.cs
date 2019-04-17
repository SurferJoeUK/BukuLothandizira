using System.Linq;
using System.Text;

namespace AssetRegister.Helpers
{
	public static class StringHelper
	{
		public static string FilterText(this string input)
		{
			StringBuilder sb = new StringBuilder(input);
			sb.Replace(".", "");

			return sb.ToString();
		}
		public static string GetSafeKey(this string input)
		{
			StringBuilder sb = new StringBuilder(input);
			sb.Replace(" ", "_");
			sb.Replace("/", "-");
			sb.Replace("(", "_");
			sb.Replace(")", "_");

			return sb.ToString();
		}

		public static string DoTrim(this string input)
		{
			return input.Trim();
		}

		/// <summary>
		/// Returns null if input is N/A
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string GetRealText(this string input)
		{
			input = input.Trim();

			return Constants.NA.Any(s => input == s) ? null : input;
		}

		public static bool IsADescriptionNotATag(string colValue)
		{
			bool hasMultipleWhiteSpace = false;
			bool hasUpperLowerCase = false;
			bool isLong = false;


			#region hasMultipleWhiteSpace

			bool space1 = false;
			foreach (char ch in colValue)
			{
				if (ch == ' ')
				{
					if (space1 == false)
					{
						space1 = true;
					}
					else
					{
						hasMultipleWhiteSpace = true;
						break;
					}
				}
			}

			#endregion hasMultipleWhiteSpace

			#region hasUpperLowerCase

			if (colValue.Any(char.IsLower) && colValue.Any(char.IsUpper))
			{
				hasUpperLowerCase = true;
			}

			#endregion hasUpperLowerCase

			#region isLong

			if (colValue.Length >= 10)
			{
				isLong = true;
			}

			#endregion isLong

			return (new[] {hasMultipleWhiteSpace, hasUpperLowerCase, isLong}).Count(b => b) > 1;
		}
	}
}