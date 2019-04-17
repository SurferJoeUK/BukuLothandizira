using System.Collections.Generic;
using System.Xml;

namespace AssetRegister.Attributes.Warranty
{
	public interface IWarranty
	{
		List<KeyValuePair<string, string>> Details { get; set; }
		

		void AddDetails(object obj);
		//void AddWarrantyDetail(string value, string colTitle);
		void WriteXml(XmlWriter writer, string elementName, string type);
		void AddDetails(string key, string value);
	}
}
