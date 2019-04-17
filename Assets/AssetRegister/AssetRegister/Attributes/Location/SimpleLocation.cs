using System.Xml;

namespace AssetRegister.Attributes.Location
{
	public class SimpleLocation : IAssetLocation
	{
		public SimpleLocation(string location)
		{
			this.Location = location;
		}

		public string Location { get; set; }
		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteElementString(nameof(this.Location), this.Location);
			writer.WriteEndElement();
		}
	}
}