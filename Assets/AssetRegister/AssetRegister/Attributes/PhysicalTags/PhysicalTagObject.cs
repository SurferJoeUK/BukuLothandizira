using System.Xml;

namespace AssetRegister.Attributes.PhysicalTags
{
	public class PhysicalTagObject : IPhysicalTag
	{
		public PhysicalTagObject(string type, string value)
		{
			this.Type = type;
			this.Value = value;
		}
		public string Type { get; set; }
		public string Value { get; set; }
		public void WriteXml(XmlWriter writer, string elementName, string type)
		{
			writer.WriteStartElement(elementName);
			writer.WriteAttributeString(type, this.GetType().Name);
			writer.WriteAttributeString(nameof(this.Type), this.Type);
			writer.WriteAttributeString(nameof(this.Value), this.Value);
			writer.WriteEndElement();

		}
	}
}