using System.Xml;
using AssetRegister.Objects;

namespace AssetRegister.Attributes.Supplier
{
	public class Supplier
	{
		public Supplier(string colValue)
		{
			this.Name = colValue;
		}

		public string Name { get; set; }
		public PhoneNumber Phone { get; set; }
		public Address Address { get; set; }

		public void WriteXml(XmlWriter writer, string elementName)
		{
			writer.WriteStartElement(elementName);
			writer.WriteElementString(nameof(this.Name), this.Name);
			writer.WriteEndElement();
		}
	}
}