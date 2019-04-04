using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetRegister.Attributes.PhysicalTags
{
	public interface IPhysicalTag
	{

	}

	public class BarcodeObject : IPhysicalTag
	{
		public BarcodeObject(string barcode)
		{
			this.Barcode = barcode;
		}

		public string Barcode { get; set; }
	}
}
