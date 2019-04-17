using AssetRegister.Attributes.Warranty;

namespace AssetRegister.Helpers
{
	public class WarrantyHelper
	{
		private const string ResidentialWarranty = "RESIDENTIAL WARRANTY";
		private const string ManufacturerWarranty = "MANUFACTURER WARRANTY";

		public static IWarranty GetWarranty(string colTitle, string value)
		{
			IWarranty warranty;
			
			string upper = value.ToUpper();
			if (upper.EndsWith(ResidentialWarranty))
			{
				warranty = new WarrantyWithType("Residential", value.Substring(0, value.Length - ResidentialWarranty.Length).Trim());												
			}
			else if (upper.EndsWith(ManufacturerWarranty))
			{
				warranty = new WarrantyWithType("Manufacturer", value.Substring(0, value.Length - ManufacturerWarranty.Length).Trim());
			}
			else
			{
				warranty = new WarrantyBasic();
				warranty.AddDetails(colTitle, value);
			}

			return warranty;
		}


		
	}
}