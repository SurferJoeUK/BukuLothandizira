using System.Collections.Generic;
using System.Xml;
using AssetRegister.Helpers;

namespace AssetRegister.Attributes.Warranty
{
	public class WarrantyWithType : IWarranty
	{
		public string WarrantyPeriod { get; set; }
		public List<KeyValuePair<string, string>> Details { get; set; }
		private string WarrantyType { get; set; }

		public WarrantyWithType(string type, string warrantyPeriod)
		{
			this.WarrantyPeriod = warrantyPeriod;
			this.WarrantyType = type;
		}

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
		public void AddDetails(string key, string value)
		{
			if (this.Details == null)
			{
				this.Details = new List<KeyValuePair<string, string>>();
			}
			this.Details.Add(new KeyValuePair<string, string>(key, value));
		}

		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteElementString(nameof(this.WarrantyType), this.WarrantyType);
			writer.WriteElementString(nameof(this.WarrantyPeriod), this.WarrantyPeriod);

			if (this.Details != null)
			{
				foreach (var detail in this.Details)
				{
					writer.WriteElementString(detail.Key.GetSafeKey(), detail.Value);
				}
			}

			writer.WriteEndElement();
		}


	}
}