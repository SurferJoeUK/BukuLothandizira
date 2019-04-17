using System.Collections.Generic;
using System.Xml;
using AssetRegister.Helpers;

namespace AssetRegister.Attributes.Warranty
{
	public class WarrantyBasic : IWarranty
	{
		public List<KeyValuePair<string, string>> Details { get; set; }

		public void AddDetails(object obj)
		{
			if (this.Details == null)
			{
				this.Details = new List<KeyValuePair<string, string>>();
			}
			if (obj is KeyValuePair<string, string> kvp)
			{
				this.Details.Add(kvp);
			}
		}

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			foreach (var detail in this.Details)
			{
				writer.WriteElementString(detail.Key.GetSafeKey(), detail.Value);
			}

			writer.WriteEndElement();
		}

		public void AddDetails(string key, string value)
		{
			if (this.Details == null)
			{
				this.Details = new List<KeyValuePair<string, string>>();
			}
			this.Details.Add(new KeyValuePair<string, string>(key, value));
		}
	}
}